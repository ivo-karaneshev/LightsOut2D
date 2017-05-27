using LightsOut2.Extensions;
using LightsOut2.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace LightsOut2.Screens
{
    public class GameOverScreen : GameScreen
    {
        private TouchCollection touchCollection;

        private int time;
        private int bestTime;
        private Label winLabel;
        private Label currentScoreLabel;
        private Label bestScoreLabel;
        private Button restartBtn;

        public GameOverScreen(ScreenManager manager, int time) : base(manager)
        {
            this.time = time;
            var game = ScreenManager.Game as GameMain;
            bestTime = game.PlayersManager.GetBestTime().Value;
        }

        public override void Initialize()
        {
            InitializeLayout();
        }

        public override void Update(GameTime gameTime)
        {
            touchCollection = TouchPanel.GetState();

            foreach (var touch in touchCollection)
            {
                if (touch.State == TouchLocationState.Pressed)
                {
                    if (restartBtn.ItemBox.Contains(touch.Position))
                    {
                        ScreenManager.SetActiveScreen(new GameplayScreen(ScreenManager));
                    }
                }
            }

            if (GamePad.GetState(0).Buttons.Back == ButtonState.Pressed)
            {
                ScreenManager.SetActiveScreen(new MainMenuScreen(ScreenManager));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.LightGreen);

            var spriteBatch = ScreenManager.SpriteBatch;
            var fontColor = new Color(70, 70, 70);

            spriteBatch.Begin();

            spriteBatch.DrawString(ScreenManager.GameFont, winLabel.Text, winLabel.TextPosition, fontColor, winLabel.TextScale);
            spriteBatch.DrawString(ScreenManager.GameFont, currentScoreLabel.Text, currentScoreLabel.TextPosition, fontColor);
            spriteBatch.DrawString(ScreenManager.GameFont, bestScoreLabel.Text, bestScoreLabel.TextPosition, fontColor);

            //spriteBatch.Draw(ScreenManager.WhiteTexture, restartBtn.ItemBox, Color.Red);
            spriteBatch.DrawString(ScreenManager.GameFont, restartBtn.Text, restartBtn.TextPosition, fontColor);

            spriteBatch.End();
        }

        private void InitializeLayout()
        {
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var panel = new Rectangle();
            panel.Width = (int)(viewport.Width * 0.8);
            panel.Height = (int)(viewport.Height * 0.8);
            panel.X = (viewport.Width - panel.Width) / 2;
            panel.Y = (viewport.Height - panel.Height) / 3;

            winLabel = new Label();
            winLabel.Text = "You win!";
            var textSize = ScreenManager.GameFont.MeasureString(winLabel.Text);
            winLabel.TextPosition = new Vector2()
            {
                X = panel.X + (panel.Width - textSize.X) / 2,
                Y = panel.Y
            };
            winLabel.TextScale = 3 / 2;

            currentScoreLabel = new Label();
            currentScoreLabel.Text = "Current time: " + time;
            textSize = ScreenManager.GameFont.MeasureString(currentScoreLabel.Text);
            currentScoreLabel.TextPosition = new Vector2()
            {
                X = panel.X + (panel.Width - textSize.X) / 2,
                Y = panel.Y + 2 * textSize.Y
            };

            bestScoreLabel = new Label();
            bestScoreLabel.Text = "Best time: " + bestTime;
            textSize = ScreenManager.GameFont.MeasureString(bestScoreLabel.Text);
            bestScoreLabel.TextPosition = new Vector2()
            {
                X = panel.X + (panel.Width - textSize.X) / 2,
                Y = panel.Y + 3 * textSize.Y
            };

            restartBtn = new Button();
            restartBtn.Text = "Restart";
            textSize = ScreenManager.GameFont.MeasureString(restartBtn.Text);
            restartBtn.TextPosition = new Vector2()
            {
                X = panel.X + (panel.Width - textSize.X) / 2,
                Y = panel.Y + panel.Height - textSize.Y
            };
            var btnBox = new Rectangle();
            btnBox.Width = (int)(textSize.X * 1.2);
            btnBox.Height = (int)(textSize.Y * 1.2);
            btnBox.X = panel.X + (panel.Width - btnBox.Width) / 2;
            btnBox.Y = panel.Y + panel.Height - btnBox.Height;
            restartBtn.ItemBox = btnBox;
        }
    }
}