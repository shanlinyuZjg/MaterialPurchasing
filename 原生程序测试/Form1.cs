using SoftBrands.FourthShift.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Global.Helper;
using Global;

namespace 原生程序测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFSLoginTest_Click(object sender, EventArgs e)
        {
            if(FSFunctionLib.FSConfigFileInitialize("Z:\\mfgsys\\fs.cfg", tbUserId.Text.Trim(), tbPassword.Text.Trim()))
            {
                MessageBox.Show("登陆成功！");
            }
            else
            {
                MessageBox.Show("登陆失败！");
            }
        }

        private void btnFSLoginDisconnect_Click(object sender, EventArgs e)
        {
            FSFunctionLib.FSExit();
        }

        private void tbShortcutTest_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }


        private void tbShortcutTest_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Modifiers != 0)
            {
                if(e.Alt && e.KeyValue==114)
                {
                    MessageBox.Show("按下的是ALT+F3键！", "提示");
                }
            }
        }

        private void tbShortcutTest_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnPORVTest_Click(object sender, EventArgs e)
        {
            PORV("");
        }

        private bool PORV(string poNumber)//,DataGridViewRow dgvr)
        {     
            PORV01 porv01 = new PORV01();
            porv01.PONumber.Value = "PM-101518-001";
            porv01.POLineNumber.Value = "002";
            porv01.POLineUM.Value = "KG";
            porv01.POReceiptActionType.Value = "R";
            porv01.Stockroom1.Value = "M3";
            porv01.Bin1.Value = "01";
            porv01.InventoryCategory1.Value = "I";// dgvr.Cells["检验状态"].Value.ToString();
            porv01.InspectionCode1.Value = "N";
            porv01.ReceiptQuantityMove1.Value = "100";
            
            porv01.POLineType.Value = "P";
            porv01.ItemNumber.Value = "M05009";
            porv01.NewLot.Value = "Y";
            porv01.LotNumberAssignmentPolicy.Value = "C";
            porv01.LotNumberDefault.Value = "444";
            porv01.LotNumber.Value = "444";
            porv01.VendorLotNumber.Value = "441234";
            porv01.LotDescription.Value = "SHENGCHANSHANG";
            porv01.LotUserDefined5.Value = "666666";

            porv01.POReceiptDate.Value = "102518"; //DateTime.Now.ToString("MMddyy");//此处不确定
            porv01.PromisedDate.Value = "103018";//dgvr.Cells["需求日期FS"].Value.ToString();
            porv01.LotExpirationDate.Value = "093020";//dgvr.Cells["过期日期FS"].Value.ToString(); 

            string transactionString;
            transactionString = porv01.GetString(TransactionStringFormat.fsCDF);
            tbTest.Text = transactionString;

            try
            {
                if (FSFunctionLib.fstiClient.ProcessId(porv01, null))
                {
                    MessageBox.Show("四班中" + poNumber + "物料入库成功！", "提示");
                    return true;
                }
                else
                {
                    MessageBox.Show("四班中" + poNumber + "订单下达失败！", "提示");
                    FSFunctionLib.FSErrorMsg("四班异常信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常：" + ex.Message);
            }

            return false;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {

                long value = 0x532E320A;
                byte[] tmps = BitConverter.GetBytes(value);
            //     Single value2 = BitConverter.ToSingle(tmps, 0);


            string value2 = string.Empty;
            foreach(byte b in tmps)
            {
                value2 += Convert.ToString(b, 10);
            }
                MessageBox.Show(value2.ToString());           
                    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Sex", typeof(string)));
            dt.Columns.Add(new DataColumn("Age", typeof(string)));
            DataRow dr = null;
            for(int i = 0;i <= 5;i++)
            {
                dr = dt.NewRow();
                dr["Name"] = "小明"+i.ToString();
                dr["Sex"] = "性别"+ i.ToString();
                dr["Age"] =i.ToString() ;
                dt.Rows.Add(dr);
            }
       
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
         
            MessageBox.Show("CellClick,CellContentClikc共有" + i.ToString() + "行被选中");
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_CellClick(sender, e);
        }

        private void dgv_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
         
            MessageBox.Show("MouseUp共有" + i.ToString() + "行被选中");
        }

        private void dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
      
            MessageBox.Show("CellMouseLeave共有" + i.ToString() + "行被选中");
        }

        DataTable dtTemp = null;
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT
	                            VendorNumber,
	                            VendorName,
	                            ManufacturerNumber,
	                            ManufacturerName,
	                            ItemNumber,
	                            OperateDateTime,
	                            Operator,
	                            PONumber,
	                            LineNumberString,
	                            ForeignOrderNumber,
	                            DateTime
                            FROM PurchaseOrderByCMF";
          /*  dtTemp = FrmMain.Helper.SQLHelper.GetDataTable(GlobalSpace., sql);
            MessageBox.Show("共计" + dtTemp.Rows.Count.ToString() + "条数据");*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void btnContinuousAddItem_Click(object sender, EventArgs e)
        {

        }

        private bool AddItemBasicInfo()
        {

            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string appName = "供应办公程序";
            string appVersion = "1.0";
            if( CommonOperate.IsApplicationVersionValid("供应办公程序",appVersion))
            {
                MessageBox.Show("没问题！");
            }
            else
            {
                MessageBox.Show("you问题！需要升级！");
            }
        }
    }
}
