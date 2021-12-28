
namespace Global.Purchase
{
    partial class DeptItemRequirementPlaceOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.dgvDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.tbPOPostfix = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.labelX27 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbPOMiddle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnPlaceOrder = new DevComponents.DotNetBar.ButtonX();
            this.tbPOHeader = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rbtnAutomatic = new System.Windows.Forms.RadioButton();
            this.rbtnMannual = new System.Windows.Forms.RadioButton();
            this.btnRequirementFinish = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(12, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 23);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetail.EnableHeadersVisualStyles = false;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(12, 46);
            this.dgvDetail.Name = "dgvDetail";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetail.RowHeadersWidth = 20;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDetail.Size = new System.Drawing.Size(1122, 590);
            this.dgvDetail.TabIndex = 14;
            // 
            // Check
            // 
            this.Check.Checked = true;
            this.Check.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.Check.CheckValue = "N";
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Check.Width = 54;
            // 
            // tbPOPostfix
            // 
            // 
            // 
            // 
            this.tbPOPostfix.Border.Class = "TextBoxBorder";
            this.tbPOPostfix.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPOPostfix.Location = new System.Drawing.Point(313, 11);
            this.tbPOPostfix.Name = "tbPOPostfix";
            this.tbPOPostfix.PreventEnterBeep = true;
            this.tbPOPostfix.Size = new System.Drawing.Size(48, 21);
            this.tbPOPostfix.TabIndex = 22;
            this.tbPOPostfix.Click += new System.EventHandler(this.tbPOPostfix_Click);
            this.tbPOPostfix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPOPostfix_KeyPress);
            // 
            // labelX16
            // 
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Location = new System.Drawing.Point(303, 11);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(10, 23);
            this.labelX16.TabIndex = 18;
            this.labelX16.Text = "-";
            // 
            // labelX27
            // 
            // 
            // 
            // 
            this.labelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX27.Location = new System.Drawing.Point(236, 11);
            this.labelX27.Name = "labelX27";
            this.labelX27.Size = new System.Drawing.Size(10, 23);
            this.labelX27.TabIndex = 19;
            this.labelX27.Text = "-";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(66, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(56, 23);
            this.labelX3.TabIndex = 20;
            this.labelX3.Text = "采购单号";
            // 
            // tbPOMiddle
            // 
            // 
            // 
            // 
            this.tbPOMiddle.Border.Class = "TextBoxBorder";
            this.tbPOMiddle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPOMiddle.Location = new System.Drawing.Point(249, 11);
            this.tbPOMiddle.Name = "tbPOMiddle";
            this.tbPOMiddle.PreventEnterBeep = true;
            this.tbPOMiddle.ReadOnly = true;
            this.tbPOMiddle.Size = new System.Drawing.Size(53, 21);
            this.tbPOMiddle.TabIndex = 17;
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlaceOrder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlaceOrder.Location = new System.Drawing.Point(367, 11);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(46, 23);
            this.btnPlaceOrder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlaceOrder.TabIndex = 21;
            this.btnPlaceOrder.Text = "下达";
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click);
            // 
            // tbPOHeader
            // 
            // 
            // 
            // 
            this.tbPOHeader.Border.Class = "TextBoxBorder";
            this.tbPOHeader.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPOHeader.Location = new System.Drawing.Point(195, 11);
            this.tbPOHeader.Name = "tbPOHeader";
            this.tbPOHeader.PreventEnterBeep = true;
            this.tbPOHeader.Size = new System.Drawing.Size(37, 21);
            this.tbPOHeader.TabIndex = 22;
            this.tbPOHeader.TextChanged += new System.EventHandler(this.tbPOHeader_TextChanged);
            this.tbPOHeader.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPOHeader_KeyPress);
            // 
            // rbtnAutomatic
            // 
            this.rbtnAutomatic.AutoSize = true;
            this.rbtnAutomatic.Checked = true;
            this.rbtnAutomatic.Location = new System.Drawing.Point(128, 24);
            this.rbtnAutomatic.Name = "rbtnAutomatic";
            this.rbtnAutomatic.Size = new System.Drawing.Size(47, 16);
            this.rbtnAutomatic.TabIndex = 23;
            this.rbtnAutomatic.TabStop = true;
            this.rbtnAutomatic.Text = "自动";
            this.rbtnAutomatic.UseVisualStyleBackColor = true;
            // 
            // rbtnMannual
            // 
            this.rbtnMannual.AutoSize = true;
            this.rbtnMannual.Location = new System.Drawing.Point(128, 5);
            this.rbtnMannual.Name = "rbtnMannual";
            this.rbtnMannual.Size = new System.Drawing.Size(47, 16);
            this.rbtnMannual.TabIndex = 24;
            this.rbtnMannual.Text = "手动";
            this.rbtnMannual.UseVisualStyleBackColor = true;
            // 
            // btnRequirementFinish
            // 
            this.btnRequirementFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRequirementFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRequirementFinish.Location = new System.Drawing.Point(510, 11);
            this.btnRequirementFinish.Name = "btnRequirementFinish";
            this.btnRequirementFinish.Size = new System.Drawing.Size(66, 23);
            this.btnRequirementFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRequirementFinish.TabIndex = 21;
            this.btnRequirementFinish.Text = "订单完成";
            this.btnRequirementFinish.Click += new System.EventHandler(this.btnRequirementFinish_Click);
            // 
            // DeptItemRequirementPlaceOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 648);
            this.Controls.Add(this.rbtnAutomatic);
            this.Controls.Add(this.rbtnMannual);
            this.Controls.Add(this.tbPOHeader);
            this.Controls.Add(this.tbPOPostfix);
            this.Controls.Add(this.labelX16);
            this.Controls.Add(this.labelX27);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.tbPOMiddle);
            this.Controls.Add(this.btnRequirementFinish);
            this.Controls.Add(this.btnPlaceOrder);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvDetail);
            this.DoubleBuffered = true;
            this.Name = "DeptItemRequirementPlaceOrder";
            this.Text = "订单下达";
            this.Load += new System.EventHandler(this.DeptItemRequirementPlaceOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDetail;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn Check;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPOPostfix;
        private DevComponents.DotNetBar.LabelX labelX16;
        private DevComponents.DotNetBar.LabelX labelX27;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPOMiddle;
        private DevComponents.DotNetBar.ButtonX btnPlaceOrder;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPOHeader;
        private System.Windows.Forms.RadioButton rbtnAutomatic;
        private System.Windows.Forms.RadioButton rbtnMannual;
        private DevComponents.DotNetBar.ButtonX btnRequirementFinish;
    }
}