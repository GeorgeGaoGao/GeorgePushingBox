using George.PushingBox.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Maps
{
    /// <summary>
    /// 地图元素的基类。抽象类。每个元素可自己绘出自己。
    /// </summary>
    public abstract class MapElement
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Avatar { get; set; }//形象
        protected MapElement(int x, int y, char avatar)
        {
            PositionX = x; PositionY = y; Avatar = avatar;
        }
       

    }

}
