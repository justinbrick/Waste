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
			var player = Player.Local;
			var inventory = player.Inventory as WasteInventory;
			
            Current = this;
            IsOpen = false;
            AddChild<PlayerScreen>();
            Inventory = Add.Panel("inventory");
			Inventory.AddChild<Slot>("pocket");
			Inventory.AddChild<Slot>( "backpack" );
			Inventory.AddChild<Slot>( "case" );

			// This is actually terrible but I don't know a better way to do this.
			var pockets = inventory.Pockets.Window;
			var backpack = inventory.Backpack.Window;
			var itemCase = inventory.Case.Window;
			pockets.Parent = Inventory;
			pockets.Style.Position = PositionMode.Absolute;
			pockets.Style.Left = Length.Pixels(110);
			pockets.Style.Top = Length.Pixels(25);
			backpack.Parent = Inventory;
			backpack.Style.Position = PositionMode.Absolute;
			backpack.Style.Left = Length.Pixels( 110);
			backpack.Style.Top = Length.Pixels( 135 );
			itemCase.Parent = Inventory;
			itemCase.Style.Position = PositionMode.Absolute;
			itemCase.Style.Left = Length.Pixels( 110 );
			itemCase.Style.Top = Length.Pixels( 495 );
			//Style.Dirty();
		}

        // Blurs the screen
        // TODO: This runs like shit
        // Find something better
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
            if (IsOpen)
                Close();
            else
                Open();
        }
    }
}
