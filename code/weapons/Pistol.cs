using Sandbox;
using Waste.Storage;
namespace Waste
{
    public class Pistol : WasteWeapon
    {
		public override string ID => "waste_pistol";

		public Pistol() : base ()
		{
			Size = new Vector2(2, 1);
		}
	}
}
