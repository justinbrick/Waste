using Sandbox;
using Sandbox.UI;
using Waste.Storage;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    class WasteMenu : Panel
    {
        private static WasteMenu _current;
        public static Panel Inventory { get; private set; }
        public static bool IsOpen { get; private set; }


        public WasteMenu()
        {
			var player = Local.Pawn;
			var inventory = player.Inventory as WasteInventory;

			_current = this;
            IsOpen = false;
            AddChild<PlayerScreen>();
            Inventory = AddChild<InventoryWindow>( "inventory" );
        }
        

		public override void OnButtonEvent( ButtonEvent e )
		{
			base.OnButtonEvent( e );
			if (e.Button == "tab" && IsOpen)
			{
				Close();
			}
		}

		

        public static void Open()
        {
            IsOpen = true;
            _current?.SetClass("isOpen", IsOpen);
			_current.AcceptsFocus = true;
			_current.Focus();
			if (InteractionPrompt.IsOpen)
				InteractionPrompt.Close();
        }

        public static void Close()
        {
			IsOpen = false;
            _current?.SetClass("isOpen", IsOpen);
			_current.AcceptsFocus = false;
			_current.Focus();
			var player = Local.Pawn as WastePlayer;
			if ( player.IsLookingAtItem ) // Are we still looking at an item?
				InteractionPrompt.Open();
		}

        public static void Toggle()
        {
	        Host.AssertClient();
            if (IsOpen)
                Close();
            else
                Open();
        }
    }
}
