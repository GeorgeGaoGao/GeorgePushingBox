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
        public GameMap CurrentGameMap { get; set; } = null!;
        public bool CheckMove(Input input, int x, int y)
        {
            var staticElements = CurrentGameMap.StaticElements;
            var boxElements = CurrentGameMap.BoxElements;

            if (staticElements[y, x] is MapBlock)//该位置是墙，输出false
            {
                return false;
            }
            if (boxElements.Any(a => a.PositionX == x && a.PositionY == y))//该位置是箱子
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
                //检查箱子新位置是否会让移动失败，若失败则退回。
                //先看有没有墙
                if (staticElements[box.PositionY, box.PositionX] is MapBlock)//新位置是墙，退回箱子，返回false
                {
                    box.PositionX = oldX;
                    box.PositionY = oldY;
                    return false;
                }
                foreach (var item in boxElements)//检查箱子新位置是否有另一个箱子
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
