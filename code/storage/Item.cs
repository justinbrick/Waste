using Sandbox;
using Sandbox.UI;
using System;
using Waste.UI;

namespace Waste
{
	public class WasteItem : BaseCarriable
	{
		public string UID { get; protected set; } // Unique ID
		public Vector2 Size { get; set; } // The size of the item
		public Vector2 SlotPosition { get; set; } // Where this item is in a container. Null if not in container.
		public Icon WeaponIcon { get; set; } // The icon of this weapon. Clientside only.
		public virtual double Value => 0.0; // The value of the item
		public virtual string ID => "UNINITIALIZED"; // Weapon ID
		public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
		public virtual string ModelPath => "weapons/rust_pistol/rust_pistol.vmdl";

		public WasteItem()
		{
			EnableTouch = false;
			EnableTouchPersists = false;
			CollisionGroup = CollisionGroup.Debris;
			UID = GenerateUID();
			RemoveCollisionLayer( CollisionLayer.Player );
			SetModel( ModelPath );
		}

		public override void Spawn()
		{
			base.Spawn();
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

		// Generate a UID for each item so that we can store it in a database.
		private static string GenerateUID()
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
