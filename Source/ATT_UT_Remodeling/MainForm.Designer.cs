namespace ATT_UT_Remodeling
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tlpMainForm = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPage = new System.Windows.Forms.Panel();
            this.tlpFunctionButtons = new System.Windows.Forms.TableLayoutPanel();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogPageImage = new System.Windows.Forms.Label();
            this.lblLogPage = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDataPageImage = new System.Windows.Forms.Label();
            this.lblDataPage = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblInspectionPageImage = new System.Windows.Forms.Label();
            this.lblMainPage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTeachingPageImage = new System.Windows.Forms.Label();
            this.lblTeachingPage = new System.Windows.Forms.Label();
            this.pnlMachineStatus = new System.Windows.Forms.Panel();
            this.tlpMachineStatus = new System.Windows.Forms.TableLayoutPanel();
            this.lblDoorlockState = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblCurrentModel = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.tmrMainForm = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMotionState = new System.Windows.Forms.Label();
            this.lblLightState = new System.Windows.Forms.Label();
            this.lblPlcStateText = new System.Windows.Forms.Label();
            this.lblPLCState = new System.Windows.Forms.Label();
            this.lblLicenseStateText = new System.Windows.Forms.Label();
            this.lblLicenseState = new System.Windows.Forms.Label();
            this.lblLafStateText = new System.Windows.Forms.Label();
            this.lblLafState = new System.Windows.Forms.Label();
            this.lblMotionStateText = new System.Windows.Forms.Label();
            this.lblLightStateText = new System.Windows.Forms.Label();
            this.tmrUpdateStates = new System.Windows.Forms.Timer(this.components);
            this.tlpMainForm.SuspendLayout();
            this.tlpFunctionButtons.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnlMachineStatus.SuspendLayout();
            this.tlpMachineStatus.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainForm
            // 
            this.tlpMainForm.ColumnCount = 1;
            this.tlpMainForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainForm.Controls.Add(this.pnlPage, 0, 2);
            this.tlpMainForm.Controls.Add(this.tlpFunctionButtons, 0, 1);
            this.tlpMainForm.Controls.Add(this.pnlMachineStatus, 0, 0);
            this.tlpMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainForm.Location = new System.Drawing.Point(0, 0);
            this.tlpMainForm.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMainForm.Name = "tlpMainForm";
            this.tlpMainForm.RowCount = 3;
            this.tlpMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainForm.Size = new System.Drawing.Size(1924, 806);
            this.tlpMainForm.TabIndex = 2;
            // 
            // pnlPage
            // 
            this.pnlPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPage.Location = new System.Drawing.Point(0, 100);
            this.pnlPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPage.Name = "pnlPage";
            this.pnlPage.Size = new System.Drawing.Size(1924, 706);
            this.pnlPage.TabIndex = 3;
            // 
            // tlpFunctionButtons
            // 
            this.tlpFunctionButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.tlpFunctionButtons.ColumnCount = 9;
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFunctionButtons.Controls.Add(this.lblCurrentTime, 8, 0);
            this.tlpFunctionButtons.Controls.Add(this.panel2, 4, 0);
            this.tlpFunctionButtons.Controls.Add(this.panel4, 3, 0);
            this.tlpFunctionButtons.Controls.Add(this.panel3, 2, 0);
            this.tlpFunctionButtons.Controls.Add(this.panel8, 0, 0);
            this.tlpFunctionButtons.Controls.Add(this.panel1, 1, 0);
            this.tlpFunctionButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFunctionButtons.Location = new System.Drawing.Point(0, 50);
            this.tlpFunctionButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpFunctionButtons.Name = "tlpFunctionButtons";
            this.tlpFunctionButtons.RowCount = 1;
            this.tlpFunctionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFunctionButtons.Size = new System.Drawing.Size(1924, 50);
            this.tlpFunctionButtons.TabIndex = 0;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentTime.Font = new System.Drawing.Font("맑은 고딕", 12.2F, System.Drawing.FontStyle.Bold);
            this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
            this.lblCurrentTime.Location = new System.Drawing.Point(1603, 3);
            this.lblCurrentTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(318, 44);
            this.lblCurrentTime.TabIndex = 3;
            this.lblCurrentTime.Text = "DateTime";
            this.lblCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(803, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 44);
            this.panel2.TabIndex = 19;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel4);
            this.panel4.Location = new System.Drawing.Point(603, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(194, 44);
            this.panel4.TabIndex = 20;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.lblLogPageImage, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblLogPage, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(194, 44);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // lblLogPageImage
            // 
            this.lblLogPageImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogPageImage.Image = global::ATT_UT_Remodeling.Properties.Resources.Log;
            this.lblLogPageImage.Location = new System.Drawing.Point(3, 0);
            this.lblLogPageImage.Name = "lblLogPageImage";
            this.lblLogPageImage.Size = new System.Drawing.Size(44, 44);
            this.lblLogPageImage.TabIndex = 1;
            this.lblLogPageImage.Click += new System.EventHandler(this.lblLogPage_Click);
            // 
            // lblLogPage
            // 
            this.lblLogPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogPage.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblLogPage.ForeColor = System.Drawing.Color.White;
            this.lblLogPage.Location = new System.Drawing.Point(53, 0);
            this.lblLogPage.Name = "lblLogPage";
            this.lblLogPage.Size = new System.Drawing.Size(138, 44);
            this.lblLogPage.TabIndex = 0;
            this.lblLogPage.Text = "LOG";
            this.lblLogPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogPage.Click += new System.EventHandler(this.lblLogPage_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(403, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(194, 44);
            this.panel3.TabIndex = 20;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.lblDataPageImage, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.lblDataPage, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(194, 44);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // lblDataPageImage
            // 
            this.lblDataPageImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataPageImage.Image = global::ATT_UT_Remodeling.Properties.Resources.Settings;
            this.lblDataPageImage.Location = new System.Drawing.Point(3, 0);
            this.lblDataPageImage.Name = "lblDataPageImage";
            this.lblDataPageImage.Size = new System.Drawing.Size(44, 44);
            this.lblDataPageImage.TabIndex = 1;
            this.lblDataPageImage.Click += new System.EventHandler(this.lblDataPage_Click);
            // 
            // lblDataPage
            // 
            this.lblDataPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataPage.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblDataPage.ForeColor = System.Drawing.Color.White;
            this.lblDataPage.Location = new System.Drawing.Point(53, 0);
            this.lblDataPage.Name = "lblDataPage";
            this.lblDataPage.Size = new System.Drawing.Size(138, 44);
            this.lblDataPage.TabIndex = 0;
            this.lblDataPage.Text = "DATA";
            this.lblDataPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDataPage.Click += new System.EventHandler(this.lblDataPage_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.tableLayoutPanel1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(194, 44);
            this.panel8.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblInspectionPageImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMainPage, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(194, 44);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblInspectionPageImage
            // 
            this.lblInspectionPageImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInspectionPageImage.Image = ((System.Drawing.Image)(resources.GetObject("lblInspectionPageImage.Image")));
            this.lblInspectionPageImage.Location = new System.Drawing.Point(3, 0);
            this.lblInspectionPageImage.Name = "lblInspectionPageImage";
            this.lblInspectionPageImage.Size = new System.Drawing.Size(44, 44);
            this.lblInspectionPageImage.TabIndex = 1;
            this.lblInspectionPageImage.Click += new System.EventHandler(this.lblMainPage_Click);
            // 
            // lblMainPage
            // 
            this.lblMainPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMainPage.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblMainPage.ForeColor = System.Drawing.Color.White;
            this.lblMainPage.Location = new System.Drawing.Point(53, 0);
            this.lblMainPage.Name = "lblMainPage";
            this.lblMainPage.Size = new System.Drawing.Size(138, 44);
            this.lblMainPage.TabIndex = 0;
            this.lblMainPage.Text = "MAIN";
            this.lblMainPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMainPage.Click += new System.EventHandler(this.lblMainPage_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(203, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 44);
            this.panel1.TabIndex = 18;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblTeachingPageImage, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblTeachingPage, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(194, 44);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblTeachingPageImage
            // 
            this.lblTeachingPageImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTeachingPageImage.Image = global::ATT_UT_Remodeling.Properties.Resources.Teaching;
            this.lblTeachingPageImage.Location = new System.Drawing.Point(3, 0);
            this.lblTeachingPageImage.Name = "lblTeachingPageImage";
            this.lblTeachingPageImage.Size = new System.Drawing.Size(44, 44);
            this.lblTeachingPageImage.TabIndex = 1;
            this.lblTeachingPageImage.Click += new System.EventHandler(this.lblTeachingPage_Click);
            // 
            // lblTeachingPage
            // 
            this.lblTeachingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTeachingPage.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblTeachingPage.ForeColor = System.Drawing.Color.White;
            this.lblTeachingPage.Location = new System.Drawing.Point(53, 0);
            this.lblTeachingPage.Name = "lblTeachingPage";
            this.lblTeachingPage.Size = new System.Drawing.Size(138, 44);
            this.lblTeachingPage.TabIndex = 0;
            this.lblTeachingPage.Text = "TEACH";
            this.lblTeachingPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTeachingPage.Click += new System.EventHandler(this.lblTeachingPage_Click);
            // 
            // pnlMachineStatus
            // 
            this.pnlMachineStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMachineStatus.Controls.Add(this.tlpMachineStatus);
            this.pnlMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMachineStatus.Location = new System.Drawing.Point(0, 0);
            this.pnlMachineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMachineStatus.Name = "pnlMachineStatus";
            this.pnlMachineStatus.Size = new System.Drawing.Size(1924, 50);
            this.pnlMachineStatus.TabIndex = 2;
            // 
            // tlpMachineStatus
            // 
            this.tlpMachineStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.tlpMachineStatus.ColumnCount = 4;
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpMachineStatus.Controls.Add(this.lblDoorlockState, 0, 0);
            this.tlpMachineStatus.Controls.Add(this.tableLayoutPanel5, 3, 0);
            this.tlpMachineStatus.Controls.Add(this.lblCurrentModel, 2, 0);
            this.tlpMachineStatus.Controls.Add(this.lblMachineName, 1, 0);
            this.tlpMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMachineStatus.Location = new System.Drawing.Point(0, 0);
            this.tlpMachineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMachineStatus.Name = "tlpMachineStatus";
            this.tlpMachineStatus.RowCount = 1;
            this.tlpMachineStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMachineStatus.Size = new System.Drawing.Size(1922, 48);
            this.tlpMachineStatus.TabIndex = 1;
            // 
            // lblDoorlockState
            // 
            this.lblDoorlockState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblDoorlockState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDoorlockState.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoorlockState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblDoorlockState.Location = new System.Drawing.Point(3, 0);
            this.lblDoorlockState.Name = "lblDoorlockState";
            this.lblDoorlockState.Size = new System.Drawing.Size(214, 48);
            this.lblDoorlockState.TabIndex = 5;
            this.lblDoorlockState.Text = "Doorlock Opened";
            this.lblDoorlockState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDoorlockState.Click += new System.EventHandler(this.lblDoorlockState_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.Controls.Add(this.lblCurrentUser, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.pictureBox2, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(1742, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(180, 48);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentUser.Font = new System.Drawing.Font("맑은 고딕", 12.2F, System.Drawing.FontStyle.Bold);
            this.lblCurrentUser.ForeColor = System.Drawing.Color.White;
            this.lblCurrentUser.Location = new System.Drawing.Point(3, 3);
            this.lblCurrentUser.Margin = new System.Windows.Forms.Padding(3);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(128, 42);
            this.lblCurrentUser.TabIndex = 2;
            this.lblCurrentUser.Text = "Maker";
            this.lblCurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCurrentUser.Click += new System.EventHandler(this.lblCurrentUser_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::ATT_UT_Remodeling.Properties.Resources.People;
            this.pictureBox2.Location = new System.Drawing.Point(134, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Padding = new System.Windows.Forms.Padding(6);
            this.pictureBox2.Size = new System.Drawing.Size(46, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.lblCurrentUser_Click);
            // 
            // lblCurrentModel
            // 
            this.lblCurrentModel.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentModel.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lblCurrentModel.ForeColor = System.Drawing.Color.White;
            this.lblCurrentModel.Location = new System.Drawing.Point(1565, 3);
            this.lblCurrentModel.Margin = new System.Windows.Forms.Padding(3);
            this.lblCurrentModel.Name = "lblCurrentModel";
            this.lblCurrentModel.Size = new System.Drawing.Size(174, 42);
            this.lblCurrentModel.TabIndex = 3;
            this.lblCurrentModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMachineName
            // 
            this.lblMachineName.BackColor = System.Drawing.Color.Transparent;
            this.lblMachineName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMachineName.Font = new System.Drawing.Font("맑은 고딕", 20.2F, System.Drawing.FontStyle.Bold);
            this.lblMachineName.ForeColor = System.Drawing.Color.White;
            this.lblMachineName.Location = new System.Drawing.Point(223, 3);
            this.lblMachineName.Margin = new System.Windows.Forms.Padding(3);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(1336, 42);
            this.lblMachineName.TabIndex = 1;
            this.lblMachineName.Text = "ATT Inspection";
            this.lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMachineName.DoubleClick += new System.EventHandler(this.lblMachineName_DoubleClick);
            // 
            // tmrMainForm
            // 
            this.tmrMainForm.Interval = 300;
            this.tmrMainForm.Tick += new System.EventHandler(this.tmrMainForm_Tick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.tableLayoutPanel2.ColumnCount = 11;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Controls.Add(this.lblMotionState, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLightState, 10, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPlcStateText, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPLCState, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLicenseStateText, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLicenseState, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLafStateText, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLafState, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblMotionStateText, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLightStateText, 9, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 781);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1924, 25);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // lblMotionState
            // 
            this.lblMotionState.AutoSize = true;
            this.lblMotionState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMotionState.Image = ((System.Drawing.Image)(resources.GetObject("lblMotionState.Image")));
            this.lblMotionState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMotionState.Location = new System.Drawing.Point(1612, 0);
            this.lblMotionState.Name = "lblMotionState";
            this.lblMotionState.Size = new System.Drawing.Size(19, 25);
            this.lblMotionState.TabIndex = 9;
            this.lblMotionState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLightState
            // 
            this.lblLightState.AutoSize = true;
            this.lblLightState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLightState.Image = ((System.Drawing.Image)(resources.GetObject("lblLightState.Image")));
            this.lblLightState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLightState.Location = new System.Drawing.Point(1902, 0);
            this.lblLightState.Name = "lblLightState";
            this.lblLightState.Size = new System.Drawing.Size(19, 25);
            this.lblLightState.TabIndex = 8;
            this.lblLightState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlcStateText
            // 
            this.lblPlcStateText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlcStateText.Location = new System.Drawing.Point(1447, 0);
            this.lblPlcStateText.Name = "lblPlcStateText";
            this.lblPlcStateText.Size = new System.Drawing.Size(54, 25);
            this.lblPlcStateText.TabIndex = 0;
            this.lblPlcStateText.Text = "PLC";
            this.lblPlcStateText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPLCState
            // 
            this.lblPLCState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPLCState.Image = ((System.Drawing.Image)(resources.GetObject("lblPLCState.Image")));
            this.lblPLCState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPLCState.Location = new System.Drawing.Point(1507, 0);
            this.lblPLCState.Name = "lblPLCState";
            this.lblPLCState.Size = new System.Drawing.Size(19, 25);
            this.lblPLCState.TabIndex = 1;
            this.lblPLCState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLicenseStateText
            // 
            this.lblLicenseStateText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLicenseStateText.Location = new System.Drawing.Point(1637, 0);
            this.lblLicenseStateText.Name = "lblLicenseStateText";
            this.lblLicenseStateText.Size = new System.Drawing.Size(79, 25);
            this.lblLicenseStateText.TabIndex = 2;
            this.lblLicenseStateText.Text = "Cognex";
            this.lblLicenseStateText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLicenseStateText.Visible = false;
            // 
            // lblLicenseState
            // 
            this.lblLicenseState.AutoSize = true;
            this.lblLicenseState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLicenseState.Image = ((System.Drawing.Image)(resources.GetObject("lblLicenseState.Image")));
            this.lblLicenseState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLicenseState.Location = new System.Drawing.Point(1722, 0);
            this.lblLicenseState.Name = "lblLicenseState";
            this.lblLicenseState.Size = new System.Drawing.Size(19, 25);
            this.lblLicenseState.TabIndex = 3;
            this.lblLicenseState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLicenseState.Visible = false;
            // 
            // lblLafStateText
            // 
            this.lblLafStateText.AutoSize = true;
            this.lblLafStateText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLafStateText.Location = new System.Drawing.Point(1747, 0);
            this.lblLafStateText.Name = "lblLafStateText";
            this.lblLafStateText.Size = new System.Drawing.Size(54, 25);
            this.lblLafStateText.TabIndex = 4;
            this.lblLafStateText.Text = "LAF";
            this.lblLafStateText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLafState
            // 
            this.lblLafState.AutoSize = true;
            this.lblLafState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLafState.Image = ((System.Drawing.Image)(resources.GetObject("lblLafState.Image")));
            this.lblLafState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLafState.Location = new System.Drawing.Point(1807, 0);
            this.lblLafState.Name = "lblLafState";
            this.lblLafState.Size = new System.Drawing.Size(19, 25);
            this.lblLafState.TabIndex = 5;
            this.lblLafState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMotionStateText
            // 
            this.lblMotionStateText.AutoSize = true;
            this.lblMotionStateText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMotionStateText.Location = new System.Drawing.Point(1532, 0);
            this.lblMotionStateText.Name = "lblMotionStateText";
            this.lblMotionStateText.Size = new System.Drawing.Size(74, 25);
            this.lblMotionStateText.TabIndex = 6;
            this.lblMotionStateText.Text = "Motion";
            this.lblMotionStateText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLightStateText
            // 
            this.lblLightStateText.AutoSize = true;
            this.lblLightStateText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLightStateText.Location = new System.Drawing.Point(1832, 0);
            this.lblLightStateText.Name = "lblLightStateText";
            this.lblLightStateText.Size = new System.Drawing.Size(64, 25);
            this.lblLightStateText.TabIndex = 7;
            this.lblLightStateText.Text = "Light";
            this.lblLightStateText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrUpdateStates
            // 
            this.tmrUpdateStates.Interval = 300;
            this.tmrUpdateStates.Tick += new System.EventHandler(this.tmrUpdateStates_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1924, 806);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tlpMainForm);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tlpMainForm.ResumeLayout(false);
            this.tlpFunctionButtons.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.pnlMachineStatus.ResumeLayout(false);
            this.tlpMachineStatus.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainForm;
        private System.Windows.Forms.Panel pnlPage;
        private System.Windows.Forms.TableLayoutPanel tlpFunctionButtons;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblLogPageImage;
        private System.Windows.Forms.Label lblLogPage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label lblDataPageImage;
        private System.Windows.Forms.Label lblDataPage;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblInspectionPageImage;
        private System.Windows.Forms.Label lblMainPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblTeachingPageImage;
        private System.Windows.Forms.Label lblTeachingPage;
        private System.Windows.Forms.Panel pnlMachineStatus;
        private System.Windows.Forms.TableLayoutPanel tlpMachineStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblCurrentModel;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Timer tmrMainForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblMotionState;
        private System.Windows.Forms.Label lblLightState;
        private System.Windows.Forms.Label lblPlcStateText;
        private System.Windows.Forms.Label lblPLCState;
        private System.Windows.Forms.Label lblLicenseStateText;
        private System.Windows.Forms.Label lblLicenseState;
        private System.Windows.Forms.Label lblLafStateText;
        private System.Windows.Forms.Label lblLafState;
        private System.Windows.Forms.Label lblMotionStateText;
        private System.Windows.Forms.Label lblLightStateText;
        private System.Windows.Forms.Timer tmrUpdateStates;
        private System.Windows.Forms.Label lblDoorlockState;
    }
}

