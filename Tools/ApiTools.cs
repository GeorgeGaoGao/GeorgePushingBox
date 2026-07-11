using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Tools
{
    public class ApiTools
    {
        public static void Draw(int x,int y,char avatar)
        {
            Console.CursorLeft = x; Console.CursorTop = y;
            Console.Write(avatar);
        }
    }
}
