using Jastech.Framework.Config;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Jastech.Apps.Winform.Core.Calibrations
{
    public class CalibrationData
    {
        #region 필드
        private static CalibrationData _instance = null;
        #endregion

        #region 속성
        public string FileName { get; private set; } = "Calibration.cfg";

        public List<MatrixPointResult> MatrixPointDataList { get; private set; } = new List<MatrixPointResult>();

        [JsonProperty]
        public List<double> CalibrationMatrix { get; private set; } = new List<double>();

        [JsonProperty]
        public double RotationCenterX { get; private set; } = 0.0;

        [JsonProperty]
        public double RotationCenterY { get; private set; } = 0.0;

        [JsonProperty]
        public double CalibrationStartPositionX { get; private set; } = 0.0;

        [JsonProperty]
        public double CalibrationStartPositionY { get; private set; } = 0.0;
        #endregion

        #region 메서드
        public static CalibrationData Instance()
        {
            if (_instance == null)
            {
                _instance = new CalibrationData();
            }

            return _instance;
        }

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
            if (CalibrationMatrix.Count > 0)
                CalibrationMatrix.Clear();

            CalibrationMatrix.AddRange(calibrationMatrix);
        }

        public void SaveCalibrationResultData()
        {
            string filePath = Path.Combine(ConfigSet.Instance().Path.Temp, FileName);
            JsonConvertHelper.Save(filePath, this);
        }

        public void SetCalibrationLogData(List<MatrixPointResult> matrixPointList)
        {
            if (MatrixPointDataList.Count > 0)
                MatrixPointDataList.Clear();

            MatrixPointDataList.AddRange(matrixPointList);
        }

        public void SaveCalibrationLogData(List<MatrixPointResult> matrixPointList)
        {
            string filename = string.Format("Calibration_{0}.csv", DateTime.Now.ToString("yyyyMMdd_hhmmss"));
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
                    "Score",
                    "Matrix",
                };

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<List<string>> dataList = new List<List<string>>();

            for (int index = 0; index < matrixPointList.Count; index++)
            {
                List<string> datas = new List<string>();

                datas.Add(index.ToString());
                datas.Add(matrixPointList[index].PixelX.ToString("F1"));
                datas.Add(matrixPointList[index].PixelY.ToString("F1"));
                datas.Add(matrixPointList[index].MotionX.ToString("F4"));
                datas.Add(matrixPointList[index].MotionY.ToString("F4"));
                datas.Add(matrixPointList[index].MotionT.ToString("F4"));
                datas.Add(matrixPointList[index].Score.ToString("F4"));
                datas.Add(CalibrationMatrix[index].ToString("F6"));

                dataList.Add(datas);
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        public void LoadCalibrationData()
        {
            string filePath = Path.Combine(ConfigSet.Instance().Path.Temp, FileName);
            JsonConvertHelper.LoadToExistingTarget<CalibrationData>(filePath, this);
        }

        public List<double> GetCalibrationResultMatrix()
        {
            if (CalibrationMatrix.Count > 0)
                return CalibrationMatrix;

            return null;
        }

        public PointF ConvertVisionToReal(PointF visionCooridnates, bool useCal)
        {
            if (CalibrationMatrix.Count <= 0)
                return new PointF();

            PointF realCoordinates = new PointF();

            double pX = visionCooridnates.X;
            double pY = visionCooridnates.Y;

            realCoordinates.X = Convert.ToSingle(pX * CalibrationMatrix[0]) + Convert.ToSingle(pY * CalibrationMatrix[1]) + Convert.ToSingle(CalibrationMatrix[2]);
            realCoordinates.Y = Convert.ToSingle(pX * CalibrationMatrix[3]) + Convert.ToSingle(pY * CalibrationMatrix[4]) + Convert.ToSingle(CalibrationMatrix[5]);
            var tempTheta = Convert.ToSingle(pX * CalibrationMatrix[6]) + Convert.ToSingle(pY * CalibrationMatrix[7]) + Convert.ToSingle(CalibrationMatrix[8]);

            if (useCal)
            {
                realCoordinates.X = realCoordinates.X / tempTheta;
                realCoordinates.Y = realCoordinates.Y / tempTheta;
            }

            return realCoordinates;
        }
        #endregion
    }

    public class MatrixPointResult
    {
        #region 속성
        public double PixelX { get; set; } = 0.0;

        public double PixelY { get; set; } = 0.0;

        public double MotionX { get; set; } = 0.0;

        public double MotionY { get; set; } = 0.0;

        public double MotionT { get; set; } = 0.0;

        public double Score { get; set; } = 0.0;
        #endregion
    }
}
