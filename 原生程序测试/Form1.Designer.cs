namespace 原生程序测试
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tbCn = new System.Windows.Forms.TextBox();
            this.tbLatin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "转换";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(34, 93);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(269, 337);
            this.rtbResult.TabIndex = 1;
            this.rtbResult.Text = "";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(131, 30);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(172, 21);
            this.tbPath.TabIndex = 2;
            this.tbPath.Text = "c:\\head.jpg";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(546, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tbCn
            // 
            this.tbCn.Location = new System.Drawing.Point(431, 64);
            this.tbCn.Name = "tbCn";
            this.tbCn.Size = new System.Drawing.Size(333, 21);
            this.tbCn.TabIndex = 2;
            // 
            // tbLatin
            // 
            this.tbLatin.Location = new System.Drawing.Point(431, 116);
            this.tbLatin.Name = "tbLatin";
            this.tbLatin.Size = new System.Drawing.Size(333, 21);
            this.tbLatin.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(392, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "中文";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(372, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "拉丁字符";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(505, 218);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button2";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 446);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbLatin);
            this.Controls.Add(this.tbCn);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbCn;
        private System.Windows.Forms.TextBox tbLatin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
    }
}