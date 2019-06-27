namespace 原生程序测试
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFSLoginTest = new System.Windows.Forms.Button();
            this.tbUserId = new System.Windows.Forms.TextBox();
            this.btnFSLoginDisconnect = new System.Windows.Forms.Button();
            this.tbShortcutTest = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPORVTest = new System.Windows.Forms.Button();
            this.tbTest = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.tbConvert = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnContinuousAddItem = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFSLoginTest
            // 
            this.btnFSLoginTest.Location = new System.Drawing.Point(95, 26);
            this.btnFSLoginTest.Name = "btnFSLoginTest";
            this.btnFSLoginTest.Size = new System.Drawing.Size(75, 23);
            this.btnFSLoginTest.TabIndex = 0;
            this.btnFSLoginTest.Text = "登录测试";
            this.btnFSLoginTest.UseVisualStyleBackColor = true;
            this.btnFSLoginTest.Click += new System.EventHandler(this.btnFSLoginTest_Click);
            // 
            // tbUserId
            // 
            this.tbUserId.Location = new System.Drawing.Point(95, 65);
            this.tbUserId.Name = "tbUserId";
            this.tbUserId.Size = new System.Drawing.Size(126, 21);
            this.tbUserId.TabIndex = 1;
            // 
            // btnFSLoginDisconnect
            // 
            this.btnFSLoginDisconnect.Location = new System.Drawing.Point(95, 128);
            this.btnFSLoginDisconnect.Name = "btnFSLoginDisconnect";
            this.btnFSLoginDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnFSLoginDisconnect.TabIndex = 0;
            this.btnFSLoginDisconnect.Text = "登陆断开";
            this.btnFSLoginDisconnect.UseVisualStyleBackColor = true;
            this.btnFSLoginDisconnect.Click += new System.EventHandler(this.btnFSLoginDisconnect_Click);
            // 
            // tbShortcutTest
            // 
            this.tbShortcutTest.Location = new System.Drawing.Point(12, 274);
            this.tbShortcutTest.Name = "tbShortcutTest";
            this.tbShortcutTest.Size = new System.Drawing.Size(355, 21);
            this.tbShortcutTest.TabIndex = 1;
            this.tbShortcutTest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbShortcutTest_KeyDown);
            this.tbShortcutTest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbShortcutTest_KeyPress);
            this.tbShortcutTest.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbShortcutTest_KeyUp);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(95, 92);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(126, 21);
            this.tbPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "UserId";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // btnPORVTest
            // 
            this.btnPORVTest.Location = new System.Drawing.Point(95, 173);
            this.btnPORVTest.Name = "btnPORVTest";
            this.btnPORVTest.Size = new System.Drawing.Size(75, 23);
            this.btnPORVTest.TabIndex = 3;
            this.btnPORVTest.Text = "PORV入库测试";
            this.btnPORVTest.UseVisualStyleBackColor = true;
            this.btnPORVTest.Click += new System.EventHandler(this.btnPORVTest_Click);
            // 
            // tbTest
            // 
            this.tbTest.Location = new System.Drawing.Point(12, 202);
            this.tbTest.Multiline = true;
            this.tbTest.Name = "tbTest";
            this.tbTest.Size = new System.Drawing.Size(355, 66);
            this.tbTest.TabIndex = 4;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(506, 45);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 5;
            this.btnConvert.Text = "button1";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // tbConvert
            // 
            this.tbConvert.Location = new System.Drawing.Point(332, 45);
            this.tbConvert.Name = "tbConvert";
            this.tbConvert.Size = new System.Drawing.Size(155, 21);
            this.tbConvert.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(506, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "获取总数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(643, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "插入数据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnContinuousAddItem
            // 
            this.btnContinuousAddItem.Location = new System.Drawing.Point(199, 173);
            this.btnContinuousAddItem.Name = "btnContinuousAddItem";
            this.btnContinuousAddItem.Size = new System.Drawing.Size(142, 23);
            this.btnContinuousAddItem.TabIndex = 8;
            this.btnContinuousAddItem.Text = "循环增加物料测试";
            this.btnContinuousAddItem.UseVisualStyleBackColor = true;
            this.btnContinuousAddItem.Click += new System.EventHandler(this.btnContinuousAddItem_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(506, 173);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "版本比较";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 390);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnContinuousAddItem);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbConvert);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.tbTest);
            this.Controls.Add(this.btnPORVTest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbShortcutTest);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserId);
            this.Controls.Add(this.btnFSLoginDisconnect);
            this.Controls.Add(this.btnFSLoginTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFSLoginTest;
        private System.Windows.Forms.TextBox tbUserId;
        private System.Windows.Forms.Button btnFSLoginDisconnect;
        private System.Windows.Forms.TextBox tbShortcutTest;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPORVTest;
        private System.Windows.Forms.TextBox tbTest;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox tbConvert;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnContinuousAddItem;
        private System.Windows.Forms.Button button3;
    }
}

