using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.IO;

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

            for (int i = 0; i < 100; i++)
                Console.WriteLine("Cell_" + i.ToString("D3"));
        }
    }
}
