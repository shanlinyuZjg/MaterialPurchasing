using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using FrmMain;
using FrmMain.Purchase;
using DevComponents.DotNetBar;
using Global.Warehouse;
using System.Data;
using Global.Helper;

namespace Global
{
    static class Program
    {
        public static bool tabTest = false;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>/
        [STAThread]
        
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LocalizationKeys.LocalizeString += CommonOperate.LocalizationKeys_LocalizeString;

            #region 判断业务逻
            
            if (Args.Length == 0)
            {
                MessageBox.Show("请登陆瑞阳应用平台后，在平台中启动该程序！", "提示");
            }
            else
            {
                //传递数据：四班代码 密码  姓名 单位 FSDB数据库连接  FSDBMR数据库连接 权限
                string userID = Args[1];
                string strfsdbmr = Args[4];
                string strfsdb = Args[5];
                string strPrivilege = Args[6];
                string loginUserID = Args[7];
                //         MessageBox.Show(strPrivilege);
                //0 FSID ,1 FSpw,2 name
                string[] dbFSDB = strfsdb.Split('|');
                string[] dbFSDBMR = strfsdbmr.Split('|');
                //FSDB数据库连接传值
                FSDBConn.ip = dbFSDB[1];
                FSDBConn.database = dbFSDB[0];
                FSDBConn.userid = dbFSDB[2];
                FSDBConn.password = dbFSDB[3];
                //FSDBMR数据库连接传值
                FSDBMRConn.ip = dbFSDBMR[1];
                FSDBMRConn.database = dbFSDBMR[0];
                FSDBMRConn.userid = dbFSDBMR[2];
                FSDBMRConn.password = dbFSDBMR[3];

                //根据权限代码，调用相应程序窗体   //
                if (strPrivilege.Contains("PPP"))
                {
                    Application.Run(new FrmMain(Args[0], Args[1], Args[2], strPrivilege));
                }
                else if (strPrivilege.Contains("FAP"))//财务
                {
                    //  MessageBox.Show( Args[2] + "-" + Args[1] + "-" + Args[0] + "-" + strPrivilege);
                    //Application.Run(new Finance.InvoiceVerify( Args[0], Args[1], Args[2]));
                    Application.Run(new FrmPurchaseDeptInvoiceFinance(Args[0], Args[1], Args[2]));
                }
                else if (strPrivilege.Contains("PPC"))
                {
                    Application.Run(new FrmMain(Args[0], Args[1], Args[2], strPrivilege, Args[7]));
                }
                else if (strPrivilege.Contains("PPS"))
                {
                    Application.Run(new FrmPurchaseDeptSupervisor(Args[0], Args[2]));
                }
                else if (strPrivilege.Contains("KPP"))
                {
                    Application.Run(new FrmMain(Args[0], Args[1], Args[2], strPrivilege));
                }
                else if (strPrivilege.Contains("TAX"))//供应只有发票匹配
                {
                    Application.Run(new FrmPurchaseDeptInvoice(Args[0], Args[2], Args[7]));
                }
                else if (strPrivilege.Contains("AUD"))//审计
                {
                    Application.Run(new FrmPurchaseDeptInvoiceAudit(Args[0], Args[2], Args[7]));
                }
                else if (strPrivilege.Contains("SAOFS") || strPrivilege.Contains("SOFS") || strPrivilege.Contains("SO") || strPrivilege.Contains("SFS"))
                {
                    Application.Run(new FrmWarehouseDeptStockKeeper(Args[0], Args[1], Args[2], strPrivilege));
                }
                else if (strPrivilege.Contains("SSUPER") || strPrivilege.Contains("SFILE") || strPrivilege.Contains("SQA"))
                {
                    Application.Run(new FrmStockUserManage(Args[0], Args[2], strPrivilege));
                }
                else
                {
                    MessageBox.Show("未查到相应权限信息，请联系管理员！" + "-" + strPrivilege);
                }
            }

            #endregion
            //tabTest = true;
            //Application.Run(new Purchase.ForeignOrderItemSubmit("P11", "沈传荣"));
            // Application.Run(new Finance.RawMaterialInfo());
            //        Application.Run(new Purchase.OldRecord("P11"));//SO  SFS
            //Application.Run(new FrmWarehouseDeptStockKeeper("S13", "Zx789789", "张霞", "SFS"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S32", "Zx234567", "吕继美", "SFS")); //
            //Application.Run(new FrmWarehouseDeptStockKeeper("S95", "zmC11111", "朱梅翠", "SOFS"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S78", "zmC11111", "石娜", "SO"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S72", "zmC11111", "曹杰", "SOFS"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S49", "KtNc9Yor", "高国剑", "SO"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S76", "Qq123123", "秦娜", "SAOFS"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S42", "Qn123456", "田晓翠", "SO"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S88", "Qn123456", "王国栋", "SO"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S69", "Qn123456", "吴蒙", "SO"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S67", "Cm555555", "崔民", "SO"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S74", "Cm555555", "杜滨慧", "SO"));
            //            Application.Run(new Purchase.ForeignOrderItemAutomaticPlaceOrder("P11", "张霞"));
            //Application.Run(new FrmPurchaseDeptSupervisor("DJB", "丁计宝"));
            //Application.Run(new FrmStockUserManage("DJB", "丁计宝", "SQA"));

