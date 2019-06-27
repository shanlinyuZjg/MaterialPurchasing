using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace Global.Helper
{
   public static class ExcelHelper
    {
        public static DataTable ExcelToDataTable(string pathName, string sheetName)
        {
            DataTable dtContainExcel = new DataTable();
            string strConn = string.Empty;
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "Sheet1";
            }
            FileInfo file = new FileInfo(pathName);
            if (!file.Exists)
            {
                throw new Exception("文件不存在！");
            }
            string extension = file.Extension;
            switch (extension)
            {
                case ".xls":
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;";
                    break;
                case ".xlsx":
                    //需要注意的是，12.0支持64位程序，因此使用的话，在生成程序时，目标平台要设置为x86。
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                    break;
                default:
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;";
                    break;
            }

            //连接Excel
            OleDbConnection cnnxls = new OleDbConnection(strConn);
            //读取Excel中表Sheet1
            OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}]", sheetName), cnnxls);
            DataSet ds = new DataSet();
            //将Excel里边表sheet1的内容部装载到内存表datatable中
            oda.Fill(dtContainExcel);
            return dtContainExcel;
        }

        /// <summary>
        /// DataTable to Excel（将数据表中的数据读取到excel格式内存中）
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="excelType">excel格式</param>
        /// <param name="sheetName">excel工作表名称</param>
        /// <returns>内存流数据</returns>
        public static Stream DataTableToExcel(DataTable dataTable, string excelType, string sheetName = "sheet1")
        {
            Stream stream = null;
            try
            {
                //根据excel文件类型创建excel数据结构
                switch (excelType)
                {
                    case ".xlsx":
                        stream = DataTableToExcelXlsx(dataTable, sheetName);
                        break;
                    case ".xls":
                        stream = DataTableToExcelXls(dataTable, sheetName);
                        break;
                    default:
                        stream = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stream;
        }

        /// <summary>
        /// DataTable to Excel97-2003（将数据表中的数据读取到excel格式内存中）
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="sheetName">excel工作表名称</param>
        /// <returns>内存流数据</returns>
        public static Stream DataTableToExcelXls(DataTable dataTable, string sheetName)
        {
            try
            {
                const int startIndex = 0;
                var fields = dataTable.Columns;
                //创建excel数据结构
                var workbook = new HSSFWorkbook();
                //创建excel工作表
                var sheet = workbook.CreateSheet(sheetName);
                sheet.DefaultRowHeight = 200 * 20;
                #region 创建标题行
                var row = sheet.CreateRow(startIndex);
                //         var headStyle = GetHeadStyle(workbook);
                foreach (DataColumn column in dataTable.Columns)
                {
                    var cellIndex = fields.IndexOf(column) + startIndex;
                    var cell = row.CreateCell(cellIndex);
                    cell.SetCellValue(column.ColumnName);
                    //         cell.CellStyle = headStyle;
                    sheet.AutoSizeColumn(cellIndex);
                }
                #endregion
                #region 创建数据行
                int rowIndex = 0;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    row = sheet.CreateRow(rowIndex + 1);
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        var cellIndex = fields.IndexOf(column) + startIndex;
                        //       var dataStyle = GetDataStyle(workbook);
                        var cell = row.CreateCell(cellIndex);
                        //        cell.CellStyle = dataStyle;
                        var value = dataRow[column.ColumnName];
                        switch ((value ?? string.Empty).GetType().Name.ToLower())
                        {
                            case "int32":
                            case "int64":
                            case "decimal":
                                //      dataStyle.Alignment = HorizontalAlignment.RIGHT;
                                //      cell.SetCellValue(ZConvert.To<double>(value, 0));
                                break;
                            default:
                                //       cell.CellStyle.Alignment = HorizontalAlignment.LEFT;
                                //       cell.SetCellValue(ZConvert.ToString(value));
                                break;
                        }
                    }
                    rowIndex++;
                }
                #endregion
                #region 将数据写到内存数据流
                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;//指定当前流的位置从0开始

                workbook = null;
                sheet = null;
                row = null;
                #endregion
                return ms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DataTable to Excel2007（将数据表中的数据读取到excel格式内存中）
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="sheetName">excel工作表名称</param>
        /// <returns>内存流数据</returns>
        public static Stream DataTableToExcelXlsx(DataTable dataTable, string sheetName)
        {
            try
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetName);
                    ws.Cells["A1"].LoadFromDataTable(dataTable, true);
                    MemoryStream ms = new MemoryStream();
                    pck.SaveAs(ms);
                    ms.Flush();
                    ms.Position = 0;//指定当前流的位置从0开始
                    
                    return ms;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //设置单元格格式函数，边框粗细3个可选
        public static ICellStyle GetCellStyle(IWorkbook workbook, int borderThickness)
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            NPOI.SS.UserModel.BorderStyle borderType;
            switch (borderThickness)
            {
                case 0:
                    borderType = NPOI.SS.UserModel.BorderStyle.Thin;
                    break;
                case 1:
                    borderType = NPOI.SS.UserModel.BorderStyle.Medium;
                    break;
                case 2:
                    borderType = NPOI.SS.UserModel.BorderStyle.Thick;
                    break;
                default:
                    borderType = NPOI.SS.UserModel.BorderStyle.Thin;
                    break;
            }
            cellStyle.BorderBottom = borderType;
            cellStyle.BorderTop = borderType;
            cellStyle.BorderLeft = borderType;
            cellStyle.BorderRight = borderType;

            IFont font = workbook.CreateFont();//设置字体大小和颜色
            font.FontName = "宋体";
            font.FontHeightInPoints = 13;
            cellStyle.SetFont(font);
            return cellStyle;
        }

        //设置表头格式函数
        public static ICellStyle GetTitleCellStyle(IWorkbook workbook)
        {
            ICellStyle cell1Style = workbook.CreateCellStyle();
            cell1Style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cell1Style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cell1Style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cell1Style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cell1Style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cell1Style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            IFont font = workbook.CreateFont(); //设置字体大小和颜色
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 13;
            font.Color = NPOI.HSSF.Util.HSSFColor.Blue.Index;
            cell1Style.SetFont(font);
            cell1Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
            cell1Style.FillPattern = FillPattern.SolidForeground;
            return cell1Style;
        }


    }
}
