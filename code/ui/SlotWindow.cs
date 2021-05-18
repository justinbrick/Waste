using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Waste.Storage;

namespace Waste.UI
{
    // Each slot in a container
    public class SlotWindow : Panel
    {
	    private static List<SlotWindow> _activeSlots = new List<SlotWindow>();
		public const int SLOT_SIZE = 80; // The size of each slot in pixels.
		public bool? Valid; // Is there a valid item hovering over this?
        public Slot Slot; // The slot for this container.

        public SlotWindow()
        {
			Valid = null;
			StyleSheet.Load("/ui/SlotWindow.scss");
        }
        
        public override void OnEvent( string eventName )
        {
	        base.OnEvent( eventName );

	        switch ( eventName )
	        {
		        case "onmouseover":
			        ColorSlots();
			        break;
		        case "onmouseout": // We don't want these colors staying in this case.
			        ClearColors();
			        break;
	        }
        }
        
        // Clear the colors of all slots. This is so we can change this externally.
        public static void ClearColors()
        {
	        foreach ( var slot in _activeSlots )
	        {
		        if ( slot == null ) continue;
		        slot.Valid = null;
	        }
	        _activeSlots.Clear(); // Now we clear the list.
        }

        private void ColorSlots()
        {
	        if ( Slot == null || InventoryWindow.CurrentIcon == null) return;
	        var item = InventoryWindow.CurrentIcon.Item;
	        var slots = Slot.WasteContainer.Slots;
	        var itemSize = item.Size;

	        if ( Slot.CanFit( item ) )
	        {
		        for ( int x = 0; x < itemSize.x; ++x )
		        {
			        for ( int y = 0; y < itemSize.y; ++y )
			        {
				        var slot = slots[(int)Slot.Position.x + x, (int)Slot.Position.y + y].Window;
				        slot.Valid = true;
				        _activeSlots.Add( slot ); // Add this to the active list.
			        }
		        }
	        }
	        else
	        {
		        for ( int x = (int)Slot.Position.x, realX = 0; x < Slot.WasteContainer.ContainerSize.x && realX < itemSize.x; ++x, ++realX)
		        {
			        for ( int y = (int)Slot.Position.y, realY = 0; y < Slot.WasteContainer.ContainerSize.y && realY < itemSize.y; ++y, ++realY)
			        {
				        var slot = slots[x, y].Window;
				        slot.Valid = false;
				        _activeSlots.Add( slot );
			        }
		        }
	        }
        }
        
        public override void Tick()
		{
			base.Tick();

			switch (Valid)
			{
				case null:
					SetClass( "invalid", false );
					SetClass( "valid", false );
					break;
				case true:
					SetClass( "invalid", false );
					SetClass( "valid", true );
					break;
				case false:
					SetClass( "invalid", true );
					SetClass( "valid", false );
					break;
			}
		}
	}
}
