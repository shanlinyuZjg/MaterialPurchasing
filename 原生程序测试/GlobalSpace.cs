using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global
{
    public class GlobalSpace
    {
        public static readonly string PurchaseApplicationVersion = "1.0";
        public static readonly string fsconfigfilepath = "M:\\mfgsys\\fs.cfg";
        public static readonly string fsTestconfigfilepath = "Y:\\mfgsys\\fs.cfg";
        
        public static readonly string FSDBConnstr = "server=192.168.8.11;database=FSDB;uid=xym;pwd=xym-123";
        public static readonly string FSDBMRConnstr = "server=192.168.8.11;database=fsdbmr;uid=program;pwd=program";
        public static readonly string oledbconnstrFSDBMR = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;Data Source=192.168.8.11;Initial Catalog=fsdbmr;User ID=program;Password=program";
        public static readonly string oledbconnstrFSDB = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;Data Source=192.168.8.11;Initial Catalog=fsdb;User ID=xym;Password=xym-123";

        /*
        public static readonly string FSDBConnstr = "server="+FSDBConn.ip+";database="+FSDBConn.database+";uid="+FSDBConn.userid+";pwd="+FSDBConn.password+"";
        
        public static readonly string connstrFSDBMR = "server=" + FSDBMRConn.ip + ";database=" + FSDBMRConn.database + ";uid=" + FSDBMRConn.userid + ";pwd=" + FSDBMRConn.password + "";


        public static readonly string oledbconnstrFSDBMR = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;"+ "Data Source=" + FSDBMRConn.ip + ";Initial Catalog=" + FSDBMRConn.database + ";User ID=" + FSDBMRConn.userid + ";Password=" + FSDBMRConn.password + "";

        public static readonly string oledbconnstrFSDB = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;" + "Data Source=" + FSDBConn.ip + ";Initial Catalog=" + FSDBConn.database + ";User ID=" + FSDBConn.userid + ";Password=" + FSDBConn.password + "";
        */

        
        //测试服务器连接信息
        public static readonly string connstrFSDBTR = "server=192.168.8.12;database=FSDBTR;uid=sa;pwd=ERPtest321";
        public static readonly string connstrFSDBTEST = "server=192.168.8.12;database=FSDB;uid=sa;pwd=ERPtest321";      
        public static readonly string oledbconnstrFSDBTR = "Provider=SQLOLEDB.1;Password=program;Persist Security Info=True;User ID=program;Initial Catalog=FSDBMR;Data Source=192.168.8.11;Use Procedure for Prepare=1;Auto Translate=false;";
    
        public static List<string> foItemInfoList;
        public static List<string> boxList = new List<string>();
        public static List<string> carbonList = new List<string>();
        public static List<string> labelList = new List<string>();
    }

    public static class FSDBConn
    {
        private static string IP;
        public static string ip { get { return IP; } set { IP = value; } }
        private static string DataBase;
        public static string database { get { return DataBase; } set { DataBase = value; } }
        private static string UserID;
        public static string userid { get { return UserID; } set { UserID = value; } }
        private static string Password;
        public static string password { get { return Password; } set { Password = value; } }       
    }

    public static class FSDBMRConn
    {
        private static string IP;
        public static string ip { get { return IP; } set { IP = value; } }
        private static string DataBase;
        public static string database { get { return DataBase; } set { DataBase = value; } }
        private static string UserID;
        public static string userid { get { return UserID; } set { UserID = value; } }
        private static string Password;
        public static string password { get { return Password; } set { Password = value; } }
    }
}
