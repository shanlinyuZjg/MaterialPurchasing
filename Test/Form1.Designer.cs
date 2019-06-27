namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rcMenu = new DevComponents.DotNetBar.RibbonControl();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // rcMenu
            // 
            // 
            // 
            // 
            this.rcMenu.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rcMenu.CaptionVisible = true;
            this.rcMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.rcMenu.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.rcMenu.Location = new System.Drawing.Point(5, 1);
            this.rcMenu.Name = "rcMenu";
            this.rcMenu.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.rcMenu.Size = new System.Drawing.Size(690, 99);
            this.rcMenu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rcMenu.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.rcMenu.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.rcMenu.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.rcMenu.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.rcMenu.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.rcMenu.SystemText.QatDialogAddButton = "&Add >>";
            this.rcMenu.SystemText.QatDialogCancelButton = "Cancel";
            this.rcMenu.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.rcMenu.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.rcMenu.SystemText.QatDialogOkButton = "OK";
            this.rcMenu.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.rcMenu.SystemText.QatDialogRemoveButton = "&Remove";
            this.rcMenu.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.rcMenu.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.rcMenu.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.rcMenu.TabGroupHeight = 14;
            this.rcMenu.TabIndex = 0;
            this.rcMenu.Text = "ribbonControl1";
            this.rcMenu.Click += new System.EventHandler(this.rcMenu_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(118, 62);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "buttonX1";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(303, 62);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 2;
            this.buttonX2.Text = "buttonX2";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check});
            this.dgv.Location = new System.Drawing.Point(44, 106);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(603, 307);
            this.dgv.TabIndex = 3;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellMouseLeave);
            this.dgv.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseUp);
            // 
            // Check
            // 
            this.Check.HeaderText = "选择";
            this.Check.Name = "Check";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 441);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.rcMenu);
            this.EnableGlass = false;
            this.Name = "Form1";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonControl rcMenu;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
    }
}

