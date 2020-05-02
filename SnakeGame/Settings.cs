using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public enum Direction
    {
        Left, Right, Up, Down
    };

    class Settings
    {
        public static int Width { set; get; }
        public static int Height { set; get; }
        public static int Speed { set; get; }
        public static int Score { set; get; }
        public static int Points { set; get; }
        public static bool GameOver { set; get; }
        public static Direction Direction { set; get; }

        public Settings()
        {
            Width = 20;
            Height = 20;
            Speed = 10;
            Score = 0;
            Points = 100;
            GameOver = false;
            Direction = Direction.Right;
        }
    }
}
