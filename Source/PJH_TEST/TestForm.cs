using Jastech.Apps.Winform.Service;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PJH_TEST
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string dateTime = txtDateTime.Text;
            //int cellIndex = Convert.ToInt32(txtCellIndex.Text);
            //string sourceFile = @"D:\99. Private\JastechApps\Jastech.Apps\Runtime\Result\[20230516_091010][Cell_1]Akkon_Stage1_Top_Tab1.csv";

            //for (int tabNo = 0; tabNo < 5; tabNo++)
            //{
            //    string destFile = dateTime + "[Cell_" + cellIndex.ToString("D3") + "]" + "Akkon_Stage1_Top_Tab" + (tabNo + 1).ToString() + ".csv";
            //    string destPath = @"D:\99. Private\JastechApps\Jastech.Apps\Runtime\Result\tlqkf\" + destFile;

            //    System.IO.File.Copy(sourceFile, destPath, true);
            //}

            //txtCellIndex.Text = (cellIndex + 1).ToString();


            //DirectoryInfo di = new DirectoryInfo(@"D:\99. Private\JastechApps\Jastech.Apps\Runtime\Result\Akkon");


            //foreach (FileInfo file in di.GetFiles()) 
            //{
            //    string tt = file.Name;

            //    string date = tt.Substring(1, 8);
            //    string time = tt.Substring(10, 6);

            //    string oldText = "[09:00:00]";
            //    string newText = "[" + time.Substring(0, 2) + ":" + time.Substring(2, 2) + ":" + time.Substring(4) + "]";

            //    StreamWriter sw = new StreamWriter(file.FullName, false);
            //}

            //List<string> list = new List<string>();

            //Random rand = new Random();

            //for (int i = 0; i < 100; i++)
            //{
            //    string hour = rand.Next(0, 23).ToString("D2");
            //    string min = rand.Next(0, 59).ToString("D2");
            //    string sec = rand.Next(0, 59).ToString("D2");

            //    string result = "[" + hour + ":" + min + ":" + sec + "]";

            //    for(int j = 0; j < 5; j++) 
            //        list.Add(result);
            //}

            //list.Sort();

            //for (int i = 0; i < list.Count;i++)
            //    Console.WriteLine(list[i]);

            //for (int i = 0; i < 100; i++)
            //    Console.WriteLine("Cell_" + i.ToString("D3"));
        }

        private void btnAlignTest_Click(object sender, EventArgs e)
        {
            //var dailyInfo = DailyInfoService.GetDailyInfo();

            //DailyData dailyData = new DailyData();

            //for (int tabNo = 0; tabNo < 4; tabNo++)
            //{
            //    AlignDailyInfo alignInfo = new AlignDailyInfo();

            //    alignInfo.InspectionTime = DateTime.Now.ToString("yyyyMMdd_HHmm");
            //    alignInfo.PanelID = DateTime.Now.ToString("yyyyMMdd_HHmm");
            //    alignInfo.TabNo = tabNo;
            //    alignInfo.Judgement = Judgement.OK;

            //    alignInfo.LX = (tabNo * 1) + tabNo;
            //    alignInfo.LY = (tabNo * 2) + tabNo;
            //    alignInfo.RX = (tabNo * 3) + tabNo;
            //    alignInfo.RY = (tabNo * 4) + tabNo;
            //    alignInfo.CX = (tabNo * 5) + tabNo;

            //    dailyData.AddAlignInfo(alignInfo);
            //}

            //dailyInfo.SetAlignDailyData(dailyData.AlignDailyInfoList);
            //int gg = 0;
        }

        private void btnAkkonTest_Click(object sender, EventArgs e)
        {
            //var dailyInfo = DailyInfoService.GetDailyInfo();

            //DailyData dailyData = new DailyData();

            //for (int tabNo = 0; tabNo < 4; tabNo++)
            //{
            //    AkkonDailyInfo akkonInfo = new AkkonDailyInfo();

            //    akkonInfo.InspectionTime = DateTime.Now.ToString("yyyyMMdd_HHmm");
            //    akkonInfo.PanelID = DateTime.Now.ToString("yyyyMMdd_HHmm");
            //    akkonInfo.TabNo = tabNo;
            //    akkonInfo.Judgement = Judgement.OK;

            //    akkonInfo.AvgBlobCount = (tabNo * 10) + tabNo;
            //    akkonInfo.AvgLength = (tabNo * 20) + tabNo;
            //    akkonInfo.AvgStrength = (tabNo * 30) + tabNo;
            //    akkonInfo.AvgSTD = (tabNo * 40) + tabNo;

            //    dailyData.AddAkkonInfo(akkonInfo);
            //}

            //dailyInfo.SetAkkonDailyData(dailyData.AkkonDailyInfoList);
            //int gg = 0;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //int gg = 0;

            //var dailyInfo = DailyInfoService.GetDailyInfo();

            //int tt = 0;

            //dailyInfo.AddDailyDataList(dailyInfo.GetDailyData());
        }
    }
}
