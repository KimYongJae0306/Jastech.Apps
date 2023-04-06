namespace ATT.UI.Forms
{
    partial class MotionSettingsForm
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
            this.tlpMotionSettings = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMotionFunction = new System.Windows.Forms.TableLayoutPanel();
            this.tlpStatus = new System.Windows.Forms.TableLayoutPanel();
            this.pnlJog = new System.Windows.Forms.Panel();
            this.pnlTeachingPositionList = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMoveToTeachingPosition = new System.Windows.Forms.Button();
            this.tlpMotionParameter = new System.Windows.Forms.TableLayoutPanel();
            this.tlpVariableParameters = new System.Windows.Forms.TableLayoutPanel();
            this.lblVariableParameter = new System.Windows.Forms.Label();
            this.tlpVariableParameter = new System.Windows.Forms.TableLayoutPanel();
            this.tlpCommonParameters = new System.Windows.Forms.TableLayoutPanel();
            this.lblCommonParameter = new System.Windows.Forms.Label();
            this.tlpCommonParameter = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCancelImage = new System.Windows.Forms.Label();
            this.lblCancel = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.lblApplyImage = new System.Windows.Forms.Label();
            this.lblSave = new System.Windows.Forms.Label();
            this.tlpMotionSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpMotionFunction.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpMotionParameter.SuspendLayout();
            this.tlpVariableParameters.SuspendLayout();
            this.tlpCommonParameters.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMotionSettings
            // 
            this.tlpMotionSettings.ColumnCount = 2;
            this.tlpMotionSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpMotionSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpMotionSettings.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpMotionSettings.Controls.Add(this.tlpMotionParameter, 1, 0);
            this.tlpMotionSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMotionSettings.Location = new System.Drawing.Point(0, 0);
            this.tlpMotionSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMotionSettings.Name = "tlpMotionSettings";
            this.tlpMotionSettings.RowCount = 1;
            this.tlpMotionSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotionSettings.Size = new System.Drawing.Size(1084, 1030);
            this.tlpMotionSettings.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tlpMotionFunction, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.pnlTeachingPositionList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 700F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(487, 1030);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tlpMotionFunction
            // 
            this.tlpMotionFunction.ColumnCount = 1;
            this.tlpMotionFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotionFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMotionFunction.Controls.Add(this.tlpStatus, 0, 0);
            this.tlpMotionFunction.Controls.Add(this.pnlJog, 0, 1);
            this.tlpMotionFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMotionFunction.Location = new System.Drawing.Point(0, 264);
            this.tlpMotionFunction.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMotionFunction.Name = "tlpMotionFunction";
            this.tlpMotionFunction.RowCount = 2;
            this.tlpMotionFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionFunction.Size = new System.Drawing.Size(487, 700);
            this.tlpMotionFunction.TabIndex = 0;
            // 
            // tlpStatus
            // 
            this.tlpStatus.ColumnCount = 4;
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStatus.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tlpStatus.Location = new System.Drawing.Point(0, 0);
            this.tlpStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStatus.Name = "tlpStatus";
            this.tlpStatus.RowCount = 1;
            this.tlpStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatus.Size = new System.Drawing.Size(487, 350);
            this.tlpStatus.TabIndex = 0;
            // 
            // pnlJog
            // 
            this.pnlJog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlJog.Location = new System.Drawing.Point(0, 350);
            this.pnlJog.Margin = new System.Windows.Forms.Padding(0);
            this.pnlJog.Name = "pnlJog";
            this.pnlJog.Size = new System.Drawing.Size(487, 350);
            this.pnlJog.TabIndex = 1;
            // 
            // pnlTeachingPositionList
            // 
            this.pnlTeachingPositionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingPositionList.Location = new System.Drawing.Point(0, 0);
            this.pnlTeachingPositionList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingPositionList.Name = "pnlTeachingPositionList";
            this.pnlTeachingPositionList.Size = new System.Drawing.Size(487, 120);
            this.pnlTeachingPositionList.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.Controls.Add(this.btnMoveToTeachingPosition, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 152);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(487, 80);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btnMoveToTeachingPosition
            // 
            this.btnMoveToTeachingPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.btnMoveToTeachingPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveToTeachingPosition.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnMoveToTeachingPosition.ForeColor = System.Drawing.Color.White;
            this.btnMoveToTeachingPosition.Location = new System.Drawing.Point(3, 3);
            this.btnMoveToTeachingPosition.Name = "btnMoveToTeachingPosition";
            this.btnMoveToTeachingPosition.Size = new System.Drawing.Size(115, 74);
            this.btnMoveToTeachingPosition.TabIndex = 23;
            this.btnMoveToTeachingPosition.Text = "Move To\r\nTarget\r\nPosition";
            this.btnMoveToTeachingPosition.UseVisualStyleBackColor = false;
            this.btnMoveToTeachingPosition.Click += new System.EventHandler(this.btnMoveToTeachingPosition_Click);
            // 
            // tlpMotionParameter
            // 
            this.tlpMotionParameter.ColumnCount = 1;
            this.tlpMotionParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotionParameter.Controls.Add(this.tlpVariableParameters, 0, 1);
            this.tlpMotionParameter.Controls.Add(this.tlpCommonParameters, 0, 0);
            this.tlpMotionParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMotionParameter.Location = new System.Drawing.Point(487, 0);
            this.tlpMotionParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMotionParameter.Name = "tlpMotionParameter";
            this.tlpMotionParameter.RowCount = 2;
            this.tlpMotionParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionParameter.Size = new System.Drawing.Size(597, 1030);
            this.tlpMotionParameter.TabIndex = 1;
            // 
            // tlpVariableParameters
            // 
            this.tlpVariableParameters.ColumnCount = 1;
            this.tlpVariableParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVariableParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVariableParameters.Controls.Add(this.lblVariableParameter, 0, 0);
            this.tlpVariableParameters.Controls.Add(this.tlpVariableParameter, 0, 1);
            this.tlpVariableParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVariableParameters.Location = new System.Drawing.Point(0, 515);
            this.tlpVariableParameters.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVariableParameters.Name = "tlpVariableParameters";
            this.tlpVariableParameters.RowCount = 2;
            this.tlpVariableParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpVariableParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVariableParameters.Size = new System.Drawing.Size(597, 515);
            this.tlpVariableParameters.TabIndex = 1;
            // 
            // lblVariableParameter
            // 
            this.lblVariableParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblVariableParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariableParameter.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblVariableParameter.ForeColor = System.Drawing.Color.White;
            this.lblVariableParameter.Location = new System.Drawing.Point(3, 0);
            this.lblVariableParameter.Name = "lblVariableParameter";
            this.lblVariableParameter.Size = new System.Drawing.Size(591, 40);
            this.lblVariableParameter.TabIndex = 6;
            this.lblVariableParameter.Text = "Variable";
            this.lblVariableParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpVariableParameter
            // 
            this.tlpVariableParameter.ColumnCount = 1;
            this.tlpVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVariableParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVariableParameter.Location = new System.Drawing.Point(0, 40);
            this.tlpVariableParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVariableParameter.Name = "tlpVariableParameter";
            this.tlpVariableParameter.RowCount = 3;
            this.tlpVariableParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpVariableParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpVariableParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpVariableParameter.Size = new System.Drawing.Size(597, 475);
            this.tlpVariableParameter.TabIndex = 7;
            // 
            // tlpCommonParameters
            // 
            this.tlpCommonParameters.ColumnCount = 1;
            this.tlpCommonParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCommonParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCommonParameters.Controls.Add(this.lblCommonParameter, 0, 0);
            this.tlpCommonParameters.Controls.Add(this.tlpCommonParameter, 0, 1);
            this.tlpCommonParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCommonParameters.Location = new System.Drawing.Point(0, 0);
            this.tlpCommonParameters.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCommonParameters.Name = "tlpCommonParameters";
            this.tlpCommonParameters.RowCount = 2;
            this.tlpCommonParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpCommonParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCommonParameters.Size = new System.Drawing.Size(597, 515);
            this.tlpCommonParameters.TabIndex = 0;
            // 
            // lblCommonParameter
            // 
            this.lblCommonParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblCommonParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCommonParameter.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblCommonParameter.ForeColor = System.Drawing.Color.White;
            this.lblCommonParameter.Location = new System.Drawing.Point(3, 0);
            this.lblCommonParameter.Name = "lblCommonParameter";
            this.lblCommonParameter.Size = new System.Drawing.Size(591, 40);
            this.lblCommonParameter.TabIndex = 6;
            this.lblCommonParameter.Text = "Common";
            this.lblCommonParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpCommonParameter
            // 
            this.tlpCommonParameter.ColumnCount = 1;
            this.tlpCommonParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCommonParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCommonParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCommonParameter.Location = new System.Drawing.Point(0, 40);
            this.tlpCommonParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCommonParameter.Name = "tlpCommonParameter";
            this.tlpCommonParameter.RowCount = 3;
            this.tlpCommonParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCommonParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCommonParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCommonParameter.Size = new System.Drawing.Size(597, 475);
            this.tlpCommonParameter.TabIndex = 7;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tlpMotionSettings, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1084, 1100);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 1033);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1078, 64);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Controls.Add(this.panel10, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.panel8, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1078, 64);
            this.tableLayoutPanel9.TabIndex = 3;
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.tableLayoutPanel11);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(881, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(194, 58);
            this.panel10.TabIndex = 4;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.lblCancelImage, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblCancel, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(192, 56);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // lblCancelImage
            // 
            this.lblCancelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCancelImage.Image = global::ATT.Properties.Resources.Cancel_White;
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
            this.lblCancel.Size = new System.Drawing.Size(136, 56);
            this.lblCancel.TabIndex = 0;
            this.lblCancel.Text = "Cancel";
            this.lblCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancel.Click += new System.EventHandler(this.lblCancel_Click);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.tableLayoutPanel10);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(681, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(194, 58);
            this.panel8.TabIndex = 5;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.lblApplyImage, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblSave, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(192, 56);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // lblApplyImage
            // 
            this.lblApplyImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApplyImage.Image = global::ATT.Properties.Resources.Save_White;
            this.lblApplyImage.Location = new System.Drawing.Point(3, 0);
            this.lblApplyImage.Name = "lblApplyImage";
            this.lblApplyImage.Size = new System.Drawing.Size(44, 56);
            this.lblApplyImage.TabIndex = 1;
            this.lblApplyImage.Click += new System.EventHandler(this.lblSave_Click);
            // 
            // lblSave
            // 
            this.lblSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSave.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblSave.ForeColor = System.Drawing.Color.White;
            this.lblSave.Location = new System.Drawing.Point(53, 0);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(136, 56);
            this.lblSave.TabIndex = 0;
            this.lblSave.Text = "Save";
            this.lblSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSave.Click += new System.EventHandler(this.lblSave_Click);
            // 
            // MotionSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1084, 1100);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MotionSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MotionSettingsForm_Load);
            this.tlpMotionSettings.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlpMotionFunction.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpMotionParameter.ResumeLayout(false);
            this.tlpVariableParameters.ResumeLayout(false);
            this.tlpCommonParameters.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMotionSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpMotionFunction;
        private System.Windows.Forms.TableLayoutPanel tlpStatus;
        private System.Windows.Forms.Panel pnlJog;
        private System.Windows.Forms.TableLayoutPanel tlpMotionParameter;
        private System.Windows.Forms.Panel pnlTeachingPositionList;
        private System.Windows.Forms.TableLayoutPanel tlpCommonParameters;
        private System.Windows.Forms.TableLayoutPanel tlpVariableParameters;
        private System.Windows.Forms.Label lblVariableParameter;
        private System.Windows.Forms.Label lblCommonParameter;
        private System.Windows.Forms.TableLayoutPanel tlpCommonParameter;
        private System.Windows.Forms.TableLayoutPanel tlpVariableParameter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnMoveToTeachingPosition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Label lblCancelImage;
        private System.Windows.Forms.Label lblCancel;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label lblApplyImage;
        private System.Windows.Forms.Label lblSave;
    }
}
