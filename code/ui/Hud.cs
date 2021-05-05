using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    class WasteHud : Hud
    {
        public WasteHud()
        {
            if (!IsClient) return; // Are we not a client? Go away.

            RootPanel.StyleSheet.Load("/ui/Hud.scss");

            RootPanel.AddChild<ChatBox>();
            RootPanel.AddChild<WasteMenu>();
            RootPanel.AddChild<InteractionPrompt>();
			RootPanel.Add.Panel("literal_dot");
        }
    }
}
