﻿using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    class WasteMenu : Panel
    {
        private static WasteMenu Current;
        public static bool IsOpen { get; private set; }
        private Panel Inventory;
		private Panel BackpackSlot;
		private Panel CaseSlot;

        public WasteMenu()
        {
			var player = Player.Local;
			var inventory = player.Inventory as WasteInventory;

			Current = this;
            IsOpen = false;
            AddChild<PlayerScreen>();
            Inventory = Add.Panel("inventory");
			Inventory.AddChild<SlotWindow>("pocket");
			Inventory.AddChild<SlotWindow>( "vest" );
			var icon = Icon.GenerateIcon(new Pistol());
			icon.Parent = Inventory;
			icon.Style.Width = Length.Pixels( 160 );
			icon.Style.Height = Length.Pixels( 80 );
			BackpackSlot = Inventory.AddChild<SlotWindow>( "backpack" );
			CaseSlot = Inventory.AddChild<SlotWindow>( "case" );

			// This is actually terrible but I don't know a better way to do this.
			var pockets = inventory?.Pockets;
			var backpack = inventory?.Backpack;
			var itemCase = inventory?.Case;
			var vest = inventory?.Vest;
			if (pockets != null)
			{
				var pocketWindow = pockets.Window;
				pocketWindow.Parent = Inventory;
				pocketWindow.Style.Position = PositionMode.Absolute;
				pocketWindow.Style.Left = Length.Pixels( 110 );
				pocketWindow.Style.Top = Length.Pixels( 25 );
			}
			float vestSize = SlotWindow.SLOT_SIZE;
			if ( vest != null )
			{
				vestSize = vest.Size.y * SlotWindow.SLOT_SIZE;
				var vestWindow = vest.Window;
				vestWindow.Parent = Inventory;
				vestWindow.Style.Position = PositionMode.Absolute;
				vestWindow.Style.Left = Length.Pixels( 110 );
				vestWindow.Style.Top = Length.Pixels( 135 );
			}
			float backpackSize = SlotWindow.SLOT_SIZE;
			if (backpack != null)
			{
				backpackSize = backpack.Size.y * SlotWindow.SLOT_SIZE;
				var backpackWindow = backpack.Window;
				backpackWindow.Parent = Inventory;
				backpackWindow.Style.Position = PositionMode.Absolute;
				backpackWindow.Style.Left = Length.Pixels( 110 );
				backpackWindow.Style.Top = Length.Pixels( 165 + vestSize);
				BackpackSlot.Style.Top = Length.Pixels(170 + vestSize);
			}
			if (itemCase != null)
			{
				var itemCaseWindow = itemCase.Window;
				itemCaseWindow.Parent = Inventory;
				itemCaseWindow.Style.Position = PositionMode.Absolute;
				itemCaseWindow.Style.Left = Length.Pixels( 110 );
				itemCaseWindow.Style.Top = Length.Pixels( 195 + vestSize + backpackSize);
				CaseSlot.Style.Top = Length.Pixels(200 + vestSize + backpackSize);
			}
		}

		// If the sizes have changed on the backpack or similar, then this will resize them properly so they don't break.
		public static void UpdateSizes()
		{
			var inventory = Player.Local.Inventory as WasteInventory;
			float vestSize = SlotWindow.SLOT_SIZE;
			if (inventory?.Vest != null)
			{
				vestSize = SlotWindow.SLOT_SIZE * inventory.Vest.Size.y; 
			}
			float backpackSize = SlotWindow.SLOT_SIZE;
			if (inventory?.Backpack != null)
			{
				var backpackWindow = inventory.Backpack.Window;
				backpackWindow.Style.Top = Length.Pixels( 165 + vestSize );
				Current.BackpackSlot.Style.Top = Length.Pixels( 170 + vestSize );
			}
			if (inventory?.Case != null)
			{
				var caseWindow = inventory.Case.Window;
				caseWindow.Style.Top = Length.Pixels( 195 + vestSize + backpackSize );
				Current.CaseSlot.Style.Top = Length.Pixels( 200 + vestSize + backpackSize );
			}
		}

		public override void OnButtonEvent( ButtonEvent e )
		{
			base.OnButtonEvent( e );
			if (e.Button == "tab" && IsOpen)
			{
				Close();
			}
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
			Current.AcceptsFocus = true;
			Current.Focus();
			if (InteractionPrompt.IsOpen)
				InteractionPrompt.Close();
            DoBlur();
        }

        public static void Close()
        {
			IsOpen = false;
            Current?.SetClass("isOpen", IsOpen);
			Current.AcceptsFocus = false;
			Current.Focus();
            DoBlur();
			var player = Player.Local as WastePlayer;
			if ( player.IsLookingAtItem ) // Are we still looking at an item?
				InteractionPrompt.Open();
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
