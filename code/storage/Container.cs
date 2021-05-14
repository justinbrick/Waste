using Sandbox;
using Waste.UI;
using System.Collections.Generic;

namespace Waste
{

    public class Container : WasteItem
    {
		public Vector2 ContainerSize { get; protected set; } // Most cases this will be the same size as the container, but for others we want it to be different.
        public List<WasteItem> Items { get; protected set; } // List of all the items currently in the container.
        public ContainerWindow Window { get; protected set; } // Window representation of this container.
		public Slot[,] Slots { get; set; }

		public Container()
		{
			Items = new List<WasteItem>();
		}

        public Container(int sizeX, int sizeY, bool isHeadless = false)  
        {
			Size = new Vector2( sizeX, sizeY );
			Items = new List<WasteItem>();
			Slots = new Slot[sizeX, sizeY];

			for (int x = 0; x < sizeX; ++x )
				for ( int y = 0; y < sizeY; ++y )
					Slots[x, y] = new Slot()
					{
						Container = this,
						Position = new Vector2(x,y)
					};

			Window = ContainerWindow.GetWindowRepresentation( this, isHeadless );
		}

		// Trying to add something in a certain position.
		// TODO: Add functionalities
		public bool AddItem(WasteItem item, Vector2 position)
		{
			return CanAddItem( item, position );
		}

		public bool AddItem(WasteItem item)
		{
			if (CanAddItem(item))
			{
				
			}
			return false;
		}

		// TODO: This is questionable, have someone review this
		public bool CanAddItem(WasteItem item, Vector2 position)
		{
			if ( position.x > ContainerSize.x || position.y > ContainerSize.y ) return false; // Can this item even fit in this container?
			if ( position.x + item.Size.x > ContainerSize.x || position.y + item.Size.y > ContainerSize.y ) return false; // Will it fit adjusting for the item?
			for (int itemX = 0; itemX < item.Size.x; ++itemX ) // For the entire size of the item, check if each slot has an item or not.
			{
				for ( int itemY = 0; itemY < item.Size.y; ++itemY)
				{
					if ( Slots[(int)position.x-1 + itemX,(int) position.y-1 + itemY].HasItem ) return false;
				}
			}
			return true;
		}

		public bool CanAddItem(WasteItem item )
        {
			Vector2 pos = new();
			for (int slotX = 0; slotX < ContainerSize.x; ++slotX )
			{
				for (int slotY = 0; slotY < ContainerSize.y; ++slotY )
				{
					if (!Slots[slotX, slotY].HasItem) // Look at which slots don't have items, that means this has a potential to fit something.
					{
						pos.x = slotX; 
						pos.y = slotY;
						if ( CanAddItem( item, pos ) ) return true;
					}
				}
			}
            return false;
        }
    }
}
