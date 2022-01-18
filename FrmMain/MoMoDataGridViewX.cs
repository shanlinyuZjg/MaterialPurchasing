using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace Global
{

    public partial class MoDataGridView : DevComponents.DotNetBar.Controls.DataGridViewX
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                this.OnKeyPress(new KeyPressEventArgs('f'));
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                this.OnKeyPress(new KeyPressEventArgs('e'));
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                this.OnKeyPress(new KeyPressEventArgs('d'));
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    //public partial class MoMoDataGridViewX : Component
    //{
    //    public MoMoDataGridViewX()
    //    {
    //        InitializeComponent();
    //    }

    //    public MoMoDataGridViewX(IContainer container)
    //    {
    //        container.Add(this);

    //        InitializeComponent();
    //    }
    //}
}
