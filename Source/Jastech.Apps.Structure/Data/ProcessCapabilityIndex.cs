using System;
using System.Collections.Generic;
using System.Linq;

namespace Jastech.Apps.Structure.Data
{
    public class ProcessCapabilityIndex
    {
        private const double WEIGHT = 2.0;
        private const double d2 = 1.128;
        private const int TOLERANCE = 6;

        public PcResult GetResult(string type, List<double> valueList, double upperSpecLimit, double lowerSpecLimit)
        {
            if (valueList.Count <= 1)
                return new PcResult();

            PcResult result = new PcResult();
            result.Type = type;

            double Average = valueList.Average();
            double StdOverall = GetStandardDeviation(valueList);

            double T = Math.Abs(upperSpecLimit - lowerSpecLimit);

            double PpU = Math.Round((upperSpecLimit - Average) / (3 * StdOverall), 2);
            double PpL = Math.Round((Average - lowerSpecLimit) / (3 * StdOverall), 2);
            result.Pp = Math.Round(T / (6 * StdOverall), 2);
            result.Ppk = Math.Min(PpU, PpL);

            List<double> listMove = new List<double>();

            for (int index = 0; index < valueList.Count; index++)
            {
                if (index == valueList.Count - 1)
                    continue;

                double diff = Math.Abs(valueList[index + 1] - valueList[index]);
                listMove.Add(diff);
            }

            double RBar = listMove.Sum() / (valueList.Count - WEIGHT + 1);
            double StdWithin = RBar / d2;

            double CpU = Math.Round((upperSpecLimit - Average) / (TOLERANCE / 2 * StdWithin), 2);
            double CpL = Math.Round((Average - lowerSpecLimit) / (TOLERANCE / 2 * StdWithin), 2);
            result.Cp = Math.Round(T / (6 * StdWithin), 2);
            result.Cpk = Math.Min(CpU, CpL);

            return result;
        }

        private double GetStandardDeviation(List<double> valueList)
        {
            if (valueList.Count <= 0)
                return 0.0;

            double average = valueList.Average();
            double sumOfDeviation = 0.0;

            foreach (var item in valueList)
                sumOfDeviation += Math.Pow(item - average, 2);

            double std = Math.Sqrt(sumOfDeviation / (valueList.Count - 1));
            return std;
        }
    }

    public class PcResult
    {
        public string Type { get; set; } = "NaN";

        public double Cp { get; set; } = 0.0;

        //public double CpL { get; set; } = 0.0;

        //public double CpU { get; set; } = 0.0;

        public double Cpk { get; set; } = 0.0;

        public double Pp { get; set; } = 0.0;

        //public double PpL { get; set; } = 0.0;

        //public double PpU { get; set; } = 0.0;

        public double Ppk { get; set; } = 0.0;

        //public double StdOverall { get; set; } = 0.0;

        //public double StdWithin { get; set; } = 0.0;

        //public double Average { get; set; } = 0.0;
    }
}
