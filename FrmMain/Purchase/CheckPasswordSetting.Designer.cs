namespace Global.Purchase
{
    partial class CheckPasswordSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckPasswordSetting));
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnSendEMails = new DevComponents.DotNetBar.ButtonX();
            this.btnViewPwd = new System.Windows.Forms.Button();
            this.dtpFinish = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.btnSetPassword = new DevComponents.DotNetBar.ButtonX();
            this.btnInvalidateCurrentPassword = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnSendEMails);
            this.groupPanel1.Controls.Add(this.btnViewPwd);
            this.groupPanel1.Controls.Add(this.dtpFinish);
            this.groupPanel1.Controls.Add(this.dtpStart);
            this.groupPanel1.Controls.Add(this.btnSetPassword);
            this.groupPanel1.Controls.Add(this.btnInvalidateCurrentPassword);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.tbPassword);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(958, 94);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 3;
            this.groupPanel1.Text = "出差审核密码设置";
            // 
            // btnSendEMails
            // 
            this.btnSendEMails.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSendEMails.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSendEMails.Location = new System.Drawing.Point(729, 19);
            this.btnSendEMails.Name = "btnSendEMails";
            this.btnSendEMails.Size = new System.Drawing.Size(88, 23);
            this.btnSendEMails.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSendEMails.TabIndex = 6;
            this.btnSendEMails.Text = "发送邮件";
            this.btnSendEMails.Click += new System.EventHandler(this.btnSendEMails_Click);
            // 
            // btnViewPwd
            // 
            this.btnViewPwd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewPwd.BackgroundImage")));
            this.btnViewPwd.FlatAppearance.BorderSize = 0;
            this.btnViewPwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewPwd.Location = new System.Drawing.Point(499, 19);
            this.btnViewPwd.Name = "btnViewPwd";
            this.btnViewPwd.Size = new System.Drawing.Size(18, 18);
            this.btnViewPwd.TabIndex = 5;
            this.btnViewPwd.UseVisualStyleBackColor = true;
            this.btnViewPwd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnViewPwd_MouseDown);
            this.btnViewPwd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnViewPwd_MouseUp);
            // 
            // dtpFinish
            // 
            this.dtpFinish.Location = new System.Drawing.Point(237, 19);
            this.dtpFinish.Name = "dtpFinish";
            this.dtpFinish.Size = new System.Drawing.Size(112, 21);
            this.dtpFinish.TabIndex = 4;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(59, 19);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(112, 21);
            this.dtpStart.TabIndex = 4;
            // 
            // btnSetPassword
            // 
            this.btnSetPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetPassword.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetPassword.Location = new System.Drawing.Point(530, 19);
            this.btnSetPassword.Name = "btnSetPassword";
            this.btnSetPassword.Size = new System.Drawing.Size(88, 23);
            this.btnSetPassword.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSetPassword.TabIndex = 0;
            this.btnSetPassword.Text = "设置";
            this.btnSetPassword.Click += new System.EventHandler(this.btnSetPassword_Click);
            // 
            // btnInvalidateCurrentPassword
            // 
            this.btnInvalidateCurrentPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInvalidateCurrentPassword.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInvalidateCurrentPassword.Location = new System.Drawing.Point(631, 19);
            this.btnInvalidateCurrentPassword.Name = "btnInvalidateCurrentPassword";
            this.btnInvalidateCurrentPassword.Size = new System.Drawing.Size(88, 23);
            this.btnInvalidateCurrentPassword.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInvalidateCurrentPassword.TabIndex = 0;
            this.btnInvalidateCurrentPassword.Text = "取消当前密码";
            this.btnInvalidateCurrentPassword.Click += new System.EventHandler(this.btnInvalidateCurrentPassword_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(359, 19);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(29, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "密码";
            // 
            // tbPassword
            // 
            // 
            // 
            // 
            this.tbPassword.Border.Class = "TextBoxBorder";
            this.tbPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPassword.Location = new System.Drawing.Point(393, 19);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.PreventEnterBeep = true;
            this.tbPassword.Size = new System.Drawing.Size(100, 21);
            this.tbPassword.TabIndex = 1;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(181, 19);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 23);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "结束日期";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 19);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "起始日期";
            // 
            // CheckPasswordSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 231);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.Name = "CheckPasswordSetting";
            this.Text = "CheckPasswordSetting";
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnSendEMails;
        private System.Windows.Forms.Button btnViewPwd;
        private System.Windows.Forms.DateTimePicker dtpFinish;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private DevComponents.DotNetBar.ButtonX btnSetPassword;
        private DevComponents.DotNetBar.ButtonX btnInvalidateCurrentPassword;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPassword;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}