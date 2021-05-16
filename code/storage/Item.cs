using Sandbox;
using System;
using Waste.UI;

namespace Waste.Storage
{
	public class WasteItem : BaseCarriable
	{
		// ReSharper disable once InconsistentNaming
		public string UID { get; protected set; } // Unique ID
		public Vector2 SlotPosition { get; set; } // Where this item is in a container. Null if not in container.
		public Icon Icon { get; set; } // The icon of this weapon. Clientside only.
		public virtual Vector2 Size => Vector2.Zero; // The size of the item

		public virtual double Value => 0.0; // The value of the item
		// ReSharper disable once InconsistentNaming
		public virtual string ID => "UNINITIALIZED"; // Weapon ID
		public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
		public virtual string ModelPath => "weapons/rust_pistol/rust_pistol.vmdl";

		public WasteItem()
		{
			UID = GenerateUID();

			EnableTouch = false;
			EnableTouchPersists = false;
			CollisionGroup = CollisionGroup.Debris;
			RemoveCollisionLayer( CollisionLayer.Trigger );
			RemoveCollisionLayer( CollisionLayer.PICKUP );
			SetModel( ModelPath );
		}

		public override void ActiveStart( Entity ent )
		{
			base.ActiveStart( ent );
			ent.ActiveChild = this;
		}
		
		// Generate a UID for each item so that we can store it in a database.
		// ReSharper disable once InconsistentNaming
		private static string GenerateUID()
        {
            return Guid.NewGuid().ToString();
        }

		// TODO: Implement
		public virtual bool Use(Entity user)
		{
			return false;
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
