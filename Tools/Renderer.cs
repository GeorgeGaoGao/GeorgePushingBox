using George.PushingBox.GamePlayer;
using George.PushingBox.Maps;
using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Tools
{
    public class Renderer
    {
        public Renderer() { }
        public static void Render(GameMap map,Player player)
        {
            
            foreach (var item in map.StaticElements)
            {
                ApiTools.Draw(item.PositionX, item.PositionY, item.Avatar);
            }

            foreach (var item in map.TargetElements)
            {
                ApiTools.Draw(item.PositionX, item.PositionY, item.Avatar);
            }
            foreach (var item in map.BoxElements)//先绘目标再绘箱子，箱子可以覆盖在目标之上
            {
                ApiTools.Draw(item.PositionX, item.PositionY, item.Avatar);
            }

            ApiTools.Draw(player.PositionX, player.PositionY,player.Avatar);//最后绘玩家，确保在顶上
        }
    }
}
