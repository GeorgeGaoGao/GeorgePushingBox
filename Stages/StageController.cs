using George.PushingBox.GamePlayer;
using George.PushingBox.Maps;
using George.PushingBox.Tools;
using Newtonsoft.Json;
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
        /// <param name="jsonPath"></param>
        public void GetStageInfos(string jsonPath)
        {
            string jsonString = File.ReadAllText(jsonPath);
            StageInfos = JsonConvert.DeserializeObject<StageInfo[]>(jsonString)!;
        }


        /// <summary>
        /// 显示所有关卡选项。选定的结果交给字段_stageSelected
        /// </summary>
        public void ShowSelection()
        {
            Console.Clear();
            Console.WriteLine($"********************欢迎来到推箱子的世界********************");

            //显示所有关卡的名字
            for (int i = 0; i < StageInfos.Length; i++)
            {
                Console.WriteLine($"{i + 1} {StageInfos[i].StageName}");
            }
            Console.WriteLine($"按Q键退出游戏");
            //显示光标，让光标只在关卡名字间移动，同时记录下关卡数保存到_stageSelected，后续根据关卡数启动该关卡
            Console.CursorVisible = true;
            Console.CursorTop = 1;//因为欢迎语只占用了一行即第0行，所以移到第1行。
            var stageIndex = 0;

            //让用户选择关卡，死循环，终止条件是选定了
            bool isSelectionContinue = true;
            while (isSelectionContinue)
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
                        isSelectionContinue = false;
                        break;
                    case Input.Q:
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
        public void StartStage()
        {
            Console.CursorVisible = false;
            //初始化该关卡的地图
            var stage = StageInfos[_stageSelected];
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
                    Console.WriteLine($"congratuations! ");
                    Console.WriteLine($"press any key to continue!");
                    Console.ReadKey(true);
                    StageController.Instance.ShowSelection();
                    StageController.Instance.StartStage();

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