using Sandbox;
using System;
using System.Linq;

namespace Waste.Storage
{
    class WasteInventory : BaseInventory
    {
        public Container Pockets;
		public Container Vest;
        public Container Backpack;
        public Container Case;

        public WasteInventory(Player player) : base(player)
        {
            Pockets = new Container(4, 1, 4, 1, true);
			Vest = new Container(6, 6, 6, 6, true);
			Backpack = new Container( 4, 8, 4, 8, true);
			Case = new Container(2, 2, 2, 2, true);
        }

        public bool HasSpace(Entity ent)
        {
            if (ent is WasteItem item) // Is this an item we can pick up?
            {
                return Pockets.CanAddItem(item) || Backpack.CanAddItem(item) || Case.CanAddItem(item);
            }
            return false;
        }

        public override bool Add(Entity ent, bool makeActive = false)
        {
			if ( ent == null ) return false; // Does this entity even exist?
			if ( Owner.ActiveChild != null ) return false; // Does the owner already have an active item?
			return base.Add( ent, makeActive );
        }

        public bool IsCarryingType(Type t)
        {
            return List.Any(x => x.GetType() == t);
        }
    }
}
