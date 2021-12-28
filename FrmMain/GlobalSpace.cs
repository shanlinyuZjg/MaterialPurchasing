using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Global
{
    public class GlobalSpace
    {
        public static readonly string fsconfigfilepath = "M:\\mfgsys\\fs.cfg";
    //    public static readonly string fsconfigfilepath = "Z:\\mfgsys\\fs.cfg";
        public static readonly string fsTestconfigfilepath = "Z:\\mfgsys\\fs.cfg";

        public static readonly string FDAConnstr = "server=192.168.29.100;database=RYZY_YJM;uid=erp;pwd=123456";
        public static readonly string FSDBConnstr = "server=192.168.8.11;database=FSDB;uid=xym;pwd=xym-123";
        public static readonly string TestFSDBConnstr = "server=192.168.8.12;database=FSDB;uid=xym;pwd=xym-123";

        public static readonly string FSDBMRConnstr = "server=192.168.8.11;database=fsdbmr;uid=program;pwd=program";
        public static readonly string TestFSDBMRConnstr = "server=192.168.8.51;database=fsdbtr;uid=program;pwd=program";

        public static readonly string oledbconnstrFSDBMR = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;Data Source=192.168.8.11;Initial Catalog=fsdbmr;User ID=program;Password=program";
        public static readonly string oledbconnstrFSDB = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;Data Source=192.168.8.11;Initial Catalog=fsdb;User ID=xym;Password=xym-123";
      //  public static readonly string connstrBPM = "server=192.168.8.67;database=UltimusBusiness;uid=fs;pwd=ERP1075+-*/";
        public static readonly string connstrBPM = "server=192.168.8.28;database=BPMDBRY;uid=workflow;pwd=workflow";
        public static readonly string GeneralEBRConnStr = "server=192.168.8.49;database=RYStockEBR;uid=stock;pwd=stockEBR2020!";
        public static readonly string WHOEBRConnStr = "server=192.168.8.49;database=RYStockEBR;uid=stock;pwd=stockEBR2020!";
       // public static readonly string GeneralEBRConnStr = "server=192.168.8.49;database=RYStockWHOEBR;uid=stock;pwd=stockEBR2020!";
    //    public static  string EBRConnStr = string.Empty;
        public static  string EBRConnStr = "server=192.168.8.49;database=RYStockEBR;uid=stock;pwd=stockEBR2020!";
        public static  string RYData = "server=192.168.8.49;database=RYDATA;uid=xym;pwd=xym-123";

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
        public static List<string> vendorEmailList;
        public  List<string> DomesticItemPriceList = null;
        public static List<Carbon> carbonList = new List<Carbon>();
        public static List<Label> labelList = new List<Label>();
        public static List<Box> boxList = new List<Box>();
        public static List<Others> othersList = new List<Others>();
        public static List<Specification> specificationList = new List<Specification>();
        public static double ItemPrice = 0;
        public static double VialPrice = 0;
        public static Dictionary<string, string> dictFDAItem = new Dictionary<string, string>();
        //用于开票
        public static DataTable dtInvoice;
        public static string ShouCeNumber = string.Empty;
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

    public static class PurchaseUser
    {

        private static string userEmail;
        private static string userEmailDecryptedPassword;
        private static int userStatus;
        private static string supervisorID;
        private static string supervisorEmail;
        private static string purchaseType;
        private static string userPOType;
        private static int notFollowPORules;
        private static string pOTypeWithRange;
        private static string itemReceiveType;
        public static string UserEmail
        {
            get
            {
                return userEmail;
            }

            set
            {
                userEmail = value;
            }
        }
        private static int pOTogether;
        private static string userName;
        private static string group;
        private static string confirmGroup;
        private static int partlyNotCompareItemPrice;
        public static string UserEmailDecryptedPassword
        {
            get
            {
                return userEmailDecryptedPassword;
            }

            set
            {
                userEmailDecryptedPassword = value;
            }
        }

        public static int UserStatus
        {
            get
            {
                return userStatus;
            }

            set
            {
                userStatus = value;
            }
        }

        public static string SupervisorID
        {
            get
            {
                return supervisorID;
            }

            set
            {
                supervisorID = value;
            }
        }

        public static string SupervisorEmail
        {
            get
            {
                return supervisorEmail;
            }

            set
            {
                supervisorEmail = value;
            }
        }

        public static string PurchaseType
        {
            get
            {
                return purchaseType;
            }

            set
            {
                purchaseType = value;
            }
        }

        public static string UserPOType
        {
            get
            {
                return userPOType;
            }
            set
            {
                userPOType = value;
            }
        }

        public static int PriceCompare
        {
            get
            {
                return priceCompare;
            }

            set
            {
                priceCompare = value;
            }
        }

        public static string PONumberSequenceNumberRange
        {
            get
            {
                return pONumberSequenceNumberRange;
            }

            set
            {
                pONumberSequenceNumberRange = value;
            }
        }

        public static string POItemConfirmType
        {
            get
            {
                return pOItemConfirmType;
            }

            set
            {
                pOItemConfirmType = value;
            }
        }

        public static int POItemOthersConfirm
        {
            get
            {
                return pOItemOthersConfirm;
            }

            set
            {
                pOItemOthersConfirm = value;
            }
        }

        public static int NotFollowPORules
        {
            get
            {
                return notFollowPORules;
            }

            set
            {
                notFollowPORules = value;
            }
        }

        public static string POTypeWithRange
        {
            get
            {
                return pOTypeWithRange;
            }

            set
            {
                pOTypeWithRange = value;
            }
        }

        public static int POTogether
        {
            get
            {
                return pOTogether;
            }

            set
            {
                pOTogether = value;
            }
        }

        public static string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public static string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public static string ItemReceiveType
        {
            get
            {
                return itemReceiveType;
            }

            set
            {
                itemReceiveType = value;
            }
        }

        public static string Group
        {
            get
            {
                return group;
            }

            set
            {
                group = value;
            }
        }

        public static string ConfirmGroup
        {
            get
            {
                return confirmGroup;
            }

            set
            {
                confirmGroup = value;
            }
        }

        public static int PartlyNotCompareItemPrice
        {
            get
            {
                return partlyNotCompareItemPrice;
            }

            set
            {
                partlyNotCompareItemPrice = value;
            }
        }

        private static int pOItemOthersConfirm;
        private static string pONumberSequenceNumberRange;
        private static string pOItemConfirmType;
        private static int priceCompare;
        private static string userID;
    }

    public static class StockUser
    {
        private static string userID;
        private static string userName;
        private static string password;
        private static string type;
        private static string privilege;
        private static string number;
        private static string district;
        private static string isVial;
        private static string isDirectERP;
        private static string isERP;
        private static string fileTracedNumber;
        private static string fileEdition;
        private static string effectiveDate;
        private static string stock;
        private static string recordArea;

        public static string RecordArea
        {
            get
            {
                return recordArea;
            }

            set
            {
                recordArea = value;
            }
        }
        public static string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }
        private static string supervisor;
        public static string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public static string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public static string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public static string Privilege
        {
            get
            {
                return privilege;
            }

            set
            {
                privilege = value;
            }
        }

        public static string Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }

        public static string Supervisor
        {
            get
            {
                return supervisor;
            }

            set
            {
                supervisor = value;
            }
        }

        public static string District
        {
            get
            {
                return district;
            }

            set
            {
                district = value;
            }
        }
        public static string IsVial { get => isVial; set => isVial = value; }
        public static string IsDirectERP { get => isDirectERP; set => isDirectERP = value; }
        public static string IsERP { get => isERP; set => isERP = value; }
        public static string FileTracedNumber { get => fileTracedNumber; set => fileTracedNumber = value; }
        public static string FileEdition { get => fileEdition; set => fileEdition = value; }
        public static string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public static string Stock { get => stock; set => stock = value; }
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
        private double paperUnitAreaPrice;
        private double cellUnitAreaPrice;
        private string carbonRequirements;
        private string vendorNumber;
        private string vendorName;
        private bool bexist;

        public double PaperUnitAreaPrice
        {
            get { return paperUnitAreaPrice; }
            set { paperUnitAreaPrice = value; }
        }

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
        private string Remark;
        private double price;
        private double area;
        private string calType;
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
        public string remark
        {
            set { Remark = value; }
            get { return Remark; }
        }

        public double Area
        {
            get
            {
                return area;
            }

            set
            {
                area = value;
            }
        }

        public string CalType
        {
            get
            {
                return calType;
            }

            set
            {
                calType = value;
            }
        }
    }

  
    public static class ItemInfoForSearch
    {
        private static string itemNumber;
        private static string itemDescription;
        private static string itemUM;

        public static string ItemNumber
        {
            get
            {
                return itemNumber;
            }

            set
            {
                itemNumber = value;
            }
        }

        public static string ItemDescription
        {
            get
            {
                return itemDescription;
            }

            set
            {
                itemDescription = value;
            }
        }

        public static string ItemUM
        {
            get
            {
                return itemUM;
            }

            set
            {
                itemUM = value;
            }
        }
    }



}
