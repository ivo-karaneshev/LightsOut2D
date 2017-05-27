using LightsOut2.Extensions;
using LightsOut2.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Linq;

namespace LightsOut2.Screens
{
    public class MainMenuScreen : GameScreen
    {
        private TouchCollection touchCollection;

        private List<Button> menuItems;

        public MainMenuScreen(ScreenManager screenManager) : base(screenManager)
        {
            InitializeMenuItems();
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
                foreach (var item in menuItems)
                {
                    if (item.ItemBox.Contains(touch.Position) && touch.State == TouchLocationState.Pressed)
                    {
                        switch (item.Text)
                        {
                            case "Play":
                                ScreenManager.SetActiveScreen(new GameplayScreen(ScreenManager));
                                break;
                            case "Highscores":
                                ScreenManager.SetActiveScreen(new HighscoreScreen(ScreenManager));
                                break;
                            case "Credits":
                                ScreenManager.SetActiveScreen(new CreditsScreen(ScreenManager));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(DefaultBackgroundColor);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            foreach (var item in menuItems)
            {
                DrawMenuItem(item, spriteBatch);
            }
            spriteBatch.End();
        }

        private void InitializeMenuItems()
        {
            menuItems = new List<Button>
            {
                new Button
                {
                    Text = "Play"
                },
                new Button
                {
                    Text = "Highscores"
                },
                new Button
                {
                    Text = "Credits"
                }
            };
        }

        private void InitializeLayout()
        {
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var menu = new Rectangle();
            menu.Width = (int)(viewport.Width * 0.8);
            menu.Height = (int)(viewport.Height * 0.6);
            menu.X = (viewport.Width - menu.Width) / 2;
            menu.Y = (viewport.Height - menu.Height) / 3;
            var itemBoxHeight = menu.Height / menuItems.Count;
            var scaleFactor = CalculateTextScaleFactor(menu.Width, itemBoxHeight);

            for (var i = 0; i < menuItems.Count; i++)
            {
                var textSize = ScreenManager.GameFont.MeasureString(menuItems[i].Text);
                textSize.X *= scaleFactor;
                textSize.Y *= scaleFactor;
                menuItems[i].TextScale = scaleFactor;

                var menuItemBox = new Rectangle();
                menuItemBox.Width = (int)(textSize.X * 1.2);
                menuItemBox.Height = itemBoxHeight;
                menuItemBox.X = menu.X + (menu.Width - menuItemBox.Width) / 2;
                menuItemBox.Y = menu.Y + i * itemBoxHeight;
                menuItems[i].ItemBox = menuItemBox;

                var textPosition = new Vector2();
                textPosition.X = menuItemBox.X + (menuItemBox.Width - textSize.X) / 2;
                textPosition.Y = menuItemBox.Y + (menuItemBox.Height - textSize.Y) / 2;
                menuItems[i].TextPosition = textPosition;
            }
        }

        private float CalculateTextScaleFactor(int width, int height)
        {
            var longest = menuItems.Select(x => x.Text).Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
            var textSize = ScreenManager.GameFont.MeasureString(longest);

            var scaleFactor = (float)(height * 0.8 / textSize.Y);
            if (textSize.X * scaleFactor > width * 0.8)
            {
                scaleFactor = (float)(width * 0.8 / textSize.X);
            }

            return scaleFactor;
        }

        private void DrawMenuItem(Button item, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(ScreenManager.WhiteTexture, item.ItemBox, Color.Red);
            var fontColor = new Color(70, 70, 70);
            spriteBatch.DrawString(ScreenManager.GameFont, item.Text, item.TextPosition, fontColor, item.TextScale);
        }
    }
}
