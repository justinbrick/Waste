using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    // Each slot in a container
    class Slot : Panel
    {
        public Container SlotParent;

        public Slot()
        {
            StyleSheet.Load("/ui/Slot.scss");
        }
    }
}
