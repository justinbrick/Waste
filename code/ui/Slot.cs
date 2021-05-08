﻿using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    // Each slot in a container
    class Slot : Panel
    {
		public static readonly int SLOT_SIZE = 80; // The size of each slot in pixels.
		public bool? Valid; // Is there a valid item hovering over this?
        public Container SlotParent;

        public Slot()
        {
			Valid = null;
            StyleSheet.Load("/ui/Slot.scss");
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
