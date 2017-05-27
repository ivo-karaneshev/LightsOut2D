using Microsoft.Xna.Framework;
using LightsOut2.GameComponents;
using Microsoft.Xna.Framework.Input;

namespace LightsOut2.Screens
{
    public class HighscoreScreen : GameScreen
    {
        private Label scoreLabel;
        private int? bestTime;

        public HighscoreScreen(ScreenManager manager) : base(manager)
        {
            var game = ScreenManager.Game as GameMain;
            bestTime = game.PlayersManager.GetBestTime();
        }

        public override void Initialize()
        {
            InitializeLayout();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(0).Buttons.Back == ButtonState.Pressed)
            {
                ScreenManager.SetActiveScreen(new MainMenuScreen(ScreenManager));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.LightSalmon);

            var spriteBatch = ScreenManager.SpriteBatch;
            var fontColor = new Color(70, 70, 70);

            spriteBatch.Begin();

            spriteBatch.DrawString(ScreenManager.GameFont, scoreLabel.Text, scoreLabel.TextPosition, fontColor);

            spriteBatch.End();
        }

        private void InitializeLayout()
        {
            var viewport = ScreenManager.GraphicsDevice.Viewport;

            scoreLabel = new Label();
            if (bestTime.HasValue)
            {
                scoreLabel.Text = "Best time: " + bestTime.Value;
            }
            else
            {
                scoreLabel.Text = "No scores yet";
            }

            var textSize = ScreenManager.GameFont.MeasureString(scoreLabel.Text);
            scoreLabel.TextPosition = new Vector2()
            {
                X = (viewport.Width - textSize.X) / 2,
                Y = (viewport.Height - textSize.Y) /2
            };
        }
    }
}