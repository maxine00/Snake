using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    class Input
    {
        private static Hashtable KeyInput = new Hashtable();

        public static bool KeyPress(Keys key)
        {
            if (KeyInput[key] == null)
            {
                return false;
            }
            return (bool)KeyInput[key];
        }

        public static void ChangeState(Keys key, bool State)
        {
            KeyInput[key] = State;
        }
    }
}