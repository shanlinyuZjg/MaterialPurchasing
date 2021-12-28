
namespace Global.Purchase
{
    partial class InvoiceFinisdedcs
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
            this.btnRecover = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbPONumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgvDetail = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRecover
            // 
            this.btnRecover.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRecover.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRecover.Location = new System.Drawing.Point(418, 10);
            this.btnRecover.Name = "btnRecover";
            this.btnRecover.Size = new System.Drawing.Size(52, 23);
            this.btnRecover.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRecover.TabIndex = 0;
            this.btnRecover.Text = "恢复";
            this.btnRecover.Click += new System.EventHandler(this.btnRecover_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "订单编号";
            // 
            // tbPONumber
            // 
            // 
            // 
            // 
            this.tbPONumber.Border.Class = "TextBoxBorder";
            this.tbPONumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPONumber.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbPONumber.Location = new System.Drawing.Point(64, 12);
            this.tbPONumber.Name = "tbPONumber";
            this.tbPONumber.PreventEnterBeep = true;
            this.tbPONumber.Size = new System.Drawing.Size(100, 21);
            this.tbPONumber.TabIndex = 2;
            this.tbPONumber.Text = "输入后按回车键";
            this.tbPONumber.Click += new System.EventHandler(this.tbPONumber_Click);
            this.tbPONumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPONumber_KeyPress);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDetail.Location = new System.Drawing.Point(3, 41);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.RowHeadersWidth = 20;
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(467, 530);
            this.dgvDetail.TabIndex = 3;
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            this.Check.Width = 50;
            // 
            // InvoiceFinisdedcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 583);
            this.Controls.Add(this.dgvDetail);
            this.Controls.Add(this.tbPONumber);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnRecover);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvoiceFinisdedcs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "已确认订单管理";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnRecover;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPONumber;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDetail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
    }
}