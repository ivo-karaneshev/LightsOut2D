using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using LightsOut2.GameComponents;
using LightsOut2.Extensions;

namespace LightsOut2.Screens
{
    public class GameplayScreen : GameScreen
    {
        private TouchCollection touchCollection;
        private GridItem[,] gridItems;
        private Rectangle grid;
        private Timer timer;
        private Button restartBtn;
        private Label timeLabel;
        private int time;
        private GameMain game;

        public GameplayScreen(ScreenManager manager) : base(manager)
        {
            game = manager.Game as GameMain;
            gridItems = new GridItem[5, 5];
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                time += 1;
            };
        }

        public override void Initialize()
        {
            InitializeLayout();
            InitializeLights();
            time = 0;
            timer.Stop();
            timer.Start();
        }

        public override void UnloadContent()
        {
            timer.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameOver())
            {
                timer.Stop();
                game.PlayersManager.Save(time);

                ScreenManager.SetActiveScreen(new GameOverScreen(ScreenManager, time));
            }

            touchCollection = TouchPanel.GetState();
            foreach (var touch in touchCollection)
            {
                if (touch.State == TouchLocationState.Pressed)
                {
                    if (restartBtn.ItemBox.Contains(touch.Position))
                    {
                        Initialize();
                        continue;
                    }

                    var length = gridItems.GetLength(0);
                    for (var i = 0; i < length; i++)
                    {
                        for (var j = 0; j < length; j++)
                        {
                            var item = gridItems[i, j];
                            if (item.ItemBox.Contains(touch.Position))
                            {
                                item.On = !item.On;
                                if (i - 1 >= 0)
                                {
                                    gridItems[i - 1, j].On = !gridItems[i - 1, j].On;
                                }
                                if (i + 1 < length)
                                {
                                    gridItems[i + 1, j].On = !gridItems[i + 1, j].On;
                                }
                                if (j - 1 >= 0)
                                {
                                    gridItems[i, j - 1].On = !gridItems[i, j - 1].On;
                                }
                                if (j + 1 < length)
                                {
                                    gridItems[i, j + 1].On = !gridItems[i, j + 1].On;
                                }
                            }
                        }
                    }
                }
            }

            if (GamePad.GetState(0).Buttons.Back == ButtonState.Pressed)
            {
                ScreenManager.SetActiveScreen(new MainMenuScreen(ScreenManager));
            }

            timeLabel.Text = "Time: " + time;
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.Orange);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.DrawString(ScreenManager.GameFont, timeLabel.Text, timeLabel.TextPosition, DefaultFontColor);
            spriteBatch.Draw(ScreenManager.WhiteTexture, grid, DefaultFontColor);
            foreach (var item in gridItems)
            {
                var color = Color.Gray;
                if (item.On)
                {
                    color = Color.Wheat;
                }
                spriteBatch.Draw(ScreenManager.WhiteTexture, item.ItemBox, color);
            }
            spriteBatch.DrawString(ScreenManager.GameFont, restartBtn.Text, restartBtn.TextPosition, DefaultFontColor);
            spriteBatch.End();
        }

        private void InitializeLayout()
        {
            var viewport = ScreenManager.GraphicsDevice.Viewport;

            // Game field
            grid = new Rectangle();
            grid.Width = (int)(viewport.Width * 0.8);
            grid.Height = grid.Width;
            grid.X = (viewport.Width - grid.Width) / 2;
            grid.Y = (viewport.Height - grid.Height) / 2;
            var padding = grid.Width * 0.01;
            var length = gridItems.GetLength(0);

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    var gridItemBox = new Rectangle();
                    gridItemBox.Width = (int)((grid.Width - (padding * (length + 1))) / length);
                    gridItemBox.Height = gridItemBox.Width;
                    gridItemBox.X = (int)(grid.X + padding + i * (gridItemBox.Width + padding));
                    gridItemBox.Y = (int)(grid.Y + padding + j * (gridItemBox.Height + padding));

                    gridItems[i, j] = new GridItem
                    {
                        On = false,
                        ItemBox = gridItemBox
                    };
                }
            }

            // Timer
            timeLabel = new Label
            {
                Text = "Time: 0"
            };
            var textSize = ScreenManager.GameFont.MeasureString(timeLabel.Text);
            timeLabel.TextPosition = new Vector2((viewport.Width - textSize.X) / 2, grid.Y - textSize.Y * 2);

            // Restart button
            restartBtn = new Button
            {
                Text = "Restart"
            };
            textSize = ScreenManager.GameFont.MeasureString(restartBtn.Text);
            restartBtn.TextPosition = new Vector2((viewport.Width - textSize.X) / 2, grid.Y + grid.Height + textSize.Y);
            var btnBox = new Rectangle();
            btnBox.Width = (int)(textSize.X * 1.2);
            btnBox.Height = (int)(textSize.Y * 1.2);
            btnBox.X = (int)(restartBtn.TextPosition.X - (btnBox.Width - textSize.X));
            btnBox.Y = (int)(restartBtn.TextPosition.Y - (btnBox.Height - textSize.Y));
            restartBtn.ItemBox = btnBox;
        }

        private void InitializeLights()
        {
            var lights = new PuzzleGenerator().GeneratePuzzle();
            var length = lights.GetLength(0);

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    if (lights[i, j] == 1)
                    {
                        gridItems[i, j].On = true;
                    }
                }
            }
        }

        private bool GameOver()
        {
            var gameOver = true;
            foreach (var item in gridItems)
            {
                if (item.On)
                {
                    gameOver = false;
                }
            }

            return gameOver;
        }
    }
}