using George.PushingBox.Maps;
using George.PushingBox.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.GamePlayer
{
    public class PlayerController
    {
        private static readonly PlayerController _instance=new PlayerController();
        public static PlayerController Instance => _instance;
      
        private PlayerController() { }
        public Player CurrentPlayer { get; set; } = null!;


        public void Move(Input input)
        {
            int oldPositionX = CurrentPlayer.PositionX;
            int oldPositionY = CurrentPlayer.PositionY;
            switch (input)
            {
                case Input.UP: CurrentPlayer.PositionY--; break;
                case Input.LEFT: CurrentPlayer.PositionX--; break;
                case Input.DOWN: CurrentPlayer.PositionY++; break;
                case Input.RIGHT: CurrentPlayer.PositionX++; break;
                default: break;
            }
            //在地图中检查新位置是否适合，即是否能移动成功，若不成功，则退回之前的位置。
            if (!GameMapController.Instance.CheckMove(input, CurrentPlayer.PositionX, CurrentPlayer.PositionY))
            {
                CurrentPlayer.PositionX = oldPositionX;
                CurrentPlayer.PositionY = oldPositionY;
            }
        }

      
    }
}
