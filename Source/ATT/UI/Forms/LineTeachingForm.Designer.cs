﻿namespace ATT.UI.Forms
{
    partial class LineTeachingForm
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpTeachingPage = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTeachingPage = new System.Windows.Forms.Panel();
            this.tlpTeaching = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpCommon = new System.Windows.Forms.TableLayoutPanel();
            this.tlpUnit = new System.Windows.Forms.TableLayoutPanel();
            this.btnMotionPopup = new System.Windows.Forms.Button();
            this.lblStageCam = new System.Windows.Forms.Label();
            this.tlpLoadImage = new System.Windows.Forms.TableLayoutPanel();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnGrabStop = new System.Windows.Forms.Button();
            this.btnGrabStart = new System.Windows.Forms.Button();
            this.pnlTeach = new System.Windows.Forms.Panel();
            this.pnlTeachingItem = new System.Windows.Forms.Panel();
            this.tlpTeachingItem = new System.Windows.Forms.TableLayoutPanel();
            this.btnAutoFocus = new System.Windows.Forms.Button();
            this.btnAkkon = new System.Windows.Forms.Button();
            this.btnAlign = new System.Windows.Forms.Button();
            this.btnMark = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tlpTeachingPage.SuspendLayout();
            this.pnlTeachingPage.SuspendLayout();
            this.tlpTeaching.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpCommon.SuspendLayout();
            this.tlpUnit.SuspendLayout();
            this.tlpLoadImage.SuspendLayout();
            this.pnlTeachingItem.SuspendLayout();
            this.tlpTeachingItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTeachingPage
            // 
            this.tlpTeachingPage.ColumnCount = 2;
            this.tlpTeachingPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tlpTeachingPage.Controls.Add(this.pnlTeachingPage, 0, 0);
            this.tlpTeachingPage.Controls.Add(this.pnlTeachingItem, 1, 0);
            this.tlpTeachingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeachingPage.Location = new System.Drawing.Point(0, 0);
            this.tlpTeachingPage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeachingPage.Name = "tlpTeachingPage";
            this.tlpTeachingPage.RowCount = 1;
            this.tlpTeachingPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingPage.Size = new System.Drawing.Size(1229, 779);
            this.tlpTeachingPage.TabIndex = 1;
            // 
            // pnlTeachingPage
            // 
            this.pnlTeachingPage.Controls.Add(this.tlpTeaching);
            this.pnlTeachingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingPage.Location = new System.Drawing.Point(0, 0);
            this.pnlTeachingPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingPage.Name = "pnlTeachingPage";
            this.pnlTeachingPage.Size = new System.Drawing.Size(1085, 779);
            this.pnlTeachingPage.TabIndex = 0;
            // 
            // tlpTeaching
            // 
            this.tlpTeaching.ColumnCount = 2;
            this.tlpTeaching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.Controls.Add(this.pnlDisplay, 0, 0);
            this.tlpTeaching.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tlpTeaching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeaching.Location = new System.Drawing.Point(0, 0);
            this.tlpTeaching.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeaching.Name = "tlpTeaching";
            this.tlpTeaching.RowCount = 1;
            this.tlpTeaching.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.Size = new System.Drawing.Size(1085, 779);
            this.tlpTeaching.TabIndex = 0;
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(0, 0);
            this.pnlDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(542, 779);
            this.pnlDisplay.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tlpCommon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlTeach, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(544, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(539, 775);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tlpCommon
            // 
            this.tlpCommon.ColumnCount = 1;
            this.tlpCommon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCommon.Controls.Add(this.tlpUnit, 0, 0);
            this.tlpCommon.Controls.Add(this.tlpLoadImage, 0, 1);
            this.tlpCommon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCommon.Location = new System.Drawing.Point(0, 0);
            this.tlpCommon.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCommon.Name = "tlpCommon";
            this.tlpCommon.RowCount = 2;
            this.tlpCommon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCommon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCommon.Size = new System.Drawing.Size(539, 80);
            this.tlpCommon.TabIndex = 3;
            // 
            // tlpUnit
            // 
            this.tlpUnit.ColumnCount = 2;
            this.tlpUnit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66F));
            this.tlpUnit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tlpUnit.Controls.Add(this.btnMotionPopup, 0, 0);
            this.tlpUnit.Controls.Add(this.lblStageCam, 0, 0);
            this.tlpUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUnit.Location = new System.Drawing.Point(0, 0);
            this.tlpUnit.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUnit.Name = "tlpUnit";
            this.tlpUnit.RowCount = 1;
            this.tlpUnit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnit.Size = new System.Drawing.Size(539, 40);
            this.tlpUnit.TabIndex = 287;
            // 
            // btnMotionPopup
            // 
            this.btnMotionPopup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnMotionPopup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMotionPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMotionPopup.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnMotionPopup.ForeColor = System.Drawing.Color.White;
            this.btnMotionPopup.Location = new System.Drawing.Point(359, 0);
            this.btnMotionPopup.Margin = new System.Windows.Forms.Padding(0);
            this.btnMotionPopup.Name = "btnMotionPopup";
            this.btnMotionPopup.Size = new System.Drawing.Size(180, 40);
            this.btnMotionPopup.TabIndex = 295;
            this.btnMotionPopup.Text = "MOTION";
            this.btnMotionPopup.UseVisualStyleBackColor = false;
            this.btnMotionPopup.Click += new System.EventHandler(this.btnMotionPopup_Click);
            // 
            // lblStageCam
            // 
            this.lblStageCam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblStageCam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStageCam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStageCam.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblStageCam.ForeColor = System.Drawing.Color.White;
            this.lblStageCam.Location = new System.Drawing.Point(0, 0);
            this.lblStageCam.Margin = new System.Windows.Forms.Padding(0);
            this.lblStageCam.Name = "lblStageCam";
            this.lblStageCam.Size = new System.Drawing.Size(359, 40);
            this.lblStageCam.TabIndex = 294;
            this.lblStageCam.Text = "STAGE : / CAM :";
            this.lblStageCam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpLoadImage
            // 
            this.tlpLoadImage.ColumnCount = 3;
            this.tlpLoadImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpLoadImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpLoadImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpLoadImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpLoadImage.Controls.Add(this.btnLoadImage, 0, 0);
            this.tlpLoadImage.Controls.Add(this.btnGrabStop, 0, 0);
            this.tlpLoadImage.Controls.Add(this.btnGrabStart, 0, 0);
            this.tlpLoadImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLoadImage.Location = new System.Drawing.Point(0, 40);
            this.tlpLoadImage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLoadImage.Name = "tlpLoadImage";
            this.tlpLoadImage.RowCount = 1;
            this.tlpLoadImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLoadImage.Size = new System.Drawing.Size(539, 40);
            this.tlpLoadImage.TabIndex = 2;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnLoadImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLoadImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadImage.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnLoadImage.ForeColor = System.Drawing.Color.White;
            this.btnLoadImage.Location = new System.Drawing.Point(358, 0);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(0);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(181, 40);
            this.btnLoadImage.TabIndex = 201;
            this.btnLoadImage.Text = "LOAD IMAGE";
            this.btnLoadImage.UseVisualStyleBackColor = false;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnGrabStop
            // 
            this.btnGrabStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnGrabStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGrabStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrabStop.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnGrabStop.ForeColor = System.Drawing.Color.White;
            this.btnGrabStop.Location = new System.Drawing.Point(179, 0);
            this.btnGrabStop.Margin = new System.Windows.Forms.Padding(0);
            this.btnGrabStop.Name = "btnGrabStop";
            this.btnGrabStop.Size = new System.Drawing.Size(179, 40);
            this.btnGrabStop.TabIndex = 200;
            this.btnGrabStop.Text = "GRAB STOP";
            this.btnGrabStop.UseVisualStyleBackColor = false;
            this.btnGrabStop.Click += new System.EventHandler(this.btnGrabStop_Click);
            // 
            // btnGrabStart
            // 
            this.btnGrabStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnGrabStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGrabStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrabStart.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnGrabStart.ForeColor = System.Drawing.Color.White;
            this.btnGrabStart.Location = new System.Drawing.Point(0, 0);
            this.btnGrabStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnGrabStart.Name = "btnGrabStart";
            this.btnGrabStart.Size = new System.Drawing.Size(179, 40);
            this.btnGrabStart.TabIndex = 199;
            this.btnGrabStart.Text = "GRAB START";
            this.btnGrabStart.UseVisualStyleBackColor = false;
            this.btnGrabStart.Click += new System.EventHandler(this.btnGrabStart_Click);
            // 
            // pnlTeach
            // 
            this.pnlTeach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeach.Location = new System.Drawing.Point(0, 80);
            this.pnlTeach.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeach.Name = "pnlTeach";
            this.pnlTeach.Size = new System.Drawing.Size(539, 695);
            this.pnlTeach.TabIndex = 0;
            // 
            // pnlTeachingItem
            // 
            this.pnlTeachingItem.Controls.Add(this.tlpTeachingItem);
            this.pnlTeachingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingItem.Location = new System.Drawing.Point(1085, 0);
            this.pnlTeachingItem.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingItem.Name = "pnlTeachingItem";
            this.pnlTeachingItem.Size = new System.Drawing.Size(144, 779);
            this.pnlTeachingItem.TabIndex = 1;
            // 
            // tlpTeachingItem
            // 
            this.tlpTeachingItem.ColumnCount = 1;
            this.tlpTeachingItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingItem.Controls.Add(this.btnAutoFocus, 0, 0);
            this.tlpTeachingItem.Controls.Add(this.btnAkkon, 0, 3);
            this.tlpTeachingItem.Controls.Add(this.btnAlign, 0, 2);
            this.tlpTeachingItem.Controls.Add(this.btnMark, 0, 1);
            this.tlpTeachingItem.Controls.Add(this.btnCancel, 0, 7);
            this.tlpTeachingItem.Controls.Add(this.btnSave, 0, 6);
            this.tlpTeachingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeachingItem.Location = new System.Drawing.Point(0, 0);
            this.tlpTeachingItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeachingItem.Name = "tlpTeachingItem";
            this.tlpTeachingItem.RowCount = 8;
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.Size = new System.Drawing.Size(144, 779);
            this.tlpTeachingItem.TabIndex = 0;
            // 
            // btnAutoFocus
            // 
            this.btnAutoFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnAutoFocus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAutoFocus.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnAutoFocus.ForeColor = System.Drawing.Color.White;
            this.btnAutoFocus.Location = new System.Drawing.Point(2, 2);
            this.btnAutoFocus.Margin = new System.Windows.Forms.Padding(2);
            this.btnAutoFocus.Name = "btnAutoFocus";
            this.btnAutoFocus.Size = new System.Drawing.Size(140, 96);
            this.btnAutoFocus.TabIndex = 19;
            this.btnAutoFocus.Text = "Linescan";
            this.btnAutoFocus.UseVisualStyleBackColor = false;
            this.btnAutoFocus.Click += new System.EventHandler(this.btnAutoFocus_Click);
            // 
            // btnAkkon
            // 
            this.btnAkkon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnAkkon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAkkon.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnAkkon.ForeColor = System.Drawing.Color.White;
            this.btnAkkon.Location = new System.Drawing.Point(2, 302);
            this.btnAkkon.Margin = new System.Windows.Forms.Padding(2);
            this.btnAkkon.Name = "btnAkkon";
            this.btnAkkon.Size = new System.Drawing.Size(140, 96);
            this.btnAkkon.TabIndex = 19;
            this.btnAkkon.Text = "Akkon";
            this.btnAkkon.UseVisualStyleBackColor = false;
            this.btnAkkon.Click += new System.EventHandler(this.btnAkkon_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAlign.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnAlign.ForeColor = System.Drawing.Color.White;
            this.btnAlign.Location = new System.Drawing.Point(2, 202);
            this.btnAlign.Margin = new System.Windows.Forms.Padding(2);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(140, 96);
            this.btnAlign.TabIndex = 19;
            this.btnAlign.Text = "Align";
            this.btnAlign.UseVisualStyleBackColor = false;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // btnMark
            // 
            this.btnMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnMark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMark.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnMark.ForeColor = System.Drawing.Color.White;
            this.btnMark.Location = new System.Drawing.Point(2, 102);
            this.btnMark.Margin = new System.Windows.Forms.Padding(2);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(140, 96);
            this.btnMark.TabIndex = 19;
            this.btnMark.Text = "Mark";
            this.btnMark.UseVisualStyleBackColor = false;
            this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(2, 681);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 96);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(2, 581);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 96);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // LineTeachingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1229, 779);
            this.Controls.Add(this.tlpTeachingPage);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LineTeachingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LineTeachingForm_FormClosing);
            this.Load += new System.EventHandler(this.LineTeachingForm_Load);
            this.tlpTeachingPage.ResumeLayout(false);
            this.pnlTeachingPage.ResumeLayout(false);
            this.tlpTeaching.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlpCommon.ResumeLayout(false);
            this.tlpUnit.ResumeLayout(false);
            this.tlpLoadImage.ResumeLayout(false);
            this.pnlTeachingItem.ResumeLayout(false);
            this.tlpTeachingItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpTeachingPage;
        private System.Windows.Forms.Panel pnlTeachingPage;
        private System.Windows.Forms.TableLayoutPanel tlpTeaching;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlTeach;
        private System.Windows.Forms.Panel pnlTeachingItem;
        private System.Windows.Forms.TableLayoutPanel tlpTeachingItem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAutoFocus;
        private System.Windows.Forms.Button btnAlign;
        private System.Windows.Forms.Button btnAkkon;
        private System.Windows.Forms.Button btnMark;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tlpCommon;
        private System.Windows.Forms.TableLayoutPanel tlpUnit;
        private System.Windows.Forms.Button btnMotionPopup;
        private System.Windows.Forms.Label lblStageCam;
        private System.Windows.Forms.TableLayoutPanel tlpLoadImage;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnGrabStop;
        private System.Windows.Forms.Button btnGrabStart;
    }
}