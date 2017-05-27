using Microsoft.Xna.Framework;

namespace LightsOut2.Screens
{
    public class ChangePlayerScreen : GameScreen
    {
        public ChangePlayerScreen(ScreenManager manager) : base(manager)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Purple);
        }
    }
}