namespace Jastech.Apps.Winform.UI.Forms
{
    partial class ManualMatchingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualMatchingForm));
            this.pnlManualMatching = new System.Windows.Forms.Panel();
            this.tlpManualMatching = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTop = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.pnlApply = new System.Windows.Forms.Panel();
            this.tlpApply = new System.Windows.Forms.TableLayoutPanel();
            this.lblApplyImage = new System.Windows.Forms.Label();
            this.lblApply = new System.Windows.Forms.Label();
            this.pnlCancel = new System.Windows.Forms.Panel();
            this.tlpCancel = new System.Windows.Forms.TableLayoutPanel();
            this.lblCancelImage = new System.Windows.Forms.Label();
            this.lblCancel = new System.Windows.Forms.Label();
            this.tlpManualMatch = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.pnlTeach = new System.Windows.Forms.Panel();
            this.pnlPatternImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cogPatternDisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.pnlROIJog = new System.Windows.Forms.Panel();
            this.tlpROIJog = new System.Windows.Forms.TableLayoutPanel();
            this.tlpJog = new System.Windows.Forms.TableLayoutPanel();
            this.lblMoveRight = new System.Windows.Forms.Label();
            this.lblMoveUp = new System.Windows.Forms.Label();
            this.lblMoveLeft = new System.Windows.Forms.Label();
            this.lblMoveDown = new System.Windows.Forms.Label();
            this.tlpJogMode = new System.Windows.Forms.TableLayoutPanel();
            this.lblPitch = new System.Windows.Forms.Label();
            this.lblMovePixel = new System.Windows.Forms.Label();
            this.pnlLight = new System.Windows.Forms.Panel();
            this.pnlManualMatching.SuspendLayout();
            this.tlpManualMatching.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.tlpBottom.SuspendLayout();
            this.pnlApply.SuspendLayout();
            this.tlpApply.SuspendLayout();
            this.pnlCancel.SuspendLayout();
            this.tlpCancel.SuspendLayout();
            this.tlpManualMatch.SuspendLayout();
            this.pnlTeach.SuspendLayout();
            this.pnlPatternImage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogPatternDisplay)).BeginInit();
            this.pnlROIJog.SuspendLayout();
            this.tlpROIJog.SuspendLayout();
            this.tlpJog.SuspendLayout();
            this.tlpJogMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlManualMatching
            // 
            this.pnlManualMatching.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlManualMatching.Controls.Add(this.tlpManualMatching);
            this.pnlManualMatching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManualMatching.Location = new System.Drawing.Point(0, 0);
            this.pnlManualMatching.Margin = new System.Windows.Forms.Padding(0);
            this.pnlManualMatching.Name = "pnlManualMatching";
            this.pnlManualMatching.Size = new System.Drawing.Size(1061, 650);
            this.pnlManualMatching.TabIndex = 293;
            // 
            // tlpManualMatching
            // 
            this.tlpManualMatching.ColumnCount = 1;
            this.tlpManualMatching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpManualMatching.Controls.Add(this.pnlTop, 0, 0);
            this.tlpManualMatching.Controls.Add(this.pnlBottom, 0, 2);
            this.tlpManualMatching.Controls.Add(this.tlpManualMatch, 0, 1);
            this.tlpManualMatching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpManualMatching.Location = new System.Drawing.Point(0, 0);
            this.tlpManualMatching.Margin = new System.Windows.Forms.Padding(0);
            this.tlpManualMatching.Name = "tlpManualMatching";
            this.tlpManualMatching.RowCount = 3;
            this.tlpManualMatching.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpManualMatching.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpManualMatching.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpManualMatching.Size = new System.Drawing.Size(1059, 648);
            this.tlpManualMatching.TabIndex = 291;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.pnlTop.Controls.Add(this.lblTop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1059, 60);
            this.pnlTop.TabIndex = 1;
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTop.ForeColor = System.Drawing.Color.White;
            this.lblTop.Location = new System.Drawing.Point(12, 14);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(188, 30);
            this.lblTop.TabIndex = 1;
            this.lblTop.Text = "Manual Matching";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.tlpBottom);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 588);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1059, 60);
            this.pnlBottom.TabIndex = 4;
            // 
            // tlpBottom
            // 
            this.tlpBottom.ColumnCount = 3;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpBottom.Controls.Add(this.pnlApply, 0, 0);
            this.tlpBottom.Controls.Add(this.pnlCancel, 2, 0);
            this.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBottom.Location = new System.Drawing.Point(0, 0);
            this.tlpBottom.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.Size = new System.Drawing.Size(1057, 58);
            this.tlpBottom.TabIndex = 2;
            // 
            // pnlApply
            // 
            this.pnlApply.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlApply.Controls.Add(this.tlpApply);
            this.pnlApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlApply.Location = new System.Drawing.Point(0, 0);
            this.pnlApply.Margin = new System.Windows.Forms.Padding(0);
            this.pnlApply.Name = "pnlApply";
            this.pnlApply.Size = new System.Drawing.Size(160, 58);
            this.pnlApply.TabIndex = 3;
            // 
            // tlpApply
            // 
            this.tlpApply.ColumnCount = 2;
            this.tlpApply.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpApply.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpApply.Controls.Add(this.lblApplyImage, 0, 0);
            this.tlpApply.Controls.Add(this.lblApply, 1, 0);
            this.tlpApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpApply.Location = new System.Drawing.Point(0, 0);
            this.tlpApply.Margin = new System.Windows.Forms.Padding(0);
            this.tlpApply.Name = "tlpApply";
            this.tlpApply.RowCount = 1;
            this.tlpApply.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpApply.Size = new System.Drawing.Size(158, 56);
            this.tlpApply.TabIndex = 1;
            // 
            // lblApplyImage
            // 
            this.lblApplyImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApplyImage.Image = global::Jastech.Apps.Winform.Properties.Resources.Select_White;
            this.lblApplyImage.Location = new System.Drawing.Point(3, 0);
            this.lblApplyImage.Name = "lblApplyImage";
            this.lblApplyImage.Size = new System.Drawing.Size(44, 56);
            this.lblApplyImage.TabIndex = 1;
            this.lblApplyImage.Click += new System.EventHandler(this.lblApply_Click);
            // 
            // lblApply
            // 
            this.lblApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApply.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblApply.ForeColor = System.Drawing.Color.White;
            this.lblApply.Location = new System.Drawing.Point(53, 0);
            this.lblApply.Name = "lblApply";
            this.lblApply.Size = new System.Drawing.Size(102, 56);
            this.lblApply.TabIndex = 0;
            this.lblApply.Text = "Apply";
            this.lblApply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblApply.Click += new System.EventHandler(this.lblApply_Click);
            // 
            // pnlCancel
            // 
            this.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCancel.Controls.Add(this.tlpCancel);
            this.pnlCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCancel.Location = new System.Drawing.Point(897, 0);
            this.pnlCancel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCancel.Name = "pnlCancel";
            this.pnlCancel.Size = new System.Drawing.Size(160, 58);
            this.pnlCancel.TabIndex = 3;
            // 
            // tlpCancel
            // 
            this.tlpCancel.ColumnCount = 2;
            this.tlpCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCancel.Controls.Add(this.lblCancelImage, 0, 0);
            this.tlpCancel.Controls.Add(this.lblCancel, 1, 0);
            this.tlpCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCancel.Location = new System.Drawing.Point(0, 0);
            this.tlpCancel.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCancel.Name = "tlpCancel";
            this.tlpCancel.RowCount = 1;
            this.tlpCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCancel.Size = new System.Drawing.Size(158, 56);
            this.tlpCancel.TabIndex = 2;
            // 
            // lblCancelImage
            // 
            this.lblCancelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCancelImage.Image = global::Jastech.Apps.Winform.Properties.Resources.Cancel_White;
            this.lblCancelImage.Location = new System.Drawing.Point(3, 0);
            this.lblCancelImage.Name = "lblCancelImage";
            this.lblCancelImage.Size = new System.Drawing.Size(44, 56);
            this.lblCancelImage.TabIndex = 1;
            this.lblCancelImage.Click += new System.EventHandler(this.lblCancel_Click);
            // 
            // lblCancel
            // 
            this.lblCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCancel.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblCancel.ForeColor = System.Drawing.Color.White;
            this.lblCancel.Location = new System.Drawing.Point(53, 0);
            this.lblCancel.Name = "lblCancel";
            this.lblCancel.Size = new System.Drawing.Size(102, 56);
            this.lblCancel.TabIndex = 0;
            this.lblCancel.Text = "Cancel";
            this.lblCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancel.Click += new System.EventHandler(this.lblCancel_Click);
            // 
            // tlpManualMatch
            // 
            this.tlpManualMatch.ColumnCount = 2;
            this.tlpManualMatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpManualMatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpManualMatch.Controls.Add(this.pnlDisplay, 0, 0);
            this.tlpManualMatch.Controls.Add(this.pnlTeach, 1, 0);
            this.tlpManualMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpManualMatch.Location = new System.Drawing.Point(0, 60);
            this.tlpManualMatch.Margin = new System.Windows.Forms.Padding(0);
            this.tlpManualMatch.Name = "tlpManualMatch";
            this.tlpManualMatch.RowCount = 1;
            this.tlpManualMatch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpManualMatch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 434F));
            this.tlpManualMatch.Size = new System.Drawing.Size(1059, 528);
            this.tlpManualMatch.TabIndex = 5;
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(0, 0);
            this.pnlDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(529, 528);
            this.pnlDisplay.TabIndex = 0;
            // 
            // pnlTeach
            // 
            this.pnlTeach.Controls.Add(this.pnlPatternImage);
            this.pnlTeach.Controls.Add(this.pnlROIJog);
            this.pnlTeach.Controls.Add(this.pnlLight);
            this.pnlTeach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeach.Location = new System.Drawing.Point(529, 0);
            this.pnlTeach.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeach.Name = "pnlTeach";
            this.pnlTeach.Size = new System.Drawing.Size(530, 528);
            this.pnlTeach.TabIndex = 1;
            // 
            // pnlPatternImage
            // 
            this.pnlPatternImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPatternImage.Controls.Add(this.tableLayoutPanel1);
            this.pnlPatternImage.Location = new System.Drawing.Point(257, 27);
            this.pnlPatternImage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPatternImage.Name = "pnlPatternImage";
            this.pnlPatternImage.Size = new System.Drawing.Size(240, 240);
            this.pnlPatternImage.TabIndex = 302;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cogPatternDisplay, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(238, 238);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Trained Image";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cogPatternDisplay
            // 
            this.cogPatternDisplay.ColorMapLowerClipColor = System.Drawing.SystemColors.AppWorkspace;
            this.cogPatternDisplay.ColorMapLowerRoiLimit = 0D;
            this.cogPatternDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogPatternDisplay.ColorMapUpperClipColor = System.Drawing.SystemColors.AppWorkspace;
            this.cogPatternDisplay.ColorMapUpperRoiLimit = 1D;
            this.cogPatternDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogPatternDisplay.DoubleTapZoomCycleLength = 2;
            this.cogPatternDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.cogPatternDisplay.Location = new System.Drawing.Point(3, 43);
            this.cogPatternDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.None;
            this.cogPatternDisplay.MouseWheelSensitivity = 1D;
            this.cogPatternDisplay.Name = "cogPatternDisplay";
            this.cogPatternDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogPatternDisplay.OcxState")));
            this.cogPatternDisplay.Size = new System.Drawing.Size(232, 192);
            this.cogPatternDisplay.TabIndex = 1;
            // 
            // pnlROIJog
            // 
            this.pnlROIJog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlROIJog.Controls.Add(this.tlpROIJog);
            this.pnlROIJog.Location = new System.Drawing.Point(26, 27);
            this.pnlROIJog.Margin = new System.Windows.Forms.Padding(0);
            this.pnlROIJog.Name = "pnlROIJog";
            this.pnlROIJog.Size = new System.Drawing.Size(210, 240);
            this.pnlROIJog.TabIndex = 301;
            // 
            // tlpROIJog
            // 
            this.tlpROIJog.ColumnCount = 1;
            this.tlpROIJog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpROIJog.Controls.Add(this.tlpJog, 0, 1);
            this.tlpROIJog.Controls.Add(this.tlpJogMode, 0, 0);
            this.tlpROIJog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpROIJog.Location = new System.Drawing.Point(0, 0);
            this.tlpROIJog.Margin = new System.Windows.Forms.Padding(0);
            this.tlpROIJog.Name = "tlpROIJog";
            this.tlpROIJog.RowCount = 2;
            this.tlpROIJog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpROIJog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpROIJog.Size = new System.Drawing.Size(208, 238);
            this.tlpROIJog.TabIndex = 0;
            // 
            // tlpJog
            // 
            this.tlpJog.ColumnCount = 3;
            this.tlpJog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpJog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpJog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpJog.Controls.Add(this.lblMoveRight, 2, 1);
            this.tlpJog.Controls.Add(this.lblMoveUp, 1, 0);
            this.tlpJog.Controls.Add(this.lblMoveLeft, 0, 1);
            this.tlpJog.Controls.Add(this.lblMoveDown, 1, 2);
            this.tlpJog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpJog.Location = new System.Drawing.Point(0, 40);
            this.tlpJog.Margin = new System.Windows.Forms.Padding(0);
            this.tlpJog.Name = "tlpJog";
            this.tlpJog.RowCount = 3;
            this.tlpJog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpJog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpJog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpJog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpJog.Size = new System.Drawing.Size(208, 198);
            this.tlpJog.TabIndex = 1;
            // 
            // lblMoveRight
            // 
            this.lblMoveRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMoveRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMoveRight.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMoveRight.Image = ((System.Drawing.Image)(resources.GetObject("lblMoveRight.Image")));
            this.lblMoveRight.Location = new System.Drawing.Point(138, 66);
            this.lblMoveRight.Margin = new System.Windows.Forms.Padding(0);
            this.lblMoveRight.Name = "lblMoveRight";
            this.lblMoveRight.Size = new System.Drawing.Size(70, 66);
            this.lblMoveRight.TabIndex = 10;
            this.lblMoveRight.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblMoveRight.Click += new System.EventHandler(this.lblMoveRight_Click);
            // 
            // lblMoveUp
            // 
            this.lblMoveUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMoveUp.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("lblMoveUp.Image")));
            this.lblMoveUp.Location = new System.Drawing.Point(69, 0);
            this.lblMoveUp.Margin = new System.Windows.Forms.Padding(0);
            this.lblMoveUp.Name = "lblMoveUp";
            this.lblMoveUp.Size = new System.Drawing.Size(69, 66);
            this.lblMoveUp.TabIndex = 10;
            this.lblMoveUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblMoveUp.Click += new System.EventHandler(this.lblMoveUp_Click);
            // 
            // lblMoveLeft
            // 
            this.lblMoveLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMoveLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMoveLeft.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMoveLeft.Image = ((System.Drawing.Image)(resources.GetObject("lblMoveLeft.Image")));
            this.lblMoveLeft.Location = new System.Drawing.Point(0, 66);
            this.lblMoveLeft.Margin = new System.Windows.Forms.Padding(0);
            this.lblMoveLeft.Name = "lblMoveLeft";
            this.lblMoveLeft.Size = new System.Drawing.Size(69, 66);
            this.lblMoveLeft.TabIndex = 10;
            this.lblMoveLeft.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblMoveLeft.Click += new System.EventHandler(this.lblMoveLeft_Click);
            // 
            // lblMoveDown
            // 
            this.lblMoveDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMoveDown.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("lblMoveDown.Image")));
            this.lblMoveDown.Location = new System.Drawing.Point(69, 132);
            this.lblMoveDown.Margin = new System.Windows.Forms.Padding(0);
            this.lblMoveDown.Name = "lblMoveDown";
            this.lblMoveDown.Size = new System.Drawing.Size(69, 66);
            this.lblMoveDown.TabIndex = 10;
            this.lblMoveDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblMoveDown.Click += new System.EventHandler(this.lblMoveDown_Click);
            // 
            // tlpJogMode
            // 
            this.tlpJogMode.ColumnCount = 2;
            this.tlpJogMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJogMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJogMode.Controls.Add(this.lblPitch, 0, 0);
            this.tlpJogMode.Controls.Add(this.lblMovePixel, 0, 0);
            this.tlpJogMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpJogMode.Location = new System.Drawing.Point(0, 0);
            this.tlpJogMode.Margin = new System.Windows.Forms.Padding(0);
            this.tlpJogMode.Name = "tlpJogMode";
            this.tlpJogMode.RowCount = 1;
            this.tlpJogMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJogMode.Size = new System.Drawing.Size(208, 40);
            this.tlpJogMode.TabIndex = 2;
            // 
            // lblPitch
            // 
            this.lblPitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblPitch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPitch.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblPitch.ForeColor = System.Drawing.Color.White;
            this.lblPitch.Location = new System.Drawing.Point(104, 0);
            this.lblPitch.Margin = new System.Windows.Forms.Padding(0);
            this.lblPitch.Name = "lblPitch";
            this.lblPitch.Size = new System.Drawing.Size(104, 40);
            this.lblPitch.TabIndex = 9;
            this.lblPitch.Text = "1";
            this.lblPitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPitch.Click += new System.EventHandler(this.lblPitch_Click);
            // 
            // lblMovePixel
            // 
            this.lblMovePixel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblMovePixel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMovePixel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMovePixel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMovePixel.ForeColor = System.Drawing.Color.White;
            this.lblMovePixel.Location = new System.Drawing.Point(0, 0);
            this.lblMovePixel.Margin = new System.Windows.Forms.Padding(0);
            this.lblMovePixel.Name = "lblMovePixel";
            this.lblMovePixel.Size = new System.Drawing.Size(104, 40);
            this.lblMovePixel.TabIndex = 8;
            this.lblMovePixel.Text = "Move Pixel";
            this.lblMovePixel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLight
            // 
            this.pnlLight.Location = new System.Drawing.Point(26, 294);
            this.pnlLight.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLight.Name = "pnlLight";
            this.pnlLight.Size = new System.Drawing.Size(471, 194);
            this.pnlLight.TabIndex = 300;
            // 
            // ManualMatchingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1061, 650);
            this.Controls.Add(this.pnlManualMatching);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManualMatchingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManualMatchingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManualMatchingForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManualMatchingForm_FormClosed);
            this.Load += new System.EventHandler(this.ManualMatchingForm_Load);
            this.pnlManualMatching.ResumeLayout(false);
            this.tlpManualMatching.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.tlpBottom.ResumeLayout(false);
            this.pnlApply.ResumeLayout(false);
            this.tlpApply.ResumeLayout(false);
            this.pnlCancel.ResumeLayout(false);
            this.tlpCancel.ResumeLayout(false);
            this.tlpManualMatch.ResumeLayout(false);
            this.pnlTeach.ResumeLayout(false);
            this.pnlPatternImage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogPatternDisplay)).EndInit();
            this.pnlROIJog.ResumeLayout(false);
            this.tlpROIJog.ResumeLayout(false);
            this.tlpJog.ResumeLayout(false);
            this.tlpJogMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlManualMatching;
        private System.Windows.Forms.TableLayoutPanel tlpManualMatching;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.Panel pnlApply;
        private System.Windows.Forms.TableLayoutPanel tlpApply;
        private System.Windows.Forms.Label lblApplyImage;
        private System.Windows.Forms.Label lblApply;
        private System.Windows.Forms.Panel pnlCancel;
        private System.Windows.Forms.TableLayoutPanel tlpCancel;
        private System.Windows.Forms.Label lblCancelImage;
        private System.Windows.Forms.Label lblCancel;
        private System.Windows.Forms.TableLayoutPanel tlpManualMatch;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Panel pnlTeach;
        private System.Windows.Forms.Panel pnlLight;
        private System.Windows.Forms.Panel pnlROIJog;
        private System.Windows.Forms.TableLayoutPanel tlpJog;
        private System.Windows.Forms.Label lblMoveRight;
        private System.Windows.Forms.Label lblMoveUp;
        private System.Windows.Forms.Label lblMoveLeft;
        private System.Windows.Forms.Label lblMoveDown;
        private System.Windows.Forms.TableLayoutPanel tlpROIJog;
        private System.Windows.Forms.TableLayoutPanel tlpJogMode;
        private System.Windows.Forms.Label lblMovePixel;
        private System.Windows.Forms.Label lblPitch;
        private System.Windows.Forms.Panel pnlPatternImage;
        private Cognex.VisionPro.CogRecordDisplay cogPatternDisplay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
    }
}