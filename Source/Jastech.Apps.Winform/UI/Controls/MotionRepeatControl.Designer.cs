namespace Jastech.Apps.Winform.UI.Controls
{
    partial class MotionRepeatControl
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
            this.tlpRepeatOption = new System.Windows.Forms.TableLayoutPanel();
            this.lblRepeatRemain = new System.Windows.Forms.Label();
            this.lblRepeatVelocityValue = new System.Windows.Forms.Label();
            this.lblRepeatVelocity = new System.Windows.Forms.Label();
            this.lblRepeatAccelerationValue = new System.Windows.Forms.Label();
            this.lblAxisName = new System.Windows.Forms.Label();
            this.lblRepeat = new System.Windows.Forms.Label();
            this.lblRepeatAcceleration = new System.Windows.Forms.Label();
            this.lblScanLength = new System.Windows.Forms.Label();
            this.lblScanDirection = new System.Windows.Forms.Label();
            this.lblScanXLength = new System.Windows.Forms.Label();
            this.lblOperationAxis = new System.Windows.Forms.Label();
            this.lblDwellTime = new System.Windows.Forms.Label();
            this.lblDwellTimeValue = new System.Windows.Forms.Label();
            this.lblRepeatCount = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.tlpScanDirection = new System.Windows.Forms.TableLayoutPanel();
            this.lblForward = new System.Windows.Forms.Label();
            this.lblBackward = new System.Windows.Forms.Label();
            this.tlpRepeatCount = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRepeatOption.SuspendLayout();
            this.tlpScanDirection.SuspendLayout();
            this.tlpRepeatCount.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpRepeatOption
            // 
            this.tlpRepeatOption.ColumnCount = 3;
            this.tlpRepeatOption.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpRepeatOption.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpRepeatOption.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRepeatOption.Controls.Add(this.lblRepeatVelocityValue, 1, 1);
            this.tlpRepeatOption.Controls.Add(this.lblRepeatVelocity, 0, 1);
            this.tlpRepeatOption.Controls.Add(this.lblRepeatAccelerationValue, 1, 2);
            this.tlpRepeatOption.Controls.Add(this.lblAxisName, 0, 0);
            this.tlpRepeatOption.Controls.Add(this.lblRepeat, 0, 6);
            this.tlpRepeatOption.Controls.Add(this.lblRepeatAcceleration, 0, 2);
            this.tlpRepeatOption.Controls.Add(this.lblScanLength, 0, 5);
            this.tlpRepeatOption.Controls.Add(this.lblScanDirection, 0, 4);
            this.tlpRepeatOption.Controls.Add(this.lblScanXLength, 1, 5);
            this.tlpRepeatOption.Controls.Add(this.lblOperationAxis, 1, 0);
            this.tlpRepeatOption.Controls.Add(this.lblDwellTime, 0, 3);
            this.tlpRepeatOption.Controls.Add(this.lblDwellTimeValue, 1, 3);
            this.tlpRepeatOption.Controls.Add(this.lblStart, 1, 7);
            this.tlpRepeatOption.Controls.Add(this.tlpScanDirection, 1, 4);
            this.tlpRepeatOption.Controls.Add(this.tlpRepeatCount, 1, 6);
            this.tlpRepeatOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRepeatOption.Location = new System.Drawing.Point(0, 0);
            this.tlpRepeatOption.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRepeatOption.Name = "tlpRepeatOption";
            this.tlpRepeatOption.RowCount = 9;
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpRepeatOption.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRepeatOption.Size = new System.Drawing.Size(334, 338);
            this.tlpRepeatOption.TabIndex = 2;
            // 
            // lblRepeatRemain
            // 
            this.lblRepeatRemain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRepeatRemain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeatRemain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRepeatRemain.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRepeatRemain.ForeColor = System.Drawing.Color.White;
            this.lblRepeatRemain.Location = new System.Drawing.Point(80, 0);
            this.lblRepeatRemain.Margin = new System.Windows.Forms.Padding(0);
            this.lblRepeatRemain.Name = "lblRepeatRemain";
            this.lblRepeatRemain.Size = new System.Drawing.Size(80, 40);
            this.lblRepeatRemain.TabIndex = 4;
            this.lblRepeatRemain.Text = "0 / 0";
            this.lblRepeatRemain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRepeatVelocityValue
            // 
            this.lblRepeatVelocityValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRepeatVelocityValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeatVelocityValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRepeatVelocityValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRepeatVelocityValue.ForeColor = System.Drawing.Color.White;
            this.lblRepeatVelocityValue.Location = new System.Drawing.Point(160, 40);
            this.lblRepeatVelocityValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblRepeatVelocityValue.Name = "lblRepeatVelocityValue";
            this.lblRepeatVelocityValue.Size = new System.Drawing.Size(160, 40);
            this.lblRepeatVelocityValue.TabIndex = 3;
            this.lblRepeatVelocityValue.Text = "10";
            this.lblRepeatVelocityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRepeatVelocityValue.Click += new System.EventHandler(this.lblRepeatVelocityValue_Click);
            // 
            // lblRepeatVelocity
            // 
            this.lblRepeatVelocity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRepeatVelocity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeatVelocity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRepeatVelocity.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRepeatVelocity.ForeColor = System.Drawing.Color.White;
            this.lblRepeatVelocity.Location = new System.Drawing.Point(0, 40);
            this.lblRepeatVelocity.Margin = new System.Windows.Forms.Padding(0);
            this.lblRepeatVelocity.Name = "lblRepeatVelocity";
            this.lblRepeatVelocity.Size = new System.Drawing.Size(160, 40);
            this.lblRepeatVelocity.TabIndex = 3;
            this.lblRepeatVelocity.Text = "VELOCITY";
            this.lblRepeatVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRepeatAccelerationValue
            // 
            this.lblRepeatAccelerationValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRepeatAccelerationValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeatAccelerationValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRepeatAccelerationValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRepeatAccelerationValue.ForeColor = System.Drawing.Color.White;
            this.lblRepeatAccelerationValue.Location = new System.Drawing.Point(160, 80);
            this.lblRepeatAccelerationValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblRepeatAccelerationValue.Name = "lblRepeatAccelerationValue";
            this.lblRepeatAccelerationValue.Size = new System.Drawing.Size(160, 40);
            this.lblRepeatAccelerationValue.TabIndex = 3;
            this.lblRepeatAccelerationValue.Text = "20";
            this.lblRepeatAccelerationValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRepeatAccelerationValue.Click += new System.EventHandler(this.lblRepeatAccelerationValue_Click);
            // 
            // lblAxisName
            // 
            this.lblAxisName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAxisName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAxisName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAxisName.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAxisName.ForeColor = System.Drawing.Color.White;
            this.lblAxisName.Location = new System.Drawing.Point(0, 0);
            this.lblAxisName.Margin = new System.Windows.Forms.Padding(0);
            this.lblAxisName.Name = "lblAxisName";
            this.lblAxisName.Size = new System.Drawing.Size(160, 40);
            this.lblAxisName.TabIndex = 0;
            this.lblAxisName.Text = "AXIS";
            this.lblAxisName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRepeat
            // 
            this.lblRepeat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRepeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRepeat.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRepeat.ForeColor = System.Drawing.Color.White;
            this.lblRepeat.Location = new System.Drawing.Point(0, 240);
            this.lblRepeat.Margin = new System.Windows.Forms.Padding(0);
            this.lblRepeat.Name = "lblRepeat";
            this.lblRepeat.Size = new System.Drawing.Size(160, 40);
            this.lblRepeat.TabIndex = 213;
            this.lblRepeat.Text = "REPEAT";
            this.lblRepeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRepeatAcceleration
            // 
            this.lblRepeatAcceleration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRepeatAcceleration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeatAcceleration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRepeatAcceleration.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRepeatAcceleration.ForeColor = System.Drawing.Color.White;
            this.lblRepeatAcceleration.Location = new System.Drawing.Point(0, 80);
            this.lblRepeatAcceleration.Margin = new System.Windows.Forms.Padding(0);
            this.lblRepeatAcceleration.Name = "lblRepeatAcceleration";
            this.lblRepeatAcceleration.Size = new System.Drawing.Size(160, 40);
            this.lblRepeatAcceleration.TabIndex = 3;
            this.lblRepeatAcceleration.Text = "ACCELERATION";
            this.lblRepeatAcceleration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScanLength
            // 
            this.lblScanLength.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblScanLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScanLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScanLength.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblScanLength.ForeColor = System.Drawing.Color.White;
            this.lblScanLength.Location = new System.Drawing.Point(0, 200);
            this.lblScanLength.Margin = new System.Windows.Forms.Padding(0);
            this.lblScanLength.Name = "lblScanLength";
            this.lblScanLength.Size = new System.Drawing.Size(160, 40);
            this.lblScanLength.TabIndex = 144;
            this.lblScanLength.Text = "SCAN LENGTH\r\n(mm)";
            this.lblScanLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScanDirection
            // 
            this.lblScanDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblScanDirection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScanDirection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScanDirection.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblScanDirection.ForeColor = System.Drawing.Color.White;
            this.lblScanDirection.Location = new System.Drawing.Point(0, 160);
            this.lblScanDirection.Margin = new System.Windows.Forms.Padding(0);
            this.lblScanDirection.Name = "lblScanDirection";
            this.lblScanDirection.Size = new System.Drawing.Size(160, 40);
            this.lblScanDirection.TabIndex = 141;
            this.lblScanDirection.Text = "SCAN\r\nDIRECTION";
            this.lblScanDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScanXLength
            // 
            this.lblScanXLength.AutoSize = true;
            this.lblScanXLength.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblScanXLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScanXLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScanXLength.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblScanXLength.ForeColor = System.Drawing.Color.White;
            this.lblScanXLength.Location = new System.Drawing.Point(160, 200);
            this.lblScanXLength.Margin = new System.Windows.Forms.Padding(0);
            this.lblScanXLength.Name = "lblScanXLength";
            this.lblScanXLength.Size = new System.Drawing.Size(160, 40);
            this.lblScanXLength.TabIndex = 209;
            this.lblScanXLength.Text = "5.0";
            this.lblScanXLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblScanXLength.Click += new System.EventHandler(this.lblScanXLength_Click);
            // 
            // lblOperationAxis
            // 
            this.lblOperationAxis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblOperationAxis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOperationAxis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperationAxis.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblOperationAxis.ForeColor = System.Drawing.Color.White;
            this.lblOperationAxis.Location = new System.Drawing.Point(160, 0);
            this.lblOperationAxis.Margin = new System.Windows.Forms.Padding(0);
            this.lblOperationAxis.Name = "lblOperationAxis";
            this.lblOperationAxis.Size = new System.Drawing.Size(160, 40);
            this.lblOperationAxis.TabIndex = 0;
            this.lblOperationAxis.Text = "AXIS";
            this.lblOperationAxis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDwellTime
            // 
            this.lblDwellTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblDwellTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDwellTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDwellTime.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblDwellTime.ForeColor = System.Drawing.Color.White;
            this.lblDwellTime.Location = new System.Drawing.Point(0, 120);
            this.lblDwellTime.Margin = new System.Windows.Forms.Padding(0);
            this.lblDwellTime.Name = "lblDwellTime";
            this.lblDwellTime.Size = new System.Drawing.Size(160, 40);
            this.lblDwellTime.TabIndex = 3;
            this.lblDwellTime.Text = "DWELL TIME";
            this.lblDwellTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDwellTimeValue
            // 
            this.lblDwellTimeValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblDwellTimeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDwellTimeValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDwellTimeValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblDwellTimeValue.ForeColor = System.Drawing.Color.White;
            this.lblDwellTimeValue.Location = new System.Drawing.Point(160, 120);
            this.lblDwellTimeValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblDwellTimeValue.Name = "lblDwellTimeValue";
            this.lblDwellTimeValue.Size = new System.Drawing.Size(160, 40);
            this.lblDwellTimeValue.TabIndex = 3;
            this.lblDwellTimeValue.Text = "1";
            this.lblDwellTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDwellTimeValue.Click += new System.EventHandler(this.lblDwellTimeValue_Click);
            // 
            // lblRepeatCount
            // 
            this.lblRepeatCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRepeatCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRepeatCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRepeatCount.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRepeatCount.ForeColor = System.Drawing.Color.White;
            this.lblRepeatCount.Location = new System.Drawing.Point(0, 0);
            this.lblRepeatCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblRepeatCount.Name = "lblRepeatCount";
            this.lblRepeatCount.Size = new System.Drawing.Size(80, 40);
            this.lblRepeatCount.TabIndex = 3;
            this.lblRepeatCount.Text = "3";
            this.lblRepeatCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRepeatCount.Click += new System.EventHandler(this.lblRepeatCount_Click);
            // 
            // lblStart
            // 
            this.lblStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStart.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblStart.ForeColor = System.Drawing.Color.White;
            this.lblStart.Location = new System.Drawing.Point(160, 280);
            this.lblStart.Margin = new System.Windows.Forms.Padding(0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(160, 40);
            this.lblStart.TabIndex = 214;
            this.lblStart.Text = "Start";
            this.lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStart.Click += new System.EventHandler(this.lblStart_Click);
            // 
            // tlpScanDirection
            // 
            this.tlpScanDirection.ColumnCount = 2;
            this.tlpScanDirection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpScanDirection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpScanDirection.Controls.Add(this.lblBackward, 0, 0);
            this.tlpScanDirection.Controls.Add(this.lblForward, 0, 0);
            this.tlpScanDirection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpScanDirection.Location = new System.Drawing.Point(160, 160);
            this.tlpScanDirection.Margin = new System.Windows.Forms.Padding(0);
            this.tlpScanDirection.Name = "tlpScanDirection";
            this.tlpScanDirection.RowCount = 1;
            this.tlpScanDirection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpScanDirection.Size = new System.Drawing.Size(160, 40);
            this.tlpScanDirection.TabIndex = 215;
            // 
            // lblForward
            // 
            this.lblForward.AutoSize = true;
            this.lblForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblForward.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblForward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblForward.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblForward.ForeColor = System.Drawing.Color.White;
            this.lblForward.Location = new System.Drawing.Point(0, 0);
            this.lblForward.Margin = new System.Windows.Forms.Padding(0);
            this.lblForward.Name = "lblForward";
            this.lblForward.Size = new System.Drawing.Size(80, 40);
            this.lblForward.TabIndex = 210;
            this.lblForward.Text = "Forward";
            this.lblForward.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblForward.Click += new System.EventHandler(this.lblFoward_Click);
            // 
            // lblBackward
            // 
            this.lblBackward.AutoSize = true;
            this.lblBackward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblBackward.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBackward.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblBackward.ForeColor = System.Drawing.Color.White;
            this.lblBackward.Location = new System.Drawing.Point(80, 0);
            this.lblBackward.Margin = new System.Windows.Forms.Padding(0);
            this.lblBackward.Name = "lblBackward";
            this.lblBackward.Size = new System.Drawing.Size(80, 40);
            this.lblBackward.TabIndex = 211;
            this.lblBackward.Text = "Backward";
            this.lblBackward.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBackward.Click += new System.EventHandler(this.lblBackward_Click);
            // 
            // tlpRepeatCount
            // 
            this.tlpRepeatCount.ColumnCount = 2;
            this.tlpRepeatCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRepeatCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRepeatCount.Controls.Add(this.lblRepeatRemain, 1, 0);
            this.tlpRepeatCount.Controls.Add(this.lblRepeatCount, 0, 0);
            this.tlpRepeatCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRepeatCount.Location = new System.Drawing.Point(160, 240);
            this.tlpRepeatCount.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRepeatCount.Name = "tlpRepeatCount";
            this.tlpRepeatCount.RowCount = 1;
            this.tlpRepeatCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRepeatCount.Size = new System.Drawing.Size(160, 40);
            this.tlpRepeatCount.TabIndex = 216;
            // 
            // MotionRepeatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpRepeatOption);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MotionRepeatControl";
            this.Size = new System.Drawing.Size(334, 338);
            this.tlpRepeatOption.ResumeLayout(false);
            this.tlpRepeatOption.PerformLayout();
            this.tlpScanDirection.ResumeLayout(false);
            this.tlpScanDirection.PerformLayout();
            this.tlpRepeatCount.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpRepeatOption;
        private System.Windows.Forms.Label lblRepeatRemain;
        private System.Windows.Forms.Label lblRepeatVelocityValue;
        private System.Windows.Forms.Label lblRepeatVelocity;
        private System.Windows.Forms.Label lblRepeatAccelerationValue;
        private System.Windows.Forms.Label lblAxisName;
        private System.Windows.Forms.Label lblRepeat;
        private System.Windows.Forms.Label lblRepeatAcceleration;
        private System.Windows.Forms.Label lblScanLength;
        private System.Windows.Forms.Label lblScanDirection;
        private System.Windows.Forms.Label lblScanXLength;
        private System.Windows.Forms.Label lblOperationAxis;
        private System.Windows.Forms.Label lblDwellTime;
        private System.Windows.Forms.Label lblDwellTimeValue;
        private System.Windows.Forms.Label lblRepeatCount;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.TableLayoutPanel tlpScanDirection;
        private System.Windows.Forms.Label lblBackward;
        private System.Windows.Forms.Label lblForward;
        private System.Windows.Forms.TableLayoutPanel tlpRepeatCount;
    }
}
