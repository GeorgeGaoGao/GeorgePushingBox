using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Tools
{
    public enum Input
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }
    public class InputController
    {
        public static Input GetInput()
        {
            return Console.ReadKey(true).Key switch
            {
                ConsoleKey.W => Input.UP,
                ConsoleKey.A => Input.LEFT,
                ConsoleKey.S => Input.DOWN,
                ConsoleKey.D => Input.RIGHT,

                ConsoleKey.UpArrow => Input.UP,
                ConsoleKey.LeftArrow => Input.LEFT,
                ConsoleKey.DownArrow => Input.DOWN,
                ConsoleKey.RightArrow => Input.RIGHT,

                _ => Input.NONE,
            };
        }
    }
}
