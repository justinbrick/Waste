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
			        OnMouseOver();
			        break;
		        case "onmouseout":
			        OnMouseOut();
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
        
        public void OnMouseOver()
        {
	        // Sometimes we'll hover over the slots representing items themselves. They don't have slots associated. Either this, or there is no icon currently being hovered.
	        ColorSlots( true );
        }

        public void OnMouseOut()
        {
	        ColorSlots( null );
        }

        private void ColorSlots(bool? valid)
        {
	        ClearColors(); // Clear all the colors before we do any more stuff. 
	        if ( Slot == null || WasteMenu.CurrentIcon == null) return;
	        var item = WasteMenu.CurrentIcon.Item;
	        var slots = Slot.Container.Slots;
	        var itemSize = item.Size;
	       
	        if ( !Slot.CanFit( item ) ) return; 
		    for ( int x = 0; x < itemSize.x; ++x )
	        {
		        for ( int y = 0; y < itemSize.y; ++y )
		        {
			        var slot = slots[(int)Slot.Position.x + x,(int)Slot.Position.y + y].Window; 
			        slot.Valid = valid;
			        _activeSlots.Add( slot ); // Add this to the active list.
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
