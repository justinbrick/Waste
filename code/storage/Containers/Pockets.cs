using System.Buffers.Text;

namespace Waste.Storage.Containers
{
	public class Pockets : Container
	{
		public override Vector2 ContainerSize => new Vector2( 4,1 );
		public override Vector2 Size => Vector2.Zero;

		public Pockets() : base()
		{
			
		}
		
		public Pockets(bool isHeadless = false) : base(isHeadless)
		{
		}
	}
}
