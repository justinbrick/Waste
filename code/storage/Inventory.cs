using Sandbox;
using System;
using System.Linq;

namespace Waste
{
    class WasteInventory : BaseInventory
    {
        public Container Pockets;
        public Container Backpack;
        public Container Case;

        public WasteInventory(Player player) : base(player)
        {
            Pockets = new Container(4,1);
			Backpack = new Container( 4,4);
			Case = new Container(2,2);
        }

        public virtual bool HasSpace(Entity ent)
        {
            if (ent is WasteItem item) // Is this an item we can pick up?
            {
                return Pockets.CanAddItem(item) || Backpack.CanAddItem(item) || Case.CanAddItem(item);
            }
            return false;
        }

        public override bool Add(Entity ent, bool makeActive = false)
        {
            if (ent is WasteItem item && HasSpace(item))
            {

            }
            return false;
        }

        public bool IsCarryingType(Type t)
        {
            return List.Any(x => x.GetType() == t);
        }
    }
}
