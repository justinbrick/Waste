using Sandbox;
using Waste.UI;
using System.Collections.Generic;
using System.Linq;

namespace Waste.Storage
{

    public class Container : WasteItem
    {
	    public virtual Vector2 ContainerSize { get; protected set; } // Most cases this will be the same size as the container, but for others we want it to be different.
        public List<WasteItem> Items { get; protected set; } // List of all the items currently in the container.
        public ContainerWindow Window { get; protected set; } // Window representation of this container.
		public Slot[,] Slots { get; set; }

		public Container()
		{
			Items = new List<WasteItem>();
		}

		// TODO: This is temporary, we will be setting size & container size by overriding variables.
        public Container(int sizeX, int sizeY, int containerSizeX, int containerSizeY, bool isHeadless = false)  
        {
			Size = new Vector2( sizeX, sizeY );
			ContainerSize = new Vector2( containerSizeX, containerSizeY );
			Items = new List<WasteItem>();
			Slots = new Slot[containerSizeX, containerSizeY];

			for (int x = 0; x < containerSizeX; ++x )
				for ( int y = 0; y < containerSizeY; ++y )
					Slots[x, y] = new Slot()
					{
						Container = this,
						Position = new Vector2(x,y)
					};

			Window = ContainerWindow.GetWindowRepresentation( this, isHeadless );
		}

		
        // We want to try and add something to this container in a certain spot.
		public bool AddItem(WasteItem item, Vector2 position)
		{
			if ( !CanAddItem( item, position ) ) return false;
			// TODO: Implement
			return true;
		}

		// We want to try and add an item anywhere into this container.
		public bool AddItem(WasteItem item)
		{
			if ( !CanAddItem( item ) ) return false;
			// TODO: Implement
			return true;
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
