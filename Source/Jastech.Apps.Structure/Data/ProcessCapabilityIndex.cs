using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jastech.Apps.Structure.Data
{
    public class ProcessCapabilityIndex
    {
        public double CapabilityUSL { private get; set; }
        public double CapabilityLSL { private get; set; }
        public double PerformanceUSL_Center { private get; set; }
        public double PerformanceLSL_Center { private get; set; }
        public double PerformanceUSL_Side { private get; set; }
        public double PerformanceLSL_Side { private get; set; }

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

            // Round the results
            result.Cp = Math.Round(result.Cp, 3);
            result.Cpk = Math.Round(result.Cpk, 3);
            result.Pp = Math.Round(result.Pp, 3);
            result.Ppk = Math.Round(result.Ppk, 3);

            return result;
        }
    }

    public class PcResult
    {
        public string Type { get; set; } = "NaN";

        public double Cp { get; set; } = 0.0;

        public double Cpk { get; set; } = 0.0;

        public double Pp { get; set; } = 0.0;

        public double Ppk { get; set; } = 0.0;
    }
}
