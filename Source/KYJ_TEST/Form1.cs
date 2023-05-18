using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Imaging.Ipp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KYJ_TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AkkonAlgorithm AkkonAlgorithm = new AkkonAlgorithm();

            Mat mat = new Mat(@"D:\enh_raw.bmp", ImreadModes.Grayscale);
            int size = mat.Width * mat.Height;
            byte[] dataArray = new byte[size];
            Marshal.Copy(mat.DataPointer, dataArray, 0, size);

            AkkonAlgorithm.Run(dataArray, mat.Width, mat.Height, new AkkonParameters());
            //Test(dataArray, mat.Width, mat.Height);
            //LiveViewPanel = new LiveViewPanel();
            //LiveViewPanel.Dock = DockStyle.Fill;
            //panel1.Controls.Add(LiveViewPanel);
        }


        public static void Test(byte[] sourceData, int width, int height)
        {
            int srcStep;
            int dstStep;
            IntPtr src16Ptr = IPPWrapper.ippiMalloc_16s_C1(width, height, out srcStep); // 16비트 메모리 할당

            IntPtr dst16Ptr = IPPWrapper.ippiMalloc_16s_C1(width, height, out dstStep); // 16비트 메모리 할당
            int dst16Size = dstStep * height;
            short[] dst16Data = new short[dst16Size];
            Marshal.Copy(dst16Ptr, dst16Data, 0, dst16Size);

            IPPWrapper.ippiScale_8u16s_C1R(sourceData, srcStep, dst16Data, dstStep, width, height, 0.0, -128); // 8Bit->16Bit로 변경


            IPPWrapper.ippiFree(src16Ptr);
            IPPWrapper.ippiFree(dst16Ptr);
        }

        //void SepConv16s_rowfirst(Ipp16s* pSrcBuffer, Ipp16s* pDstBuffer, IppiSize size, Ipp16s* xkernel, int xn, int xdivisor, Ipp16s* ykernel, int yn, int ydivisor)
        //{

        //    int sizerow, sizecol, i;
        //    int maxKernelSize = (xn > yn) ? xn : yn;
        //    int xAnchor = (xn >> 1); //+ 1;
        //    int yAnchor = (yn >> 1); //+ 1;

        //    int Nc = yn;
        //    int Nr = xn;

        //    // compute the kernel semisizes
        //    int Ncss = Nc >> 1;
        //    int Nrss = Nr >> 1;

        //    // compute the kernel offsets (0 -> odd, 1 -> even)
        //    int co = 1 - (Nc % 2);
        //    int ro = 1 - (Nr % 2);

        //    // allocate temporary dst buffer
        //    int tmpStep, padStep;
        //    // The IPP filter functions seem to need 1 more row allocated
        //    // than is obvious or they sometimes crash.
        //    IppiSize tmpSize;
        //    tmpSize.width = size.width; tmpSize.height = size.height + Nc + 1;
        //    if (!(pTmp = ippiMalloc_16s_C1(tmpSize.width, tmpSize.height, &tmpStep)))
        //        throw exception("nIppSepFilterCR mem-alloc error.");


        //    //ppSrc = (Ipp16s**) ippsMalloc_16s( size.height + Nc + 1 );
        //    //ppDst = (Ipp16s**) ippsMalloc_16s( size.height );

        //    ppSrc = (Ipp16s**)ippsMalloc_8u((size.height + Nc + 1) * sizeof(Ipp16s*));
        //    ppDst = (Ipp16s**)ippsMalloc_8u(size.height * sizeof(Ipp16s*));

        //    // size of temporary buffers

        //    status = ippiFilterRowBorderPipelineGetBufferSize_16s_C1R(size, Nr, &sizerow);
        //    status = ippiFilterColumnPipelineGetBufferSize_16s_C1R(size, Nc, &sizecol);


        //    // allocate temporary buffers
        //    pBufferCol = ippsMalloc_8u(sizecol);
        //    pBufferRow = ippsMalloc_8u(sizerow);


        //    Nrss -= ro;
        //    Ncss -= co;

        //    //	organize dst buffer
        //    for (int ii = 0, jj = Ncss; ii < size.height; ++ii, ++jj)
        //    {
        //        ppDst[ii] = pTmp + jj * size.width;
        //        ppSrc[jj] = pTmp + jj * size.width;


        //    }

        //    // for border replicate
        //    for (int ii = 0, jj = size.height + Ncss; ii < Ncss; ii++, jj++)
        //    {
        //        ppSrc[ii] = ppSrc[Ncss];
        //        ppSrc[jj] = ppSrc[size.height + Ncss - 1];
        //    }

        //    if (co)
        //    {
        //        ppSrc[size.height + (Ncss * 2)] = ppSrc[size.height + Ncss - 1];
        //    }


        //    status = ippiFilterRowBorderPipeline_16s_C1R((const Ipp16s*)pSrcBuffer, size.width * sizeof(Ipp16s), ppDst, size, xkernel, xn, Nrss, ippBorderRepl, 0, xdivisor, pBufferRow);
        //    status = ippiFilterColumnPipeline_16s_C1R((const Ipp16s**)ppSrc, pDstBuffer, size.width * sizeof(Ipp16s), size, ykernel, yn, ydivisor, pBufferCol);



        //    ippsFree(pTmp);
        //    ippsFree(ppSrc);

        //    ippsFree(ppDst);
        //    //ippsFree(ppTmp);

        //    ippsFree(pBufferCol);

        //    ippsFree(pBufferRow);

        //}

    }
}
