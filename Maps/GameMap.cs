using George.PushingBox.Tools;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace George.PushingBox.Maps
{
    public class GameMap
    {

        //长度，宽度
        public int Width { get; set; }
        public int Height { get; set; }

        //静态元素数组
        public MapElement[,] StaticElements { get; set; } = new MapElement[0,0];

        //箱子元素列表
        public List<MapElement> BoxElements { get; set; } =new List<MapElement>();
        //目标元素列表
        public List<MapElement> TargetElements { get; set; } = new List<MapElement>();

        /// <summary>
        /// 由输入的二维int数组来建立地图，0：空格 1：围墙# 2：箱子@ 3：目标A
        /// 元素数组所需的长度从输入的参数数组中取得。
        /// </summary>
        /// <param name="mapArray"></param>
        public GameMap() { }

        public void InitMap(int[,] mapArray)
        {
            Height = mapArray.GetLength(0);
            Width = mapArray.GetLength(1);

            //根据行数和列数初始化静态元素数组
            StaticElements = new MapElement[Height, Width];
            TargetElements = new List<MapElement>();
            BoxElements = new List<MapElement>();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    switch (mapArray[y, x])
                    {
                        case 0://空区域，存入静态元素数组。整个地图区域必须全填满，否则不能盖住前一层的玩家影像。
                            StaticElements[y, x] = new MapEmpty(x, y, ' ');
                            break;
                        case 1://墙区域，存入静态元素数组
                            StaticElements[y, x] = new MapBlock(x, y, '#');
                            break;
                        case 2://箱子区域，存入箱子元素数组。对应静态地图中表示为空区域
                            BoxElements.Add(new MapBox(x, y, '@'));
                            StaticElements[y, x] = new MapEmpty(x, y, ' ');
                            break;
                        case 3://目标区域，存入目标元素数组。对应静态地图中表示为空区域
                            TargetElements.Add(new MapBox(x, y, 'A'));
                            StaticElements[y, x] = new MapEmpty(x, y, ' ');
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
