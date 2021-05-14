using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Waste.Storage;

namespace Waste.UI
{

	// Visual Representation of each container
	public class ContainerWindow : Panel
	{
		public Vector2 Size { get; private set; }
		private bool _headless;
		public bool Headless
		{
			get => _headless;
			set
			{
				Style.Height = value ? Length.Pixels( SlotWindow.SLOT_SIZE * Size.y + 12) : Length.Pixels( SlotWindow.SLOT_SIZE * Size.y + 27 );

				SetClass( "isHeadless", value );
				_headless = value;
			}
		}

        public ContainerWindow()
        {
			StyleSheet.Load( "/ui/ContainerWindow.scss" );
			throw new System.Exception("Container Window Constructed without container reference!");
        }
        
        public ContainerWindow( Container container, bool isHeadless = false )
        {
	        Host.AssertClient();
			StyleSheet.Load("/ui/ContainerWindow.scss");
			var titleBar = Add.Panel( "title_bar" ); // TODO: Use title bar
			titleBar.Add.Panel("exit_button");
			Size = container.Size;
			Style.Width = Length.Pixels( SlotWindow.SLOT_SIZE * container.Size.x + 12 );
			Style.Height = Length.Pixels( SlotWindow.SLOT_SIZE * container.Size.y + 12 );
			Headless = isHeadless;

			// For some reason, you need to do this backwards or the slots will reverse themselves.
			for ( int y = 0; y < container.Size.y; ++y )
			{
				for ( int x = 0; x < container.Size.x; ++x )
				{
					container.Slots[x, y].Window.Parent = this;
				}
			}
        }

		public void Close()
		{
			if ( !Headless ) return; // If this is not headless we don't want the ability to close it.
		}
		
    }
}
