using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Framework.Util
{
    public class CoordinateTransform
    {
        public double DiffRadian { get; private set; } = 0.0;

        public PointF OffsetPoint { get; private set; } = new PointF();

        CoordinateData ReferenceData = null;

        CoordinateData TargetData = null;

        public void SetReferenceData(PointF referencePoint1, PointF referencePoint2)
        {
            ReferenceData = new CoordinateData();
            ReferenceData.SetParam(referencePoint1, referencePoint2);
        }

        public void SetTargetData(PointF targetPoint1, PointF targetPoint2)
        {
            TargetData = new CoordinateData();
            TargetData.SetParam(targetPoint1, targetPoint2);
        }

        //public CoordinateTransform DeepCopy()
        //{
        //    CoordinateTransform coordinateTransform = new CoordinateTransform();

        //    coordinateTransform.DiffRadian = DiffRadian;
        //    coordinateTransform.OffsetPoint = OffsetPoint;

        //    if (ReferenceData != null)
        //        coordinateTransform.ReferenceData = ReferenceData.DeepCopy();

        //    if (TargetData != null)
        //        coordinateTransform.TargetData = TargetData.DeepCopy();

        //    return coordinateTransform;
        //}

        //public void Dispose()
        //{
        //    ReferenceData.Dispose();
        //    TargetData.Dispose();
        //}

        private void SetOffsetPoint()
        {
            if (ReferenceData == null || TargetData == null)
                return;

            var referenceCenterPoint = ReferenceData.GetCenterPoint();
            var targetCenterPoint = TargetData.GetCenterPoint();

            OffsetPoint =  MathHelper.GetOffset(referenceCenterPoint, targetCenterPoint);
        }

        private PointF GetOffsetPoint()
        {
            return OffsetPoint;
        }

        private void SetDiffAngle()
        {
            if (ReferenceData == null || TargetData == null)
                return;

            var referenceRadian = ReferenceData.GetRadian();
            var targetRadian = TargetData.GetRadian();

            DiffRadian = referenceRadian - targetRadian;
        }

        private double GetDiffRadian()
        {
            return DiffRadian;
        }

        public void ExecuteCoordinate()
        {
            SetOffsetPoint();
            SetDiffAngle();
        }

        public PointF GetCoordinate(PointF inputPoint)
        {
            var diffRadian = GetDiffRadian();
            var offsetPoint = GetOffsetPoint();

            if (diffRadian == 0.0 || offsetPoint == null)
                return inputPoint;

            var targetCenterPoint = TargetData.GetCenterPoint();
            return MathHelper.GetCoordinate(targetCenterPoint, diffRadian, offsetPoint, inputPoint);
        }
    }

    public class CoordinateData
    {
        public PointF Point1 { get; private set; }

        public PointF Point2 { get; private set; }

        public void SetParam(PointF point1, PointF point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public PointF GetCenterPoint()
        {
            return MathHelper.GetCenterPoint(Point1, Point2);
        }

        public double GetRadian()
        {
            double radian = MathHelper.GetRadian(Point1, Point2);
            if (radian > 180.0)
                radian -= 360;

            return radian;
        }

        //public CoordinateData DeepCopy()
        //{
        //    CoordinateData coordinateData = new CoordinateData();

        //    coordinateData.Point1 = Point1;
        //    coordinateData.Point2 = Point2;

        //    return coordinateData;
        //}

        //public void Dispose()
        //{
        //    this.Dispose();
        //}
    }
}
