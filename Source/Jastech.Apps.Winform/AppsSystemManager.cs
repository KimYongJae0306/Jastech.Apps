using Jastech.Apps.Structure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public abstract class AppsSystemManager
    {
        public TeachingData TeachingData { get; private set; } = new TeachingData();

        public TeachingData GetTeachingData()
        {
            return TeachingData;
        }

        //public void UpdateTeachingData<T>()
        //{
        //    var currentModel = ModelManager.Instance().CurrentModel as AppsInspModel;
        //    if (currentModel != null)
        //    {
        //        TeachingData.Dispose();
        //        TeachingData.Initialize(currentModel);
        //    }
        //}
        // public void Update
    }
}
