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

        public Result GetResult(List<double> valueList, double upperSpecLimit, double lowerSpecLimit)
        {
            if (valueList.Count <= 1)
                return new Result();

            Result result = new Result();

            result.Average = valueList.Average();
            result.StdOverall = GetStandardDeviation(valueList);

            double T = Math.Abs(upperSpecLimit - lowerSpecLimit);

            result.Pp = Math.Round(T / (6 * result.StdOverall), 2);
            result.PpU = Math.Round((upperSpecLimit - result.Average) / (3 * result.StdOverall), 2);
            result.PpL = Math.Round((result.Average - lowerSpecLimit) / (3 * result.StdOverall), 2);
            result.Ppk = Math.Min(result.PpU, result.PpL);

            List<double> listMove = new List<double>();

            for (int index = 0; index < valueList.Count; index++)
            {
                if (index == valueList.Count - 1)
                    continue;

                double diff = Math.Abs(valueList[index + 1] - valueList[index]);
                listMove.Add(diff);
            }

            double RBar = listMove.Sum() / (valueList.Count - WEIGHT + 1);
            
            result.StdWithin = RBar / d2;

            result.Cp = Math.Round(T / (6 * result.StdWithin), 2);
            result.CpU = Math.Round((upperSpecLimit - result.Average) / (TOLERANCE / 2 * result.StdWithin), 2);
            result.CpL = Math.Round((result.Average - lowerSpecLimit) / (TOLERANCE / 2 * result.StdWithin), 2);
            result.Cpk = Math.Min(result.CpU, result.CpL);

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

    public class Result
    {
        public double Cp { get; set; } = 0.0;

        public double CpL { get; set; } = 0.0;

        public double CpU { get; set; } = 0.0;

        public double Cpk { get; set; } = 0.0;

        public double Pp { get; set; } = 0.0;

        public double PpL { get; set; } = 0.0;

        public double PpU { get; set; } = 0.0;

        public double Ppk { get; set; } = 0.0;

        public double StdOverall { get; set; } = 0.0;

        public double StdWithin { get; set; } = 0.0;

        public double Average { get; set; } = 0.0;
    }
}
