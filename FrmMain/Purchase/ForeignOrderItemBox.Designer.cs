namespace Global.Purchase
{
    partial class ForeignOrderItemBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbBoxMannual = new System.Windows.Forms.CheckBox();
            this.btnBoxAddWithoutRecord = new DevComponents.DotNetBar.ButtonX();
            this.dgvBox = new System.Windows.Forms.DataGridView();
            this.btnBoxSubmit = new DevComponents.DotNetBar.ButtonX();
            this.labelX18 = new DevComponents.DotNetBar.LabelX();
            this.tbBoxLength = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbBoxTexture = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.tbBoxHeight = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbBoxProcessRequirement = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX17 = new DevComponents.DotNetBar.LabelX();
            this.labelX15 = new DevComponents.DotNetBar.LabelX();
            this.tbBoxWidth = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel3
            // 
            this.groupPanel3.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.cbBoxMannual);
            this.groupPanel3.Controls.Add(this.btnBoxAddWithoutRecord);
            this.groupPanel3.Controls.Add(this.dgvBox);
            this.groupPanel3.Controls.Add(this.btnBoxSubmit);
            this.groupPanel3.Controls.Add(this.labelX18);
            this.groupPanel3.Controls.Add(this.tbBoxLength);
            this.groupPanel3.Controls.Add(this.tbBoxTexture);
            this.groupPanel3.Controls.Add(this.labelX14);
            this.groupPanel3.Controls.Add(this.tbBoxHeight);
            this.groupPanel3.Controls.Add(this.tbBoxProcessRequirement);
            this.groupPanel3.Controls.Add(this.labelX17);
            this.groupPanel3.Controls.Add(this.labelX15);
            this.groupPanel3.Controls.Add(this.tbBoxWidth);
            this.groupPanel3.Controls.Add(this.labelX16);
            this.groupPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel3.Location = new System.Drawing.Point(3, 14);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(982, 308);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 5;
            this.groupPanel3.Text = "纸盒设置";
            // 
            // cbBoxMannual
            // 
            this.cbBoxMannual.AutoSize = true;
            this.cbBoxMannual.Location = new System.Drawing.Point(8, 37);
            this.cbBoxMannual.Name = "cbBoxMannual";
            this.cbBoxMannual.Size = new System.Drawing.Size(54, 18);
            this.cbBoxMannual.TabIndex = 11;
            this.cbBoxMannual.Text = "手工";
            this.cbBoxMannual.UseVisualStyleBackColor = true;
            this.cbBoxMannual.CheckedChanged += new System.EventHandler(this.cbBoxMannual_CheckedChanged);
            // 
            // btnBoxAddWithoutRecord
            // 
            this.btnBoxAddWithoutRecord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBoxAddWithoutRecord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBoxAddWithoutRecord.Location = new System.Drawing.Point(875, 4);
            this.btnBoxAddWithoutRecord.Name = "btnBoxAddWithoutRecord";
            this.btnBoxAddWithoutRecord.Size = new System.Drawing.Size(87, 27);
            this.btnBoxAddWithoutRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBoxAddWithoutRecord.TabIndex = 10;
            this.btnBoxAddWithoutRecord.Text = "无记录添加";
            this.btnBoxAddWithoutRecord.Click += new System.EventHandler(this.btnBoxAddWithoutRecord_Click);
            // 
            // dgvBox
            // 
            this.dgvBox.AllowUserToAddRows = false;
            this.dgvBox.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBox.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(211)))), ((int)(((byte)(255)))));
            this.dgvBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBox.Location = new System.Drawing.Point(12, 71);
            this.dgvBox.Name = "dgvBox";
            this.dgvBox.RowTemplate.Height = 23;
            this.dgvBox.Size = new System.Drawing.Size(950, 192);
            this.dgvBox.TabIndex = 4;
            // 
            // btnBoxSubmit
            // 
            this.btnBoxSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBoxSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBoxSubmit.Location = new System.Drawing.Point(875, 37);
            this.btnBoxSubmit.Name = "btnBoxSubmit";
            this.btnBoxSubmit.Size = new System.Drawing.Size(87, 27);
            this.btnBoxSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBoxSubmit.TabIndex = 2;
            this.btnBoxSubmit.Text = "多条添加";
            this.btnBoxSubmit.Click += new System.EventHandler(this.btnBoxSubmit_Click);
            // 
            // labelX18
            // 
            this.labelX18.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX18.Location = new System.Drawing.Point(8, 4);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new System.Drawing.Size(35, 27);
            this.labelX18.TabIndex = 0;
            this.labelX18.Text = "长度";
            // 
            // tbBoxLength
            // 
            // 
            // 
            // 
            this.tbBoxLength.Border.Class = "TextBoxBorder";
            this.tbBoxLength.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoxLength.Location = new System.Drawing.Point(48, 4);
            this.tbBoxLength.Name = "tbBoxLength";
            this.tbBoxLength.PreventEnterBeep = true;
            this.tbBoxLength.Size = new System.Drawing.Size(79, 23);
            this.tbBoxLength.TabIndex = 0;
            this.tbBoxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBoxLength_KeyPress);
            // 
            // tbBoxTexture
            // 
            // 
            // 
            // 
            this.tbBoxTexture.Border.Class = "TextBoxBorder";
            this.tbBoxTexture.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoxTexture.Location = new System.Drawing.Point(118, 33);
            this.tbBoxTexture.Name = "tbBoxTexture";
            this.tbBoxTexture.PreventEnterBeep = true;
            this.tbBoxTexture.Size = new System.Drawing.Size(313, 23);
            this.tbBoxTexture.TabIndex = 1;
            // 
            // labelX14
            // 
            this.labelX14.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Location = new System.Drawing.Point(437, 31);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(138, 27);
            this.labelX14.TabIndex = 0;
            this.labelX14.Text = "工艺要求及表面处理";
            // 
            // tbBoxHeight
            // 
            // 
            // 
            // 
            this.tbBoxHeight.Border.Class = "TextBoxBorder";
            this.tbBoxHeight.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoxHeight.Location = new System.Drawing.Point(315, 4);
            this.tbBoxHeight.Name = "tbBoxHeight";
            this.tbBoxHeight.PreventEnterBeep = true;
            this.tbBoxHeight.Size = new System.Drawing.Size(79, 23);
            this.tbBoxHeight.TabIndex = 1;
            this.tbBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBoxHeight_KeyPress);
            // 
            // tbBoxProcessRequirement
            // 
            // 
            // 
            // 
            this.tbBoxProcessRequirement.Border.Class = "TextBoxBorder";
            this.tbBoxProcessRequirement.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoxProcessRequirement.Location = new System.Drawing.Point(581, 36);
            this.tbBoxProcessRequirement.Name = "tbBoxProcessRequirement";
            this.tbBoxProcessRequirement.PreventEnterBeep = true;
            this.tbBoxProcessRequirement.Size = new System.Drawing.Size(288, 23);
            this.tbBoxProcessRequirement.TabIndex = 1;
            // 
            // labelX17
            // 
            this.labelX17.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX17.Location = new System.Drawing.Point(142, 4);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new System.Drawing.Size(35, 27);
            this.labelX17.TabIndex = 0;
            this.labelX17.Text = "宽度";
            // 
            // labelX15
            // 
            this.labelX15.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX15.Location = new System.Drawing.Point(276, 4);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new System.Drawing.Size(35, 27);
            this.labelX15.TabIndex = 0;
            this.labelX15.Text = "高度";
            // 
            // tbBoxWidth
            // 
            // 
            // 
            // 
            this.tbBoxWidth.Border.Class = "TextBoxBorder";
            this.tbBoxWidth.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoxWidth.Location = new System.Drawing.Point(180, 4);
            this.tbBoxWidth.Name = "tbBoxWidth";
            this.tbBoxWidth.PreventEnterBeep = true;
            this.tbBoxWidth.Size = new System.Drawing.Size(79, 23);
            this.tbBoxWidth.TabIndex = 1;
            this.tbBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbBoxWidth_KeyPress);
            // 
            // labelX16
            // 
            this.labelX16.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Location = new System.Drawing.Point(76, 33);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(35, 27);
            this.labelX16.TabIndex = 0;
            this.labelX16.Text = "材质";
            // 
            // ForeignOrderItemBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 323);
            this.Controls.Add(this.groupPanel3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "ForeignOrderItemBox";
            this.Text = "纸盒选择";
            this.Load += new System.EventHandler(this.ForeignOrderItemBox_Load);
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.ButtonX btnBoxAddWithoutRecord;
        private System.Windows.Forms.DataGridView dgvBox;
        private DevComponents.DotNetBar.ButtonX btnBoxSubmit;
        private DevComponents.DotNetBar.LabelX labelX18;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoxLength;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoxTexture;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoxHeight;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoxProcessRequirement;
        private DevComponents.DotNetBar.LabelX labelX17;
        private DevComponents.DotNetBar.LabelX labelX15;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoxWidth;
        private DevComponents.DotNetBar.LabelX labelX16;
        private System.Windows.Forms.CheckBox cbBoxMannual;
    }
}