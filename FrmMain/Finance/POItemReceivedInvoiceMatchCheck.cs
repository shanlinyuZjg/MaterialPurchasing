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


namespace Global.Finance
{
    public partial class POItemReceivedInvoiceMatchCheck : Office2007Form
    {
        string UserID = string.Empty;
        public POItemReceivedInvoiceMatchCheck(string userID)
        {
            InitializeComponent();
            UserID = userID;
            this.EnableGlass = false;
        }
    }
}
