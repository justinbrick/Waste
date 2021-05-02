using Sandbox;
using Waste.UI;

namespace Waste
{
    [Library("waste", Title = "Waste")]
    class WasteGame : Game 
    {
        public WasteGame()
        {
            Log.Info("Game Started");
            if (IsServer)
                new WasteHud();
        }

        public override Player CreatePlayer() => new WastePlayer();
        
    }
}
