using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class LAFData
    {
        #region 속성
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public double LowerReturndB { get; set; } = -1.0;

        [JsonProperty]
        public double UpperReturndB { get; set; } = -25.0;

        [JsonProperty]
        public double NegativeSoftwareLimit { get; set; } = 1.0;

        [JsonProperty]
        public double PositiveSoftwareLimit { get; set; } = 10.0;
        #endregion

        #region 메서드
        public LAFData DeepCopy()
        {
            LAFData lafData = new LAFData();

            lafData.Name = Name;
            lafData.LowerReturndB = LowerReturndB;
            lafData.UpperReturndB = UpperReturndB;
            lafData.NegativeSoftwareLimit = NegativeSoftwareLimit;
            lafData.PositiveSoftwareLimit = PositiveSoftwareLimit;

            return lafData;
        }
        #endregion
    }
}
