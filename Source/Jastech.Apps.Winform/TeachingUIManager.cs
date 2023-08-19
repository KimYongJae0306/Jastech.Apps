using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform.VisionPro.Controls;

namespace Jastech.Apps.Winform
{
    public class TeachingUIManager
    {
        #region 필드
        private static TeachingUIManager _instance = null;
        #endregion

        #region 속성
        private CogDisplayControl TeachingDisplay { get; set; } = null;

        private ICogImage OriginCogImageBuffer { get; set; } = null;

        private Mat OriginMatImageBuffer { get; set; } = null;

        private ICogImage BinaryCogImageBuffer { get; set; } = null;

        private ICogImage ResultCogImageBuffer { get; set; } = null;

        private ICogImage AkkonCogImageBuffer { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public static TeachingUIManager Instance()
        {
            if (_instance == null)
            {
                _instance = new TeachingUIManager();
            }

            return _instance;
        }

        public CogDisplayControl GetDisplay()
        {
            return TeachingDisplay;
        }

        public void SetDisplay(CogDisplayControl display)
        {
            TeachingDisplay = display;
        }

        public ICogImage GetOriginCogImageBuffer(bool isDeepCopy)
        {
            if (isDeepCopy)
            {
                if (OriginCogImageBuffer == null)
                    return null;

                return OriginCogImageBuffer.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }

            return OriginCogImageBuffer;
        }

        public void SetOrginCogImageBuffer(ICogImage cogImage)
        {
            if(OriginCogImageBuffer != null)
                (OriginCogImageBuffer as CogImage8Grey).Dispose();

            if (BinaryCogImageBuffer != null)
                (BinaryCogImageBuffer as CogImage8Grey).Dispose();

            if (ResultCogImageBuffer != null)
                (ResultCogImageBuffer as CogImage24PlanarColor).Dispose();

            if(AkkonCogImageBuffer != null)
                (AkkonCogImageBuffer as CogImage8Grey).Dispose();

            OriginCogImageBuffer = null;
            BinaryCogImageBuffer = null;
            ResultCogImageBuffer = null;
            AkkonCogImageBuffer = null;
            OriginCogImageBuffer = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);

            if (OriginMatImageBuffer != null)
            {
                OriginMatImageBuffer.Dispose();
                OriginMatImageBuffer = null;
            }

            //TeachingDisplay?.SetImage(OrginCogImageBuffer);
        }

        public void SetOriginMatImageBuffer(Mat mat)
        {
            if(OriginMatImageBuffer != null)
            {
                OriginMatImageBuffer.Dispose();
                OriginMatImageBuffer = null;
            }
            OriginMatImageBuffer = mat;
        }

        public Mat GetOriginMatImageBuffer(bool isDeepCopy)
        {
            if(isDeepCopy)
                return MatHelper.DeepCopy(OriginMatImageBuffer);

            return OriginMatImageBuffer;
        }

        public void SetBinaryCogImageBuffer(ICogImage cogImage)
        {
            BinaryCogImageBuffer = cogImage;
            TeachingDisplay?.SetImage(BinaryCogImageBuffer);
        }

        public ICogImage GetBinaryCogImage(bool isDeepCopy)
        {
            if (isDeepCopy)
            {
                if (BinaryCogImageBuffer == null)
                    return null;

                return BinaryCogImageBuffer.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }

            return BinaryCogImageBuffer;
        }

        public void SetResultCogImage(ICogImage cogImage)
        {
            if (ResultCogImageBuffer is CogImage8Grey grey)
            {
                grey.Dispose();
                grey = null;
            }
            if (ResultCogImageBuffer is CogImage24PlanarColor color)
            {
                color.Dispose();
                color = null;
            }

            ResultCogImageBuffer = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
           // ResultCogImageBuffer = cogImage;
            TeachingDisplay?.SetImage(ResultCogImageBuffer);
        }

        public ICogImage GetResultCogImage(bool isDeepCopy)
        {
            if (isDeepCopy)
            {
                if (ResultCogImageBuffer == null)
                    return null;

                return ResultCogImageBuffer.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }

            return ResultCogImageBuffer;
        }

        public void SetAkkonCogImage(ICogImage cogImage)
        {
            if (AkkonCogImageBuffer is CogImage8Grey grey)
            {
                grey.Dispose();
                grey = null;
            }
            if (AkkonCogImageBuffer is CogImage24PlanarColor color)
            {
                color.Dispose();
                color = null;
            }

            AkkonCogImageBuffer = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
            //AkkonCogImageBuffer = cogImage;
            TeachingDisplay?.SetImage(AkkonCogImageBuffer);
        }

        public ICogImage GetAkkonCogImage(bool isDeepCopy)
        {
            if (isDeepCopy)
            {
                if (AkkonCogImageBuffer == null)
                    return null;

                return AkkonCogImageBuffer.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }

            return AkkonCogImageBuffer;
        }
        #endregion

    }
}
