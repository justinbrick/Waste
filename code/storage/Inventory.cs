using Sandbox;
using System;
using System.Linq;
using Waste.Storage.Containers;

namespace Waste.Storage
{
    class WasteInventory : BaseInventory
    {
	    public WasteWeapon Primary; // The primary weapon that we have.
	    public WasteWeapon Secondary; // A secondary weapon that we have.
	    public WasteItem Tertiary; // A third item, which can be either melee, pistol, or miscellaneous.
        public WasteContainer Pockets; // Our pockets, which we will always have on us.
        // These are optional containers - this means that they have potential to be null.
        // If they are not null, then they can be used to store stuff, or will have their own stuff inside of them.
        public WasteContainer Vest;
        public WasteContainer Backpack;
        public WasteContainer Case;

        // Upon construction, the only thing that we can guarantee is that we will have pockets.
        public WasteInventory(Player player) : base(player)
        {
	        Pockets = new Pockets() {Owner = player}; 
        }
        
        // What do we want to do when we're trying to add something to the inventory?
        public override bool Add(Entity ent, bool makeActive = false)
        {
	        if ( ent.Owner != null || ent is not WasteItem item ) return false;
	        return Pockets?.AddItem( item ) == true || Vest?.AddItem( item ) == true ||
	               Backpack?.AddItem( item ) == true || Case?.AddItem( item ) == true;
        }
        
        // Check whether or not an entity can even be added.
        public override bool CanAdd( Entity ent )
        {
	        if ( ent is not WasteItem {Owner: null} item) return false; // We don't want to try and add it if it's not one of ours.
	        return (Pockets?.CanAddItem( item ) == true || Backpack?.CanAddItem( item ) == true ||
	                Case?.CanAddItem( item ) == true ||
	                Vest?.CanAddItem( item ) == true);
        }

        // If we need to set this as an active item, we can do it through this.
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
