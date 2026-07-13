using George.PushingBox.GamePlayer;
using George.PushingBox.Maps;
using George.PushingBox.Tools;
using Newtonsoft.Json;
using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace George.PushingBox.Stages
{
    internal class StageController
    {
        private static StageController _instance = new StageController();
        public static StageController Instance => _instance;
        private StageController() { }
        private int _stageSelected;

        public StageInfo[] StageInfos { get; set; } = Array.Empty<StageInfo>();

        /// <summary>
        /// 根据JSON文件，得到所有关卡信息，交给属性StageInfos
        /// </summary>
        /// <param name="filePath"></param>
        public void GetStageInfos(string filePath)
        {
            if (filePath.EndsWith("json"))
            {
                string jsonString = File.ReadAllText(filePath);
                StageInfos = JsonConvert.DeserializeObject<StageInfo[]>(jsonString)!;
            }
            if (filePath.EndsWith("xml"))
            {
                var serializer = new SharpSerializer();
                StageInfos = (StageInfo[])serializer.Deserialize(filePath);
            }


        }


        /// <summary>
        /// 显示所有关卡选项。选定的结果交给字段_stageSelected
        /// </summary>
        public void ShowSelection()
        {
            Console.Clear();
            ApiTools.InitCursor();
            ApiTools.PrintText($"********************欢迎来到推箱子的世界********************");

            //显示所有关卡的名字
            for (int i = 0; i < StageInfos.Length; i++)
            {
                ApiTools.PrintText($"{i + 1} {StageInfos[i].StageName}");
            }
            ApiTools.PrintText("\r");
            ApiTools.PrintText($"任意时刻按Esc键退出游戏");
            //显示光标，让光标只在关卡名字间移动，同时记录下关卡数保存到_stageSelected，后续根据关卡数启动该关卡
            Console.CursorVisible = true;
            //将光标退回到第一关所在行
            Console.CursorTop = Console.CursorTop - 3 - (StageInfos.Length - 1);
            var stageIndex = 0;

            //让用户选择关卡，死循环，终止条件是选定了后按的回车键
            bool isSelectionConfirmed = false;
            while (!isSelectionConfirmed)
            {
                Input input = ApiTools.GetInput();
                switch (input)
                {
                    case Input.UP:
                        if (stageIndex > 0)
                        {
                            stageIndex--;
                            Console.CursorTop--;
                        }
                        break;
                    case Input.DOWN:
                        if (stageIndex < StageInfos.Length - 1)
                        {
                            stageIndex++;
                            Console.CursorTop++;
                        }
                        break;
                    case Input.ENTER:
                        isSelectionConfirmed = true;
                        break;
                    case Input.QUIT:
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    default: break;
                }
            }
            _stageSelected = stageIndex;

        }


        /// <summary>
        /// 根据选定的关卡做好准备。
        /// </summary>
        public void PrepareStage()
        {
            Console.Clear();
            ApiTools.InitCursor();
            Console.CursorVisible = false;
            //初始化该关卡的地图
            var stage = StageInfos[_stageSelected];
            Console.CursorTop -= 2;
            ApiTools.PrintText(stage.StageName);
            GameMapController.Instance.CurrentGameMap.InitMap(stage.MapArray);
            //将玩家置于初始位置。渲染地图和玩家。
            PlayerController.Instance.CurrentPlayer.PositionX = stage.PlayerXStart;
            PlayerController.Instance.CurrentPlayer.PositionY = stage.PlayerYStart;
            ApiTools.Render(GameMapController.Instance.CurrentGameMap, PlayerController.Instance.CurrentPlayer);

        }


        /// <summary>
        /// 玩一个关卡
        /// </summary>
        public void PlayStage()
        {
            while (true)
            {
                Input input = ApiTools.GetInput();//接受玩家输入
                PlayerController.Instance.Move(input);//更新玩家位置。
                ApiTools.Render(GameMapController.Instance.CurrentGameMap, PlayerController.Instance.CurrentPlayer);//渲染
                if (StageController.Instance.JudgeClear())
                {
                    Console.Clear();
                    ApiTools.InitCursor();
                    //Console.WriteLine($"congratuations! ");
                    ApiTools.PrintText($"congratuations! ");
                    ApiTools.PrintText($"press any key to continue!");
                    //Console.WriteLine($"press any key to continue!");
                    Console.ReadKey(true);
                    StageController.Instance.ShowSelection();
                    StageController.Instance.PrepareStage();

                }
            }
        }
        public bool JudgeClear()
        {
            var targetElements = GameMapController.Instance.CurrentGameMap.TargetElements;
            var boxElements = GameMapController.Instance.CurrentGameMap.BoxElements;
            bool result = true;
            foreach (var target in targetElements)
            {
                if (!boxElements.Exists(a => a.PositionX == target.PositionX && a.PositionY == target.PositionY))
                {
                    result = false; break;
                }
            }
            return result;
        }
    }
}