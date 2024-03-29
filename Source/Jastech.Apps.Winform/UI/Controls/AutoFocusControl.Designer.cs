﻿namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AutoFocusControl
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
            this.tlpAutoFocusControl = new System.Windows.Forms.TableLayoutPanel();
            this.lblTrackingOnOff = new System.Windows.Forms.Label();
            this.lblTargetPositionValue = new System.Windows.Forms.Label();
            this.lblTargetPosition = new System.Windows.Forms.Label();
            this.lblCurrentCogValue = new System.Windows.Forms.Label();
            this.lblCurrentCog = new System.Windows.Forms.Label();
            this.lblTeachCog = new System.Windows.Forms.Label();
            this.lblTeachCogValue = new System.Windows.Forms.Label();
            this.lblCuttentPositionValue = new System.Windows.Forms.Label();
            this.lblCurrentPosition = new System.Windows.Forms.Label();
            this.lblSetCurrentToTarget = new System.Windows.Forms.Label();
            this.lblCurrentToTeach = new System.Windows.Forms.Label();
            this.lblTrackingOff = new System.Windows.Forms.Label();
            this.lblTrackingOn = new System.Windows.Forms.Label();
            this.lblSoftwareLimit = new System.Windows.Forms.Label();
            this.tlpPlusLimit = new System.Windows.Forms.TableLayoutPanel();
            this.lblPositiveLimit = new System.Windows.Forms.Label();
            this.lblPositive = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNegativeLimit = new System.Windows.Forms.Label();
            this.lblNegative = new System.Windows.Forms.Label();
            this.lblReturndB = new System.Windows.Forms.Label();
            this.lblCurrentReturndB = new System.Windows.Forms.Label();
            this.tlpSetReturndB = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLowerReturn = new System.Windows.Forms.Label();
            this.lblUpperReturn = new System.Windows.Forms.Label();
            this.cbxEnableRetrundB = new System.Windows.Forms.CheckBox();
            this.lblLowerReturndB = new System.Windows.Forms.Label();
            this.lblUpperReturndB = new System.Windows.Forms.Label();
            this.tlpAutoFocusControl.SuspendLayout();
            this.tlpPlusLimit.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpSetReturndB.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAutoFocusControl
            // 
            this.tlpAutoFocusControl.ColumnCount = 3;
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpAutoFocusControl.Controls.Add(this.lblTrackingOnOff, 0, 6);
            this.tlpAutoFocusControl.Controls.Add(this.lblTargetPositionValue, 1, 0);
            this.tlpAutoFocusControl.Controls.Add(this.lblTargetPosition, 0, 0);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentCogValue, 1, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentCog, 0, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblTeachCog, 0, 2);
            this.tlpAutoFocusControl.Controls.Add(this.lblTeachCogValue, 1, 2);
            this.tlpAutoFocusControl.Controls.Add(this.lblCuttentPositionValue, 1, 1);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentPosition, 0, 1);
            this.tlpAutoFocusControl.Controls.Add(this.lblSetCurrentToTarget, 2, 1);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentToTeach, 2, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblTrackingOff, 2, 6);
            this.tlpAutoFocusControl.Controls.Add(this.lblTrackingOn, 1, 6);
            this.tlpAutoFocusControl.Controls.Add(this.lblSoftwareLimit, 0, 4);
            this.tlpAutoFocusControl.Controls.Add(this.tlpPlusLimit, 2, 4);
            this.tlpAutoFocusControl.Controls.Add(this.tableLayoutPanel1, 1, 4);
            this.tlpAutoFocusControl.Controls.Add(this.lblReturndB, 0, 5);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentReturndB, 1, 5);
            this.tlpAutoFocusControl.Controls.Add(this.tlpSetReturndB, 2, 5);
            this.tlpAutoFocusControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAutoFocusControl.Location = new System.Drawing.Point(0, 0);
            this.tlpAutoFocusControl.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAutoFocusControl.Name = "tlpAutoFocusControl";
            this.tlpAutoFocusControl.RowCount = 7;
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAutoFocusControl.Size = new System.Drawing.Size(400, 280);
            this.tlpAutoFocusControl.TabIndex = 3;
            // 
            // lblTrackingOnOff
            // 
            this.lblTrackingOnOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTrackingOnOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrackingOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrackingOnOff.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrackingOnOff.Location = new System.Drawing.Point(0, 234);
            this.lblTrackingOnOff.Margin = new System.Windows.Forms.Padding(0);
            this.lblTrackingOnOff.Name = "lblTrackingOnOff";
            this.lblTrackingOnOff.Size = new System.Drawing.Size(133, 46);
            this.lblTrackingOnOff.TabIndex = 205;
            this.lblTrackingOnOff.Text = "TRACKING \r\nON/OFF";
            this.lblTrackingOnOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTargetPositionValue
            // 
            this.lblTargetPositionValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTargetPositionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTargetPositionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTargetPositionValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTargetPositionValue.Location = new System.Drawing.Point(133, 0);
            this.lblTargetPositionValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTargetPositionValue.Name = "lblTargetPositionValue";
            this.lblTargetPositionValue.Size = new System.Drawing.Size(133, 39);
            this.lblTargetPositionValue.TabIndex = 1;
            this.lblTargetPositionValue.Text = "0.0";
            this.lblTargetPositionValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTargetPositionValue.Click += new System.EventHandler(this.lblTargetPositionZValue_Click);
            // 
            // lblTargetPosition
            // 
            this.lblTargetPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTargetPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTargetPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTargetPosition.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTargetPosition.Location = new System.Drawing.Point(0, 0);
            this.lblTargetPosition.Margin = new System.Windows.Forms.Padding(0);
            this.lblTargetPosition.Name = "lblTargetPosition";
            this.lblTargetPosition.Size = new System.Drawing.Size(133, 39);
            this.lblTargetPosition.TabIndex = 1;
            this.lblTargetPosition.Text = "TARGET\r\nPOSITION";
            this.lblTargetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentCogValue
            // 
            this.lblCurrentCogValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentCogValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentCogValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentCogValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentCogValue.Location = new System.Drawing.Point(133, 117);
            this.lblCurrentCogValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentCogValue.Name = "lblCurrentCogValue";
            this.lblCurrentCogValue.Size = new System.Drawing.Size(133, 39);
            this.lblCurrentCogValue.TabIndex = 1;
            this.lblCurrentCogValue.Text = "0";
            this.lblCurrentCogValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentCog
            // 
            this.lblCurrentCog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentCog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentCog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentCog.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentCog.Location = new System.Drawing.Point(0, 117);
            this.lblCurrentCog.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentCog.Name = "lblCurrentCog";
            this.lblCurrentCog.Size = new System.Drawing.Size(133, 39);
            this.lblCurrentCog.TabIndex = 203;
            this.lblCurrentCog.Text = "CURRENT COG";
            this.lblCurrentCog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTeachCog
            // 
            this.lblTeachCog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTeachCog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTeachCog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTeachCog.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTeachCog.Location = new System.Drawing.Point(0, 78);
            this.lblTeachCog.Margin = new System.Windows.Forms.Padding(0);
            this.lblTeachCog.Name = "lblTeachCog";
            this.lblTeachCog.Size = new System.Drawing.Size(133, 39);
            this.lblTeachCog.TabIndex = 203;
            this.lblTeachCog.Text = "COG";
            this.lblTeachCog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTeachCogValue
            // 
            this.lblTeachCogValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTeachCogValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTeachCogValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTeachCogValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTeachCogValue.Location = new System.Drawing.Point(133, 78);
            this.lblTeachCogValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTeachCogValue.Name = "lblTeachCogValue";
            this.lblTeachCogValue.Size = new System.Drawing.Size(133, 39);
            this.lblTeachCogValue.TabIndex = 202;
            this.lblTeachCogValue.Text = "0";
            this.lblTeachCogValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTeachCogValue.Click += new System.EventHandler(this.lblTeachCogValue_Click);
            // 
            // lblCuttentPositionValue
            // 
            this.lblCuttentPositionValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCuttentPositionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuttentPositionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCuttentPositionValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCuttentPositionValue.Location = new System.Drawing.Point(133, 39);
            this.lblCuttentPositionValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblCuttentPositionValue.Name = "lblCuttentPositionValue";
            this.lblCuttentPositionValue.Size = new System.Drawing.Size(133, 39);
            this.lblCuttentPositionValue.TabIndex = 1;
            this.lblCuttentPositionValue.Text = "0.0";
            this.lblCuttentPositionValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPosition
            // 
            this.lblCurrentPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPosition.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPosition.Location = new System.Drawing.Point(0, 39);
            this.lblCurrentPosition.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentPosition.Name = "lblCurrentPosition";
            this.lblCurrentPosition.Size = new System.Drawing.Size(133, 39);
            this.lblCurrentPosition.TabIndex = 1;
            this.lblCurrentPosition.Text = "CURRENT\r\nPOSITION";
            this.lblCurrentPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSetCurrentToTarget
            // 
            this.lblSetCurrentToTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblSetCurrentToTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSetCurrentToTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSetCurrentToTarget.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblSetCurrentToTarget.Location = new System.Drawing.Point(266, 39);
            this.lblSetCurrentToTarget.Margin = new System.Windows.Forms.Padding(0);
            this.lblSetCurrentToTarget.Name = "lblSetCurrentToTarget";
            this.lblSetCurrentToTarget.Size = new System.Drawing.Size(134, 39);
            this.lblSetCurrentToTarget.TabIndex = 202;
            this.lblSetCurrentToTarget.Text = "SET";
            this.lblSetCurrentToTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSetCurrentToTarget.Click += new System.EventHandler(this.lblSetCurrentToTarget_Click);
            // 
            // lblCurrentToTeach
            // 
            this.lblCurrentToTeach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentToTeach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentToTeach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentToTeach.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentToTeach.Location = new System.Drawing.Point(266, 117);
            this.lblCurrentToTeach.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentToTeach.Name = "lblCurrentToTeach";
            this.lblCurrentToTeach.Size = new System.Drawing.Size(134, 39);
            this.lblCurrentToTeach.TabIndex = 202;
            this.lblCurrentToTeach.Text = "SET";
            this.lblCurrentToTeach.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCurrentToTeach.Click += new System.EventHandler(this.lblCurrentToTeach_Click);
            // 
            // lblTrackingOff
            // 
            this.lblTrackingOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTrackingOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrackingOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrackingOff.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrackingOff.Location = new System.Drawing.Point(266, 234);
            this.lblTrackingOff.Margin = new System.Windows.Forms.Padding(0);
            this.lblTrackingOff.Name = "lblTrackingOff";
            this.lblTrackingOff.Size = new System.Drawing.Size(134, 46);
            this.lblTrackingOff.TabIndex = 202;
            this.lblTrackingOff.Text = "OFF";
            this.lblTrackingOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTrackingOff.Click += new System.EventHandler(this.lblTrackingOff_Click);
            // 
            // lblTrackingOn
            // 
            this.lblTrackingOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTrackingOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrackingOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrackingOn.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrackingOn.Location = new System.Drawing.Point(133, 234);
            this.lblTrackingOn.Margin = new System.Windows.Forms.Padding(0);
            this.lblTrackingOn.Name = "lblTrackingOn";
            this.lblTrackingOn.Size = new System.Drawing.Size(133, 46);
            this.lblTrackingOn.TabIndex = 202;
            this.lblTrackingOn.Text = "ON";
            this.lblTrackingOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTrackingOn.Click += new System.EventHandler(this.lblTrackingOn_Click);
            // 
            // lblSoftwareLimit
            // 
            this.lblSoftwareLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblSoftwareLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSoftwareLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoftwareLimit.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblSoftwareLimit.Location = new System.Drawing.Point(0, 156);
            this.lblSoftwareLimit.Margin = new System.Windows.Forms.Padding(0);
            this.lblSoftwareLimit.Name = "lblSoftwareLimit";
            this.lblSoftwareLimit.Size = new System.Drawing.Size(133, 39);
            this.lblSoftwareLimit.TabIndex = 203;
            this.lblSoftwareLimit.Text = "S/W Limit";
            this.lblSoftwareLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpPlusLimit
            // 
            this.tlpPlusLimit.ColumnCount = 2;
            this.tlpPlusLimit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpPlusLimit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlpPlusLimit.Controls.Add(this.lblPositiveLimit, 1, 0);
            this.tlpPlusLimit.Controls.Add(this.lblPositive, 0, 0);
            this.tlpPlusLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPlusLimit.Location = new System.Drawing.Point(266, 156);
            this.tlpPlusLimit.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPlusLimit.Name = "tlpPlusLimit";
            this.tlpPlusLimit.RowCount = 1;
            this.tlpPlusLimit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPlusLimit.Size = new System.Drawing.Size(134, 39);
            this.tlpPlusLimit.TabIndex = 206;
            // 
            // lblPositiveLimit
            // 
            this.lblPositiveLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblPositiveLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPositiveLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPositiveLimit.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblPositiveLimit.Location = new System.Drawing.Point(26, 0);
            this.lblPositiveLimit.Margin = new System.Windows.Forms.Padding(0);
            this.lblPositiveLimit.Name = "lblPositiveLimit";
            this.lblPositiveLimit.Size = new System.Drawing.Size(108, 39);
            this.lblPositiveLimit.TabIndex = 203;
            this.lblPositiveLimit.Text = "0";
            this.lblPositiveLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPositiveLimit.Click += new System.EventHandler(this.lblPositiveLimit_Click);
            // 
            // lblPositive
            // 
            this.lblPositive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblPositive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPositive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPositive.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblPositive.Location = new System.Drawing.Point(0, 0);
            this.lblPositive.Margin = new System.Windows.Forms.Padding(0);
            this.lblPositive.Name = "lblPositive";
            this.lblPositive.Size = new System.Drawing.Size(26, 39);
            this.lblPositive.TabIndex = 203;
            this.lblPositive.Text = "+";
            this.lblPositive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.lblNegativeLimit, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNegative, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(133, 156);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(133, 39);
            this.tableLayoutPanel1.TabIndex = 206;
            // 
            // lblNegativeLimit
            // 
            this.lblNegativeLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblNegativeLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNegativeLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNegativeLimit.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblNegativeLimit.Location = new System.Drawing.Point(26, 0);
            this.lblNegativeLimit.Margin = new System.Windows.Forms.Padding(0);
            this.lblNegativeLimit.Name = "lblNegativeLimit";
            this.lblNegativeLimit.Size = new System.Drawing.Size(107, 39);
            this.lblNegativeLimit.TabIndex = 203;
            this.lblNegativeLimit.Text = "0";
            this.lblNegativeLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNegativeLimit.Click += new System.EventHandler(this.lblNegativeLimit_Click);
            // 
            // lblNegative
            // 
            this.lblNegative.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblNegative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNegative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNegative.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblNegative.Location = new System.Drawing.Point(0, 0);
            this.lblNegative.Margin = new System.Windows.Forms.Padding(0);
            this.lblNegative.Name = "lblNegative";
            this.lblNegative.Size = new System.Drawing.Size(26, 39);
            this.lblNegative.TabIndex = 203;
            this.lblNegative.Text = "-";
            this.lblNegative.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReturndB
            // 
            this.lblReturndB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblReturndB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReturndB.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblReturndB.Location = new System.Drawing.Point(0, 195);
            this.lblReturndB.Margin = new System.Windows.Forms.Padding(0);
            this.lblReturndB.Name = "lblReturndB";
            this.lblReturndB.Size = new System.Drawing.Size(133, 39);
            this.lblReturndB.TabIndex = 203;
            this.lblReturndB.Text = "Return dB";
            this.lblReturndB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentReturndB
            // 
            this.lblCurrentReturndB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentReturndB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentReturndB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentReturndB.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentReturndB.Location = new System.Drawing.Point(133, 195);
            this.lblCurrentReturndB.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentReturndB.Name = "lblCurrentReturndB";
            this.lblCurrentReturndB.Size = new System.Drawing.Size(133, 39);
            this.lblCurrentReturndB.TabIndex = 1;
            this.lblCurrentReturndB.Text = "0";
            this.lblCurrentReturndB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpSetReturndB
            // 
            this.tlpSetReturndB.ColumnCount = 2;
            this.tlpSetReturndB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSetReturndB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlpSetReturndB.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tlpSetReturndB.Controls.Add(this.cbxEnableRetrundB, 1, 0);
            this.tlpSetReturndB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSetReturndB.Location = new System.Drawing.Point(266, 195);
            this.tlpSetReturndB.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSetReturndB.Name = "tlpSetReturndB";
            this.tlpSetReturndB.RowCount = 1;
            this.tlpSetReturndB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSetReturndB.Size = new System.Drawing.Size(134, 39);
            this.tlpSetReturndB.TabIndex = 207;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblLowerReturn, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblUpperReturn, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblLowerReturndB, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblUpperReturndB, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(122, 39);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblLowerReturn
            // 
            this.lblLowerReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLowerReturn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLowerReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLowerReturn.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblLowerReturn.Location = new System.Drawing.Point(0, 0);
            this.lblLowerReturn.Margin = new System.Windows.Forms.Padding(0);
            this.lblLowerReturn.Name = "lblLowerReturn";
            this.lblLowerReturn.Size = new System.Drawing.Size(61, 19);
            this.lblLowerReturn.TabIndex = 203;
            this.lblLowerReturn.Text = "Lower";
            this.lblLowerReturn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpperReturn
            // 
            this.lblUpperReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblUpperReturn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUpperReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpperReturn.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblUpperReturn.Location = new System.Drawing.Point(0, 19);
            this.lblUpperReturn.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpperReturn.Name = "lblUpperReturn";
            this.lblUpperReturn.Size = new System.Drawing.Size(61, 20);
            this.lblUpperReturn.TabIndex = 203;
            this.lblUpperReturn.Text = "Upper";
            this.lblUpperReturn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxEnableRetrundB
            // 
            this.cbxEnableRetrundB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxEnableRetrundB.Location = new System.Drawing.Point(122, 0);
            this.cbxEnableRetrundB.Margin = new System.Windows.Forms.Padding(0);
            this.cbxEnableRetrundB.Name = "cbxEnableRetrundB";
            this.cbxEnableRetrundB.Size = new System.Drawing.Size(12, 39);
            this.cbxEnableRetrundB.TabIndex = 1;
            this.cbxEnableRetrundB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbxEnableRetrundB.UseVisualStyleBackColor = true;
            this.cbxEnableRetrundB.CheckedChanged += new System.EventHandler(this.cbxEnableRetrundB_CheckedChanged);
            // 
            // lblLowerReturndB
            // 
            this.lblLowerReturndB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLowerReturndB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLowerReturndB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLowerReturndB.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblLowerReturndB.Location = new System.Drawing.Point(61, 0);
            this.lblLowerReturndB.Margin = new System.Windows.Forms.Padding(0);
            this.lblLowerReturndB.Name = "lblLowerReturndB";
            this.lblLowerReturndB.Size = new System.Drawing.Size(61, 19);
            this.lblLowerReturndB.TabIndex = 1;
            this.lblLowerReturndB.Text = "0";
            this.lblLowerReturndB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLowerReturndB.Click += new System.EventHandler(this.lblLowerReturndB_Click);
            // 
            // lblUpperReturndB
            // 
            this.lblUpperReturndB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblUpperReturndB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUpperReturndB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpperReturndB.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblUpperReturndB.Location = new System.Drawing.Point(61, 19);
            this.lblUpperReturndB.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpperReturndB.Name = "lblUpperReturndB";
            this.lblUpperReturndB.Size = new System.Drawing.Size(61, 20);
            this.lblUpperReturndB.TabIndex = 1;
            this.lblUpperReturndB.Text = "0";
            this.lblUpperReturndB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUpperReturndB.Click += new System.EventHandler(this.lblUpperReturndB_Click);
            // 
            // AutoFocusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAutoFocusControl);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AutoFocusControl";
            this.Size = new System.Drawing.Size(400, 280);
            this.Load += new System.EventHandler(this.AutoFocusControl_Load);
            this.tlpAutoFocusControl.ResumeLayout(false);
            this.tlpPlusLimit.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlpSetReturndB.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpAutoFocusControl;
        public System.Windows.Forms.Label lblTargetPositionValue;
        private System.Windows.Forms.Label lblTargetPosition;
        private System.Windows.Forms.Label lblCurrentCogValue;
        private System.Windows.Forms.Label lblCurrentCog;
        private System.Windows.Forms.Label lblTeachCog;
        private System.Windows.Forms.Label lblTeachCogValue;
        private System.Windows.Forms.Label lblCuttentPositionValue;
        private System.Windows.Forms.Label lblCurrentPosition;
        private System.Windows.Forms.Label lblSetCurrentToTarget;
        private System.Windows.Forms.Label lblCurrentToTeach;
        private System.Windows.Forms.Label lblTrackingOnOff;
        private System.Windows.Forms.Label lblTrackingOff;
        private System.Windows.Forms.Label lblTrackingOn;
        private System.Windows.Forms.Label lblSoftwareLimit;
        private System.Windows.Forms.TableLayoutPanel tlpPlusLimit;
        private System.Windows.Forms.Label lblPositiveLimit;
        private System.Windows.Forms.Label lblPositive;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblNegativeLimit;
        private System.Windows.Forms.Label lblNegative;
        private System.Windows.Forms.Label lblReturndB;
        private System.Windows.Forms.Label lblCurrentReturndB;
        private System.Windows.Forms.TableLayoutPanel tlpSetReturndB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblLowerReturn;
        private System.Windows.Forms.Label lblUpperReturn;
        private System.Windows.Forms.CheckBox cbxEnableRetrundB;
        private System.Windows.Forms.Label lblLowerReturndB;
        private System.Windows.Forms.Label lblUpperReturndB;
    }
}
