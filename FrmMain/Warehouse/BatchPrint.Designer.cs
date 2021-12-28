namespace Global.Warehouse
{
    partial class BatchPrint
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
            this.tbFileNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnView = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // tbFileNumber
            // 
            // 
            // 
            // 
            this.tbFileNumber.Border.Class = "TextBoxBorder";
            this.tbFileNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbFileNumber.Location = new System.Drawing.Point(59, 12);
            this.tbFileNumber.Name = "tbFileNumber";
            this.tbFileNumber.PreventEnterBeep = true;
            this.tbFileNumber.Size = new System.Drawing.Size(97, 21);
            this.tbFileNumber.TabIndex = 6;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(42, 24);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "流水号";
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnView.Location = new System.Drawing.Point(178, 10);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(48, 23);
            this.btnView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnView.TabIndex = 4;
            this.btnView.Text = "查找";
            // 
            // BatchPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 614);
            this.Controls.Add(this.tbFileNumber);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnView);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "BatchPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请验验收记录打印";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbFileNumber;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnView;
    }
}