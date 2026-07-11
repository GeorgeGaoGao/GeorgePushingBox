using George.PushingBox.GamePlayer;
using George.PushingBox.Maps;
using George.PushingBox.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox
{
    public class Renderer
    {
        public Renderer()
        {
            
        }
        public void Render(GameMap map,Player player)
        {
            
            foreach (var item in map.StaticElements)
            {
                ApiTools.Draw(item.PositionX, item.PositionY, item.Avatar);
            }

            foreach (var item in map.TargetElements)
            {
                ApiTools.Draw(item.PositionX, item.PositionY, item.Avatar);
            }
            foreach (var item in map.BoxElements)//最后绘箱子，箱子可以覆盖在目标之上
            {
                ApiTools.Draw(item.PositionX, item.PositionY, item.Avatar);
            }

            ApiTools.Draw(player.PositionX, player.PositionY,player.Avatar);
        }
    }
}
