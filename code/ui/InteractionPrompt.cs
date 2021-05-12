using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    public class InteractionPrompt : Panel
    {
        public static bool IsOpen { get; private set; }
        private static Panel _current;
        private Label text;
        public string Text
        {
	        set
	        {
		        text.Text = value;
		        _current?.Style.Dirty();
	        }
        }

        public InteractionPrompt()
        {
            _current = this;
            IsOpen = false;
            StyleSheet.Load("/ui/InteractionPrompt.scss");
            Add.Panel("interact_key").Add.Label("F");
            text = Add.Label("Pickup", "text"); // TODO: Change prompt based on interaction
        }

        public static void Open()
        {
            if (WasteMenu.IsOpen || Host.IsServer) return; // If this is open, ignore all prompts to show this. 
            IsOpen = true;
            _current?.SetClass("visible", IsOpen);
        }

		public static void Close()
        {
			if ( Host.IsServer ) return;
            IsOpen = false;
            _current?.SetClass("visible", IsOpen);
        }
    }
}
