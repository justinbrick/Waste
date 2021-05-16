using Sandbox;
using System;
using System.Linq;
using Waste.Storage.Containers;

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
	        Pockets = new Pockets(true);
        }
        
        public override bool Add(Entity ent, bool makeActive = false)
        {
	        if ( ent.Owner != null || ent is not WasteItem item || !CanAdd( item) ) return false;
	        Log.Info( "Attempted to Add Item" );
	        return Pockets?.AddItem( item ) == true || Vest?.AddItem( item ) == true ||
	               Backpack?.AddItem( item ) == true || Case?.AddItem( item ) == true;
        }
        
        public override bool CanAdd( Entity ent )
        {
	        if ( ent is not WasteItem {Owner: null} item) return false; // We don't want to try and add it if it's not one of ours.
	        return (Pockets?.CanAddItem( item ) == true || Backpack?.CanAddItem( item ) == true ||
	                Case?.CanAddItem( item ) == true ||
	                Vest?.CanAddItem( item ) == true);
        }

        public override bool SetActive( Entity ent )
        {
	        if ( ent is not WasteItem item ) return false; // Don't try to set stuff as our active child if it's not of our type.
	        if ( Owner.ActiveChild != null && CanAdd( Owner.ActiveChild ) )
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
