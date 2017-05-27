using Microsoft.Xna.Framework;

namespace LightsOut2
{
    public abstract class GameScreen
    {
        #region Fields

        private ScreenManager screenManager;

        #endregion

        #region Properties

        public Color DefaultFontColor
        {
            get
            {
                return Color.Black;
            }
        }

        public Color DefaultBackgroundColor
        {
            get
            {
                return Color.PeachPuff;
            }
        }

        public ScreenManager ScreenManager
        {
            get
            {
                return screenManager;
            }
        }

        #endregion

        #region Initialization

        public GameScreen(ScreenManager manager)
        {
            this.screenManager = manager;
        }

        public virtual void Initialize() { }

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }

        #endregion

        #region Update and Draw

        public virtual void Update(GameTime gameTime) { }

        public abstract void Draw(GameTime gameTime);

        #endregion
    }
}

