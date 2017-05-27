using LightsOut2.Screens;
using Microsoft.Xna.Framework;

namespace LightsOut2
{
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        ScreenManager screenManager;
        PlayerManager playersManager;

        public PlayerManager PlayersManager
        {
            get
            {
                return playersManager;
            }
        }

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            playersManager = new PlayerManager();
            playersManager.InitializePlayer();

            InitializeMobileGraphics();
        }

        protected override void Initialize()
        {
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            screenManager.ShowFPS = true;
            screenManager.SetActiveScreen(new MainMenuScreen(screenManager));

            base.Initialize();
        }

        private void InitializeMobileGraphics()
        {
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;

            graphics.ApplyChanges();
        }
    }
}
