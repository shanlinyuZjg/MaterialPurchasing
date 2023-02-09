using Global.Helper;
using SoftBrands.FourthShift.Transaction;
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
    public partial class PurchaseOrderClose : Form
    {
        public PurchaseOrderClose()
        {
            InitializeComponent();
        }
        private string FsUser;
        private string FsPassword;
        public PurchaseOrderClose(string FSuser,string FSpassword)
        {
            InitializeComponent();
            FsUser = FSuser;
            FsPassword = FSpassword;
        }
        private void PurchaseOrderClose_Load(object sender, EventArgs e)
        {

        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            string strSelect = "SELECT b.POCreatedDate as '订单头创建日期' ,b.PONumber as '采购订单号',b.Buyer as '下单员',a.POLineNumberString as '行号',c.ItemNumber as '物料编码',c.ItemDescription as '物料描述',a.LineItemOrderedQuantity as '订单数量',a.ReceivedPercent as '入库百分比',a.ReceiptQuantity as '接收数量',a.RequiredDate as '需求日期',c.InspectionLeadTimeDays as '提前期','' as '承诺交货日'  FROM [dbo].[_NoLock_FS_POLine] as a INNER JOIN [dbo].[_NoLock_FS_POHeader] as b on a.POHeaderKey=b.POHeaderKey INNER JOIN [dbo].[_NoLock_FS_Item] as c on a.ItemKey=c.ItemKey    where a.ReceivedPercent >=" + TbPercent.Text.Trim()+ " and a.POLineStatus=4 and b.Buyer ='"+FsUser+"'  ";
            if (CbDate.Checked)
            {
                strSelect += " and b.POCreatedDate >= '" + dateTimePicker1.Value + "' and b.POCreatedDate < '" + dateTimePicker2.Value+"'";
            }
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSelect);
            dataGridView1.DataSource = dtTemp;
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            dataGridView1.Rows[e.RowIndex].Cells["Check"].Value = !Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Check"].Value);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                this.dataGridView1.Rows[i].Cells["Check"].Value = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                this.dataGridView1.Rows[i].Cells["Check"].Value = false;
            }
        }

        private void BtnPromise_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["Check"].Value))
                {
                    this.dataGridView1.Rows[i].Cells["承诺交货日"].Value = (Convert.ToDateTime(dataGridView1.Rows[i].Cells["需求日期"].Value).AddDays(-Convert.ToInt32(dataGridView1.Rows[i].Cells["提前期"].Value))).ToString("MMddyy");
                }
            }
        }

        private void BtnOrderClose_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["Check"].Value))
                {
                    string poNumber = dataGridView1.Rows[i].Cells["采购订单号"].Value.ToString().Trim();
                    string lineNumber = dataGridView1.Rows[i].Cells["行号"].Value.ToString().Trim();
                    string itemNumber = dataGridView1.Rows[i].Cells["物料编码"].Value.ToString().Trim();
                    string promisedDateOld = dataGridView1.Rows[i].Cells["承诺交货日"].Value.ToString().Trim();
                    string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set LineStatus = 5  Where PONumber = '" + poNumber + "' and LineNumber ='"+ lineNumber + "'";
                    FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FsUser, FsPassword);

                    POMT12 myPomt12 = new POMT12();
                    myPomt12.PONumber.Value = poNumber;
                    myPomt12.POLineNumber.Value = lineNumber;
                    myPomt12.ItemNumber.Value = itemNumber;
                    //myPomt12.PromisedDate.Value = promisedDate;
                    myPomt12.PromisedDateOld.Value = promisedDateOld;
                    myPomt12.POLineSubType.Value = "L";
                    myPomt12.POLineStatus.Value = "5";
                    int AllSuccess = 1;
                    if (FSFunctionLib.fstiClient.ProcessId(myPomt12, null))
                    {
                        if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                        {
                            //Custom.MsgEx("关闭成功！");
                            
                        }
                        else
                        {
                            MessageBox.Show("四班关闭成功，记录修改失败！");
                        }

                    }
                    else
                    {
                        //MessageBox.Show("关闭失败");
                        AllSuccess = 0;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    FSFunctionLib.FSExit();
                    if (AllSuccess == 1)
                    {
                        MessageBox.Show("全部修改成功");
                    }
                    else
                    {
                        MessageBox.Show("部分修改失败，已红色标示。");
                    }
                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, ((DataGridView)sender).RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), ((DataGridView)sender).RowHeadersDefaultCellStyle.Font, rectangle, ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
