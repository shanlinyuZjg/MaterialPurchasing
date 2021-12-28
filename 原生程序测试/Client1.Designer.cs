namespace 原生程序测试
{
    partial class Client1
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
            this.text_log2 = new System.Windows.Forms.RichTextBox();
            this.text_log1 = new System.Windows.Forms.RichTextBox();
            this.text_port = new System.Windows.Forms.TextBox();
            this.text_ip = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnSendDataTable = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // text_log2
            // 
            this.text_log2.Location = new System.Drawing.Point(26, 223);
            this.text_log2.Name = "text_log2";
            this.text_log2.Size = new System.Drawing.Size(521, 107);
            this.text_log2.TabIndex = 8;
            this.text_log2.Text = "";
            // 
            // text_log1
            // 
            this.text_log1.Location = new System.Drawing.Point(26, 100);
            this.text_log1.Name = "text_log1";
            this.text_log1.Size = new System.Drawing.Size(521, 96);
            this.text_log1.TabIndex = 9;
            this.text_log1.Text = "";
            // 
            // text_port
            // 
            this.text_port.Location = new System.Drawing.Point(229, 21);
            this.text_port.Name = "text_port";
            this.text_port.Size = new System.Drawing.Size(100, 21);
            this.text_port.TabIndex = 6;
            this.text_port.Text = "8888";
            // 
            // text_ip
            // 
            this.text_ip.Location = new System.Drawing.Point(26, 21);
            this.text_ip.Name = "text_ip";
            this.text_ip.Size = new System.Drawing.Size(155, 21);
            this.text_ip.TabIndex = 7;
            this.text_ip.Text = "192.168.43.62";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(567, 278);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(498, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(405, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(26, 379);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(521, 208);
            this.dgv.TabIndex = 10;
            // 
            // btnSendDataTable
            // 
            this.btnSendDataTable.Location = new System.Drawing.Point(567, 336);
            this.btnSendDataTable.Name = "btnSendDataTable";
            this.btnSendDataTable.Size = new System.Drawing.Size(96, 23);
            this.btnSendDataTable.TabIndex = 11;
            this.btnSendDataTable.Text = "发送DataTable";
            this.btnSendDataTable.UseVisualStyleBackColor = true;
            this.btnSendDataTable.Click += new System.EventHandler(this.btnSendDataTable_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(579, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Client1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 623);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSendDataTable);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.text_log2);
            this.Controls.Add(this.text_log1);
            this.Controls.Add(this.text_port);
            this.Controls.Add(this.text_ip);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConnect);
            this.Name = "Client1";
            this.Text = "Client1";
            this.Load += new System.EventHandler(this.Client1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox text_log2;
        private System.Windows.Forms.RichTextBox text_log1;
        private System.Windows.Forms.TextBox text_port;
        private System.Windows.Forms.TextBox text_ip;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnSendDataTable;
        private System.Windows.Forms.Button button1;
    }
}