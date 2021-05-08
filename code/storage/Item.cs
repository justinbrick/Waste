using Sandbox;
using Sandbox.UI;
using System;

namespace Waste
{
	public class WasteItem : BaseCarriable
	{
		public string UID { get; protected set; } // Unique ID
		public Vector2 Size { get; set; } // The size of the item
		public Vector2 SlotPosition { get; set; } // Where this item is in a container. Null if not in container.
		public virtual double Value => 0.0; // The value of the item
		public virtual string ID => "UNINITIALIZED"; // Weapon ID
		public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
		public virtual string ModelPath => "weapons/rust_pistol/rust_pistol.vmdl";

		public override void Spawn()
		{
			base.Spawn();
			EnableTouch = false;
			EnableTouchPersists = false;
			CollisionGroup = CollisionGroup.Debris;
			RemoveCollisionLayer(CollisionLayer.Player);
			SetModel( ModelPath );
			GenerateIcon();
		}

		public override void ActiveStart( Entity ent )
		{
			base.ActiveStart( ent );
			ent.ActiveChild = this;
		}

		public override void ActiveEnd( Entity ent, bool dropped )
		{
			base.ActiveEnd( ent, dropped );
		}

		protected virtual void GenerateIcon()
		{
			if ( Host.IsServer ) return;
			using (SceneWorld.SetCurrent(new SceneWorld()))
			{
				Light.Point( Vector3.Up * 10.0f + Vector3.Forward * 100.0f, 200, Color.White * 15000.0f );
				var angles = new Angles(0, 180, 0);
				var capture = SceneCapture.Create(ID, 512, 512);
				capture.SetCamera(angles.Direction * -50, angles, 70);
			}
		}

		public static Image GetIconRepresentation(WasteItem item)
		{
			Host.AssertClient();
			var image = new Image();
			image.SetTexture("scene:" + item.ID);
			return image;
		}

		// Generate a UID for each item so that we can store it in a database.
		public static string GenerateUID()
        {
            return Guid.NewGuid().ToString();
        }

		// TODO: Implement
		public virtual bool Use(Entity user)
		{
			return false;
		}

        public override bool CanCarry(Entity carrier)
        {
			return base.CanCarry( carrier );
        }

        public override void OnCarryDrop(Entity parentEntity)
        {
			base.OnCarryDrop(parentEntity);
        }

        public override void OnCarryStart(Entity parentEntity)
        {
			base.OnCarryStart(parentEntity);
		}
    }
}
