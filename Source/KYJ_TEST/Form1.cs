using Cognex.VisionPro;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            Test t = new Test();
            t.Save();
        }
    }

    public class Test
    {
        //double centerX, double centerY, double sideXLength, double sideYLength, double rotation = 0, double skew = 0, bool
        //interactive = true, CogRectangleAffineDOFConstants constants = CogRectangleAffineDOFConstants.All
        [JsonProperty]
        public double CenterX { get; set; } = 1;

        [JsonProperty]
        public double CenterY { get; set; } = 2;

        [JsonProperty]
        public double SideXLength { get; set; } = 3;

        [JsonProperty]
        public double SideYLength { get; set; } = 4;

        [JsonProperty]
        public double Rotation { get; set; } = 5;

        [JsonProperty]
        public double Skew { get; set; } = 6;

        [JsonProperty]
        public bool Interactive { get; set; } = false;

        [JsonProperty]
        public CogRectangleAffineDOFConstants constants = CogRectangleAffineDOFConstants.All;


        public void Save()
        {
            string filePath = @"D:\123.cfg";
            JsonConvertHelper.Save(filePath, this);
        }
    }

}
