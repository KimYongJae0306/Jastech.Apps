using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

namespace Jastech.Apps.Structure.Data
{
    public class ProcessCapabilityIndex
    {
        #region 속성
        public double CapabilityUSL { private get; set; }

        public double CapabilityLSL { private get; set; }

        public double PerformanceUSL_Center { private get; set; }

        public double PerformanceLSL_Center { private get; set; }

        public double PerformanceUSL_Side { private get; set; }

        public double PerformanceLSL_Side { private get; set; }
        #endregion

        #region 메서드
        public PcResult GetResult(string type, List<double> valueList)
        {
            PcResult result = new PcResult { Type = type };

            if (valueList.Count == 0)
                return result;

            // Get average, standard deviations
            double mean = valueList.Average();
            double sampleStdDev = MathHelper.GetSampleStandardDeviation(valueList, mean);
            double populationStdDev = MathHelper.GetPopulationStandardDeviation(valueList, mean);

            // Calculate Cp
            result.Cp = (CapabilityUSL - CapabilityLSL) / (sampleStdDev * 6);

            // Calculate Cpk
            double upperCandidateCpk = (CapabilityUSL - mean) / (sampleStdDev * 3);
            double lowerCandidateCpk = (mean - CapabilityLSL) / (sampleStdDev * 3);
            result.Cpk = Math.Min(upperCandidateCpk, lowerCandidateCpk);

            // Calculate Pp
            double performanceUSL = type.ToUpper() == "CX" ? PerformanceUSL_Center : PerformanceUSL_Side;
            double performanceLSL = type.ToUpper() == "CX" ? PerformanceLSL_Center : PerformanceLSL_Side;
            result.Pp = (performanceUSL - performanceLSL) / (populationStdDev * 6);

            // Calculate Ppk
            double upperCandidatePpk = (performanceUSL - mean) / (sampleStdDev * 3);
            double lowerCandidatePpk = (mean - performanceLSL) / (sampleStdDev * 3);
            result.Ppk = Math.Min(upperCandidatePpk, lowerCandidatePpk);

            // Round the results and Get absolute value
            result.Cp = Math.Abs(Math.Round(result.Cp, 3));
            result.Cpk = Math.Abs(Math.Round(result.Cpk, 3));
            result.Pp = Math.Abs(Math.Round(result.Pp, 3));
            result.Ppk = Math.Abs(Math.Round(result.Ppk, 3));

            // Round the results and Get absolute value
            result.Max = Math.Round(valueList.Max());
            result.Min = Math.Round(valueList.Min());
            result.Range = Math.Round(valueList.Max() - valueList.Min(), 3);
            result.Mean = Math.Round(mean, 3);
            result.Sigma = Math.Round(populationStdDev, 3);
            result.SixSigma = Math.Round(populationStdDev * 6, 3);

            return result;
        }
        #endregion
    }

    public class PcResult
    {
        #region 속성
        public string Type { get; set; } = "NaN";

        public double Cp { get; set; } = 0.0;

        public double Cpk { get; set; } = 0.0;

        public double Pp { get; set; } = 0.0;

        public double Ppk { get; set; } = 0.0;

        public double Max { get; set; } = 0.0;

        public double Min { get; set; } = 0.0;

        public double Range { get; set; } = 0.0;

        public double Mean { get; set; } = 0.0;

        public double Sigma { get; set; } = 0.0;

        public double SixSigma { get; set; } = 0.0;
        #endregion
    }
}
