using Sandbox;
using Waste.UI;

namespace Waste
{
    [Library("waste", Title = "Waste")]
    class WasteGame : Game 
    {
        public WasteGame()
        {
	        new WasteHud();
        }


        public override void ClientJoined( Client client )
        {
	        base.ClientJoined( client );
	        var player = new WastePlayer();
	        client.Pawn = player;
	        player.Respawn();
        }
    }
}
