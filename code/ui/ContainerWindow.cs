using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{

    // Visual Representation of each container
    public class ContainerWindow : Panel
    {
        public ContainerWindow()
        {
			
            StyleSheet.Load("/ui/ContainerWindow.scss");
            // TODO: implement
        }

        public static ContainerWindow GetWindowRepresentation(Container container, bool isHeadless = false)
        {
			if ( Host.IsServer ) return null;
            ContainerWindow window = new();
            window.Style.Width = Length.Pixels(80 * container.Size.x + 12);
            window.Style.Height = Length.Pixels(80 * container.Size.y + 12);
	
            // TODO: implement
            for (int x = 0; x < container.Size.x; ++x )
			{
				for (int y = 0; y < container.Size.y; ++y )
				{
					window.AddChild<Slot>( "slot" ); // Add this as slot class.
				}
			}
            return window;
        }
    }
}
