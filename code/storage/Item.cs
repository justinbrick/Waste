using Sandbox;

namespace Waste
{
    public class WasteItem : BaseCarriable
    {
        public long ID { get; protected set; } // Weapon ID
        public string UID { get; protected set; } // Unique ID
        public double Value { get; protected set; } // The value of the item
        public Vector2 Size { get; protected set; } // The size of the item

        public override bool CanCarry(Entity carrier)
        {
            throw new System.NotImplementedException();
        }

        public override void OnCarryDrop(Entity parentEntity)
        {
            throw new System.NotImplementedException();
        }

        public override void OnCarryStart(Entity parentEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}
