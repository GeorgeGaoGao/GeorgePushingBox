using George.PushingBox.GamePlayer;
using George.PushingBox.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Tools
{
    public enum Input
    {
        UP, DOWN, LEFT, RIGHT, ENTER, NONE, Q
    }
    public class ApiTools
    {
        /// <summary>
        /// 画基本元素的方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="avatar"></param>
        public static void Draw(int x,int y,char avatar)
        {
            x = x < 0 ? 0 : x;
            y= y < 0 ? 0 : y;
           
            Console.CursorLeft = x; Console.CursorTop = y;
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
                ConsoleKey.Q => Input.Q,

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
