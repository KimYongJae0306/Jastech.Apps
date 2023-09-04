using System;
using System.Collections.Generic;
using System.Linq;
using Jastech.Apps.Winform.Core;

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
            double sampleStdDev = GetSampleStandardDeviation(valueList, mean);
            double populationStdDev = GetPopulationStandardDeviation(valueList, mean);

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

        private double GetSampleStandardDeviation(List<double> valueList, double mean)
        {
            double squaredDifferencesSum = valueList.Sum(value => Math.Pow(value - mean, 2));
            double squaredDifferencesMean = squaredDifferencesSum / (valueList.Count - 1);
            double standardDeviation = Math.Sqrt(squaredDifferencesMean);

            return standardDeviation;
        }

        private double GetPopulationStandardDeviation(List<double> valueList, double mean)
        {
            if (valueList.Count < 2)
                return 0;

            double squaredDifferencesSum = valueList.Select(value => Math.Pow(value - mean, 2)).Sum();
            double squaredDifferencesMean = squaredDifferencesSum / valueList.Count;
            double standardDeviation = Math.Sqrt(squaredDifferencesMean);

            return standardDeviation;
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
