using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox;
using System.Collections.Generic;
using Waste.Storage;


namespace Waste.UI
{

	// TODO: This will create
	public class Icon : Panel
	{
		private static Dictionary<string, SceneWorld> _icons = new();
		private bool _beingDragged;
		private Image _image;
		private Length? _lastLeft;
		private Length? _lastTop;
		public Panel Container; // This is the container that this icon is currently stored in.
		public WasteItem Item { get; private set; } // The item that this icon is currently representing.
		
		public Icon()
		{
			StyleSheet.Load("ui/Icon.scss");
		}

		public Icon(WasteItem item)
		{
			Host.AssertClient();
			StyleSheet.Load("ui/Icon.scss");

			_image = Add.Image( "scene:" + item.ID );
			Item = item;
			Style.Width = Length.Pixels( item.Size.x * SlotWindow.SLOT_SIZE );
			Style.Height = Length.Pixels( item.Size.y * SlotWindow.SLOT_SIZE );
			if ( _icons.ContainsKey( item.ID ) ) return;

			using (SceneWorld.SetCurrent(new SceneWorld()))
			{
				var sceneModel = new SceneObject( Model.Load( item.ModelPath ), Transform.Zero );
				var angles = new Angles( 0, 90, 0 );
				Light.Point(angles.Direction * -10, 200, Color.White * 100f );
				var capture = SceneCapture.Create( item.ID,(int) (128 * item.Size.x),(int) (128 * item.Size.y) );
				capture.SetCamera( Vector3.Right * (item.Size.x/2) + angles.Direction * -30, angles, 70 );
			}
		}

		public override void Tick()
		{
			base.Tick();

			if ( _beingDragged )
			{
				Style.Left = Length.Pixels( Mouse.Position.x - Item.Size.x * SlotWindow.SLOT_SIZE / 2);
				Style.Top = Length.Pixels( Mouse.Position.y - Item.Size.y * SlotWindow.SLOT_SIZE / 2);
				Style.Dirty(); // Dirty the stylesheet so it re-renders.
			}
		}
		
		public override void OnEvent( string eventName )
		{
			switch ( eventName )
			{
				case "onmousedown":
					OnMouseDown();
					break;
				case "onmouseup":
					OnMouseUp();
					break;
			}
		}
		
		private void OnMouseDown()
		{
			_beingDragged = true;
			_lastLeft = Style.Left;
			_lastTop = Style.Top;
			Style.PointerEvents = "none"; // Let pointer events pass through now.
			Parent = Local.Hud;
			InventoryWindow.CurrentIcon = this;
		}

		private void OnMouseUp()
		{
			_beingDragged = false;
			Style.Left = _lastLeft;
			Style.Top = _lastTop;
			Style.PointerEvents = "all";
			Parent = Container;
			SlotWindow.ClearColors(); // Clear all the colors. This is because they'll remain if we don't do this.
			InventoryWindow.CurrentIcon = null;
		}
	}
}
