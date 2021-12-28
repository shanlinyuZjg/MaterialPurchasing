
namespace Global.Purchase
{
    partial class Cusotoms
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
            this.btnChoose = new DevComponents.DotNetBar.ButtonX();
            this.cbbe = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnChoose
            // 
            this.btnChoose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChoose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChoose.Location = new System.Drawing.Point(243, 31);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(49, 23);
            this.btnChoose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChoose.TabIndex = 0;
            this.btnChoose.Text = "确定";
            this.btnChoose.Click += new System.EventHandler(this.btn_Click);
            // 
            // cbbe
            // 
            this.cbbe.DisplayMember = "Text";
            this.cbbe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbe.FormattingEnabled = true;
            this.cbbe.ItemHeight = 15;
            this.cbbe.Location = new System.Drawing.Point(56, 31);
            this.cbbe.Name = "cbbe";
            this.cbbe.Size = new System.Drawing.Size(167, 21);
            this.cbbe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbe.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 31);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "手册号";
            // 
            // Cusotoms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 79);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cbbe);
            this.Controls.Add(this.btnChoose);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cusotoms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择手册号";
            this.Load += new System.EventHandler(this.Cusotoms_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnChoose;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbe;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}