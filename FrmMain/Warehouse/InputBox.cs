using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Warehouse
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        public InputBox(string label)
        {
            InitializeComponent();
            label1.Text = label;

            //this.AcceptButton = this.button1;
            //this.CancelButton = this.button2;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        public InputBox(string label, string title)
        {
            InitializeComponent();
            label1.Text = label;
            this.Value = title;

            //this.AcceptButton = this.button1;
            //this.CancelButton = this.button2;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        //确定
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Value = textBox1.Text;
            this.Close();
        }
        private void InputBox_Load(object sender, EventArgs e)
        {
            //textBox1.Focus();
            textBox1.Text = Value;
            //让文本框获取焦点
            this.textBox1.Focus();
            //设置光标的位置到文本尾
            this.textBox1.Select(this.textBox1.TextLength, 0);
            //滚动到控件光标处
            this.textBox1.ScrollToCaret();
        }

        public string Value { get; set; }
    }
}
