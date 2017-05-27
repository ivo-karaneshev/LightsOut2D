using Microsoft.Xna.Framework;
using LightsOut2.GameComponents;
using Microsoft.Xna.Framework.Input;

namespace LightsOut2.Screens
{
    public class CreditsScreen : GameScreen
    {
        private Rectangle tuLogoRect;
        private Label firstNameLabel;
        private Label lastNameLabel;

        public CreditsScreen(ScreenManager manager) : base(manager) { }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(0).Buttons.Back == ButtonState.Pressed)
            {
                ScreenManager.SetActiveScreen(new MainMenuScreen(ScreenManager));
            }
        }

        public override void Initialize()
        {
            InitializeLayout();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(DefaultBackgroundColor);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(ScreenManager.LogoTexture, tuLogoRect, DefaultBackgroundColor);
            spriteBatch.DrawString(ScreenManager.GameFont, lastNameLabel.Text, lastNameLabel.TextPosition, DefaultFontColor);
            spriteBatch.DrawString(ScreenManager.GameFont, firstNameLabel.Text, firstNameLabel.TextPosition, DefaultFontColor);

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

            tuLogoRect = new Rectangle();
            tuLogoRect.Width = ScreenManager.LogoTexture.Width > panel.Width ? panel.Width : ScreenManager.LogoTexture.Width;
            float scale = tuLogoRect.Width / ScreenManager.LogoTexture.Width;
            tuLogoRect.Height = (int)(ScreenManager.LogoTexture.Height * scale);
            tuLogoRect.X = panel.X + (panel.Width - tuLogoRect.Width) / 2;
            tuLogoRect.Y = panel.Y;

            lastNameLabel = new Label();
            lastNameLabel.Text = "Karaneshev";
            var textSize = ScreenManager.GameFont.MeasureString(lastNameLabel.Text);
            lastNameLabel.TextPosition = new Vector2()
            {
                X = panel.X + (panel.Width - textSize.X) / 2,
                Y = panel.Y + tuLogoRect.Height + textSize.Y
            };

            firstNameLabel = new Label();
            firstNameLabel.Text = "Ivo";
            textSize = ScreenManager.GameFont.MeasureString(firstNameLabel.Text);
            firstNameLabel.TextPosition = new Vector2()
            {
                X = panel.X + (panel.Width - textSize.X) / 2,
                Y = panel.Y + tuLogoRect.Height + 2 * textSize.Y
            };
        }
    }
}