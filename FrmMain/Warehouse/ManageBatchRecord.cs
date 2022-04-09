using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global;
using Global.Helper;
using gregn6Lib;

namespace Global.Warehouse
{
    public partial class ManageBatchRecord : Office2007Form
    {
        public string UserName = string.Empty;
        public string UserId = string.Empty;
        public ManageBatchRecord(string id,string name)
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            UserName = name;
            UserId = id;
        }

        private void tbFileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if (!string.IsNullOrWhiteSpace(tbFileNumber.Text))
                {
                   dgvDetail.DataSource =  GetBatchRecord("FileNumber", tbFileNumber.Text.Trim());
                    dgvDetail.Columns["Guid"].Visible = false;
                    for (int i = 1; i < dgvDetail.Columns.Count; i++)
                    {
                        dgvDetail.Columns[i].ReadOnly = true;
                    }
                    this.Tag = true;
                }
            }
        }

        private void tbItemDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (!string.IsNullOrWhiteSpace(tbItemDesc.Text))
                {
                    dgvDetail.DataSource = GetBatchRecord("ItemDesc",tbItemDesc.Text.Trim());
                    dgvDetail.Columns["Guid"].Visible = false;
                    for (int i = 1; i < dgvDetail.Columns.Count; i++)
                    {
                        dgvDetail.Columns[i].ReadOnly = true;
                    }
                    this.Tag = true;
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Guid"].Value))
                {
                    string sqlUpdate = @"Update EBR_ReceiveRecordForInspect Set ";
                    /*
                    if(cbQuantity.Checked)
                    {
                        sqlUpdate += " ReceiveQuantity=" + Convert.ToDouble(dgvr.Cells["入库数量"].Value) + ",";
                    }
                    if(cbLotNumber.Checked)
                    {
                        sqlUpdate += " LotNumber='" + dgvr.Cells["批号"].Value.ToString()+"',InternalLotNumber='" + dgvr.Cells["公司批号"].Value.ToString() + "'";
                    }*/
                    string sqlCriteria = @" Where Guid = '"+ dgvr.Cells["Guid"].Value.ToString() + "'";
                    sqlList.Add(sqlUpdate + sqlCriteria);
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

        /* private DataTable GetBatchRecord(string type,string value)
            {
                string sqlSelect = @"SELECT
                                                        FileNumber AS 受控流水号,ApplyDate AS 请验日期,
                                                        VendorName AS 供应商名,
                                                        ManufacturerName AS 生产商名,
                                                        ItemNumber AS 物料编码,
                                                        ItemDescription AS 品名,
                                                        LineUM AS 单位,
                                                        LotNumber AS 批号,
                                                        InternalLotNumber AS 公司批号,
                                                        ManufacturedDate AS 生产日期,
                                                        ExpiredDate AS 过期日期,
                                                        ReceiveQuantity AS 入库数量,
                                                        PackageQuantity AS 整包件数,
                                                        PackageSpecification AS 包装规格,	                                                
                                                        PackageOdd AS 零头标示值,
                                                        PackageUM AS 整包件数单位,
                                                        FileTracedNumber AS 追溯文件编号,
                                                        FileEdition AS 版本,Guid,Operator AS 库管员,ForeignNumber AS 联系单号
                                                    FROM
                                                        EBR_ReceiveRecordForInspect
                                                    WHERE  ";
                string sqlCriteria = string.Empty;

                if(type == "FileNumber")
                {
                    sqlCriteria = " FileNumber='"+value+"' And Operator='"+UserName+"'";
                }
                else if(type == "Day")
                {
                    sqlCriteria = " ApplyDate='" + value + "' And Operator='" + UserName + "'";
                }
                else if(type == "Month")
                {
                    sqlCriteria = " Left(ApplyDate,7)='" + value + "' And Operator='" + UserName + "'";
                }
                else
                {
                    sqlCriteria = " ItemDescription Like '%"+value+ "%'  And Operator='"+ UserName + "'";
                }

                return SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelect+sqlCriteria);
            }*/

        private DataTable GetBatchRecord(string type, string value)
        {
            string sqlSelect = @"SELECT
	                                                FileNumber AS 受控流水号,ApplyDate AS 请验日期,
	                                                VendorName AS 供应商名,
	                                                ManufacturerName AS 生产商名,
	                                                ItemNumber AS 物料编码,
	                                                ItemDescription AS 品名,
                                                    LineUM AS 单位,
	                                                LotNumber AS 批号,
	                                                InternalLotNumber AS 公司批号,
	                                                ManufacturedDate AS 生产日期,
	                                                ExpiredDate AS 过期日期,
	                                                ReceiveQuantity AS 入库数量,
	                                                PackageQuantity AS 整包件数,
	                                                PackageSpecification AS 包装规格,	                                                
	                                                PackageOdd AS 零头标示值,
                                                    PackageUM AS 整包件数单位,
	                                                FileTracedNumber AS 追溯文件编号,
	                                                FileEdition AS 版本,EffectiveDate AS 生效日期,Guid,Operator AS 库管员,ForeignNumber AS 联系单号,QualityCheckStandard AS 检验标准,
                                            Checker as 复核人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
                              DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告
                                                FROM
	                                                EBR_ReceiveRecordForInspect
                                                WHERE  (Operator='" + UserName + "' or Operator='" + UserId + "|" + UserName + "') and ";
            string sqlCriteria = string.Empty;

            if (type == "FileNumber")
            {
                sqlCriteria = " FileNumber='" + value + "'";
            }
            else if (type == "Day")
            {
                sqlCriteria = " ApplyDate='" + value + "'";
            }
            else if (type == "Month")
            {
                sqlCriteria = " Left(ApplyDate,7)='" + value + "'";
            }
            else
            {
                sqlCriteria = " ItemDescription Like '%" + value + "%'";
            }
            
            return SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelect + sqlCriteria);
        }

        private void ManageBatchRecord_Load(object sender, EventArgs e)
        {
            this.Tag = false;
        }

        //private void btnPrintBatchRecord_Click(object sender, EventArgs e)
        //{
        //    int i = 0;
        //    List<string> guidList = new List<string>();
        //    DataTable dt0 = (DataTable)dgvDetail.DataSource;
        //    DataTable dtTemp = dt0.Clone();
        //    DataRow drTemp = dtTemp.NewRow();
        //    foreach (DataGridViewRow dgvr in dgvDetail.Rows)
        //    {
        //        if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
        //        {
        //            guidList.Add(dgvr.Cells["Guid"].Value.ToString());
        //            drTemp = (dgvr.DataBoundItem as DataRowView).Row;
        //            dtTemp.Rows.Add(drTemp.ItemArray);
        //        }
        //    }

        //    List<string> cmpList = new List<string>();
        //    cmpList.Add(guidList[0]);

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Guid");
        //    dt.Columns.Add("ApplyDate");
        //    dt.Columns.Add("ItemNumber");
        //    dt.Columns.Add("ItemDescription");
        //    dt.Columns.Add("VendorLotNumber");
        //    dt.Columns.Add("InternalLotNumber");
        //    dt.Columns.Add("MfgName");
        //    dt.Columns.Add("VendorName");
        //    dt.Columns.Add("PackageQuantity");
        //    dt.Columns.Add("Receiver");
        //    dt.Columns.Add("UM");
        //    dt.Columns.Add("Quantity");
        //    dt.Columns.Add("PackageOdd");
        //    dt.Columns.Add("PackageSpecification");
        //    dt.Columns.Add("MfgDate");
        //    dt.Columns.Add("ExpiredDate");
        //    dt.Columns.Add("PackageUM");
        //    dt.Columns.Add("FONumber");
        //    dt.Columns.Add("PONumber");
        //    dt.Columns.Add("LineNumber");
        //    dt.Columns.Add("FileNumber");

        //    foreach (DataRow dr in dtTemp.Rows)
        //    {
        //        DataRow drNew = dt.NewRow();
        //        drNew["Guid"] = dr["Guid"].ToString();
        //        drNew["FileNumber"] = dr["受控流水号"].ToString();
        //        drNew["ApplyDate"] = dr["请验日期"].ToString(); ;
        //        drNew["ItemNumber"] = dr["物料编码"].ToString();
        //        drNew["ItemDescription"] = dr["品名"];
        //        drNew["VendorLotNumber"] = drTemp["批号"];
        //        drNew["PackageUM"] = dr["整包件数单位"];
        //        drNew["InternalLotNumber"] = dr["公司批号"];
        //        drNew["MfgName"] = dr["生产商名"];
        //        drNew["VendorName"] = dr["供应商名"];
        //        drNew["PackageQuantity"] = dr["整包件数"];
        //        drNew["Receiver"] = dr["库管员"].ToString();
        //        drNew["UM"] = dr["单位"];
        //        drNew["Quantity"] = dr["入库数量"];
        //        drNew["PackageOdd"] = dr["零头标示值"];
        //        drNew["PackageSpecification"] = dr["包装规格"];
        //        drNew["MfgDate"] = dr["生产日期"];
        //        drNew["ExpiredDate"] = dr["过期日期"];
        //        drNew["FONumber"] = dr["联系单号"];
        //        dt.Rows.Add(drNew);
        //    }

        //    PrintBatchRecord pbr = new PrintBatchRecord(true, dt, "\\入库请验单.grf");
        //    pbr.ShowDialog();
        //}

        private void btnOccupyFileNumber_Click(object sender, EventArgs e)
        {
            Warehouse.OccupyFileNumber ofu = new OccupyFileNumber();
            ofu.ShowDialog();
        }

        private void btnView_Click_1(object sender, EventArgs e)
        {
            if(rbtnDay.Checked)
            {
                dgvDetail.DataSource = GetBatchRecord("Day", dtpDate.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                dgvDetail.DataSource = GetBatchRecord("Month", dtpDate.Value.ToString("yyyy-MM"));
            }
            dgvDetail.Columns["Guid"].Visible = false;

            for (int i = 1; i < dgvDetail.Columns.Count;i++)
            {
                dgvDetail.Columns[i].ReadOnly = true;
            }
            this.Tag = true;
        }

        /*      private void btnBatchPrint_Click(object sender, EventArgs e)
              {
                  DataTable dtTemp0 = (DataTable)dgvDetail.DataSource;
                  DataTable dtTemp = dtTemp0.Clone();

                  for (int m = 0; m < dgvDetail.Rows.Count; m++)
                  {
                      if (Convert.ToBoolean(dgvDetail.Rows[m].Cells["Check"].Value) == true)
                      {
                          DataRow dr = dtTemp.NewRow();
                          dr = (dgvDetail.Rows[m].DataBoundItem as DataRowView).Row;
                          dtTemp.Rows.Add(dr.ItemArray);
                      }
                  }

                  DataTable dt = new DataTable();
                  dt.Columns.Add("Guid");
                  dt.Columns.Add("ApplyDate");
                  dt.Columns.Add("ItemNumber");
                  dt.Columns.Add("ItemDescription");
                  dt.Columns.Add("VendorLotNumber");
                  dt.Columns.Add("InternalLotNumber");
                  dt.Columns.Add("MfgName");
                  dt.Columns.Add("VendorName");
                  dt.Columns.Add("PackageQuantity");
                  dt.Columns.Add("Receiver");
                  dt.Columns.Add("UM");
                  dt.Columns.Add("Quantity");
                  dt.Columns.Add("PackageOdd");
                  dt.Columns.Add("PackageSpecification");
                  dt.Columns.Add("MfgDate");
                  dt.Columns.Add("ExpiredDate");
                  dt.Columns.Add("PackageUM");
                  dt.Columns.Add("FONumber");
                  dt.Columns.Add("PONumber");
                  dt.Columns.Add("LineNumber");
                  dt.Columns.Add("FileNumber");

                  foreach (DataRow drTemp in dtTemp.Rows)
                  {
                      DataRow dr = dt.NewRow();
                      dr["Guid"] = drTemp["Guid"].ToString();
                      dr["ApplyDate"] = drTemp["请验日期"].ToString();
                      dr["FileNumber"] = drTemp["受控流水号"].ToString();
                      dr["ItemNumber"] = drTemp["物料编码"].ToString();
                      dr["ItemDescription"] = drTemp["品名"].ToString();
                      dr["VendorLotNumber"] = drTemp["批号"].ToString();
                      dr["FONumber"] = drTemp["联系单号"].ToString();
                      dr["PackageUM"] = drTemp["整包件数单位"].ToString();
                      dr["VendorLotNumber"] = drTemp["批号"];
                      dr["InternalLotNumber"] = drTemp["公司批号"];
                      dr["MfgName"] = drTemp["生产商名"];
                      dr["VendorName"] = drTemp["供应商名"];
                      dr["PackageQuantity"] = drTemp["整包件数"];
                      dr["Receiver"] = drTemp["库管员"].ToString();
                      dr["UM"] = drTemp["单位"];
                      dr["Quantity"] = drTemp["入库数量"];
                      dr["PackageOdd"] = drTemp["零头标示值"];
                      dr["PackageSpecification"] = drTemp["包装规格"];
                      dr["MfgDate"] = drTemp["生产日期"];
                      dr["ExpiredDate"] = drTemp["过期日期"];
                      dt.Rows.Add(dr.ItemArray);
                  }

                  DataView dv = dt.DefaultView;
                  DataTable dtFileNumber = dv.ToTable(true, "FileNumber");
                  double totalQuantity = 0;
                  double totalPackage = 0;
                  double totalOdd = 0;
                  DataTable dtNew = dt.Clone();
                  List<string> fileNumberList = new List<string>();
                  for (int i = 0; i < dtFileNumber.Rows.Count; i++)
                  {
                      DataRow[] drs = dt.Select("FileNumber='" + dtFileNumber.Rows[i]["FileNumber"].ToString() + "'");
                      DataRow drNew = dtNew.NewRow();
                      if (!fileNumberList.Contains(dtFileNumber.Rows[i]["FileNumber"].ToString()))
                      {
                          if (drs.Length > 1)
                          {
                              for (int j = 0; j < drs.Length; j++)
                              {
                                  totalQuantity += Convert.ToDouble(drs[j]["Quantity"]);
                                  totalPackage += Convert.ToDouble(drs[j]["PackageQuantity"]);
                                  totalOdd += Convert.ToDouble(drs[j]["PackageOdd"]);
                              }
                              drNew = drs[0];
                              drNew["Quantity"] = totalQuantity;
                              drNew["PackageQuantity"] = totalPackage;
                              drNew["PackageOdd"] = totalOdd;
                              dtNew.Rows.Add(drNew.ItemArray);
                          }
                          else
                          {
                              drNew = drs[0];
                              dtNew.Rows.Add(drNew.ItemArray);
                          }
                      }
                      else
                      {
                          continue;
                      }
                  }

                  GridppReport Report = new GridppReport();
                  //加载报表文件
                  Report.LoadFromFile(Application.StartupPath + "\\入库请验单.grf");
                  DataRow DR = dtNew.NewRow();
                  foreach (DataRow dr in dtNew.Rows)
                  {
                      DR = dr;
                      DataRow[] drs = dtNew.Select("FileNumber='" + DR["FileNumber"].ToString() + "'");             
                      Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
                      Report.ParameterByName("FileNumber").AsString = DR["FileNumber"].ToString();
                      Report.ParameterByName("ItemNumber").AsString = DR["ItemNumber"].ToString();
                      Report.ParameterByName("ItemDescription").AsString = DR["ItemDescription"].ToString();
                      Report.ParameterByName("VendorLotNumber").AsString = DR["VendorLotNumber"].ToString();
                      Report.ParameterByName("InternalLotNumber").AsString = DR["InternalLotNumber"].ToString();
                      Report.ParameterByName("MfgName").AsString = DR["MfgName"].ToString();
                      Report.ParameterByName("VendorName").AsString = DR["VendorName"].ToString();
                      Report.ParameterByName("Packages").AsString = DR["PackageQuantity"].ToString();
                      Report.ParameterByName("UM").AsString = "总标示值（" + DR["UM"].ToString() + "）";
                      Report.ParameterByName("PackageOddUM").AsString = "零头标示值（" + DR["UM"].ToString() + "）";
                      Report.ParameterByName("PackageUM").AsString = "整包件数（" + DR["PackageUM"].ToString() + "）";
                      Report.ParameterByName("Quantity").AsString =  DR["Quantity"].ToString();
                      Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString();
                      Report.ParameterByName("PackageOdd").AsString = DR["PackageOdd"].ToString();
                      Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString();
                      Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString();
                      Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
                      Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
                      Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
                      Report.ParameterByName("FONumber").AsString = DR["FONumber"].ToString();
                      //默认直接打印
                      Report.Print(false);
                  }


              }**/

        private void btnBatchPrint_Click(object sender, EventArgs e)
        {
            if (dgvDetail.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前无可用的记录！", "提示");
                return;
            }
            if (!Convert.ToBoolean(this.Tag))
            { MessageBoxEx.Show("请刷新后打印");return; }
            DataTable dtTemp0 = (DataTable)dgvDetail.DataSource;
            DataTable dtTemp = dtTemp0.Clone();

            for (int m = 0; m < dgvDetail.Rows.Count; m++)
            {
                if (Convert.ToBoolean(dgvDetail.Rows[m].Cells["Check"].Value) == true)
                {
                    DataRow dr = dtTemp.NewRow();
                    dr = (dgvDetail.Rows[m].DataBoundItem as DataRowView).Row;
                    dtTemp.Rows.Add(dr.ItemArray);
                }
            }
            if (dtTemp.Rows.Count == 0) { MessageBoxEx.Show("未选中记录！", "提示"); return; }
            DataTable dt = new DataTable();
            dt.Columns.Add("Guid");
            dt.Columns.Add("ApplyDate");
            dt.Columns.Add("ItemNumber");
            dt.Columns.Add("ItemDescription");
            dt.Columns.Add("VendorLotNumber");
            dt.Columns.Add("InternalLotNumber");
            dt.Columns.Add("MfgName");
            dt.Columns.Add("VendorName");
            dt.Columns.Add("PackageQuantity");
            dt.Columns.Add("Receiver");
            dt.Columns.Add("UM");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("PackageOdd");
            dt.Columns.Add("PackageSpecification");
            dt.Columns.Add("MfgDate");
            dt.Columns.Add("ExpiredDate");
            dt.Columns.Add("PackageUM");
            dt.Columns.Add("FONumber");
            dt.Columns.Add("PONumber");
            dt.Columns.Add("LineNumber");
            dt.Columns.Add("FileNumber");
            dt.Columns.Add("FileTracedNumber");
            dt.Columns.Add("FileEdition");
            dt.Columns.Add("EffectiveDate");
            dt.Columns.Add("QualityCheckStandard");
            //Checker as 复核人,Receiver as 接收请验人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
            //DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告
            dt.Columns.Add("Checker");
            dt.Columns.Add("ReceiverAPP");
            dt.Columns.Add("Conclusion");
            dt.Columns.Add("ConclusionText");
            dt.Columns.Add("IsAnyDeviation");
            dt.Columns.Add("DeviationNumber");
            dt.Columns.Add("deviationIsClosed");
            dt.Columns.Add("IsReport");
            dt.Columns.Add("QualityManageIdea");
            dt.Columns.Add("Sign");
            dt.Columns.Add("SignDate");
            dt.Columns.Add("IsRequireClean");
            dt.Columns.Add("PollutionSituation");
            dt.Columns.Add("CleanMethod");
            dt.Columns.Add("IsComplete");
            dt.Columns.Add("DamageSituation");
            dt.Columns.Add("CauseInvestigation1");
            dt.Columns.Add("IsSealed");
            dt.Columns.Add("UnsealedCondition");
            dt.Columns.Add("CauseInvestigation2");
            dt.Columns.Add("IsAnyMaterialWithPollutionRisk");
            dt.Columns.Add("IsAnyProblemAffectedMaterialQuality");
            dt.Columns.Add("Question");
            dt.Columns.Add("CauseInvestigation3");
            dt.Columns.Add("LotNumberType");
            dt.Columns.Add("IsApprovedVendor");
            dt.Columns.Add("StorageCondition");
            dt.Columns.Add("TransportTemperature");
            dt.Columns.Add("TransportCondition");
            dt.Columns.Add("TransportationControlRecord");
            dt.Columns.Add("Shape");
            dt.Columns.Add("Colour");
            dt.Columns.Add("Font");
            dt.Columns.Add("RoughWeight");
            dt.Columns.Add("NetWeight");
            dt.Columns.Add("ApprovalNumber");
            dt.Columns.Add("ReportType");
            dt.Columns.Add("Report");
            foreach (DataRow drTemp in dtTemp.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Guid"] = drTemp["Guid"].ToString();
                dr["ApplyDate"] = drTemp["请验日期"].ToString();
                dr["FileNumber"] = drTemp["受控流水号"].ToString();
                dr["ItemNumber"] = drTemp["物料编码"].ToString();
                dr["ItemDescription"] = drTemp["品名"].ToString();
                dr["VendorLotNumber"] = drTemp["批号"].ToString();
                dr["FONumber"] = drTemp["联系单号"].ToString();
                dr["PackageUM"] = drTemp["整包件数单位"].ToString();
                dr["VendorLotNumber"] = drTemp["批号"];
                dr["InternalLotNumber"] = drTemp["公司批号"];
                dr["MfgName"] = drTemp["生产商名"];
                dr["VendorName"] = drTemp["供应商名"];
                dr["PackageQuantity"] = drTemp["整包件数"];
                dr["Receiver"] = drTemp["库管员"].ToString();
                dr["UM"] = drTemp["单位"];
                dr["Quantity"] = drTemp["入库数量"];
                dr["PackageOdd"] = drTemp["零头标示值"];
                dr["PackageSpecification"] = drTemp["包装规格"];
                dr["MfgDate"] = drTemp["生产日期"];
                dr["ExpiredDate"] = drTemp["过期日期"];

                dr["FileTracedNumber"] = drTemp["追溯文件编号"] == DBNull.Value ? "" : drTemp["追溯文件编号"];
                dr["FileEdition"] = drTemp["版本"] == DBNull.Value ? "" : drTemp["版本"];
                dr["EffectiveDate"] = drTemp["生效日期"] == DBNull.Value ? "" : drTemp["生效日期"];
                //新增字段
                dr["QualityCheckStandard"] = drTemp["检验标准"];
                dr["Checker"] = drTemp["复核人"];
                //dr["ReceiverAPP"] = drTemp["接收请验人"];
                dr["Conclusion"] = drTemp["结论"];
                dr["ConclusionText"] = drTemp["结论其他内容"];
                dr["IsAnyDeviation"] = drTemp["物料验收过程是否出现偏差"];
                dr["DeviationNumber"] = drTemp["偏差编号"];
                dr["deviationIsClosed"] = drTemp["偏差是否已处理关闭"];
                dr["IsReport"] = drTemp["问题是否已报告"];
                dr["QualityManageIdea"] = drTemp["质量管理部门意见"];
                dr["Sign"] = drTemp["签名"];
                dr["SignDate"] = drTemp["签名日期"];
                dr["IsRequireClean"] = drTemp["是否需要清洁"];
                dr["PollutionSituation"] = drTemp["污染情况"];
                dr["CleanMethod"] = drTemp["清洁方式"];
                dr["IsComplete"] = drTemp["外包装是否完整"];
                dr["DamageSituation"] = drTemp["损坏情况"];
                dr["CauseInvestigation1"] = drTemp["原因调查1"];
                dr["IsSealed"] = drTemp["外包装是否密封"];
                dr["UnsealedCondition"] = drTemp["不密封情况"];
                dr["CauseInvestigation2"] = drTemp["原因调查2"];
                dr["IsAnyMaterialWithPollutionRisk"] = drTemp["运输工具内是否存在造成污染交叉污染的物料"];
                dr["IsAnyProblemAffectedMaterialQuality"] = drTemp["是否有其他可能影响物料质量的问题"];
                dr["Question"] = drTemp["问题"];
                dr["CauseInvestigation3"] = drTemp["原因调查3"];
                dr["LotNumberType"] = drTemp["批号类型"];
                dr["IsApprovedVendor"] = drTemp["是否为质量管理部门批准的供应商"];
                dr["StorageCondition"] = drTemp["规定贮存条件"];
                dr["TransportTemperature"] = drTemp["运输条件检查结果"];
                dr["TransportCondition"] = drTemp["运输条件是否符合"];
                dr["TransportationControlRecord"] = drTemp["是否有运输条件控制记录"];
                dr["Shape"] = drTemp["形状是否一致"];
                dr["Colour"] = drTemp["颜色是否一致"];
                dr["Font"] = drTemp["字体是否一致"];
                dr["RoughWeight"] = drTemp["有无毛重"];
                dr["NetWeight"] = drTemp["有无净重"];
                dr["ApprovalNumber"] = drTemp["有无批准文号"];
                dr["ReportType"] = drTemp["报告类型"];
                dr["Report"] = drTemp["有无报告"];
                dt.Rows.Add(dr.ItemArray);
            }

            DataView dv = dt.DefaultView;
            DataTable dtFileNumber = dv.ToTable(true, "FileNumber");
            double totalQuantity = 0;
            double totalPackage = 0;
            double totalOdd = 0;
            DataTable dtNew = dt.Clone();
            List<string> fileNumberList = new List<string>();
            for (int i = 0; i < dtFileNumber.Rows.Count; i++)
            {
                DataRow[] drs = dt.Select("FileNumber='" + dtFileNumber.Rows[i]["FileNumber"].ToString() + "'");
                DataRow drNew = dtNew.NewRow();
                if (!fileNumberList.Contains(dtFileNumber.Rows[i]["FileNumber"].ToString()))
                {
                    if (drs.Length > 1)
                    {
                        for (int j = 0; j < drs.Length; j++)
                        {
                            totalQuantity += Convert.ToDouble(drs[j]["Quantity"]);
                            totalPackage += Convert.ToDouble(drs[j]["PackageQuantity"]);
                            totalOdd += Convert.ToDouble(drs[j]["PackageOdd"]);
                        }
                        drNew = drs[0];
                        drNew["Quantity"] = totalQuantity;
                        drNew["PackageQuantity"] = totalPackage;
                        drNew["PackageOdd"] = totalOdd;
                        dtNew.Rows.Add(drNew.ItemArray);
                    }
                    else
                    {
                        drNew = drs[0];
                        dtNew.Rows.Add(drNew.ItemArray);
                    }
                }
                else
                {
                    continue;
                }
            }

            GridppReport Report = new GridppReport();
            //加载报表文件
            Report.LoadFromFile(Application.StartupPath + "\\入库请验单.grf");
            DataRow DR = dtNew.NewRow();
            String Gang = "-";
            foreach (DataRow dr in dtNew.Rows)
            {
                DR = dr;
                DataRow[] drs = dtNew.Select("FileNumber='" + DR["FileNumber"].ToString() + "'");
                Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
                Report.ParameterByName("FileNumber").AsString = DR["FileNumber"].ToString();
                Report.ParameterByName("ItemNumber").AsString = DR["ItemNumber"].ToString();
                Report.ParameterByName("ItemDescription").AsString = DR["ItemDescription"].ToString();
                Report.ParameterByName("VendorLotNumber").AsString = DR["VendorLotNumber"].ToString();
                Report.ParameterByName("InternalLotNumber").AsString = DR["InternalLotNumber"].ToString();
                Report.ParameterByName("MfgName").AsString = DR["MfgName"].ToString();
                Report.ParameterByName("VendorName").AsString = DR["VendorName"].ToString();
                Report.ParameterByName("Packages").AsString = DR["PackageQuantity"].ToString();
                Report.ParameterByName("UM").AsString = "总标示值（" + DR["UM"].ToString() + "）";
                Report.ParameterByName("PackageOddUM").AsString = "零头标示值（" + DR["UM"].ToString() + "）";
                //Report.ParameterByName("PackageUM").AsString = "整包件数（" + DR["PackageUM"].ToString() + "）";
                Report.ParameterByName("PackageUM").AsString = "整包件数（" + (DR["PackageUM"].ToString().Trim().Length == 0 ? "  " : DR["PackageUM"].ToString().Trim()) + "）";
                Report.ParameterByName("Quantity").AsString = DR["Quantity"].ToString();
                Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString();
                Report.ParameterByName("PackageOdd").AsString = DR["PackageOdd"].ToString();
                //Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString();
                //Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString();
                if (DR["MfgDate"] == DBNull.Value)
                { Report.ParameterByName("ywMfgDate").AsString = "生产日期（□有☑无）"; }
                else if (string.IsNullOrWhiteSpace(DR["MfgDate"].ToString()))
                { Report.ParameterByName("ywMfgDate").AsString = "生产日期（□有☑无）"; }
                else
                {
                    Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString().Trim().Replace("-", ".");
                    Report.ParameterByName("ywMfgDate").AsString = "生产日期（☑有□无）";
                }

                if (DR["ExpiredDate"] == DBNull.Value)
                { Report.ParameterByName("ywExpiredDate").AsString = "有效期/复验期（□有☑无）"; }
                else if (string.IsNullOrWhiteSpace(DR["ExpiredDate"].ToString()))
                { Report.ParameterByName("ywExpiredDate").AsString = "有效期/复验期（□有☑无）"; }
                else
                {
                    Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString().Trim().Replace("-", ".");
                    Report.ParameterByName("ywExpiredDate").AsString = "有效期/复验期（☑有□无）";
                }

                //Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
                //Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
                //Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
                Report.ParameterByName("FileTracedNumber").AsString = DR["FileTracedNumber"].ToString().Trim();
                Report.ParameterByName("FileEdition").AsString = DR["FileEdition"].ToString().Trim();
                Report.ParameterByName("EffectiveDate").AsString = DR["EffectiveDate"].ToString().Trim();
                //Report.ParameterByName("FONumber").AsString = DR["FONumber"].ToString();
                Report.ParameterByName("FONumber").AsString = DR["FONumber"] == DBNull.Value ? Gang : (String.IsNullOrWhiteSpace(DR["FONumber"].ToString()) ? Gang : DR["FONumber"].ToString());
                //新增字段
                //if (DR["ReceiverAPP"] != DBNull.Value)  //手写
                //    Report.ParameterByName("ReceiverAPP").AsString = DR["ReceiverAPP"].ToString();
                //if (DR["Checker"] != DBNull.Value)
                //    Report.ParameterByName("Checker").AsString = DR["Checker"].ToString();

                if (DR["QualityCheckStandard"] != DBNull.Value)
                    Report.ParameterByName("QualityCheckStandard").AsString = DR["QualityCheckStandard"].ToString().Trim();

                if (DR["Conclusion"] == DBNull.Value)
                {
                    Report.ParameterByName("Conclusion").AsString = "结论： □接收入库  □退回供应商  □其他：______________________________________________________";
                }
                else if (DR["Conclusion"].ToString() == "接收入库")
                {
                    Report.ParameterByName("Conclusion").AsString = "结论： ☑接收入库  □退回供应商  □其他：______________________________________________________";
                }
                else if (DR["Conclusion"].ToString() == "退回供应商")
                {
                    Report.ParameterByName("Conclusion").AsString = "结论： □接收入库  ☑退回供应商  □其他：______________________________________________________";
                }
                else if (DR["Conclusion"].ToString() == "其他")
                {
                    Report.ParameterByName("Conclusion").AsString = "结论： □接收入库  □退回供应商  ☑其他：" + (DR["ConclusionText"] == DBNull.Value ? "" : DR["ConclusionText"].ToString().Trim());
                }
                else
                { Report.ParameterByName("Conclusion").AsString = "结论： □接收入库  □退回供应商  □其他：______________________________________________________"; }
                int IsAnyDeviationBJ = 0;
                if (DR["IsAnyDeviation"] == DBNull.Value)
                {
                    Report.ParameterByName("IsAnyDeviation").AsString = "物料验收过程是否出现偏差（□是  □否）";
                }
                else if (DR["IsAnyDeviation"].ToString() == "是")
                {
                    Report.ParameterByName("IsAnyDeviation").AsString = "物料验收过程是否出现偏差（☑是  □否）";
                }
                else if (DR["IsAnyDeviation"].ToString() == "否")
                {
                    Report.ParameterByName("IsAnyDeviation").AsString = "物料验收过程是否出现偏差（□是  ☑否）";
                    IsAnyDeviationBJ = 1;
                }
                else
                {
                    Report.ParameterByName("IsAnyDeviation").AsString = "物料验收过程是否出现偏差（□是  □否）";
                }
                if (IsAnyDeviationBJ == 1)
                {
                    Report.ParameterByName("DeviationNumber").AsString = Gang;
                    Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（☒是  ☒否）";
                }
                else
                {
                    Report.ParameterByName("DeviationNumber").AsString = DR["DeviationNumber"] == DBNull.Value ? "" : DR["DeviationNumber"].ToString().Trim();

                    if (DR["deviationIsClosed"] == DBNull.Value)
                    {
                        Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（□是   □否）";
                    }
                    else if (DR["deviationIsClosed"].ToString() == "是")
                    {
                        Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（☑是  □否）";
                    }
                    else if (DR["deviationIsClosed"].ToString() == "否")
                    {
                        Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（□是  ☑否）";
                    }
                    else
                    {
                        Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（□是  □否）";
                    }
                }
                int IsReportBJ = 0;
                if (DR["IsReport"] == DBNull.Value)
                {
                    Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （□是  □否  □不需报告）";
                }
                else if (DR["IsReport"].ToString() == "是")
                {
                    Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （☑是  □否  □不需报告）";
                }
                else if (DR["IsReport"].ToString() == "否")
                {
                    Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （□是  ☑否  □不需报告）";
                }
                else if (DR["IsReport"].ToString() == "不需报告")
                {
                    Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （□是  □否  ☑不需报告）";
                    IsReportBJ = 1;
                }
                else
                {
                    Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （□是  □否  □不需报告）";
                }
                if (IsReportBJ == 1)
                {
                    Report.ParameterByName("QualityManageIdea").AsString = Gang;
                    Report.ParameterByName("Sign").AsString = Gang;
                    Report.ParameterByName("SignDate").AsString = Gang;
                }
                else
                {
                    Report.ParameterByName("QualityManageIdea").AsString = DR["QualityManageIdea"] == DBNull.Value ? "" : DR["QualityManageIdea"].ToString().Trim();
                    Report.ParameterByName("Sign").AsString = DR["Sign"] == DBNull.Value ? "" : DR["Sign"].ToString().Trim();
                    Report.ParameterByName("SignDate").AsString = DR["SignDate"] == DBNull.Value ? "" : DR["SignDate"].ToString().Trim().Replace("-", ".");
                }
                int IsRequireCleanBJ = 0;
                if (DR["IsRequireClean"] == DBNull.Value)
                {
                    Report.ParameterByName("IsRequireClean").AsString = "外包装是否需要清洁\r\n（□是   □否）";
                }
                else if (DR["IsRequireClean"].ToString() == "是")
                {
                    Report.ParameterByName("IsRequireClean").AsString = "外包装是否需要清洁\r\n（☑是   □否）";
                }
                else if (DR["IsRequireClean"].ToString() == "否")
                {
                    Report.ParameterByName("IsRequireClean").AsString = "外包装是否需要清洁\r\n（□是   ☑否）";
                    IsRequireCleanBJ = 1;
                }
                else
                {
                    Report.ParameterByName("IsRequireClean").AsString = "外包装是否需要清洁\r\n（□是   □否）";
                }
                if (IsRequireCleanBJ == 1)
                {
                    Report.ParameterByName("PollutionSituation").AsString = Gang;
                    Report.ParameterByName("CleanMethod").AsString = Gang;
                }
                else
                {
                    Report.ParameterByName("PollutionSituation").AsString = DR["PollutionSituation"] == DBNull.Value ? "" : DR["PollutionSituation"].ToString().Trim();
                    Report.ParameterByName("CleanMethod").AsString = DR["CleanMethod"] == DBNull.Value ? "" : DR["CleanMethod"].ToString().Trim();
                }
                int IsCompleteBJ = 0;
                if (DR["IsComplete"] == DBNull.Value)
                {
                    Report.ParameterByName("IsComplete").AsString = "外包装是否完整\r\n（□是   □否）";
                }
                else if (DR["IsComplete"].ToString() == "是")
                {
                    Report.ParameterByName("IsComplete").AsString = "外包装是否完整\r\n（☑是   □否）";
                    IsCompleteBJ = 1;
                }
                else if (DR["IsComplete"].ToString() == "否")
                {
                    Report.ParameterByName("IsComplete").AsString = "外包装是否完整\r\n（□是   ☑否）";
                }
                else
                {
                    Report.ParameterByName("IsComplete").AsString = "外包装是否完整\r\n（□是   □否）";
                }
                if (IsCompleteBJ == 1)
                {
                    Report.ParameterByName("DamageSituation").AsString = Gang;
                    Report.ParameterByName("CauseInvestigation1").AsString = Gang;
                }
                else
                {
                    Report.ParameterByName("DamageSituation").AsString = DR["DamageSituation"] == DBNull.Value ? "" : DR["DamageSituation"].ToString().Trim();
                    Report.ParameterByName("CauseInvestigation1").AsString = DR["CauseInvestigation1"] == DBNull.Value ? "" : DR["CauseInvestigation1"].ToString().Trim();
                }
                int IsSealedBJ = 0;
                if (DR["IsSealed"] == DBNull.Value)
                {
                    Report.ParameterByName("IsSealed").AsString = "外包装是否密封\r\n（□是   □否）";
                }
                else if (DR["IsSealed"].ToString() == "是")
                {
                    Report.ParameterByName("IsSealed").AsString = "外包装是否密封\r\n（☑是   □否）";
                    IsSealedBJ = 1;
                }
                else if (DR["IsSealed"].ToString() == "否")
                {
                    Report.ParameterByName("IsSealed").AsString = "外包装是否密封\r\n（□是   ☑否）";
                }
                else
                {
                    Report.ParameterByName("IsSealed").AsString = "外包装是否密封\r\n（□是   □否）";
                }
                if (IsSealedBJ == 1)
                {
                    Report.ParameterByName("UnsealedCondition").AsString = Gang;
                    Report.ParameterByName("CauseInvestigation2").AsString = Gang;
                }
                else
                {
                    Report.ParameterByName("UnsealedCondition").AsString = DR["UnsealedCondition"] == DBNull.Value ? "" : DR["UnsealedCondition"].ToString().Trim();
                    Report.ParameterByName("CauseInvestigation2").AsString = DR["CauseInvestigation2"] == DBNull.Value ? "" : DR["CauseInvestigation2"].ToString().Trim();
                }
                if (DR["IsAnyMaterialWithPollutionRisk"] == DBNull.Value)
                {
                    Report.ParameterByName("IsAnyMaterialWithPollutionRisk").AsString = "运输工具内是否有造成污染、交叉污染风险的物料（□有 □无）";
                }
                else if (DR["IsAnyMaterialWithPollutionRisk"].ToString() == "有")
                {
                    Report.ParameterByName("IsAnyMaterialWithPollutionRisk").AsString = "运输工具内是否有造成污染、交叉污染风险的物料（☑有 □无）";
                }
                else if (DR["IsAnyMaterialWithPollutionRisk"].ToString() == "无")
                {
                    Report.ParameterByName("IsAnyMaterialWithPollutionRisk").AsString = "运输工具内是否有造成污染、交叉污染风险的物料（□有 ☑无）";
                }
                else
                {
                    Report.ParameterByName("IsAnyMaterialWithPollutionRisk").AsString = "运输工具内是否有造成污染、交叉污染风险的物料（□有 □无）";
                }
                int QuestionBJ = 0;
                if (DR["IsAnyProblemAffectedMaterialQuality"] == DBNull.Value)
                {
                    Report.ParameterByName("IsAnyProblemAffectedMaterialQuality").AsString = "是否有其他可能影响物料质量的问题    （□有 □无）";
                }
                else if (DR["IsAnyProblemAffectedMaterialQuality"].ToString() == "有")
                {
                    Report.ParameterByName("IsAnyProblemAffectedMaterialQuality").AsString = "是否有其他可能影响物料质量的问题    （☑有 □无）";
                }
                else if (DR["IsAnyProblemAffectedMaterialQuality"].ToString() == "无")
                {
                    Report.ParameterByName("IsAnyProblemAffectedMaterialQuality").AsString = "是否有其他可能影响物料质量的问题    （□有 ☑无）";
                    QuestionBJ = 1;
                }
                else
                {
                    Report.ParameterByName("IsAnyProblemAffectedMaterialQuality").AsString = "是否有其他可能影响物料质量的问题    （□有 □无）";
                }
                if (QuestionBJ == 1)
                {
                    Report.ParameterByName("Question").AsString = "问题 :\r\n                             " + Gang;
                    Report.ParameterByName("CauseInvestigation3").AsString = Gang;
                }
                else
                {
                    Report.ParameterByName("Question").AsString = "问题 :" + (DR["Question"] == DBNull.Value ? "" : DR["Question"].ToString().Trim());
                    Report.ParameterByName("CauseInvestigation3").AsString = DR["CauseInvestigation3"] == DBNull.Value ? "" : DR["CauseInvestigation3"].ToString().Trim();
                }

                if (DR["LotNumberType"] == DBNull.Value)
                {
                    Report.ParameterByName("LotNumberType").AsString = "□生产商批号   □供应商批号";
                }
                else if (DR["LotNumberType"].ToString() == "生产商批号")
                {
                    Report.ParameterByName("LotNumberType").AsString = "☑生产商批号   □供应商批号";
                }
                else if (DR["LotNumberType"].ToString() == "供应商批号")
                {
                    Report.ParameterByName("LotNumberType").AsString = "□生产商批号   ☑供应商批号";
                }
                else
                {
                    Report.ParameterByName("LotNumberType").AsString = "□生产商批号   □供应商批号";
                }

                if (DR["IsApprovedVendor"] == DBNull.Value)
                {
                    Report.ParameterByName("IsApprovedVendor").AsString = "是否为质量管理部门批准的供应商\r\n（□是 □否）";
                }
                else if (DR["IsApprovedVendor"].ToString() == "是")
                {
                    Report.ParameterByName("IsApprovedVendor").AsString = "是否为质量管理部门批准的供应商\r\n（☑是 □否）";
                }
                else if (DR["IsApprovedVendor"].ToString() == "否")
                {
                    Report.ParameterByName("IsApprovedVendor").AsString = "是否为质量管理部门批准的供应商\r\n（□是 ☑否）";
                }
                else
                {
                    Report.ParameterByName("IsApprovedVendor").AsString = "是否为质量管理部门批准的供应商\r\n（□是 □否）";
                }
                //□常温 □阴凉 □冷藏 □其他
                if (DR["StorageCondition"] == DBNull.Value)
                {
                    Report.ParameterByName("StorageCondition").AsString = "□常温 □阴凉 □冷藏 □其他";
                }
                else if (DR["StorageCondition"].ToString() == "常温")
                {
                    Report.ParameterByName("StorageCondition").AsString = "☑常温 □阴凉 □冷藏 □其他";
                }
                else if (DR["StorageCondition"].ToString() == "阴凉")
                {
                    Report.ParameterByName("StorageCondition").AsString = "□常温 ☑阴凉 □冷藏 □其他";
                }
                else if (DR["StorageCondition"].ToString() == "冷藏")
                {
                    Report.ParameterByName("StorageCondition").AsString = "□常温 □阴凉 ☑冷藏 □其他";
                }
                else if (DR["StorageCondition"].ToString() == "其他")
                {
                    Report.ParameterByName("StorageCondition").AsString = "□常温 □阴凉 □冷藏 ☑其他";
                }
                else
                {
                    Report.ParameterByName("StorageCondition").AsString = "□常温 □阴凉 □冷藏 □其他";
                }
                // ℃
                Report.ParameterByName("TransportTemperature").AsString = (DR["TransportTemperature"] == DBNull.Value ? "   " : (DR["TransportTemperature"].ToString().Trim() == "" ? "   " : DR["TransportTemperature"].ToString().Trim())) + "℃";
                if (DR["TransportCondition"] == DBNull.Value)
                {
                    Report.ParameterByName("TransportCondition").AsString = "□是 □否";
                }
                else if (DR["TransportCondition"].ToString() == "是")
                {
                    Report.ParameterByName("TransportCondition").AsString = "☑是 □否";
                }
                else if (DR["TransportCondition"].ToString() == "否")
                {
                    Report.ParameterByName("TransportCondition").AsString = "□是 ☑否";
                }
                else
                {
                    Report.ParameterByName("TransportCondition").AsString = "□是 □否";
                }

                if (DR["TransportationControlRecord"] == DBNull.Value)
                {
                    Report.ParameterByName("TransportationControlRecord").AsString = "□是 □否";
                }
                else if (DR["TransportationControlRecord"].ToString() == "有")
                {
                    Report.ParameterByName("TransportationControlRecord").AsString = "☑是 □否";
                }
                else if (DR["TransportationControlRecord"].ToString() == "无")
                {
                    Report.ParameterByName("TransportationControlRecord").AsString = "□是 ☑否";
                }
                else
                {
                    Report.ParameterByName("TransportationControlRecord").AsString = "□是 □否";
                }
                //外包装与生产商通常包装是否一致：     形状（□是  □否）       颜色（□是  □否）        字体（□是  □否）
                String bzyizhi = "外包装与生产商通常包装是否一致：     形状";
                if (DR["Shape"] == DBNull.Value)
                {
                    bzyizhi += "（□是 □否）";
                }
                else if (DR["Shape"].ToString() == "是")
                {
                    bzyizhi += "（☑是 □否）";
                }
                else if (DR["Shape"].ToString() == "否")
                {
                    bzyizhi += "（□是 ☑否）";
                }
                else
                {
                    bzyizhi += "（□是 □否）";
                }
                bzyizhi += "       颜色";
                if (DR["Colour"] == DBNull.Value)
                {
                    bzyizhi += "（□是 □否）";
                }
                else if (DR["Colour"].ToString() == "是")
                {
                    bzyizhi += "（☑是 □否）";
                }
                else if (DR["Colour"].ToString() == "否")
                {
                    bzyizhi += "（□是 ☑否）";
                }
                else
                {
                    bzyizhi += "（□是 □否）";
                }
                bzyizhi += "        字体";
                if (DR["Font"] == DBNull.Value)
                {
                    bzyizhi += "（□是 □否）";
                }
                else if (DR["Font"].ToString() == "是")
                {
                    bzyizhi += "（☑是 □否）";
                }
                else if (DR["Font"].ToString() == "否")
                {
                    bzyizhi += "（□是 ☑否）";
                }
                else
                {
                    bzyizhi += "（□是 □否）";
                }
                Report.ParameterByName("bzyizhi").AsString = bzyizhi;
                //毛重（□有  □无）
                if (DR["RoughWeight"] == DBNull.Value)
                {
                    Report.ParameterByName("RoughWeight").AsString = "毛重（□有  □无）";
                }
                else if (DR["RoughWeight"].ToString() == "有")
                {
                    Report.ParameterByName("RoughWeight").AsString = "毛重（☑有  □无）";
                }
                else if (DR["RoughWeight"].ToString() == "无")
                {
                    Report.ParameterByName("RoughWeight").AsString = "毛重（□有  ☑无）";
                }
                else
                {
                    Report.ParameterByName("RoughWeight").AsString = "毛重（□有  □无）";
                }
                //净重（□有  □无）
                if (DR["NetWeight"] == DBNull.Value)
                {
                    Report.ParameterByName("NetWeight").AsString = "净重（□有  □无）";
                }
                else if (DR["NetWeight"].ToString() == "有")
                {
                    Report.ParameterByName("NetWeight").AsString = "净重（☑有  □无）";
                }
                else if (DR["NetWeight"].ToString() == "无")
                {
                    Report.ParameterByName("NetWeight").AsString = "净重（□有  ☑无）";
                }
                else
                {
                    Report.ParameterByName("NetWeight").AsString = "净重（□有  □无）";
                }
                //批准文号（□有  □无）
                if (DR["ApprovalNumber"] == DBNull.Value)
                {
                    Report.ParameterByName("ApprovalNumber").AsString = "批准文号（□有  □无）";
                }
                else if (DR["ApprovalNumber"].ToString() == "有")
                {
                    Report.ParameterByName("ApprovalNumber").AsString = "批准文号（☑有  □无）";
                }
                else if (DR["ApprovalNumber"].ToString() == "无")
                {
                    Report.ParameterByName("ApprovalNumber").AsString = "批准文号（□有  ☑无）";
                }
                else
                {
                    Report.ParameterByName("ApprovalNumber").AsString = "批准文号（□有  □无）";
                }
                //生产商（口岸）报告（□有   □无）
                String StrReport = String.Empty;
                /*
                if (DR["ReportType"] == DBNull.Value)
                {
                    StrReport = "生产商（口岸）报告";
                }
                else if (DR["ReportType"].ToString() == "生产商报告")
                {
                    StrReport = "生产商报告";
                }
                else if (DR["ReportType"].ToString() == "生产商口岸报告")
                {
                    StrReport = "生产商口岸报告";
                }
                else
                {
                    StrReport = "生产商（口岸）报告";
                }
                */
                StrReport += "生产商报告\r\n";
                if (DR["Report"] == DBNull.Value)
                {
                    StrReport += "（□有  □无）";
                }
                else if (DR["Report"].ToString() == "有")
                {
                    StrReport += "（☑有  □无）";
                }
                else if (DR["Report"].ToString() == "无")
                {
                    StrReport += "（□有  ☑无）";
                }
                else
                {
                    StrReport += "（□有  □无）";
                }
                Report.ParameterByName("Report").AsString = StrReport;
                //默认直接打印
                Report.Print(false);
            }
        }
        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                dgvr.Cells["Check"].Value = true;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (dgvDetail.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前无可用的记录！", "提示");
                return;
            }
            if (!Convert.ToBoolean(this.Tag))
            { MessageBoxEx.Show("请刷新后打印"); return; }
            DataTable dtTemp0 = (DataTable)dgvDetail.DataSource;
            DataTable dtTemp = dtTemp0.Clone();

            for (int m = 0; m < dgvDetail.Rows.Count; m++)
            {
                if (Convert.ToBoolean(dgvDetail.Rows[m].Cells["Check"].Value) == true)
                {
                    DataRow dr = dtTemp.NewRow();
                    dr = (dgvDetail.Rows[m].DataBoundItem as DataRowView).Row;
                    dtTemp.Rows.Add(dr.ItemArray);
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Guid");
            dt.Columns.Add("ApplyDate");
            dt.Columns.Add("ItemNumber");
            dt.Columns.Add("ItemDescription");
            dt.Columns.Add("VendorLotNumber");
            dt.Columns.Add("InternalLotNumber");
            dt.Columns.Add("MfgName");
            dt.Columns.Add("VendorName");
            dt.Columns.Add("PackageQuantity");
            dt.Columns.Add("Receiver");
            dt.Columns.Add("UM");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("PackageOdd");
            dt.Columns.Add("PackageSpecification");
            dt.Columns.Add("MfgDate");
            dt.Columns.Add("ExpiredDate");
            dt.Columns.Add("PackageUM");
            dt.Columns.Add("FONumber");
            dt.Columns.Add("PONumber");
            dt.Columns.Add("LineNumber");
            dt.Columns.Add("FileNumber");
            dt.Columns.Add("FileTracedNumber");
            dt.Columns.Add("FileEdition");
            dt.Columns.Add("EffectiveDate");
            dt.Columns.Add("QualityCheckStandard");
            //Checker as 复核人,Receiver as 接收请验人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
            //DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告
            dt.Columns.Add("Checker");
            dt.Columns.Add("ReceiverAPP");
            dt.Columns.Add("Conclusion");
            dt.Columns.Add("ConclusionText");
            dt.Columns.Add("IsAnyDeviation");
            dt.Columns.Add("DeviationNumber");
            dt.Columns.Add("deviationIsClosed");
            dt.Columns.Add("IsReport");
            dt.Columns.Add("QualityManageIdea");
            dt.Columns.Add("Sign");
            dt.Columns.Add("SignDate");
            dt.Columns.Add("IsRequireClean");
            dt.Columns.Add("PollutionSituation");
            dt.Columns.Add("CleanMethod");
            dt.Columns.Add("IsComplete");
            dt.Columns.Add("DamageSituation");
            dt.Columns.Add("CauseInvestigation1");
            dt.Columns.Add("IsSealed");
            dt.Columns.Add("UnsealedCondition");
            dt.Columns.Add("CauseInvestigation2");
            dt.Columns.Add("IsAnyMaterialWithPollutionRisk");
            dt.Columns.Add("IsAnyProblemAffectedMaterialQuality");
            dt.Columns.Add("Question");
            dt.Columns.Add("CauseInvestigation3");
            dt.Columns.Add("LotNumberType");
            dt.Columns.Add("IsApprovedVendor");
            dt.Columns.Add("StorageCondition");
            dt.Columns.Add("TransportTemperature");
            dt.Columns.Add("TransportCondition");
            dt.Columns.Add("TransportationControlRecord");
            dt.Columns.Add("Shape");
            dt.Columns.Add("Colour");
            dt.Columns.Add("Font");
            dt.Columns.Add("RoughWeight");
            dt.Columns.Add("NetWeight");
            dt.Columns.Add("ApprovalNumber");
            dt.Columns.Add("ReportType");
            dt.Columns.Add("Report");
            foreach (DataRow drTemp in dtTemp.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Guid"] = drTemp["Guid"].ToString();
                dr["ApplyDate"] = drTemp["请验日期"].ToString();
                dr["FileNumber"] = drTemp["受控流水号"].ToString();
                dr["ItemNumber"] = drTemp["物料编码"].ToString();
                dr["ItemDescription"] = drTemp["品名"].ToString();
                dr["VendorLotNumber"] = drTemp["批号"].ToString();
                dr["FONumber"] = drTemp["联系单号"].ToString();
                dr["PackageUM"] = drTemp["整包件数单位"].ToString();
                dr["VendorLotNumber"] = drTemp["批号"];
                dr["InternalLotNumber"] = drTemp["公司批号"];
                dr["MfgName"] = drTemp["生产商名"];
                dr["VendorName"] = drTemp["供应商名"];
                dr["PackageQuantity"] = drTemp["整包件数"];
                dr["Receiver"] = drTemp["库管员"].ToString();
                dr["UM"] = drTemp["单位"];
                dr["Quantity"] = drTemp["入库数量"];
                dr["PackageOdd"] = drTemp["零头标示值"];
                dr["PackageSpecification"] = drTemp["包装规格"];
                dr["MfgDate"] = drTemp["生产日期"];
                dr["ExpiredDate"] = drTemp["过期日期"];

                dr["FileTracedNumber"] = drTemp["追溯文件编号"]==DBNull.Value?"": drTemp["追溯文件编号"];
                dr["FileEdition"] = drTemp["版本"]==DBNull.Value?"": drTemp["版本"];
                dr["EffectiveDate"] = drTemp["生效日期"]==DBNull.Value?"": drTemp["生效日期"];
                //新增字段
                dr["QualityCheckStandard"] = drTemp["检验标准"];
                dr["Checker"] = drTemp["复核人"];
                //dr["ReceiverAPP"] = drTemp["接收请验人"];
                dr["Conclusion"] = drTemp["结论"];
                dr["ConclusionText"] = drTemp["结论其他内容"];
                dr["IsAnyDeviation"] = drTemp["物料验收过程是否出现偏差"];
                dr["DeviationNumber"] = drTemp["偏差编号"];
                dr["deviationIsClosed"] = drTemp["偏差是否已处理关闭"];
                dr["IsReport"] = drTemp["问题是否已报告"];
                dr["QualityManageIdea"] = drTemp["质量管理部门意见"];
                dr["Sign"] = drTemp["签名"];
                dr["SignDate"] = drTemp["签名日期"];
                dr["IsRequireClean"] = drTemp["是否需要清洁"];
                dr["PollutionSituation"] = drTemp["污染情况"];
                dr["CleanMethod"] = drTemp["清洁方式"];
                dr["IsComplete"] = drTemp["外包装是否完整"];
                dr["DamageSituation"] = drTemp["损坏情况"];
                dr["CauseInvestigation1"] = drTemp["原因调查1"];
                dr["IsSealed"] = drTemp["外包装是否密封"];
                dr["UnsealedCondition"] = drTemp["不密封情况"];
                dr["CauseInvestigation2"] = drTemp["原因调查2"];
                dr["IsAnyMaterialWithPollutionRisk"] = drTemp["运输工具内是否存在造成污染交叉污染的物料"];
                dr["IsAnyProblemAffectedMaterialQuality"] = drTemp["是否有其他可能影响物料质量的问题"];
                dr["Question"] = drTemp["问题"];
                dr["CauseInvestigation3"] = drTemp["原因调查3"];
                dr["LotNumberType"] = drTemp["批号类型"];
                dr["IsApprovedVendor"] = drTemp["是否为质量管理部门批准的供应商"];
                dr["StorageCondition"] = drTemp["规定贮存条件"];
                dr["TransportTemperature"] = drTemp["运输条件检查结果"];
                dr["TransportCondition"] = drTemp["运输条件是否符合"];
                dr["TransportationControlRecord"] = drTemp["是否有运输条件控制记录"];
                dr["Shape"] = drTemp["形状是否一致"];
                dr["Colour"] = drTemp["颜色是否一致"];
                dr["Font"] = drTemp["字体是否一致"];
                dr["RoughWeight"] = drTemp["有无毛重"];
                dr["NetWeight"] = drTemp["有无净重"];
                dr["ApprovalNumber"] = drTemp["有无批准文号"];
                dr["ReportType"] = drTemp["报告类型"];
                dr["Report"] = drTemp["有无报告"];
                dt.Rows.Add(dr.ItemArray);
            }

            DataView dv = dt.DefaultView;
            DataTable dtFileNumber = dv.ToTable(true, "FileNumber");
            double totalQuantity = 0;
            double totalPackage = 0;
            double totalOdd = 0;
            DataTable dtNew = dt.Clone();
            List<string> fileNumberList = new List<string>();
            for (int i = 0; i < dtFileNumber.Rows.Count; i++)
            {
                DataRow[] drs = dt.Select("FileNumber='" + dtFileNumber.Rows[i]["FileNumber"].ToString() + "'");
                DataRow drNew = dtNew.NewRow();
                if (!fileNumberList.Contains(dtFileNumber.Rows[i]["FileNumber"].ToString()))
                {
                    if (drs.Length > 1)
                    {
                        for (int j = 0; j < drs.Length; j++)
                        {
                            totalQuantity += Convert.ToDouble(drs[j]["Quantity"]);
                            totalPackage += Convert.ToDouble(drs[j]["PackageQuantity"]);
                            totalOdd += Convert.ToDouble(drs[j]["PackageOdd"]);
                        }
                        drNew = drs[0];
                        drNew["Quantity"] = totalQuantity;
                        drNew["PackageQuantity"] = totalPackage;
                        drNew["PackageOdd"] = totalOdd;
                        dtNew.Rows.Add(drNew.ItemArray);
                    }
                    else
                    {
                        drNew = drs[0];
                        dtNew.Rows.Add(drNew.ItemArray);
                    }
                }
                else
                {
                    continue;
                }

            }

            PrintBatchRecord pp = new PrintBatchRecord(dtNew, "\\入库请验单.grf", 0, 1,true);
            pp.ShowDialog();

        }
        private void tbFileNumber_Click(object sender, EventArgs e)
        {
            tbFileNumber.Text = "";
            tbFileNumber.ForeColor = Color.Black;
        }

        private void tbItemDesc_Click(object sender, EventArgs e)
        {
            tbItemDesc.Text = "";
            tbItemDesc.ForeColor = Color.Black;
        }
        
        private void btnUpdateRecord_Click(object sender, EventArgs e)
        {
            ManageBatchRecordUpdateHistory Uh = new ManageBatchRecordUpdateHistory(UserId, UserName);
            Uh.ShowDialog();
           
        }

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;
            dgvDetail["Check", RowIndex].Value = !Convert.ToBoolean(dgvDetail["Check", RowIndex].Value);
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex=e.ColumnIndex;
            int UpdateItemCount = 0;
            if (dgvDetail[columnIndex, rowIndex].Style.ForeColor != Color.Red)
            {
                for (int i = 0; i < dgvDetail.Columns.Count; i++)
                {
                    if (dgvDetail[i, rowIndex].Style.ForeColor == Color.Red) UpdateItemCount++;
                }
                if (UpdateItemCount >= 3)
                {
                    MessageBoxEx.Show("最多修改3处！");
                    return;
                }
            }
            if (rowIndex >= 0 && columnIndex >= 0 && dgvDetail.Columns[columnIndex].Name != "受控流水号" && dgvDetail.Columns[columnIndex].Name != "库管员" && dgvDetail.Columns[columnIndex].Name != "复核人")
            {
                InputBox inp = new InputBox(dgvDetail.Columns[columnIndex].Name, dgvDetail[columnIndex,rowIndex].Value.ToString());
                DialogResult dr = inp.ShowDialog();
                if (dr == DialogResult.OK && inp.Value.Length > 0)
                {
                    //MessageBoxEx.Show(inp.Value);
                    if (dgvDetail[columnIndex, rowIndex].Value.ToString().Trim() != inp.Value.Trim())
                    {
                        dgvDetail[columnIndex, rowIndex].Value = inp.Value.Trim();
                        dgvDetail[columnIndex, rowIndex].Style.ForeColor = Color.Red;
                        this.Tag = false;
                    }
                }
                inp.Dispose();
            }
        }

        private void dgvDetail_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && e.Button == MouseButtons.Right)
            {
                //MessageBoxEx.Show("右键");
                dgvDetail.ClearSelection();
                dgvDetail["受控流水号", e.RowIndex].Selected = true;
                contextMenuStrip1.Tag = e.RowIndex;
                contextMenuStrip1.Show(MousePosition.X,MousePosition.Y);
                
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strRevisionReason = RevisionReason.Text.Trim();
            if (strRevisionReason == "请输入修订原因" || RevisionReason.Visible == false ||string.IsNullOrWhiteSpace(strRevisionReason))
            {
                MessageBoxEx.Show("请按 ALT+U，在请输入修订原因文本框内输入修订原因！");
                return;

            }
            int rowIndex = Convert.ToInt32(contextMenuStrip1.Tag);
            Dictionary<string, string> DicRecord = new Dictionary<string, string>();
            DicRecord.Add("请验日期", "ApplyDate");
            DicRecord.Add("供应商名", "VendorName");
            DicRecord.Add("生产商名", "ManufacturerName");
            DicRecord.Add("物料编码", "ItemNumber");
            DicRecord.Add("品名", "ItemDescription");
            DicRecord.Add("单位", "LineUM");
            DicRecord.Add("批号", "LotNumber");
            DicRecord.Add("公司批号", "InternalLotNumber");
            DicRecord.Add("生产日期", "ManufacturedDate");
            DicRecord.Add("过期日期", "ExpiredDate");
            DicRecord.Add("入库数量", "ReceiveQuantity");
            DicRecord.Add("整包件数", "PackageQuantity");
            DicRecord.Add("包装规格", "PackageSpecification");
            DicRecord.Add("零头标示值", "PackageOdd");
            DicRecord.Add("整包件数单位", "PackageUM");
            DicRecord.Add("追溯文件编号", "FileTracedNumber");
            DicRecord.Add("版本", "FileEdition");
            DicRecord.Add("生效日期", "EffectiveDate");
            DicRecord.Add("联系单号", "ForeignNumber");
            DicRecord.Add("检验标准", "QualityCheckStandard");
            DicRecord.Add("结论", "Conclusion");
            DicRecord.Add("结论其他内容", "ConclusionText");
            DicRecord.Add("物料验收过程是否出现偏差", "IsAnyDeviation");
            DicRecord.Add("偏差编号", "DeviationNumber");
            DicRecord.Add("偏差是否已处理关闭", "deviationIsClosed");
            DicRecord.Add("问题是否已报告", "IsReport");
            DicRecord.Add("质量管理部门意见", "QualityManageIdea");
            DicRecord.Add("签名", "Sign");
            DicRecord.Add("签名日期", "SignDate");
            DicRecord.Add("是否需要清洁", "IsRequireClean");
            DicRecord.Add("污染情况", "PollutionSituation");
            DicRecord.Add("清洁方式", "CleanMethod");
            DicRecord.Add("外包装是否完整", "IsComplete");
            DicRecord.Add("损坏情况", "DamageSituation");
            DicRecord.Add("原因调查1", "CauseInvestigation1");
            DicRecord.Add("外包装是否密封", "IsSealed");
            DicRecord.Add("不密封情况", "UnsealedCondition");
            DicRecord.Add("原因调查2", "CauseInvestigation2");
            DicRecord.Add("运输工具内是否存在造成污染交叉污染的物料", "IsAnyMaterialWithPollutionRisk");
            DicRecord.Add("是否有其他可能影响物料质量的问题", "IsAnyProblemAffectedMaterialQuality");
            DicRecord.Add("问题", "Question");
            DicRecord.Add("原因调查3", "CauseInvestigation3");
            DicRecord.Add("批号类型", "LotNumberType");
            DicRecord.Add("是否为质量管理部门批准的供应商", "IsApprovedVendor");
            DicRecord.Add("规定贮存条件", "StorageCondition");
            DicRecord.Add("运输条件检查结果", "TransportTemperature");
            DicRecord.Add("运输条件是否符合", "TransportCondition");
            DicRecord.Add("是否有运输条件控制记录", "TransportationControlRecord");
            DicRecord.Add("形状是否一致", "Shape");
            DicRecord.Add("颜色是否一致", "Colour");
            DicRecord.Add("字体是否一致", "Font");
            DicRecord.Add("有无毛重", "RoughWeight");
            DicRecord.Add("有无净重", "NetWeight");
            DicRecord.Add("有无批准文号", "ApprovalNumber");
            DicRecord.Add("报告类型", "ReportType");
            DicRecord.Add("有无报告", "Report");
            #region 事务
            string SqlSet = "set ";
            string StrUpdate = string.Empty;
            foreach (string key in DicRecord.Keys)
            {
                if (dgvDetail[key, rowIndex].Style.ForeColor == Color.Red)
                {
                    StrUpdate += "【" + key + "】" + dgvDetail[key, rowIndex].Value.ToString();
                    SqlSet += DicRecord[key] + "='" + dgvDetail[key, rowIndex].Value.ToString() + "',";
                }
            }
            if (StrUpdate == string.Empty)
            {
                MessageBoxEx.Show("无修改项！");
                return;
            }

            SqlConnection con = new SqlConnection(GlobalSpace.EBRConnStr);
            con.Open();
            SqlTransaction tran = con.BeginTransaction();//先实例SqlTransaction类，使用这个事务使用的是con 这个连接，使用BeginTransaction这个方法来开始执行这个事务
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = tran;
            try
            {
                //在try{} 块里执行sqlcommand命令，
                cmd.CommandText = "INSERT INTO EBR_ReceiveRecordForInspectUpdateHistory (" +
                    "EBR_ReceiveRecordForInspectUpdateHistory.Id,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.PONumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.VendorNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.VendorName,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ManufacturerNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ManufacturerName,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.LineNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ItemNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ItemDescription,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.LineUM,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.DemandDeliveryDate,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ReceiveQuantity,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Stock,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Bin,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.LotNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.InternalLotNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ExpiredDate,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Operator,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.StockKeeper,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.CreateDate,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Status,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.LotNumberAssign,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.OrderQuantity,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ItemReceiveType,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ForeignNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.BuyerName,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.FDAFlag,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Guid,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.CreatedDateTime,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ManufacturedDate,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.NumberOfPackages,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.FileEdition,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.FileNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.FileTracedNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.EffectiveDate,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ApplyDate,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.PackageQuantity,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.PackageUM,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.PackageOdd,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.PackageSpecification,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsPreOccupied,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Checker,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Conclusion,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ConclusionText,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsAnyDeviation,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.DeviationNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.deviationIsClosed,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsReport,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.QualityManageIdea,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Sign,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.SignDate,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsRequireClean,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.PollutionSituation,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.CleanMethod,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsComplete,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.DamageSituation,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.CauseInvestigation1,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsSealed,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.UnsealedCondition,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.CauseInvestigation2,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsAnyMaterialWithPollutionRisk,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsAnyProblemAffectedMaterialQuality,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Question,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.CauseInvestigation3,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.LotNumberType,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.IsApprovedVendor,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.StorageCondition,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.TransportTemperature,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.TransportCondition,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.TransportationControlRecord,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Shape,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Colour,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Font,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.RoughWeight,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.NetWeight,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ApprovalNumber,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ReportType,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Report,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.QualityCheckStandard,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.ModifyContent,\n" +
"	EBR_ReceiveRecordForInspectUpdateHistory.Modifier,\n" +
                    "RevisionReason) SELECT *,'" + StrUpdate + "' as ModifyContent,'" + UserId + "|" + UserName + "' as Modifier,'" + strRevisionReason + "' as RevisionReason  FROM EBR_ReceiveRecordForInspect where FileNumber='" + dgvDetail["受控流水号", rowIndex].Value.ToString() + "' and Operator='" + dgvDetail["库管员", rowIndex].Value.ToString() + "' and FileTracedNumber='" + dgvDetail["追溯文件编号", rowIndex].Value.ToString() + "' and FileEdition='" + dgvDetail["版本", rowIndex].Value.ToString() + "' and EffectiveDate='" + dgvDetail["生效日期", rowIndex].Value.ToString() + "'";
                tbItemDesc.Text = cmd.CommandText;
                if (cmd.ExecuteNonQuery() != 1)
                { throw new Exception("修订历史新增条数不为1"); }
                cmd.CommandText = "update EBR_ReceiveRecordForInspect " + SqlSet.Substring(0, SqlSet.Length - 1) + " where FileNumber='" + dgvDetail["受控流水号", rowIndex].Value.ToString() + "' and Operator='" + dgvDetail["库管员", rowIndex].Value.ToString() + "' and FileTracedNumber='" + dgvDetail["追溯文件编号", rowIndex].Value.ToString() + "' and FileEdition='" + dgvDetail["版本", rowIndex].Value.ToString() + "' and EffectiveDate='" + dgvDetail["生效日期", rowIndex].Value.ToString() + "'";
                if (cmd.ExecuteNonQuery() != 1)
                { throw new Exception("记录修改条数不为1"); }
                tran.Commit();//如果两个sql命令都执行成功，则执行commit这个方法，执行这些操作
                MessageBoxEx.Show("修改成功！");
            }
            catch (Exception ex)
            {
                tran.Rollback();//如何执行不成功，发生异常，则执行rollback方法，回滚到事务操作开始之前；
                MessageBoxEx.Show("修改失败:" + ex.Message);
            }
            btnView_Click_1(sender, e);
            #endregion
        }
        bool BtnVisible = true;
        private void ManageBatchRecord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.U | Keys.Alt))//按下alt+s键
            {
                e.Handled = true;//将Handled设置为true，指示已经处理过KeyPress事件
                btnUpdateRecord.Visible = BtnVisible;
                RevisionReason.Visible = BtnVisible;
                BtnVisible = !BtnVisible;
            }
        }

        private void RevisionReason_Click(object sender, EventArgs e)
        {
            RevisionReason.Text = "";
            RevisionReason.ForeColor = Color.Black;
        }
    }
}
