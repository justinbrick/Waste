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
		private static List<string> _icons = new();
		private bool _beingDragged;
		private Image _image;
		private Length? _lastLeft;
		private Length? _lastTop;
		public Panel Container; // This is the container that this icon is currently stored in.
		public WasteItem Item { get; private set; } // The item that this icon is currently representing.
		
		public Icon()
		{
			StyleSheet.Load("ui/Icon.scss");
			Container = WasteMenu.Inventory;
			Parent = WasteMenu.Inventory;
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
			Log.Info( "EVENT: " + eventName );
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
			_lastLeft = Style.Top;
			_lastTop = Style.Top;
			Style.PointerEvents = "none"; // Let pointer events pass through now.
			Parent = WasteHud.CurrentPanel;
			WasteMenu.CurrentIcon = this;
		}

		private void OnMouseUp()
		{
			_beingDragged = false;
			Style.Left = _lastLeft;
			Style.Top = _lastTop;
			Style.PointerEvents = "all";
			Parent = Container;
			SlotWindow.ClearColors(); // Clear all the colors. This is because they'll remain if we don't do this.
			WasteMenu.CurrentIcon = null;
		}

		// Generates an icon normally, and then parents it to a container.
		public static Icon GenerateIcon( WasteItem item, Panel container )
		{
			var icon = GenerateIcon( item );
			icon.Container = container;
			icon.Parent = container;
			return icon;
		}

		// Generates an icon and returns it. Will return pre-existing icons if they've already been rendered.
		public static Icon GenerateIcon(WasteItem item)
		{
			Host.AssertClient();
			var icon = new Icon();
			// Do we already have this icon? Here you go.
			foreach ( var iconName in _icons)
			{
				if ( iconName == null || iconName != item.ID) continue;
				icon._image = icon.Add.Image("scene:" + item.ID);
				icon.Item = item;
				icon.Style.Width = Length.Pixels( item.Size.x * SlotWindow.SLOT_SIZE );
				icon.Style.Height = Length.Pixels( item.Size.y * SlotWindow.SLOT_SIZE );
				return icon;
			}
		
			using (SceneWorld.SetCurrent(new SceneWorld()))
			{
				var sceneModel = new SceneObject( Model.Load( item.ModelPath ), Transform.Zero );
				var angles = new Angles( 0, 90, 0 );
				Light.Point(angles.Direction * -10, 200, Color.White * 100f );
				var capture = SceneCapture.Create( item.ID,(int) (128 * item.Size.x),(int) (128 * item.Size.y) );
				capture.SetCamera( Vector3.Right * (item.Size.x/2) + angles.Direction * -30, angles, 70 );
			}

			icon._image = icon.Add.Image( "scene:" + item.ID );
			icon.Item = item;
			icon.Style.Width = Length.Pixels( item.Size.x * SlotWindow.SLOT_SIZE );
			icon.Style.Height = Length.Pixels( item.Size.y * SlotWindow.SLOT_SIZE );

			_icons.Add(item.ID); // We've now gotten this, so no need to make another rendering.
			return icon;
		}
	}
}
