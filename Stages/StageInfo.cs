using System;
using System.Collections.Generic;
using System.Text;

namespace George.PushingBox.Stages
{
    public class StageInfo
    {
        public string? StageName { get; set; }
        public int PlayerXStart { get; set; }
        public int PlayerYStart { get; set; }
        public int[,] MapArray { get; set; }=new int[0,0];

    }
}
