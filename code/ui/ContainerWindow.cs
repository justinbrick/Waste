using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

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
			ContainerWindow window = new(container)
			{
				Size = container.Size,
				Style =
				{
					Width = Length.Pixels( SlotWindow.SLOT_SIZE * container.Size.x + 12 ),
					Height = Length.Pixels( SlotWindow.SLOT_SIZE * container.Size.y + 12 )
				},
				Headless = isHeadless
			};

			foreach (var slot in container.Slots)
			{
				slot.Window.Parent = window;
			}

			// TODO: Query items from database and place them into backpack.
            return window;
        }
    }
}
