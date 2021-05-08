using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Waste.UI
{
    class PlayerScreen : Panel
    {
        private static Panel Current;
		private SceneCapture sceneCapture;
		private float _startTime;
		private AnimSceneObject sceneObject;
		private Angles CamAngles;

        public PlayerScreen()
        {
			CamAngles = new Angles(0, 180f, 0);
			
			using (SceneWorld.SetCurrent(new SceneWorld()))
			{
				sceneObject = new AnimSceneObject(Player.Local.GetModel(), Transform.Zero );
				_startTime = Time.Now;
				Light.Point(Vector3.Up * 10.0f + Vector3.Forward * 100.0f, 200, Color.White * 15000.0f);
				sceneCapture = SceneCapture.Create("player_view", 512, 512);
				sceneCapture.SetCamera(Vector3.Up * 40 + CamAngles.Direction * -100, CamAngles, 45);
			}

			Add.Image("scene:player_view");
		}

		public override void Tick()
		{
			base.Tick();
			UpdateAnims();	
		}

		private void UpdateAnims()
		{
			var player = Player.Local;
			if ( player == null ) return;
			sceneObject.Update( Time.Now - _startTime );
			sceneObject.SetAnimParam( "b_grounded", player.GetBoolAnimParam( "b_grounded" ) );
			sceneObject.SetAnimParam( "b_swim", player.GetBoolAnimParam( "b_swim" ) );
			sceneObject.SetAnimParam( "b_ducked", player.GetBoolAnimParam( "b_ducked" ) );
		}

		public override void OnDeleted()
		{
			base.OnDeleted();
			sceneCapture?.Delete();
			sceneCapture = null;
		}
	}
}
