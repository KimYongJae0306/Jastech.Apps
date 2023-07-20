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
        public PointF ReferencePoint1 { get; private set; }

        public PointF ReferencePoint2 { get; private set; }

        public double DiffRadian { get; private set; }

        public PointF OffsetPoint { get; private set; }

        public void SetReferencePoint(PointF referencePoint1, PointF referencePoint2)
        {
            ReferencePoint1 = referencePoint1;
            ReferencePoint2 = referencePoint2;
        }

        private void SetCoordinateParam()
        {
            PointF referenceCenterPoint = MathHelper.GetCenterPoint(ReferencePoint1, ReferencePoint2);
        }

        public void SetDiffRadian()
        {

        }

        public double GetDiffRadian()
        {
            return DiffRadian;
        }

        public void SetOffsetPoint(PointF referenceCenterPoint, PointF searchedCenterPoint)
        {
            OffsetPoint = MathHelper.GetOffset(referenceCenterPoint, searchedCenterPoint);
        }

        public PointF GetOffsetPoint()
        {
            return OffsetPoint;
        }

        public PointF GetCoordinate(PointF inputPoint)
        {
            //PointF searchedCenterPoint = GetSearchedCenterPoint();
            //double diffRadian = GetDiffRadian();
            //PointF offsetPoint = GetOffsetPoint();

            //return MathHelper.GetCoordinate(searchedCenterPoint, diffRadian, offsetPoint, inputPoint);

            return null;
        }
    }
}
