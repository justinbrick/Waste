﻿using Sandbox;
using Waste.UI;

namespace Waste.Storage
{
	public class Slot
	{
		private SlotWindow _window;
		public bool HasItem;
		public Container Container; // The container this belongs to.
		public Vector2 Position; // Where this slot is in it's respective container.
		public SlotWindow Window
		{
			get
			{
				Host.AssertClient();
				return _window;
			}
			private set
			{
				Host.AssertClient();
				_window = value; 
			}
		}
		
		public Slot()
		{
			HasItem = false;
			if (Host.IsClient)
			{
				Window = new SlotWindow {Slot = this};
			}
		}

		// Checks if this item can fit in this specified slot, true if yes, false if not.
		public bool CanFit( WasteItem item )
		{
			Log.Info( $"{Position.x} + {item.Size.x} > {Container.ContainerSize.x} or {Position.y} + {item.Size.y} > {Container.ContainerSize.y}" );
			// If this is too big then it won't fit in the first place.
			if (Position.x + item.Size.x > Container.ContainerSize.x ||
			       Position.y + item.Size.y > Container.ContainerSize.y) return false;
			
			// Check if any of the slots nearby have items inside of them.
			for ( int x = 0; x < item.Size.x; ++x )
			{
				for ( int y = 0; y < item.Size.y; ++y )
				{
					if ( Container.Slots[(int)Position.x + x,(int)Position.y + y].HasItem ) return false;
				}
			}
			
			Log.Info("Slot Can Fit!");
			return true;
		}
			
	}
}
