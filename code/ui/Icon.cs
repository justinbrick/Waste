using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox;
using System.Collections.Generic;

namespace Waste.UI
{

	// TODO: This is performance critical, we need to find a way to cache each icon into separate files instead of rendering every single one.
	public class Icon : Panel
	{
		// ID, Image
		private static List<string> icons = new();
		public WasteItem Item; // The item that this icon is currently representing.
		private Image image;
		public Icon()
		{
			StyleSheet.Load("ui/Icon.scss");
		}

		public static Icon GenerateIcon(WasteItem item)
		{
			Host.AssertClient();

			var icon = new Icon();
			// Do we already have this icon? Here you go.
			foreach ( var iconName in icons)
			{
				if ( iconName == null ) continue;
				if (iconName == item.ID)
				{
					icon.image = icon.Add.Image("scene:" + item.ID);
					icon.Item = item;
					icon.Style.Width = Length.Pixels( item.Size.x * SlotWindow.SLOT_SIZE );
					icon.Style.Height = Length.Pixels( item.Size.y * SlotWindow.SLOT_SIZE );
					return icon;
				}
			}
		
			using (SceneWorld.SetCurrent(new SceneWorld()))
			{
				var sceneModel = new SceneObject( Model.Load( item.ModelPath ), Transform.Zero );
				var angles = new Angles( 0, 90, 0 );
				Light.Point(angles.Direction * -10, 200, Color.White * 100f );
				var capture = SceneCapture.Create( item.ID,(int) (128 * item.Size.x),(int) (128 * item.Size.y) );
				capture.SetCamera( angles.Direction * -30, angles, 70 );
			}

			icon.image = icon.Add.Image( "scene:waste_pistol" );

			icon.Style.Width = Length.Pixels( item.Size.x * SlotWindow.SLOT_SIZE);
			icon.Style.Height = Length.Pixels( item.Size.y * SlotWindow.SLOT_SIZE );

			icons.Add(item.ID);
			return icon;
			
		}
	}
}
