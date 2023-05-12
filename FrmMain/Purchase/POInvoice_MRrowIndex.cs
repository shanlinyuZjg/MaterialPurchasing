using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Purchase
{
    public partial class POInvoice_MRrowIndex : Form
    {
        public POInvoice_MRrowIndex()
        {
            InitializeComponent();
        }

        private void POInvoice_MRrowIndex_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {       
            if (e.KeyCode != Keys.Enter) return;
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;
            this.Tag = textBox1.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }
    }
}
