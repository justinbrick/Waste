using Sandbox;
using Waste.UI;
using Waste.Storage;
using System.Linq;

namespace Waste
{
    partial class WastePlayer : Player
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

		// We don't want to be doing anything on touch. (YET)
		public override void Touch( Entity other )
		{
			
		}
		
		public override void StartTouch( Entity other )
		{
			
		}

		public override void EndTouch( Entity other )
		{
			
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

        public override void Simulate(Client cl)
        {
            base.Simulate(cl);
			// We want to find if someone is looking at an item.
			var trace = Trace.Ray( EyePos, EyePos + EyeRot.Forward * 100 )
					.UseHitboxes()
					.Ignore( this )
					.Radius( 2.0f )
					.Run();

			if (trace.Hit)
			{
				if ( trace.Entity is not WasteItem item ) // If this is not an item we can interact with, ignore it.
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
			else
			{
				IsLookingAtItem = false;
				LastItem = null;
				if ( IsClient && InteractionPrompt.IsOpen )
					InteractionPrompt.Close();
			}

			if (Input.Pressed(InputButton.Reload) && IsServer)
			{
				var pistol = new Pistol();
				pistol.Spawn();
				pistol.Position = EyePos + new Vector3(0, 0, 30);
			}
			if (Input.Pressed(InputButton.Score) && IsClient)
            {
				WasteMenu.Toggle();
            }
			if (Input.Pressed(InputButton.Flashlight) && IsServer && trace.Hit && IsLookingAtItem)
			{
				using ( Prediction.Off() )
				{
					Inventory.Add(LastItem, true);
				}
			}
        }
    }
}
