using Sandbox;
using Waste.UI;
using System.Collections.Generic;
using System.Linq;

namespace Waste.Storage
{

	
    public partial class Container : WasteItem
    {
	    public virtual Vector2 ContainerSize => Vector2.Zero; // Most cases this will be the same size as the container, but for others we want it to be different.
        public List<WasteItem> Items { get; protected set; } // List of all the items currently in the container.
        public ContainerWindow Window { get; protected set; } // Window representation of this container.
		public Slot[,] Slots { get; set; }

		public Container()
		{
			Items = new List<WasteItem>();
		}
		
        public Container(bool isHeadless = false)
        {
	        Items = new List<WasteItem>();
			Slots = new Slot[(int)ContainerSize.x, (int)ContainerSize.y];
		
			for (int x = 0; x < ContainerSize.x; ++x )
				for ( int y = 0; y < ContainerSize.y; ++y )
					Slots[x, y] = new Slot()
					{
						Container = this,
						Position = new Vector2(x,y)
					};

			Window = Host.IsClient ? new ContainerWindow( this, isHeadless ) : null;
        }
        
        public bool HasItem( WasteItem item ) => Items.Any(i => i == item);
        
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
			Window?.AddItem( item, slot.Position );
			return true;
		}

		// We want to try and add an item anywhere into this container.
		public bool AddItem(WasteItem item) => (from Slot slot in Slots where !slot.HasItem && slot.CanFit( item ) select AddItem( item, slot )).FirstOrDefault();
		
		public bool CanAddItem(WasteItem item) => Slots.Cast<Slot>().Any( slot => !slot.HasItem && slot.CanFit( item ) );
    }
}
