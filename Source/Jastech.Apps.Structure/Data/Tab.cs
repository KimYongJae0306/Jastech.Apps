using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using System.Xml.Linq;
using System.IO;

namespace Jastech.Apps.Structure.Data
{
    public class Tab
    {
        #region 속성
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public int Index { get; set; }

        [JsonProperty]
        public int StageIndex { get; set; }

        [JsonProperty]
        public AlignSpec AlignSpec { get; set; } = new AlignSpec();

        [JsonProperty]
        public LafTriggerOffset LafTriggerOffset { get; set; } = new LafTriggerOffset();

        [JsonProperty]
        public MarkParamter Mark = new MarkParamter();

        [JsonProperty]
        public MarkParamter AlignCamMark = null;

        [JsonProperty]
        public List<AlignParam> AlignParamList { get; set; } = new List<AlignParam>();

        [JsonIgnore]
        public AkkonParam AkkonParam { get; set; } = new AkkonParam();
        #endregion

        #region 메서드
        public Tab DeepCopy()
        {
            Tab tab = new Tab();
            tab.Name = Name;
            tab.Index = Index;
            tab.StageIndex = StageIndex;
            tab.AlignSpec = JsonConvertHelper.DeepCopy(AlignSpec) as AlignSpec;
            tab.LafTriggerOffset = JsonConvertHelper.DeepCopy(LafTriggerOffset) as LafTriggerOffset;
            tab.Mark = Mark.DeepCopy();
            tab.AlignCamMark = AlignCamMark?.DeepCopy();
            tab.AlignParamList = AlignParamList.Select(x => x.DeepCopy()).ToList();
            tab.AkkonParam = AkkonParam.DeepCopy();

            return tab;
        }

        public void Dispose()
        {
            foreach (var align in AlignParamList)
                align.Dispose();

            Mark.Dispose();
            AlignCamMark?.Dispose();
            AlignParamList.Clear();
            AkkonParam.Dispose();
        }

        public AlignParam GetAlignParam(ATTTabAlignName alignName)
        {
            return AlignParamList.Where(x => x.Name == alignName.ToString()).FirstOrDefault();
        }

        public void SetAlignParam(ATTTabAlignName alignName, AlignParam alignParam)
        {
            if (alignParam == null)
                return;

            AlignParamList.Where(x => x.Name == alignName.ToString()).First().LeadCount = alignParam.LeadCount;
            AlignParamList.Where(x => x.Name == alignName.ToString()).First().PanelToFpcOffset = alignParam.PanelToFpcOffset;
            AlignParamList.Where(x => x.Name == alignName.ToString()).First().CaliperParams = alignParam.CaliperParams.DeepCopy();
        }

        public AkkonGroup GetAkkonGroup(int index)
        {
            return AkkonParam.GetAkkonGroup(index);
        }

        public void SetAkkonGroup(int index, AkkonGroup newGroup)
        {
            AkkonParam.SetAkkonGroup(index, newGroup);
        }

        public MarkParamter GetMarkParamter(bool isAlignCamMark)
        {
            if (isAlignCamMark)
                return AlignCamMark;
            else
                return Mark;
        }

        public void SaveAkkonParam(string filePath)
        {
            if (Directory.Exists(filePath) == false)
                Directory.CreateDirectory(filePath);

            string fileFullPath = Path.Combine(filePath, "Akkon.json");

            JsonConvertHelper.Save(fileFullPath, AkkonParam);
        }

        public void LoadAkkonParam(string filePath)
        {
            AkkonParam param = new AkkonParam();

            string fileFullPath = Path.Combine(filePath, "Akkon.json");
            if (File.Exists(fileFullPath))
            {
                JsonConvertHelper.LoadToExistingTarget<AkkonParam>(fileFullPath, param);
            }
            else
            {
                param.AkkonAlgoritmParam.Initalize();
                param.AkkonAlgoritmParam.ImageFilterParam.AddMacronFilter();
            }

            AkkonParam = param;
        }
        #endregion
    }

    public class MarkParamter
    {
        #region 속성
        [JsonProperty]
        public List<MarkParam> FpcMarkList { get; set; } = new List<MarkParam>();

        [JsonProperty]
        public List<MarkParam> PanelMarkList { get; set; } = new List<MarkParam>();
        #endregion

        #region 메서드
        public MarkParam GetFPCMark(MarkDirection markDirection, MarkName markName)
        {
            MarkParam param = null;
            lock (FpcMarkList)
                param = FpcMarkList.Where(x => x.Name == markName && x.Direction == markDirection).FirstOrDefault();
            return param;
        }

        public MarkParam GetPanelMark(MarkDirection markDirection, MarkName markName)
        {
            MarkParam param = null;
            lock (PanelMarkList)
                param = PanelMarkList.Where(x => x.Name == markName && x.Direction == markDirection).FirstOrDefault();
            return param;
        }

        public void SetFPCMark(MarkDirection markDirection, MarkName markName, MarkParam param)
        {
            if (param == null)
                return;

            lock (FpcMarkList)
                FpcMarkList.Where(x => x.Name == markName && x.Direction == markDirection).First().InspParam = param.InspParam.DeepCopy();
        }

        public void SetPanelMark(MarkDirection markDirection, MarkName markName, MarkParam param, bool isAlignParam)
        {
            if (param == null)
                return;

            lock (PanelMarkList)
                PanelMarkList.Where(x => x.Name == markName && x.Direction == markDirection).First().InspParam = param.InspParam.DeepCopy();
        }

        public MarkParamter DeepCopy()
        {
            MarkParamter param = new MarkParamter();
            param.FpcMarkList = FpcMarkList.Select(x => x.DeepCopy()).ToList();
            param.PanelMarkList = PanelMarkList.Select(x => x.DeepCopy()).ToList();

            return param;
        }

        public void Dispose()
        {
            lock(FpcMarkList)
            {
                foreach (var fpc in FpcMarkList)
                    fpc.Dispose();
                FpcMarkList.Clear();
            }
            
            lock(PanelMarkList)
            {
                foreach (var panel in PanelMarkList)
                    panel.Dispose();

                PanelMarkList.Clear();
            }
        }
        #endregion
    }

    public class AlignSpec
    {
        #region 속성
        [JsonProperty]
        public float LeftSpecX_um { get; set; } = 0.5F;

        [JsonProperty]
        public float LeftSpecY_um { get; set; } = 1.0F;

        [JsonProperty]
        public float RightSpecX_um { get; set; } = 0.5F;

        [JsonProperty]
        public float RightSpecY_um { get; set; } = 1.0F;

        [JsonProperty]
        public float CenterSpecX_um { get; set; } = 0.5F;

        [JsonProperty]
        public bool UseAutoTracking { get; set; } = false;
        #endregion
    }

    public class LafTriggerOffset
    {
        #region 속성
        [JsonProperty]
        public double Left { get; set; } = 0.0;

        [JsonProperty]
        public double Right { get; set; } = 0.0;
        #endregion
    }
}
