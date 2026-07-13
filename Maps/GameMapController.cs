using George.PushingBox.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Maps
{
    public class GameMapController
    {
        private readonly static GameMapController _instance = new GameMapController();
        public static GameMapController Instance => _instance;
        private GameMapController() { }
        public GameMap CurrentGameMap { get; set; } = new GameMap();
        /// <summary>
        /// 移动逻辑是先根据指令移过去，再看在新位置能不能站住脚，若站不住脚就退回去。
        /// 玩家先移到新位置，马上用本方法做判断能否在新位置站住脚，返回false则玩家退回。
        /// 在本方法里，也是先移动，再判断，来确定箱子移动是否成功。当本方法返回true后，玩家留在新位置，玩家推动的箱子也留在了新位置。
        /// </summary>
        /// <param name="input"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsMoveOk(Input input, int x, int y)
        {
           
           
            var staticElements = CurrentGameMap.StaticElements;
            var boxElements = CurrentGameMap.BoxElements;

            if (staticElements[y, x] is MapBlock)//该位置是墙，玩家移动失败，输出false。没有箱子被移动。
            {
                return false;
            }
            if (boxElements.Any(a => a.PositionX == x && a.PositionY == y))//该位置是箱子，需要看顺着移动的方向有没有阻挡。
            {
                var box = boxElements.Where(a => a.PositionX == x && a.PositionY == y).First();//取到该箱子
                int oldX = box.PositionX, oldY = box.PositionY;
                switch (input)//根据输入移动箱子
                {
                    case Input.UP: box.PositionY--; break;
                    case Input.LEFT: box.PositionX--; break;
                    case Input.DOWN: box.PositionY++; break;
                    case Input.RIGHT: box.PositionX++; break;
                    default: break;
                }
                //01 先检查箱子的新位置有没有越界。若越界则退回。
                if (box.PositionX<0||box.PositionY<0||box.PositionX>CurrentGameMap.Width-1||box.PositionY>CurrentGameMap.Height-1)
                {
                    box.PositionX = oldX;
                    box.PositionY = oldY;
                    return false;
                }
               
                //02 再看新位置是不是墙。若是墙则退回。
                if (staticElements[box.PositionY, box.PositionX] is MapBlock)//新位置是墙，退回箱子，返回false
                {
                    box.PositionX = oldX;
                    box.PositionY = oldY;
                    return false;
                }
                //03 最后检查箱子新位置是否有另一个箱子
                foreach (var item in boxElements)//
                {
                    if (item != box)
                    {
                        if (item.PositionX == box.PositionX && item.PositionY == box.PositionY)//boxelements中有另外的箱子与当前箱子同位置
                        {
                            box.PositionX = oldX;
                            box.PositionY = oldY;
                            return false;
                        }
                    }
                }
            }
            return true;//默认输出true,再把false的情况列出来。
        }



    }
}
