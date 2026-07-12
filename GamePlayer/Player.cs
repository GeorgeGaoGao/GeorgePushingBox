using George.PushingBox.Maps;
using George.PushingBox.Tools;

namespace George.PushingBox.GamePlayer
{
    public class Player
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Avatar { get; set; }
        public Player() { }
        public Player(int x, int y, char avatar)
        {
            PositionX = x;
            PositionY = y;
            Avatar = avatar;
        }
    }
}
