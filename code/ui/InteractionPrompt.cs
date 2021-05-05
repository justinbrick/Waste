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
            text = Add.Label("Placeholder", "text");
        }



        public static void Open()
        {
            if (WasteMenu.IsOpen) return; // If this is open, ignore all prompts to show this. 
            IsOpen = true;
            Current?.SetClass("visible", IsOpen);
        }

        public static void Close()
        {
            IsOpen = false;
            Current?.SetClass("visible", IsOpen);
        }
    }
}
