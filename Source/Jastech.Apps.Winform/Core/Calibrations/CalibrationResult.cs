using Jastech.Framework.Config;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Structure.Parameters
{
    public class CalibrationResult
    {
        public string FileName { get; private set; } = "Calibration.cfg";

        public List<MatrixPointResult> MatrixPointResultList { get; set; } = new List<MatrixPointResult>();

        [JsonProperty]
        public List<double> CalibrationResultMatrix { get; set; } = new List<double>();

        [JsonProperty]
        public double RotationCenterX { get; set; } = 0.0;

        [JsonProperty]
        public double RotationCenterY { get; set; } = 0.0;

        [JsonProperty]
        public double CalibrationStartPositionX { get; set; } = 0.0;

        [JsonProperty]
        public double CalibrationStartPositionY { get; set; } = 0.0;

        public void SetRotationCenter(double x, double y)
        {
            RotationCenterX = x;
            RotationCenterY = y;
        }

        public PointF GetRotationCenter()
        {
            PointF rotationCenter = new PointF();

            rotationCenter.X = Convert.ToSingle(RotationCenterX);
            rotationCenter.Y = Convert.ToSingle(RotationCenterY);

            return rotationCenter;
        }

        public void SetCalibrationStartPosition(double x, double y)
        {
            CalibrationStartPositionX = x;
            CalibrationStartPositionY = y;
        }

        public PointF GetCalibrationStartPosition()
        {
            PointF calibrationStartPosition = new PointF();

            calibrationStartPosition.X = Convert.ToSingle(CalibrationStartPositionX);
            calibrationStartPosition.Y = Convert.ToSingle(CalibrationStartPositionY);

            return calibrationStartPosition;
        }

        public void SetCalibrationData(List<double> calibrationMatrix)
        {
            if (CalibrationResultMatrix.Count > 0)
                CalibrationResultMatrix.Clear();

            CalibrationResultMatrix.AddRange(calibrationMatrix);
        }

        public void SaveCalibrationResultData()
        {
            string filePath = Path.Combine(ConfigSet.Instance().Path.Temp, FileName);
            JsonConvertHelper.Save(filePath, this);
        }
        
        public void SetCalibrationLogData(List<MatrixPointResult> matrixPointList)
        {
            if (MatrixPointResultList.Count > 0)
                MatrixPointResultList.Clear();

            MatrixPointResultList.AddRange(matrixPointList);
        }

        public void SaveCalibrationLogData(List<MatrixPointResult> matrixPointList)
        {
            string filename = string.Format("Calibration_{0}.csv", DateTime.Now.ToShortDateString());
            string csvFile = Path.Combine(ConfigSet.Instance().Path.Temp, filename);
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Step",
                    "Pixel X",
                    "Pixel Y",
                    "Robot X",
                    "Robot Y",
                    "Robot T",
                    "Matrix",
                };

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<List<string>> dataList = new List<List<string>>();

            for (int index = 0; index < matrixPointList.Count; index++)
            {
                //List<string> datas = new List<string>
                //{
                //    index.ToString(),
                //    matrixPointList[index].PixelX.ToString("F1"),
                //    matrixPointList[index].PixelY.ToString("F1"),
                //    matrixPointList[index].MotionX.ToString("F3"),
                //    matrixPointList[index].MotionY.ToString("F3"),
                //    matrixPointList[index].MotionT.ToString("F3"),
                //    CalibrationResultMatrix[index].ToString("F6")
                //};

                List<string> datas = new List<string>();

                datas.Add(index.ToString());
                datas.Add(matrixPointList[index].PixelX.ToString("F1"));
                datas.Add(matrixPointList[index].PixelY.ToString("F1"));
                datas.Add(matrixPointList[index].MotionX.ToString("F3"));
                datas.Add(matrixPointList[index].MotionY.ToString("F3"));
                datas.Add(matrixPointList[index].MotionT.ToString("F3"));
                datas.Add(CalibrationResultMatrix[index].ToString("F6"));
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        public void LoadCalibrationData()
        {

        }

        public PointF ConvertVisionToReal(PointF visionCooridnates)
        {
            if (CalibrationResultMatrix.Count <= 0)
                return new PointF();

            PointF realCoordinates = new PointF();

            double pX = visionCooridnates.X;
            double pY = visionCooridnates.Y;

            realCoordinates.X = Convert.ToSingle((pX * CalibrationResultMatrix[0]) + (pY * CalibrationResultMatrix[1]) + CalibrationResultMatrix[2]);
            realCoordinates.Y = Convert.ToSingle((pX * CalibrationResultMatrix[3]) + (pY * CalibrationResultMatrix[4]) + CalibrationResultMatrix[5]);

            return realCoordinates;
        }
    }

    public class MatrixPointResult
    {
        public double PixelX { get; set; } = 0.0;

        public double PixelY { get; set; } = 0.0;

        public double MotionX { get; set; } = 0.0;

        public double MotionY { get; set; } = 0.0;

        public double MotionT { get; set; } = 0.0;

        public double Score { get; set; } = 0.0;
    }
}
