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
            StageController.GetInstance().ShowSelection();
            Console.CursorVisible = false;//让光标不要闪烁

            int[,] mapArray = new[,]
           {
                 { 1,1,1,1,1,0,0,0,0},
                 { 1,0,0,0,1,0,0,0,0},
                 { 1,0,2,2,1,0,1,1,1},
                 { 1,0,2,0,1,0,1,3,1},
                 { 1,1,1,0,1,1,1,3,1},
                 { 0,1,1,0,0,0,0,3,1},
                 { 0,1,0,0,0,1,0,0,1},
                 { 0,1,0,0,0,1,1,1,1},
                 { 1,0,0,0,1,0,0,0,0},
                 { 1,0,2,2,1,0,1,1,1},
                 { 1,0,2,0,1,0,1,3,1},
                 { 1,1,1,0,1,1,1,3,1},
                 { 0,1,1,0,0,0,0,3,1},
                 { 0,1,0,0,0,1,0,0,1},
                 { 0,1,0,0,0,1,1,1,1},
                 { 0,1,1,1,1,1,0,0,0},
             };

            GameMap map = new GameMap(mapArray);
            GameMapController.GetInstance().CurrentGameMap = map;

            Player player = new Player(1, 1, '&');
            PlayerController.GetInstance().CurrentPlayer = player;
            Renderer renderer = new Renderer();
            renderer.Render(GameMapController.GetInstance().CurrentGameMap, PlayerController.GetInstance().CurrentPlayer);


            while (true)
            {
                //ConsoleKeyInfo input = Console.ReadKey(true);//接受玩家输入
                Input input=InputController.GetInput();
                PlayerController.GetInstance().Move(input);//更新玩家位置。
                renderer.Render(map, PlayerController.GetInstance().CurrentPlayer);//渲染
                if (StageController.GetInstance().JudgeClear())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
    