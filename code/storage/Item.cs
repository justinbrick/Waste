﻿using Sandbox;

namespace Waste
{
    public class WasteItem : BaseCarriable
    {
        public long ID { get; protected set; } // Weapon ID
        public string UID { get; protected set; } // Unique ID
        public double Value { get; protected set; } // The value of the item
        public Vector2 Size { get; protected set; } // The size of the item
        public Vector2 SlotPosition { get; set; } // Where this item is in a container. Null if not in container.
		public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
		public virtual string ModelPath => "weapons/rust_pistol/rust_pistol.vmdl";

		public override void Spawn()
		{
			base.Spawn();
			EnableTouch = false;
			EnableTouchPersists = false;
			CollisionGroup = CollisionGroup.Debris;
			SetModel( ModelPath );
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
		public static string GenerateUID()
        {
            return "";
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
