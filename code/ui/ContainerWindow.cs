﻿using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{

	// Visual Representation of each container
	public class ContainerWindow : Panel
	{
		public Vector2 Size { get; private set;}
		//private Slot[,] Slots; // Fuck
		private bool _headless;
		public bool Headless
		{
			get => _headless;
			set
			{
				if ( value )
					Style.Height = Length.Pixels( Slot.SLOT_SIZE * Size.y + 12);
				else
					Style.Height = Length.Pixels( Slot.SLOT_SIZE * Size.y + 27 );

				SetClass( "isHeadless", value );
				_headless = value;
			}
		}

        public ContainerWindow()
        {
			StyleSheet.Load( "/ui/ContainerWindow.scss" );
			throw new System.Exception("Container Window Constructed without container reference!");
        }

		public ContainerWindow(Container container)
		{ 
			StyleSheet.Load( "/ui/ContainerWindow.scss" );
			var titleBar = Add.Panel( "title_bar" );
		}

		public void Close()
		{
			if ( !Headless ) return; // If this is not headless we don't want the ability to close it.
		}

		public static ContainerWindow GetWindowRepresentation(Container container, bool isHeadless = false)
        {
			if ( Host.IsServer ) return null; // Server isn't supposed to be messing with this stuff.
            ContainerWindow window = new(container);
			window.Size = container.Size;
            window.Style.Width = Length.Pixels(Slot.SLOT_SIZE * container.Size.x + 12);
            window.Style.Height = Length.Pixels(Slot.SLOT_SIZE * container.Size.y + 12);
			window.Headless = isHeadless;
            for (int x = 0; x < container.Size.x; ++x )
			{
				for (int y = 0; y < container.Size.y; ++y )
				{
					window.AddChild<Slot>( "slot" ); // Add this as slot class.
				}
			}
			// TODO: Query items from database and place them into backpack.
            return window;
        }
    }
}
