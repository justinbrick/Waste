using Sandbox;
using Waste.UI;
using System.Linq;

namespace Waste
{
    partial class WastePlayer : BasePlayer
    {
		public bool IsLookingAtItem { get; set; }
		public WasteItem LastItem; // The last item that we saw before this.

        [Net]
        public int Armor { get; set; }

		public WastePlayer()
		{
			IsLookingAtItem = false;
			Inventory = new WasteInventory( this );
		}

		public override void Respawn()
		{
			SetModel("models/citizen/citizen.vmdl");

			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();
			Camera = new FirstPersonCamera();
			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
			base.Respawn();
		}

        protected override void Tick()
        {
            base.Tick();
			// We want to find if someone is looking at an item.
			var trace = Trace.Ray( EyePos, EyePos + EyeRot.Forward * 5000 )
					.UseHitboxes()
					.Ignore( this )
					.Radius( 2.0f )
					.Run();

			if (trace.Hit && IsClient)
			{
				if ( !(trace.Entity is WasteItem item) ) // If this is not an item we can interact with, ignore it.
				{
					IsLookingAtItem = false;
					LastItem = null;
					InteractionPrompt.Close();
				}
				else if ( item != LastItem ) // If it's the last item then we're still looking at the same thing.
				{
					IsLookingAtItem = true;
					LastItem = item;
					InteractionPrompt.Open();
				}
			}

			if (Input.Pressed(InputButton.Reload))
			{
				var pistol = new Pistol();
				pistol.Spawn();
				pistol.WorldPos = EyePos + new Vector3(0, 0, 30);
			}
			if (Input.Pressed(InputButton.Score) && IsClient)
            {
				WasteMenu.Toggle();
            }
			if (Input.Pressed(InputButton.Attack1))
            {
				// Did the trace hit something?
				if (trace.Hit)
				{

				}
            }
        }
    }
}
