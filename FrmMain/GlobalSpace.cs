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
        //public static readonly string fsTestconfigfilepath = "Y:\\mfgsys\\fs.cfg";
        
        
        public static readonly string FSDBConnstr = "server=192.168.8.11;database=FSDB;uid=xym;pwd=xym-123";
        
        public static readonly string FSDBMRConnstr = "server=192.168.8.11;database=fsdbmr;uid=program;pwd=program";

        public static readonly string oledbconnstrFSDBMR = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;Data Source=192.168.8.11;Initial Catalog=fsdbmr;User ID=program;Password=program";
        public static readonly string oledbconnstrFSDB = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;Data Source=192.168.8.11;Initial Catalog=fsdb;User ID=xym;Password=xym-123";
        

        /*
        public static readonly string FSDBConnstr = "server="+FSDBConn.ip+";database="+FSDBConn.database+";uid="+FSDBConn.userid+";pwd="+FSDBConn.password+"";
        
        public static readonly string FSDBMRConnstr = "server=" + FSDBMRConn.ip + ";database=" + FSDBMRConn.database + ";uid=" + FSDBMRConn.userid + ";pwd=" + FSDBMRConn.password + "";

        public static readonly string oledbconnstrFSDBMR = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;"+ "Data Source=" + FSDBMRConn.ip + ";Initial Catalog=" + FSDBMRConn.database + ";User ID=" + FSDBMRConn.userid + ";Password=" + FSDBMRConn.password + "";

        public static readonly string oledbconnstrFSDB = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;" + "Data Source=" + FSDBConn.ip + ";Initial Catalog=" + FSDBConn.database + ";User ID=" + FSDBConn.userid + ";Password=" + FSDBConn.password + "";
        */

        /*
        //测试服务器连接信息
        public static readonly string connstrFSDBTR = "server=192.168.8.12;database=FSDBTR;uid=sa;pwd=ERPtest321";
        public static readonly string connstrFSDBTEST = "server=192.168.8.12;database=FSDB;uid=sa;pwd=ERPtest321";      
        public static readonly string oledbconnstrFSDBTR = "Provider=SQLOLEDB.1;Password=program;Persist Security Info=True;User ID=program;Initial Catalog=FSDBMR;Data Source=192.168.8.11;Use Procedure for Prepare=1;Auto Translate=false;";
    */
        public static List<string> foItemInfoList;


        public static List<Carbon> carbonList = new List<Carbon>();
        public static List<Label> labelList = new List<Label>();
        public static List<Box> boxList = new List<Box>();
        public static List<Others> othersList = new List<Others>();
        public static List<Specification> specificationList = new List<Specification>();
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

    public  class Carbon
    {
        private string carbonSize;
        private string cellSize;
        private string cellSizeLenWidQuantity;
        private int paperPerQuantity;
        private double carbonArea;
        private double paperArea;
        private double cellArea;   
        private double carbonUnitAreaPrice;
        private double cellUnitAreaPrice;
        private string carbonRequirements;
        private string vendorNumber;
        private string vendorName;
        private bool bexist;

        public string CarbonSize
        {
            get
            {
                return carbonSize;
            }

            set
            {
                carbonSize = value;
            }
        }

        public string CellSize
        {
            get
            {
                return cellSize;
            }

            set
            {
                cellSize = value;
            }
        }

        public string CellSizeLenWidQuantity
        {
            get
            {
                return cellSizeLenWidQuantity;
            }

            set
            {
                cellSizeLenWidQuantity = value;
            }
        }



        public double CarbonArea
        {
            get
            {
                return carbonArea;
            }

            set
            {
                carbonArea = value;
            }
        }

        public double PaperArea
        {
            get
            {
                return paperArea;
            }

            set
            {
                paperArea = value;
            }
        }

        public double CellArea
        {
            get
            {
                return cellArea;
            }

            set
            {
                cellArea = value;
            }
        }

        public double CarbonUnitAreaPrice
        {
            get
            {
                return carbonUnitAreaPrice;
            }

            set
            {
                carbonUnitAreaPrice = value;
            }
        }

        public double CellUnitAreaPrice
        {
            get
            {
                return cellUnitAreaPrice;
            }

            set
            {
                cellUnitAreaPrice = value;
            }
        }

        public string CarbonRequirements
        {
            get
            {
                return carbonRequirements;
            }

            set
            {
                carbonRequirements = value;
            }
        }

        public string VendorNumber
        {
            get
            {
                return vendorNumber;
            }

            set
            {
                vendorNumber = value;
            }
        }

        public string VendorName
        {
            get
            {
                return vendorName;
            }

            set
            {
                vendorName = value;
            }
        }

        public bool Bexist
        {
            get
            {
                return bexist;
            }

            set
            {
                bexist = value;
            }
        }
    }

    public class Label
    {
        private string labelSize;
        private double price;
        private string labelRequirements;
        private string vendorNumber;
        private string vendorName;
        private bool bexist;
        private int itype;
        public int IType
        {
            get
            {
                return itype;
            }
            set
            {
                itype = value;
            }
        }
        public string LabelSize
        {
            get
            {
                return labelSize;
            }

            set
            {
                labelSize = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public string LabelRequirements
        {
            get
            {
                return labelRequirements;
            }

            set
            {
                labelRequirements = value;
            }
        }

        public string VendorNumber
        {
            get
            {
                return vendorNumber;
            }

            set
            {
                vendorNumber = value;
            }
        }

        public string VendorName
        {
            get
            {
                return vendorName;
            }

            set
            {
                vendorName = value;
            }
        }

        public bool BExist
        {
            get
            {
                return bexist;
            }

            set
            {
                bexist = value;
            }
        }
    }

    public class Box
    {
        private string boxSize;
        public string BoxSize { get { return boxSize; } set { boxSize = value; } }
        private double boxPrice;
        public double BoxPrice { get { return boxPrice; }set { boxPrice = value; } }
        private string boxTexture;
        public string BoxTexture { get { return boxTexture; } set { boxTexture = value; } }

        public string BoxProcessRequirements
        {
            get
            {
                return boxProcessRequirements;
            }

            set
            {
                boxProcessRequirements = value;
            }
        }

        public string vendorNumber
        {
            get
            {
                return VendorNumber;
            }

            set
            {
                VendorNumber = value;
            }
        }

        public string vendorName
        {
            get
            {
                return VendorName;
            }

            set
            {
                VendorName = value;
            }
        }

        public bool BExist
        {
            get
            {
                return bExist;
            }

            set
            {
                bExist = value;
            }
        }


        private string boxProcessRequirements;
        private string VendorNumber;
        private string VendorName;
        private bool bExist;
    }
    public class Specification
    {
        private string vendorNumber;
        private string vendorName;
        private string color;
        private double price;
        public double Price
        {
            set { price = value; }
            get { return price; }
        }
        public string VendorNumber
        {
            get
            {
                return vendorNumber;
            }

            set
            {
                vendorNumber = value;
            }
        }

        public string VendorName
        {
            get
            {
                return vendorName;
            }

            set
            {
                vendorName = value;
            }
        }
        public string Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }
    }

    public class Others
    {
        private string VendorNumber;
        private string VendorName;
        private double price;
        public double Price
        {
            set { price = value; }
            get { return price; }
        }
        public string vendorNumber
        {
            get
            {
                return VendorNumber;
            }

            set
            {
                VendorNumber = value;
            }
        }

        public string vendorName
        {
            get
            {
                return VendorName;
            }

            set
            {
                VendorName = value;
            }
        }

    }
}
