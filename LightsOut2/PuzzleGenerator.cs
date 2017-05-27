using System;

namespace LightsOut2
{
    public class PuzzleGenerator
    {
        private int[,] lights;

        public PuzzleGenerator()
        {
            lights = new int[,]
            {
                {0, 0, 0, 0, 0 },
                {0, 0, 1, 0, 0 },
                {0, 1, 0, 1, 0 },
                {1, 0, 1, 0, 1 },
                {0, 1, 0, 1, 0 }
            };
        }

        public int[,] GeneratePuzzle()
        {
            var passes = lights.Length;
            var length = lights.GetLength(0);

            while (passes > 0)
            {
                Random random = new Random();
                var i = random.Next(0, length);
                var j = random.Next(0, length);
                Toggle(i, j);
                passes--;
            }

            return lights;
        }

        private void Toggle(int i, int j)
        {
            var length = lights.GetLength(0);

            lights[i, j] = 1 - lights[i, j];
            if (i - 1 >= 0)
            {
                lights[i - 1, j] = 1 - lights[i - 1, j];
            }
            if (i + 1 < length)
            {
                lights[i + 1, j] = 1 - lights[i + 1, j];
            }
            if (j - 1 >= 0)
            {
                lights[i, j - 1] = 1 - lights[i, j - 1];
            }
            if (j + 1 < length)
            {
                lights[i, j + 1] = 1 - lights[i, j + 1];
            }
        }
    }
}