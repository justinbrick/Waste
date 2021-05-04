using Sandbox;
using Waste.UI;
using System.Collections.Generic;

namespace Waste
{

    public class Container : WasteItem
    {
        // The size of the container will be the same as the amount of slots it can hold
        public List<WasteItem> Items { get; protected set; } // List of all the items currently in the container.
        public ContainerWindow Window { get; protected set; } // Window representation of this container.

		public Container()
		{
			Items = new();
		}

        public Container(int sizeX, int sizeY, bool isHeadless = false)  
        {
			Size = new( sizeX, sizeY );
			Items = new();
			Window = ContainerWindow.GetWindowRepresentation(this, isHeadless);
        }

		// Trying to add something in a certain position.
		public bool AddItem(WasteItem item, Vector2 position)
		{
			// Todo: Implement
			return false;
		}

		public bool AddItem(WasteItem item)
		{
			// Todo: Implement
			return false;
		}


		public bool CanAddItem(WasteItem item)
        {
            foreach (var containerItem in Items)
            {
				if ( Size.x + containerItem.Size.x > Size.x )
					return false;
            }
            return false;
        }
    }
}
