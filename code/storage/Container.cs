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
        
        // We want to try and add something to this container in a certain spot.
		public bool AddItem(WasteItem item, Vector2 position)
		{
			if ( !CanAddItem( item, position ) ) return false;
			item.SlotPosition = position;
			Window?.AddItem( item, position );
			return true;
		}

		// We want to try and add an item anywhere into this container.
		public bool AddItem(WasteItem item)
		{
			Log.Info("Container Add Item");
			if ( !CanAddItem( item ) ) return false;
			foreach ( var slot in Slots )
			{
				if ( !slot.HasItem && slot.CanFit( item ) )
					return AddItem( item, slot.Position );
			}
			return false;
		}

		public bool CanAddItem(WasteItem item, Vector2 position)
		{
			if ( position.x > ContainerSize.x || position.y > ContainerSize.y ||
			     position.x + item.Size.x > ContainerSize.x ||
			     position.y + item.Size.y > ContainerSize.y ) return false;

			return Slots[(int)position.x, (int)position.y].CanFit( item );
		}

		public bool CanAddItem(WasteItem item ) => Slots.Cast<Slot>().Any( slot => !slot.HasItem && slot.CanFit( item ) );
    }
}
