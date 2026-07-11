using George.PushingBox.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Stages
{
    internal class StageController
    {
        private static StageController _instance = new StageController();
        public static StageController Instance => _instance;
        public StageController() { }
        public void ShowSelection()
        {
            Console.Clear();
            Console.WriteLine($"********************欢迎来到推箱子的世界********************");
            Console.WriteLine($"1 开始游戏");
            Console.WriteLine($"2 结束游戏");
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D1: { Console.Clear(); return; }
                    case ConsoleKey.D2: Environment.Exit(0); break;

                    default: break;
                }
            }
        }
        /// <summary>
        /// 遍历目标数组，比对盒子数组中的
        /// </summary>
        /// <returns></returns>
        public bool JudgeClear()
        {
            var targetElements = GameMapController.Instance.CurrentGameMap.TargetElements;
            var boxElements = GameMapController.Instance.CurrentGameMap.BoxElements;
            bool result = true;
            foreach (var target in targetElements)
            {
                if (!boxElements.Exists( a => a.PositionX == target.PositionX && a.PositionY == target.PositionY))
                {
                    result = false;break;
                }
            }
            return result;
        }
    }
}