using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global.Helper;
using Global;


namespace Global.Warehouse
{
    public partial class BatchPrint : Office2007Form
    {
        DataTable Dt= new DataTable();
        public BatchPrint(DataTable dt)
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            Dt = dt;
        }
    }
}
