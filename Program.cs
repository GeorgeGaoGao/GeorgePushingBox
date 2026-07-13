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
            string jsonPath = @"Stages/StageInfos.json";
            StageController.Instance.GetStageInfos(jsonPath);

            var stageInfos = StageController.Instance.StageInfos;
            var serializer = new SharpSerializer();
            serializer.Serialize(stageInfos, "stageInfos.xml");

            StageController.Instance.ShowSelection();
            StageController.Instance.PrepareStage(); 
            StageController.Instance.PlayStage();
        }
    }
}
