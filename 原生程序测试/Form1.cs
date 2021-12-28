using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 原生程序测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(tbPath.Text));
            rtbResult.Text = base64.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            List<int> listInter = new List<int>();
            List<int> listLeft = new List<int>();

            list1.Add(1);
            list1.Add(2);
            list1.Add(3);
            list1.Add(4);
            list1.Add(5);
            list1.Add(6);
            list2.Add(2);
            list2.Add(3);
            list2.Add(4);
            list2.Add(9);
            list2.Add(10);

            listInter = list1.Intersect(list2).ToList(); // gives me an error.
            listLeft = list2.Except(listInter).ToList();
            string s = string.Join(",", listLeft.ToArray());
            MessageBox.Show(s);
        }
    }
}
