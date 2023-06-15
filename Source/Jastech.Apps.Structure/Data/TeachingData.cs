using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class TeachingData
    {
        private static TeachingData _instance = null;

        public static TeachingData Instance()
        {
            if (_instance == null)
            {
                _instance = new TeachingData();
            }

            return _instance;
        }

        public List<Unit> UnitList { get; set; } = new List<Unit>();

        private List<TeachingImageBuffer> ImageBufferList { get; set; } = new List<TeachingImageBuffer>();

        public void UpdateTeachingData()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel != null)
            {
                Dispose();
                Initialize(inspModel);
            }
        }

        public void Initialize(AppsInspModel inspModel)
        {
            Dispose();
            foreach (var unit in inspModel.GetUnitList())
            {
                UnitList.Add(unit.DeepCopy());
            }
        }

        public void Dispose()
        {
            foreach (var unit in UnitList)
                unit.Dispose();

            UnitList.Clear();

            ClearTeachingImageBuffer();
        }

        public void ClearTeachingImageBuffer()
        {
            lock (ImageBufferList)
            {
                foreach (var buffer in ImageBufferList)
                    buffer.Dispose();

                ImageBufferList.Clear();
            }
        }

        public TeachingImageBuffer GetBufferImage(int tabNo)
        {
            lock (ImageBufferList)
            {
                if (ImageBufferList.Count() > 0)
                {
                    foreach (var buffer in ImageBufferList)
                    {
                        if (tabNo == buffer.TabNo)
                            return buffer;
                    }
                }
                return null;
            }
        }

        public void AddBufferImage(int tabNo, Mat tabImage)
        {
            lock (ImageBufferList)
                ImageBufferList.Add(new TeachingImageBuffer 
                {
                    TabNo = tabNo,
                    TabImage = tabImage
                });
        }

        public ICogImage ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            ColorFormat format = mat.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, format);
            return cogImage;
        }
        
        public Unit GetUnit(string name)
        {
            return UnitList.Where(x => x.Name == name).FirstOrDefault();
        }

        public List<PreAlignParam> GetPreAlign(string unitName)
        {
            Unit unit = GetUnit(unitName);

            return unit.PreAligns;
        }

        public VisionProPatternMatchingParam GetPreAlignParameters(string unitName, string preAlignName)
        {
            Unit unit = GetUnit(unitName);
            return unit.PreAligns.Where(x => x.Name == preAlignName).First().InspParam as VisionProPatternMatchingParam;
        }

        public List<Tab> GetTabList(string unitName)
        {
            List<Tab> tabList = new List<Tab>();

            Unit unit = GetUnit(unitName);

            for (int i = 0; i < unit.GetTabList().Count; i++)
            {
                var tab = unit.GetTab(i);
                tabList.Add(tab);
            }

            return tabList;
        }
    }

    public class TeachingImageBuffer
    {
        public int TabNo { get; set; }

        public Mat TabImage { get; set; } = null;

        public void Dispose()
        {
            if(TabImage != null)
            {
                TabImage.Dispose();
                TabImage = null;
            }
        }
    }
}
