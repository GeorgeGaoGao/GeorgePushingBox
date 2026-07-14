using George.PushingBox.GamePlayer;
using George.PushingBox.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Tools
{
    public enum Input
    {
        UP, DOWN, LEFT, RIGHT, ENTER, NONE, QUIT
    }
    public class ApiTools
    {
        //设置原点位置，让内容显示在屏幕中间。
        public static int OriginalCursorTop { get; set; } = 5;
        public static int OriginalCursorLeft { get; set; } = 10;

        /// <summary>
        /// 将光标回到原点。
        /// </summary>
        public static void InitCursor()
        {
            Console.CursorLeft = OriginalCursorLeft;
            Console.CursorTop = OriginalCursorTop;
        }

        

        /// <summary>
        /// 考虑光标原点的情况下打印字符串，在左边留好空。
        /// </summary>
        /// <param name="text"></param>
        public static void PrintText(string text)
        {
            Console.CursorLeft = OriginalCursorLeft;
            Console.WriteLine( text);
            Console.CursorLeft = OriginalCursorLeft;

        }
        /// <summary>
        /// 画基本元素的方法，把原点位置考虑进去。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="avatar"></param>
        public static void Draw(int x,int y,char avatar)
        {
            //x = x < 0 ? 0 : x;
            //y= y < 0 ? 0 : y;
            //上面这两句不能达到目的，不能在渲染这里来保证不出零边界，应在移动光标的位置解决。
            //如果在这里解决，即使看上去没出零边界，但实际XY是负值，会导致往正向移动时按多次键都没反应。
           
            Console.CursorLeft =OriginalCursorLeft+ x; Console.CursorTop =OriginalCursorTop+ y;
            Console.Write(avatar);
        }
        /// <summary>
        /// 将键盘输入包装成自定义的枚举类型，方便统一引用。当调整输入方式时，其它用到输入的地方不需要修改，只需改这里的映射关系即可。
        /// </summary>
        /// <returns></returns>
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

                ConsoleKey.Enter => Input.ENTER,
                ConsoleKey.Escape => Input.QUIT,

                _ => Input.NONE,
            };
        }
        /// <summary>
        /// 全局渲染方法
        /// </summary>
        /// <param name="map"></param>
        /// <param name="player"></param>
        public static void Render(GameMap map, Player player)
        {

            foreach (var item in map.StaticElements)
            {
                Draw(item.PositionX, item.PositionY, item.Avatar);
            }

            foreach (var item in map.TargetElements)
            {
                Draw(item.PositionX, item.PositionY, item.Avatar);
            }
            foreach (var item in map.BoxElements)//先绘目标再绘箱子，箱子可以覆盖在目标之上
            {
                Draw(item.PositionX, item.PositionY, item.Avatar);
            }

            Draw(player.PositionX, player.PositionY, player.Avatar);//最后绘玩家，确保在顶上
        }
    }
}
