using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    // Each slot in a container
    class Slot : Panel
    {
		public static readonly int SLOT_SIZE = 80; // The size of each slot in pixels.

        public Container SlotParent;

        public Slot()
        {
            StyleSheet.Load("/ui/Slot.scss");
        }
    }
}
