using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    class WasteMenu : Panel
    {
        private static Panel Current;
        public static bool IsOpen { get; private set; }
        private Panel Inventory;

        public WasteMenu()
        {
            Current = this;
            IsOpen = false;
            AddChild<PlayerScreen>();
            Inventory = Add.Panel("inventory");
            Inventory.Add.Panel("pockets");
        }

        private static void DoBlur()
        {
            var player = Player.Local;
            if (player == null) return;
            if (IsOpen)
            {
                player.GetActiveCamera().DoFBlurSize = 1f;
                player.GetActiveCamera().DoFPoint = 2f;
            }
            else
            {
                player.GetActiveCamera().DoFBlurSize = 0;
                player.GetActiveCamera().DoFPoint = 0;
            }
        }

        // Open the menu
        public static void Open()
        {
            IsOpen = true;
            Current?.SetClass("isOpen", IsOpen);
            InteractionPrompt.Close();
            DoBlur();
        }

        public static void Close()
        {
            IsOpen = false;
            Current?.SetClass("isOpen", IsOpen);
            DoBlur();
        }

        public static void Toggle()
        {
            IsOpen = !IsOpen;
            Current?.SetClass("isOpen", IsOpen);
            DoBlur(); 
        }
    }
}
