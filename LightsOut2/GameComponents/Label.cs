using Microsoft.Xna.Framework;

namespace LightsOut2.GameComponents
{
    public class Label
    {
        public Label()
        {
            TextScale = 1;
        }

        public string Text { get; set; }

        public Vector2 TextPosition { get; set; }

        public float TextScale { get; set; }
    }
}