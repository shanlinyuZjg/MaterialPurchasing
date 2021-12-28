namespace Global.Purchase
{
    partial class PurchaseVials
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
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.btnPlacePO = new DevComponents.DotNetBar.ButtonX();
            this.btnUpdatePrice = new DevComponents.DotNetBar.ButtonX();
            this.tbTaxRate = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(12, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(56, 23);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Location = new System.Drawing.Point(12, 32);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(935, 537);
            this.dgvDetail.TabIndex = 9;
            this.dgvDetail.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellContentDoubleClick);
            this.dgvDetail.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellDoubleClick);
            // 
            // btnPlacePO
            // 
            this.btnPlacePO.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlacePO.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlacePO.Location = new System.Drawing.Point(185, 3);
            this.btnPlacePO.Name = "btnPlacePO";
            this.btnPlacePO.Size = new System.Drawing.Size(56, 23);
            this.btnPlacePO.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlacePO.TabIndex = 10;
            this.btnPlacePO.Text = "下单";
            this.btnPlacePO.Click += new System.EventHandler(this.btnPlacePO_Click);
            // 
            // btnUpdatePrice
            // 
            this.btnUpdatePrice.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdatePrice.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdatePrice.Location = new System.Drawing.Point(104, 3);
            this.btnUpdatePrice.Name = "btnUpdatePrice";
            this.btnUpdatePrice.Size = new System.Drawing.Size(56, 23);
            this.btnUpdatePrice.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdatePrice.TabIndex = 10;
            this.btnUpdatePrice.Text = "更新价格";
            this.btnUpdatePrice.Click += new System.EventHandler(this.btnUpdatePrice_Click);
            // 
            // tbTaxRate
            // 
            // 
            // 
            // 
            this.tbTaxRate.Border.Class = "TextBoxBorder";
            this.tbTaxRate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTaxRate.Location = new System.Drawing.Point(308, 4);
            this.tbTaxRate.Name = "tbTaxRate";
            this.tbTaxRate.PreventEnterBeep = true;
            this.tbTaxRate.Size = new System.Drawing.Size(56, 21);
            this.tbTaxRate.TabIndex = 35;
            this.tbTaxRate.Text = "0.13";
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(276, 3);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(29, 23);
            this.labelX7.TabIndex = 34;
            this.labelX7.Text = "税率";
            // 
            // PurchaseVials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 581);
            this.Controls.Add(this.tbTaxRate);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.btnUpdatePrice);
            this.Controls.Add(this.btnPlacePO);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvDetail);
            this.DoubleBuffered = true;
            this.Name = "PurchaseVials";
            this.Text = "西林瓶采购";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private System.Windows.Forms.DataGridView dgvDetail;
        private DevComponents.DotNetBar.ButtonX btnPlacePO;
        private DevComponents.DotNetBar.ButtonX btnUpdatePrice;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTaxRate;
        private DevComponents.DotNetBar.LabelX labelX7;
    }
}