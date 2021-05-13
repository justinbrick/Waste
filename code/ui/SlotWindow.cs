using System.Collections.Generic;
using System.Reflection.Metadata;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    // Each slot in a container
    public class SlotWindow : Panel
    {
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
	        }
        }

        public void OnMouseOver()
        {
	        // Sometimes we'll hover over the slots representing items themselves. They don't have slots associated. Either this, or there is no icon currently being hovered.
	        if ( Slot == null || WasteMenu.CurrentIcon == null ) return;
	        var slots = Slot.Container.Slots;
	        var containerHeight = slots.GetLength( 0 );
	        var containerWidth = slots.GetLength( 1 );
	       
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
