using Sandbox;

namespace Waste
{
    class Pistol : WasteWeapon
    {
		public override string ID => "waste_pistol";

		public Pistol()
		{
			UID = GenerateUID();
		}
    }
}
