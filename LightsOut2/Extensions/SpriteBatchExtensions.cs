using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LightsOut2.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color color, float scale)
        {
            spriteBatch.DrawString(spriteFont, text, position, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
        }
    }
}