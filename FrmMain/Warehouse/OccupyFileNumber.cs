using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Global;
using Global.Helper;
using DevComponents.DotNetBar;


namespace Global.Warehouse
{
    public partial class OccupyFileNumber : Office2007Form
    {
        public OccupyFileNumber()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void tbFileNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOccupyFileNumber_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbProductName.Text) && !string.IsNullOrEmpty(tbInternalLotNumber.Text))
            {
                string fileNumber = GetFileNumber(StockUser.Stock);
                string sqlInsert = @"Insert Into EBR_ReceiveRecordForInspect (FileNumber,ItemDescription,InternalLotNumber,IsPreOccupied,Operator) Values ('" + fileNumber+"','"+tbProductName.Text.Trim()+"','"+tbInternalLotNumber.Text.Trim()+"',1,'"+StockUser.UserName+"')";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.EBRConnStr,sqlInsert))
                {
                    MessageBoxEx.Show("增加成功！", "提示");
                }
                else
                {
                    MessageBoxEx.Show("添加失败！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("产品名和公司批号不能为空！","提示");
            }           
        }

        private DataTable GetPreOccupied()
        {
            string sqlSelect = @"SELECT
	                                                FileNumber AS 受控流水号,ApplyDate AS 请验日期,
	                                                VendorName AS 供应商名,
	                                                ManufacturerName AS 生产商名,
	                                                ItemNumber AS 物料编码,
	                                                ItemDescription AS 品名,
	                                                LotNumber AS 批号,
	                                                InternalLotNumber AS 公司批号,
	                                                ManufacturedDate AS 生产日期,
	                                                ExpiredDate AS 过期日期,
	                                                ReceiveQuantity AS 入库数量,
	                                                PackageQuantity AS 整包件数,
	                                                PackageSpecification AS 包装规格,
	                                                PackageUM AS 总标示值,
	                                                PackageOdd AS 零头标示值,
	                                              Guid,Operator AS 库管员,ForeignNumber AS 联系单号,Id
                                                FROM
	                                                EBR_ReceiveRecordForInspect
                                                WHERE  IsPreOccupied = 1";
            return SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelect);
        }


        private string GetFileNumber(string area)
        {
            string filenumber = string.Empty;
            /******
             * WHO厂区的格式暂不启用
             * 
             */
            /*      if(area == "WHO")
                  {
                      // 文件编号固定格式：年度后2位 + 月份（2位）+003（仓库代码）+四位流水号，每个月1号开始从-0001开始。   
                  string sqlExistDay = @"Select Count(Id) From EBR_ReceiveRecordForInspect";
                      //2.判断当天记录是否存在，如果存在，则获取最新一条记录的文件编号进行处理；不存在则产生当天的第一条记录。
                      if (SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlExistDay))
                      {
                          string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Order By Id Desc";
                          string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest).ToString();
                          int number = Convert.ToInt32(lastFileNumber);
                          string newNumber = string.Empty;
                          if (number + 1 < 10)
                          {
                              newNumber = "000000" + (number + 1).ToString();
                          }
                          else if (number + 1 < 100)
                          {
                              newNumber = "00000" + (number + 1).ToString();
                          }
                          else if (number + 1 < 1000)
                          {
                              newNumber = "0000" + (number + 1).ToString();
                          }
                          else if (number + 1 < 10000)
                          {
                              newNumber = "000" + (number + 1).ToString();
                          }
                          else if (number + 1 < 100000)
                          {
                              newNumber = "00" + (number + 1).ToString();
                          }
                          else if (number + 1 < 1000000)
                          {
                              newNumber = "0" + (number + 1).ToString();
                          }
                          else
                          {
                              newNumber = (number + 1).ToString();
                          }
                          filenumber =  newNumber;                   
                      }
                      else
                      {
                          filenumber =  "0000001";
                      }
                  }
                  else
                  {*/
            // 文件编号固定格式：年度后2位 + 月份（2位）+003（仓库代码）+四位流水号，每个月1号开始从0001开。   

            if (DateTime.Now.ToString("dd") == "01")
            {
                string sqlSelectExist = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where ApplyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'";
                if (!SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlSelectExist))
                {
                    filenumber = DateTime.Now.ToString("yyMM") + "0030001";
                }
                else
                {
                    string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'  Order By Id Desc";
                    string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest).ToString();
                    int number = Convert.ToInt32(lastFileNumber.Substring(lastFileNumber.Length - 4, 4));
                    string newNumber = string.Empty;
                    if (number + 1 < 10)
                    {
                        newNumber = "000" + (number + 1).ToString();
                    }
                    else if (number + 1 < 100)
                    {
                        newNumber = "00" + (number + 1).ToString();
                    }
                    else if (number + 1 < 1000)
                    {
                        newNumber = "0" + (number + 1).ToString();
                    }
                    else
                    {
                        newNumber = (number + 1).ToString();
                    }
                    filenumber = DateTime.Now.ToString("yyMM") + "003" + newNumber;
                }
            }
            else
            {
                string sqlExistDay = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'";
                //2.判断当天记录是否存在，如果存在，则获取最新一条记录的文件编号进行处理；不存在则产生当天的第一条记录。
                if (SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlExistDay))
                {
                    string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'  Order By Id Desc";
                    string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest).ToString();
                    int number = Convert.ToInt32(lastFileNumber.Substring(lastFileNumber.Length - 4, 4));
                    string newNumber = string.Empty;
                    if (number + 1 < 10)
                    {
                        newNumber = "000" + (number + 1).ToString();
                    }
                    else if (number + 1 < 100)
                    {
                        newNumber = "00" + (number + 1).ToString();
                    }
                    else if (number + 1 < 1000)
                    {
                        newNumber = "0" + (number + 1).ToString();
                    }
                    else
                    {
                        newNumber = (number + 1).ToString();
                    }
                    filenumber = DateTime.Now.ToString("yyMM") + "003" + newNumber;
                }
                else
                {
                    filenumber = DateTime.Now.ToString("yyMM") + "0030001";
                }
            }
            //   }
            return filenumber;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlUpdate = @"Update EBR_ReceiveRecordForInspect Set  VendorName='"+dgvr.Cells["供应商名"].Value.ToString()+"', ManufacturerName= '"+dgvr.Cells["生产商名"].Value.ToString()+"', ItemNumber = '"+dgvr.Cells["物料编码"].Value.ToString()+"',  ItemDescription = '"+dgvr.Cells["品名"].Value.ToString()+"', LotNumber = '"+dgvr.Cells["批号"].Value.ToString()+"',InternalLotNumber = '"+dgvr.Cells["公司批号"].Value.ToString()+"', ManufacturedDate = '"+dgvr.Cells["生产日期"].Value.ToString()+"',ExpiredDate = '"+dgvr.Cells["过期日期"].Value.ToString()+"', ReceiveQuantity ="+Convert.ToDouble(dgvr.Cells["入库数量"].Value)+ ", PackageQuantity=" + Convert.ToDouble(dgvr.Cells["整包件数"].Value) + ",  PackageSpecification= '" + dgvr.Cells["包装规格"].Value.ToString()+"',  PackageUM = '"+dgvr.Cells["总标示值"].Value.ToString()+ "',PackageOdd =" + Convert.ToDouble(dgvr.Cells["零头标示值"].Value) + ", Guid='" + Guid.NewGuid().ToString("N")+"',Operator= '"+dgvr.Cells["库管员"].Value.ToString()+"',ForeignNumber = '"+dgvr.Cells["联系单号"].Value.ToString()+"' Where Id='"+dgvr.Cells["Id"].Value.ToString()+"'";
                    sqlList.Add(sqlUpdate);
                }
            }

            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.EBRConnStr,sqlList))
            {
                MessageBoxEx.Show("更新成功！", "提示");
            }
            else
            {
                MessageBoxEx.Show("更新失败！", "提示");
            }

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetPreOccupied();
            dgvDetail.Columns["受控流水号"].ReadOnly = true;
            dgvDetail.Columns["请验日期"].ReadOnly = true;
            dgvDetail.Columns["Id"].Visible = false;
        }
    }
}
