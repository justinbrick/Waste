using System.Buffers;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using Sandbox;
using Sandbox.UI;
using Waste.Storage;

namespace Waste.UI
{
	public partial class InventoryWindow : Panel
	{
		private static InventoryWindow _current;
		private static Panel _pocketSlot;
		private static Panel _vestSlot;
		private static Panel _backpackSlot;
		private static Panel _caseSlot;
		private static ContainerWindow _pocketWindow;
		private static ContainerWindow _vestWindow;
		private static ContainerWindow _backpackWindow;
		private static ContainerWindow _caseWindow;
		public static Icon CurrentIcon;

		public InventoryWindow()
		{
			Host.AssertClient();
			StyleSheet.Load("/ui/InventoryWindow.scss");

			var player = Local.Pawn;
			var inventory = player.Inventory as WasteInventory;
			
			_pocketSlot = AddChild<SlotWindow>("pocket");
            _vestSlot = AddChild<SlotWindow>( "vest" );
            _vestSlot.Style.Top = Length.Pixels( 120 );
            _backpackSlot = AddChild<SlotWindow>( "backpack" );
            _backpackSlot.Style.Top = Length.Pixels( 210 );
            _caseSlot = AddChild<SlotWindow>( "case" );
            _caseSlot.Style.Top = Length.Pixels( 300 );
            
            
            // This is actually terrible but I don't know a better way to do this.
            var pockets = inventory?.Pockets;
            var backpack = inventory?.Backpack;
            var itemCase = inventory?.Case;
            var vest = inventory?.Vest;
            if (pockets != null)
            {
	            var pocketWindow =
		            new ContainerWindow( pockets, true )
		            {
			            Parent = this,
			            Style =
			            {
				            Position = PositionMode.Absolute,
				            Left = Length.Pixels( 110 ),
				            Top = Length.Pixels( 25 )
			            }
		            };
	            pockets.Window = pocketWindow;
            }
            float vestSize = SlotWindow.SLOT_SIZE;
            if ( vest != null )
            {
            	vestSize = vest.Size.y * SlotWindow.SLOT_SIZE;
                var vestWindow =
	                new ContainerWindow( vest, true )
	                {
		                Parent = this,
		                Style =
		                {
			                Position = PositionMode.Absolute,
			                Left = Length.Pixels( 110 ),
			                Top = Length.Pixels( 135 )
		                }
	                };
                vest.Window = vestWindow;
            }
            float backpackSize = SlotWindow.SLOT_SIZE;
            if (backpack != null)
            {
            	backpackSize = backpack.Size.y * SlotWindow.SLOT_SIZE;
                var backpackWindow =
	                new ContainerWindow( backpack )
	                {
		                Parent = this,
		                Style =
		                {
			                Position = PositionMode.Absolute,
			                Left = Length.Pixels( 110 ),
			                Top = Length.Pixels( 165 + vestSize )
		                }
	                };
                _backpackSlot.Style.Top = Length.Pixels(170 + vestSize);
                backpack.Window = backpackWindow;
            }
            if (itemCase != null)
            {
	            var itemCaseWindow =
		            new ContainerWindow( itemCase )
		            {
			            Parent = this,
			            Style =
			            {
				            Position = PositionMode.Absolute,
				            Left = Length.Pixels( 110 ),
				            Top = Length.Pixels( 195 + vestSize + backpackSize )
			            }
		            };
	            _caseSlot.Style.Top = Length.Pixels(200 + vestSize + backpackSize);
	            itemCase.Window = itemCaseWindow;
            }

            _current = this;

		}
		// If the sizes have changed on the backpack or similar, then this will resize them properly so they don't break.
		public static void UpdateSizes()
		{
			var inventory = Local.Pawn.Inventory as WasteInventory;
			float vestSize = SlotWindow.SLOT_SIZE;
			if (inventory?.Vest != null)
			{
				vestSize = SlotWindow.SLOT_SIZE * inventory.Vest.Size.y; 
			}
			float backpackSize = SlotWindow.SLOT_SIZE;
			if (inventory?.Backpack != null)
			{
				_backpackWindow.Style.Top = Length.Pixels( 165 + vestSize );
				_backpackSlot.Style.Top = Length.Pixels( 170 + vestSize );
			}
			if (inventory?.Case != null)
			{
				_caseWindow.Style.Top = Length.Pixels( 195 + vestSize + backpackSize );
				_caseSlot.Style.Top = Length.Pixels( 200 + vestSize + backpackSize );
			}
		}

		// The logic of these should be done serverside, we aren't going to question it, if it breaks then something
		// has fucked up on the server.

		// Add something to our inventory?
		[ClientRpc]
		public static void AddItem( WasteContainer container, Slot slot, WasteItem item)
		{
			if ( container.Window == null ) return;
			Log.Info("For the love of god it's still running");
			item.Icon ??= new Icon( item ) {Parent = container.Window};
			var icon = item.Icon;
			icon.Style.Left = Length.Pixels( SlotWindow.SLOT_SIZE * slot.Position.x );
			icon.Style.Top = Length.Pixels( SlotWindow.SLOT_SIZE * slot.Position.y );
		}

		// Whether or not someone wants to equip this item?
		[ClientRpc]
		public static void EquipItem( WasteItem item )
		{
			
		}

		// We are dropping an item.
		[ClientRpc]
		public static void DropItem( WasteItem item )
		{
			if ( item is WasteContainer container )
			{
				container.Window?.Delete();
				container.Window = null;
			}
			
			item.Icon?.Delete();
			item.Icon = null;
		}
	}
}
