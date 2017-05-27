using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LightsOut2
{
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields

        private GameScreen activeScreen;
        private SpriteBatch spriteBatch;
        private SpriteFont font30;
        private SpriteFont font60;
        private SpriteFont defaultFont;
        private SpriteFont gameFont;
        private Texture2D whiteTexture;
        private Texture2D tuLogoTexture;
        private bool initialized;

        #endregion

        #region Properties

        new public Game Game
        {
            get { return base.Game; }
        }

        new public GraphicsDevice GraphicsDevice
        {
            get { return base.GraphicsDevice; }
        }

        public bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public SpriteFont GameFont
        {
            get
            {
                if (gameFont == null)
                {
                    var viewport = GraphicsDevice.Viewport;
                    if (viewport.Width <= 480)
                    {
                        gameFont = font30;
                    }
                    else
                    {
                        gameFont = font60;
                    }
                }

                return gameFont;
            }
        }

        public SpriteFont DefaultFont
        {
            get
            {
                return defaultFont;
            }
        }

        public Texture2D WhiteTexture
        {
            get
            {
                return whiteTexture;
            }
        }

        public Texture2D LogoTexture
        {
            get
            {
                return tuLogoTexture;
            }
        }

        public GameScreen ActiveScreen
        {
            get
            {
                return activeScreen;
            }
        }

        public bool ShowFPS { get; set; }

        #endregion

        #region Initialization

        public ScreenManager(Game game) : base(game) { }

        public override void Initialize()
        {
            base.Initialize();
            activeScreen.Initialize();
        }

        protected override void LoadContent()
        {
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font30 = content.Load<SpriteFont>("font30");
            font60 = content.Load<SpriteFont>("font60");
            defaultFont = content.Load<SpriteFont>("defaultfont");
            tuLogoTexture = content.Load<Texture2D>("tu-logo");
            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData(new Color[] { Color.White });

            initialized = true;

            activeScreen.LoadContent();
        }

        protected override void UnloadContent()
        {
            activeScreen.UnloadContent();
        }

        public void SetActiveScreen(GameScreen screen)
        {
            if (screen != null)
            {
                if (Initialized)
                {
                    screen.Initialize();
                    screen.LoadContent();
                }

                if (activeScreen != null)
                {
                    activeScreen.UnloadContent();
                }

                activeScreen = screen;
            }
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            activeScreen.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            activeScreen.Draw(gameTime);

            if (ShowFPS)
            {
                DrawFps(gameTime);
            }
        }

        private void DrawFps(GameTime gameTime)
        {
            var fps = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            spriteBatch.Begin();
            spriteBatch.DrawString(DefaultFont, fps.ToString("F0"), new Vector2(0, 0), Color.Black);
            spriteBatch.End();
        }

        #endregion
    }
}