            //Application.Run(new FrmPurchaseDeptInvoice("CX", "张琴琴", "PM03"));
            //Application.Run(new FrmPurchaseDeptInvoice("DJB", "丁计宝"));
            //Application.Run(new FrmMain("P02", "123123", "唐守艳", "PPP"));
            //Application.Run(new FrmMain("P07", "123123", "崔娟", "PPP"));
            //Application.Run(new FrmPurchaseDeptInvoiceAudit("CX", "王汝艳", "WANGRUYAN"));
            //Application.Run(new FrmPurchaseDeptInvoiceFinance("F42", "123456", "崔琳"));
            //Application.Run(new Global.Purchase.POInvoice_MR( "ZJG", "左进国"));
            //Application.Run(new Global.Finance.InvoiceVerifyMR("左进国","ZJG","123456"));
            //Application.Run(new FrmMain("P03", "123123", "郑尧", "PPP"));
            //Application.Run(new FrmMain("P06", "123123", "李云", "PPP"));
            //Application.Run(new Finance.InvoiceVerify("唐守艳", "P02", "123123"));
            //Application.Run(new FrmMain("P11", "123123", "沈传荣", "PPP"));
            //Application.Run(new FrmMain("P05", "123123", "唐兆红", "PPP"));
            //Application.Run(new FrmMain("C55", "Txd002002", "段秀霞", "KPP"));//大客户
            //Application.Run(new FrmMain("WGZ", "123123", "王光柱", "PPC", "WGZ"));
            //             Application.Run(new Purchase.ForeignOrderItemOthers());
            //Application.Run(new Purchase.SupervisorForeignOrderItemCheck("DJB"));
            //   Application.Run(new Finance.POItemReceivedCheck("ZZX"));
            //Application.Run(new Purchase.ForeignOrderItemManage("P11"));
            //Application.Run(new Purchase.ManageItemPrice("P02"));
            //   Application.Run(new Finance.ForeignCurrencyARCD("F26", "11111"));
            //             Application.Run(new Warehouse.BatchPORVX());
            //#region 供应 测试用代码

            //DataTable dtTemp = GetUserInfo("P11");
            //PurchaseUser.SupervisorID = dtTemp.Rows[0]["SupervisorID"].ToString();
            //PurchaseUser.UserEmail = dtTemp.Rows[0]["Email"].ToString();
            //PurchaseUser.UserEmailDecryptedPassword = dtTemp.Rows[0]["Password"].ToString();
            //PurchaseUser.UserStatus = Convert.ToInt32(dtTemp.Rows[0]["Status"]);
            //PurchaseUser.POItemOthersConfirm = Convert.ToInt32(dtTemp.Rows[0]["POItemOthersConfirm"]);
            //PurchaseUser.PurchaseType = dtTemp.Rows[0]["PurchaseType"].ToString();
            //PurchaseUser.PriceCompare = Convert.ToInt32(dtTemp.Rows[0]["PriceCompare"]);
            //PurchaseUser.POTypeWithRange = dtTemp.Rows[0]["POTypeWithRange"].ToString();
            //PurchaseUser.POTogether = Convert.ToInt32(dtTemp.Rows[0]["POTogether"]);
            //PurchaseUser.PartlyNotCompareItemPrice = Convert.ToInt32(dtTemp.Rows[0]["PartlyNotCompareItemPrice"]);
            //PurchaseUser.Group = dtTemp.Rows[0]["Group"].ToString();
            //PurchaseUser.ConfirmGroup = dtTemp.Rows[0]["ConfirmGroup"].ToString();
            //PurchaseUser.UserID = dtTemp.Rows[0]["UserID"].ToString();
            //PurchaseUser.UserName = dtTemp.Rows[0]["Name"].ToString();

            //if (dtTemp.Rows[0]["ItemReceiveType"] != DBNull.Value)
            //{
            //    PurchaseUser.ItemReceiveType = dtTemp.Rows[0]["ItemReceiveType"].ToString();
            //}
            //else
            //{
            //    PurchaseUser.ItemReceiveType = "NotSet";
            //}

