using Sandbox;
using Waste.UI;
using System.Collections.Generic;
using System.Linq;
using NetworkWrappers;

namespace Waste.Storage
{

	
    public partial class WasteContainer : WasteItem
    {
	    public virtual Vector2 ContainerSize => Vector2.Zero; // Most cases this will be the same size as the container, but for others we want it to be different.
	    [NetLocal] public List<WasteItem> Items { get; set; }
	    public Slot[,] Slots;
		public ContainerWindow Window;
		
        public WasteContainer()
        {
	        Items = new List<WasteItem>();
			Slots = new Slot[(int)ContainerSize.x, (int)ContainerSize.y];
		
			for (int x = 0; x < ContainerSize.x; ++x )
				for ( int y = 0; y < ContainerSize.y; ++y )
					Slots[x, y] = new Slot()
					{
						WasteContainer = this,
						Position = new Vector2(x,y)
					};
        }

        public bool HasItem( WasteItem item ) => Items.Contains( item );
        
		public bool AddItem( WasteItem item, Slot slot )
		{
			if ( !slot.CanFit( item ) ) return false;
			item.SlotPosition = slot.Position;
			for ( int x = 0; x < item.Size.x; ++x )
			{
				for ( int y = 0; y < item.Size.y; ++y )
				{
					Slots[(int)slot.Position.x + x,(int)slot.Position.y + y].HasItem = true;
				}
			}
			InventoryWindow.AddItem(this, slot, item);
			return true;
		}

		// We want to try and add an item anywhere into this container.
		public bool AddItem(WasteItem item) => (from Slot slot in Slots where !slot.HasItem && slot.CanFit( item ) select AddItem( item, slot )).FirstOrDefault();
		
		public bool CanAddItem(WasteItem item) => Slots.Cast<Slot>().Any( slot => !slot.HasItem && slot.CanFit( item ) );
    }
}
