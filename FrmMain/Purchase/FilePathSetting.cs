using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global.Helper;

namespace Global.Purchase
{
    public partial class FilePathSetting : Office2007Form
    {
        string userID = string.Empty;
        public FilePathSetting()
        {
            InitializeComponent();
        }

        public FilePathSetting(string strid)
        {
            userID = strid;
            MessageBoxEx.EnableGlass = false;
            this.EnableGlass = false;
            InitializeComponent();
        }

        private void btnPOChoose_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            if(fbdFilePath.ShowDialog() == DialogResult.OK)
            {
                filePath = fbdFilePath.SelectedPath;
                tbPODetailPath.Text = filePath;
            }
        }

        private void btnInvoiceChoose_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            if (fbdFilePath.ShowDialog() == DialogResult.OK)
            {
                filePath = fbdFilePath.SelectedPath;
                tbInvoiceDetailPath.Text = filePath;
            }
        }

        private void btnPOSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbPODetailPath.Text.Trim()))
            {
                MessageBoxEx.Show("路径不能为空！", "提示");
            }
            else
            {
                string sqlSelect = @"Select Count(Id) From PurchaseDepartmentFilePathByCMF Where Name='"+tbPODetailPath.Tag+"' And BuyerID='"+userID+"'";
                if(SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlSelect))
                {
                    string sqlUpdate = @"Update PurchaseDepartmentFilePathByCMF Set FilePath='" + tbPODetailPath.Text + "' Where BuyerID='" + userID + "' And Name='"+ tbPODetailPath.Tag + "'";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                    {
                        MessageBoxEx.Show("更新成功！", "提示");
                    }
                    else
                    {
                        MessageBoxEx.Show("更新失败！", "提示");
                    }
                }
                else
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentFilePathByCMF (Name,FilePath,BuyerID) Values ('"+tbPODetailPath.Tag+"','"+tbPODetailPath.Text+"','"+userID+"')";
                    if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert) )
                    {
                        MessageBoxEx.Show("添加成功！", "提示");
                    }
                    else
                    {
                        MessageBoxEx.Show("添加失败！", "提示");
                    }
                }
                
            }
        }

        private void btnInvoiceSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbInvoiceDetailPath.Text.Trim()))
            {
                MessageBoxEx.Show("路径不能为空！", "提示");
            }
            else
            {
                string sqlSelect = @"Select Count(Id) From PurchaseDepartmentFilePathByCMF Where Name='" + tbInvoiceDetailPath.Tag + "'";
                if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlSelect))
                {
                    string sqlUpdate = @"Update PurchaseDepartmentFilePathByCMF Set FilePath='" + tbInvoiceDetailPath.Text + "' Where BuyerID='" + userID + "' And Name='" + tbInvoiceDetailPath.Tag + "'";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                    {
                        MessageBoxEx.Show("更新成功！", "提示");
                    }
                    else
                    {
                        MessageBoxEx.Show("更新失败！", "提示");
                    }
                }
                else
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentFilePathByCMF (Name,FilePath,BuyerID) Values ('" + tbInvoiceDetailPath.Tag + "','" + tbInvoiceDetailPath.Text + "','" + userID + "')";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert) )
                    {
                        MessageBoxEx.Show("添加成功！", "提示");
                    }
                    else
                    {
                        MessageBoxEx.Show("添加失败！", "提示");
                    }
                }

            }
        }

        private void FilePathSetting_Load(object sender, EventArgs e)
        {
            LoadFileExportPath(userID);
        }

        private void LoadFileExportPath(string strid)
        {
            string sqlSelect = @"Select Name,FilePath From PurchaseDepartmentFilePathByCMF Where BuyerID='"+strid+"' And Status = 0";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if(dtTemp.Rows.Count > 0)
            {
                foreach(DataRow dr in dtTemp.Rows)
                {
                    if(dr["Name"].ToString() == tbPODetailPath.Tag.ToString())
                    {
                        tbPODetailPath.Text = dr["FilePath"].ToString();
                    }
                    else if(dr["Name"].ToString() == tbInvoiceDetailPath.Tag.ToString())
                    {
                        tbInvoiceDetailPath.Text = dr["FilePath"].ToString();
                    }
                }
            }
        }
    }
}
