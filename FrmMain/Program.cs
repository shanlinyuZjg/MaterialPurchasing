using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Global.Finance;
using System.Configuration;
using FrmMain;
using FrmMain.Purchase;

namespace Global
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*此处版本对比代码有问题，会导致程序停止运行，未查到原因。
            if(!CommonOperate.IsApplicationVersionValid("供应办公程序",GlobalSpace.PurchaseApplicationVersion))
            {
                MessageBox.Show("程序版本不一致，请先升级到最新版本！", "提示");
                return;
            }*/
          
            if (Args.Length == 0)
            {
                MessageBox.Show("程序启动参数错误！");
            }
            else
            {               
                //传递数据：四班代码 密码  姓名 单位 FSDB数据库连接  FSDBMR数据库连接 权限
                string userID = Args[0];
                string strfsdbmr = Args[4];
                string strfsdb = Args[5];
                string strPrivilege = Args[6];

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

                //根据权限代码，调用相应程序窗体 
                switch (strPrivilege)
                {
                    //供应处采购员
                    case "PPP":
                        Application.Run(new FrmMain(Args[0], Args[1], Args[2]));
                        break;
                    //供应处处长
                    case "PPS":
                        Application.Run(new FrmPurchaseDeptSupervisor(Args[0], Args[2]));
                        break;
                    //大客户
                    case "KA":
                        break;
                    //仓库
                    case "SS":
                        Application.Run(new FrmWarehouseDeptStockKeeper(Args[0], Args[1], Args[2]));
                        break;
                   //仓库
                    case "SFS":
                        Application.Run(new FrmWarehouseDeptStockKeeper(Args[0], Args[1], Args[2]));
                        break;
                    default:
                        MessageBox.Show("未查到相应用户信息，请联系管理员！");
                        break;
                }
        }
    
     //     Application.Run(new Purchase.ForeignOrderItemSubmit("P11","沈传荣"));
            // Application.Run(new Finance.RawMaterialInfo());

            //     Application.Run(new FrmWarehouseDeptStockKeeper("S13","reyoung", "张霞"));
            //   Application.Run(new Warehouse.Stock2());
            //       Application.Run(new FrmPurchaseDeptSupervisor("DJB", "丁计宝"));
   //         Application.Run(new Purchase.ForeignOrderItemSubmit("P11","沈传荣"));
            //   Application.Run(new Finance.POItemReceivedCheck("ZZX"));
            //    Application.Run(new ImportForeignOrderItemInfo("P11"));
            //   Application.Run(new test());
            //   Application.Run(new Finance.ForeignCurrencyARCD("F26", "11111"));
        }
    }
    }

