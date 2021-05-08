using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    class InteractionPrompt : Panel
    {
        public static bool IsOpen { get; private set; }
        private static Panel Current;
        private Label text;
        public string Text
        {
            set
            {
                text.Text = value;
                Current?.Style.Dirty();
            }
        }

        public InteractionPrompt()
        {
            Current = this;
            IsOpen = false;
            StyleSheet.Load("/ui/InteractionPrompt.scss");
            Add.Panel("interact_key").Add.Label("F");
            text = Add.Label("Pickup", "text"); // TODO: Change prompt based on interaction
        }

        public static void Open()
        {
            if (WasteMenu.IsOpen || Host.IsServer) return; // If this is open, ignore all prompts to show this. 
            IsOpen = true;
            Current?.SetClass("visible", IsOpen);
        }

		public static void Close()
        {
			if ( Host.IsServer ) return;
            IsOpen = false;
            Current?.SetClass("visible", IsOpen);
        }
    }
}
