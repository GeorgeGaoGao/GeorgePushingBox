using George.PushingBox.GamePlayer;
using George.PushingBox.Maps;
using George.PushingBox.Stages;
using George.PushingBox.Tools;
using Newtonsoft.Json;
using Polenter.Serialization;
using System.Text.Json;


namespace George.PushingBox
{
    public class Program
    {
        static void Main(string[] args)
        {
            int width = 80, height = 30;
            Console.SetWindowSize(width,height);
            Console.SetBufferSize(width, height);
            //string filePath = @"Stages/StageInfos.json";
            string filePath = @"Stages/StageInfos.xml";
            StageController.Instance.GetStageInfos(filePath);
            StageController.Instance.ShowSelection();
            StageController.Instance.PrepareStage(); 
            StageController.Instance.PlayStage();
        }
    }
}
