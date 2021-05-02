using Sandbox;
using Waste.UI;
using System.Linq;

namespace Waste
{
    partial class WastePlayer : BasePlayer
    {
        [Net]
        public int Armor { get; set; }

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

			if (Input.Pressed(InputButton.Score) && IsClient)
            {
				WasteMenu.Toggle();
            }
			if (Input.Pressed(InputButton.Attack1) && IsClient)
            {
				var tr = Trace.Ray(EyePos, EyePos + EyeRot.Forward * 5000)
					.UseHitboxes()
					.Ignore(this)
					.Radius(2.0f)
					.Run();

				if (tr.Hit)
                {
					Log.Info(tr.Entity.GetType().ToString());
                }
            }
        }
    }
}