            //if (dtTemp.Rows[0]["PONumberSequenceNumberRange"] == DBNull.Value || string.IsNullOrEmpty(dtTemp.Rows[0]["PONumberSequenceNumberRange"].ToString()))
            //{
            //    PurchaseUser.PONumberSequenceNumberRange = "";
            //}
            //else
            //{
            //    PurchaseUser.PONumberSequenceNumberRange = dtTemp.Rows[0]["PONumberSequenceNumberRange"].ToString();
            //    string[] range = PurchaseUser.PONumberSequenceNumberRange.Split('-');
            //    //  PONumberStartNumber = Convert.ToInt32(range[0]);
            //    //   PONumberEndNumber = Convert.ToInt32(range[1]);
            //}

            //if (dtTemp.Rows[0]["POType"] == DBNull.Value || string.IsNullOrEmpty(dtTemp.Rows[0]["POType"].ToString()))
            //{
            //    PurchaseUser.UserPOType = "";
            //}
            //else
            //{
            //    PurchaseUser.UserPOType = dtTemp.Rows[0]["POType"].ToString();
            //}
            //Application.Run(new Purchase.PlaceOrder("P11", "123123", "李云", "PPP", 101, 150));

            //#endregion

            #region 仓库 测试用代码
            /*cmf
             StockUser.UserID = "S67";
             StockUser.UserName = "崔民";
             StockUser.Privilege = "SOFS";
             string sqlSelectUserInfo = @"Select UserName,Type,InternalNumber,District,IsVial,IsDirectERP,IsERP,Stock,FileTracedNumber,FileEdition,EffectiveDate From PurchaseDepartmentStockKeeperNumber Where UserID='" + StockUser.UserID + "'";
             DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectUserInfo);
             StockUser.Type = dt.Rows[0]["Type"].ToString();
             if (dt.Rows[0]["District"] != DBNull.Value || dt.Rows[0]["District"].ToString() != "")
             {
                 StockUser.District = dt.Rows[0]["District"].ToString();
             }
             else
             {
                 StockUser.District = "NotSet";
             }
             if (dt.Rows[0]["InternalNumber"] != DBNull.Value || dt.Rows[0]["InternalNumber"].ToString() != "")
             {
                 StockUser.Number = dt.Rows[0]["InternalNumber"].ToString();
             }
             else
             {
                 StockUser.Number = "NotSet";
             }

             StockUser.UserName = dt.Rows[0]["UserName"].ToString().Trim();
             StockUser.IsVial = dt.Rows[0]["IsVial"].ToString();
             StockUser.IsDirectERP = dt.Rows[0]["IsDirectERP"].ToString();
             StockUser.IsERP = dt.Rows[0]["IsERP"].ToString();
             StockUser.FileEdition = dt.Rows[0]["FileEdition"].ToString();
             StockUser.FileTracedNumber = dt.Rows[0]["FileTracedNumber"].ToString();
            StockUser.EffectiveDate = dt.Rows[0]["EffectiveDate"].ToString();
            StockUser.Stock = dt.Rows[0]["Stock"].ToString();
             Application.Run(new Warehouse.Stock("S61","123","111","SOFS"));
            */
            //Application.Run(new FrmWarehouseDeptStockKeeper("S88", "123", "111", "SOFS"));
            #endregion

            //                           Application.Run(new Purchase.POItemConfirmAPIS("HDH", "丁积峰"));
            //                Application.Run(new Purchase.ImportDemesticProduct("P04", "", "唐守艳","DJB"));
            //                Application.Run(new Warehouse.BatchPORVX("S67", "123456"));
            //        Application.Run(new Warehouse.EBatchRecordConfirm());
            //        Application.Run(new ManageBatchRecord("毕玉磊"));
            //            Application.Run(new Audit.InvoiceAudit());
            //                     Application.Run(new Purchase.DeptItemRequirement());
            //     Application.Run(new Warehouse.ManageBatchRecord("郭本强"));
            //                 Application.Run(new Finance.InvoiceVerify("常","CMF","11111111"));
            //             Application.Run(new Purchase.POInvoice())
            //         Application.Run(new Warehouse.ManageBatchRecord("秦娜"));
            //Application.Run(new FrmWarehouseDeptStockKeeper("S76", "1111111", "秦娜", "SAOFS"));
        }
        //获取用户信息
        //private static DataTable GetUserInfo(string struserid)
        //{
        //    string strSelect = @"Select UserID,SupervisorID,Status,Email,Name,Password,PurchaseType,POType,PriceCompare,POItemOthersConfirm,PONumberSequenceNumberRange,POTypeWithRange,POTogether,ItemReceiveType,[Group],ConfirmGroup,PartlyNotCompareItemPrice,FSUserID   from PurchaseDepartmentRBACByCMF where UserID='" + struserid + "'";
        //    //       Custom.Msg(strSelect);
        //    return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        //}
    }

}

