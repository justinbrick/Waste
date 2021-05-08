using Sandbox;
using Waste.UI;

namespace Waste
{
	public class Slot
	{
		private SlotWindow _window;
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
		public bool HasItem;

		public Slot()
		{
			HasItem = false;
			if (Host.IsClient)
			{
				Window = new SlotWindow();
			}
		}
	}
}
