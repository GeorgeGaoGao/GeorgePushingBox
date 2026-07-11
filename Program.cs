using George.PushingBox.GamePlayer;
using George.PushingBox.Maps;
using George.PushingBox.Stages;
using George.PushingBox.Tools;


namespace George.PushingBox
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;//让光标不要闪烁

           
            StageController.Instance.ShowSelection();

            int[,] mapArray = new[,]
             {
                 { 1,1,1,1,1,1,1,1,1},
                 { 1,0,0,2,0,0,0,3,1},

                 };
            // int[,] mapArray = new[,]
            //{
            //      { 1,1,1,1,1,0,0,0,0},
            //      { 1,0,0,0,1,0,0,0,0},
            //      { 1,0,2,2,1,0,1,1,1},
            //      { 1,0,2,0,1,0,1,3,1},
            //      { 1,1,1,0,1,1,1,3,1},
            //      { 0,1,1,0,0,0,0,3,1},
            //      { 0,1,0,0,0,1,0,0,1},
            //      { 0,1,0,0,0,1,1,1,1},
            //      { 1,0,0,0,1,0,0,0,0},
            //      { 1,0,2,2,1,0,1,1,1},
            //      { 1,0,2,0,1,0,1,3,1},
            //      { 1,1,1,0,1,1,1,3,1},
            //      { 0,1,1,0,0,0,0,3,1},
            //      { 0,1,0,0,0,1,0,0,1},
            //      { 0,1,0,0,0,1,1,1,1},
            //      { 0,1,1,1,1,1,0,0,0},
            //  };

            GameMap map = new GameMap(mapArray);
            GameMapController.Instance.CurrentGameMap = map;

            Player player = new Player(1, 1, '&');
            PlayerController.Instance.CurrentPlayer = player;

            Renderer.Render(GameMapController.Instance.CurrentGameMap, PlayerController.Instance.CurrentPlayer);//接收到用户输入前先渲染一次，避免黑屏。

            while (true)
            {


                Input input = InputController.GetInput();//接受玩家输入
                PlayerController.Instance.Move(input);//更新玩家位置。
                Renderer.Render(GameMapController.Instance.CurrentGameMap, PlayerController.Instance.CurrentPlayer);//渲染
                if (StageController.Instance.JudgeClear())
                {
                    //Environment.Exit(0);
                    Console.Clear();
                    Console.WriteLine($"congratuations! ");
                    Console.WriteLine($"press any key to continue!");
                    Console.ReadKey(true);
                    StageController.Instance.ShowSelection();
                 
                    GameMapController.Instance.CurrentGameMap.InitMap(mapArray);
                    PlayerController.Instance.CurrentPlayer.PositionX = 1;
                    PlayerController.Instance.CurrentPlayer.PositionY = 1;
                    Renderer.Render(GameMapController.Instance.CurrentGameMap, PlayerController.Instance.CurrentPlayer);
                }
            }
        }


    }
}
