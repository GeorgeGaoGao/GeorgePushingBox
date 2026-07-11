using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Tools
{
    public enum Input
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }
    public static class InputController
    {
        public static Input GetInput()
        {
            Input input = Input.NONE;
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.W: input = Input.UP; break;
                case ConsoleKey.A: input = Input.LEFT; break;
                case ConsoleKey.S: input = Input.DOWN; break;
                case ConsoleKey.D: input = Input.RIGHT; break;

                case ConsoleKey.UpArrow: input = Input.UP; break;
                case ConsoleKey.LeftArrow: input = Input.LEFT; break;
                case ConsoleKey.DownArrow: input = Input.DOWN; break;
                case ConsoleKey.RightArrow: input = Input.RIGHT; break;

                default: input = Input.NONE; break;
            }
            return input;
        }
    }
}
