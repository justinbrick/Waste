using Sandbox;
using Waste.UI;

namespace Waste
{
	public class Slot
	{
		private SlotWindow _window;
		public bool HasItem;
		public Container Container; // The container this belongs to.
		public Vector2 Position; // Where this slot is in it's respective container.
		public SlotWindow Window
		{
			get
			{
				Host.AssertClient();
				return _window;
			}
			private set
			{
				Host.AssertClient();
				_window = value; 
			}
		}

		public Slot()
		{
			HasItem = false;
			if (Host.IsClient)
			{
				Window = new SlotWindow() {Slot = this};
			}
		}
	}
}
