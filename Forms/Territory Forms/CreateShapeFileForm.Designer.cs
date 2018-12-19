namespace OMEGA.Forms.Territory_Forms
{
    partial class CreateShapeFileForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelImport = new System.Windows.Forms.Label();
            this.comboBox1Import = new System.Windows.Forms.ComboBox();
            this.labelImportAll = new System.Windows.Forms.Label();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxMainMap = new System.Windows.Forms.CheckBox();
            this.comboBoxname = new System.Windows.Forms.ComboBox();
            this.labelnametype = new System.Windows.Forms.Label();
            this.comboBoxtype = new System.Windows.Forms.ComboBox();
            this.labeltype = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupboxfileinfo = new System.Windows.Forms.GroupBox();
            this.labelOr = new System.Windows.Forms.Label();
            this.labelSaveChoice = new System.Windows.Forms.Label();
            this.radioButtonYes1 = new System.Windows.Forms.RadioButton();
            this.radioButtonNo1 = new System.Windows.Forms.RadioButton();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelpath = new System.Windows.Forms.Label();
            this.textBoxpath = new System.Windows.Forms.TextBox();
            this.labelname = new System.Windows.Forms.Label();
            this.textBoxname = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupboxfileinfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PeachPuff;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.groupboxfileinfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 337);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelImport);
            this.groupBox1.Controls.Add(this.comboBox1Import);
            this.groupBox1.Controls.Add(this.labelImportAll);
            this.groupBox1.Controls.Add(this.radioButtonYes);
            this.groupBox1.Controls.Add(this.radioButtonNo);
            this.groupBox1.Location = new System.Drawing.Point(3, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 65);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import management";
            // 
            // labelImport
            // 
            this.labelImport.AutoSize = true;
            this.labelImport.Location = new System.Drawing.Point(8, 16);
            this.labelImport.Name = "labelImport";
            this.labelImport.Size = new System.Drawing.Size(131, 13);
            this.labelImport.TabIndex = 0;
            this.labelImport.Text = "Select the Import to load : ";
            // 
            // comboBox1Import
            // 
            this.comboBox1Import.FormattingEnabled = true;
            this.comboBox1Import.Location = new System.Drawing.Point(164, 12);
            this.comboBox1Import.Name = "comboBox1Import";
            this.comboBox1Import.Size = new System.Drawing.Size(212, 21);
            this.comboBox1Import.TabIndex = 1;
            this.comboBox1Import.SelectedIndexChanged += new System.EventHandler(this.comboBox1Import_SelectedIndexChanged);
            // 
            // labelImportAll
            // 
            this.labelImportAll.AutoSize = true;
            this.labelImportAll.Enabled = false;
            this.labelImportAll.Location = new System.Drawing.Point(8, 39);
            this.labelImportAll.Name = "labelImportAll";
            this.labelImportAll.Size = new System.Drawing.Size(186, 13);
            this.labelImportAll.TabIndex = 2;
            this.labelImportAll.Text = "Select All the point from this import ? : ";
            this.toolTip1.SetToolTip(this.labelImportAll, "If the import contains a couple of lines or a couple of polygone in the same .gpx" +
        " file, check yes.");
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.AutoSize = true;
            this.radioButtonYes.Location = new System.Drawing.Point(283, 37);
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.Size = new System.Drawing.Size(43, 17);
            this.radioButtonYes.TabIndex = 3;
            this.radioButtonYes.TabStop = true;
            this.radioButtonYes.Text = "Yes";
            this.radioButtonYes.UseVisualStyleBackColor = true;
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.AutoSize = true;
            this.radioButtonNo.Checked = true;
            this.radioButtonNo.Location = new System.Drawing.Point(339, 39);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNo.TabIndex = 4;
            this.radioButtonNo.TabStop = true;
            this.radioButtonNo.Text = "No";
            this.radioButtonNo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PeachPuff;
            this.groupBox2.Controls.Add(this.checkBoxMainMap);
            this.groupBox2.Controls.Add(this.comboBoxname);
            this.groupBox2.Controls.Add(this.labelnametype);
            this.groupBox2.Controls.Add(this.comboBoxtype);
            this.groupBox2.Controls.Add(this.labeltype);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(3, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(383, 94);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data type";
            // 
            // checkBoxMainMap
            // 
            this.checkBoxMainMap.AutoSize = true;
            this.checkBoxMainMap.Checked = true;
            this.checkBoxMainMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMainMap.Font = new System.Drawing.Font("Corbel", 9F);
            this.checkBoxMainMap.Location = new System.Drawing.Point(12, 19);
            this.checkBoxMainMap.Name = "checkBoxMainMap";
            this.checkBoxMainMap.Size = new System.Drawing.Size(93, 18);
            this.checkBoxMainMap.TabIndex = 9;
            this.checkBoxMainMap.Text = "Territory map";
            this.checkBoxMainMap.UseVisualStyleBackColor = true;
            this.checkBoxMainMap.CheckedChanged += new System.EventHandler(this.checkBoxMainMap_CheckedChanged);
            // 
            // comboBoxname
            // 
            this.comboBoxname.Enabled = false;
            this.comboBoxname.FormattingEnabled = true;
            this.comboBoxname.Location = new System.Drawing.Point(153, 65);
            this.comboBoxname.Name = "comboBoxname";
            this.comboBoxname.Size = new System.Drawing.Size(224, 21);
            this.comboBoxname.TabIndex = 8;
            this.comboBoxname.SelectedIndexChanged += new System.EventHandler(this.comboBoxname_SelectedIndexChanged);
            // 
            // labelnametype
            // 
            this.labelnametype.AutoSize = true;
            this.labelnametype.Font = new System.Drawing.Font("Corbel", 9F);
            this.labelnametype.Location = new System.Drawing.Point(9, 68);
            this.labelnametype.Name = "labelnametype";
            this.labelnametype.Size = new System.Drawing.Size(42, 14);
            this.labelnametype.TabIndex = 7;
            this.labelnametype.Text = "Name :";
            // 
            // comboBoxtype
            // 
            this.comboBoxtype.Enabled = false;
            this.comboBoxtype.FormattingEnabled = true;
            this.comboBoxtype.Location = new System.Drawing.Point(153, 38);
            this.comboBoxtype.Name = "comboBoxtype";
            this.comboBoxtype.Size = new System.Drawing.Size(224, 21);
            this.comboBoxtype.TabIndex = 6;
            this.comboBoxtype.SelectedIndexChanged += new System.EventHandler(this.comboBoxtype_SelectedIndexChanged);
            // 
            // labeltype
            // 
            this.labeltype.AutoSize = true;
            this.labeltype.Font = new System.Drawing.Font("Corbel", 9F);
            this.labeltype.Location = new System.Drawing.Point(9, 45);
            this.labeltype.Name = "labeltype";
            this.labeltype.Size = new System.Drawing.Size(36, 14);
            this.labeltype.TabIndex = 5;
            this.labeltype.Text = "Type :";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(249, 308);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(342, 308);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(49, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupboxfileinfo
            // 
            this.groupboxfileinfo.BackColor = System.Drawing.Color.PeachPuff;
            this.groupboxfileinfo.Controls.Add(this.labelOr);
            this.groupboxfileinfo.Controls.Add(this.labelSaveChoice);
            this.groupboxfileinfo.Controls.Add(this.radioButtonYes1);
            this.groupboxfileinfo.Controls.Add(this.radioButtonNo1);
            this.groupboxfileinfo.Controls.Add(this.buttonBrowse);
            this.groupboxfileinfo.Controls.Add(this.labelpath);
            this.groupboxfileinfo.Controls.Add(this.textBoxpath);
            this.groupboxfileinfo.Controls.Add(this.labelname);
            this.groupboxfileinfo.Controls.Add(this.textBoxname);
            this.groupboxfileinfo.Location = new System.Drawing.Point(3, 3);
            this.groupboxfileinfo.Name = "groupboxfileinfo";
            this.groupboxfileinfo.Size = new System.Drawing.Size(383, 140);
            this.groupboxfileinfo.TabIndex = 1;
            this.groupboxfileinfo.TabStop = false;
            this.groupboxfileinfo.Text = "File Information";
            // 
            // labelOr
            // 
            this.labelOr.AutoSize = true;
            this.labelOr.Location = new System.Drawing.Point(161, 73);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(23, 13);
            this.labelOr.TabIndex = 15;
            this.labelOr.Text = "OR";
            // 
            // labelSaveChoice
            // 
            this.labelSaveChoice.AutoSize = true;
            this.labelSaveChoice.Location = new System.Drawing.Point(9, 54);
            this.labelSaveChoice.Name = "labelSaveChoice";
            this.labelSaveChoice.Size = new System.Drawing.Size(167, 13);
            this.labelSaveChoice.TabIndex = 14;
            this.labelSaveChoice.Text = "Save the shapefile on this dataset";
            // 
            // radioButtonYes1
            // 
            this.radioButtonYes1.AutoSize = true;
            this.radioButtonYes1.Checked = true;
            this.radioButtonYes1.Location = new System.Drawing.Point(287, 52);
            this.radioButtonYes1.Name = "radioButtonYes1";
            this.radioButtonYes1.Size = new System.Drawing.Size(43, 17);
            this.radioButtonYes1.TabIndex = 12;
            this.radioButtonYes1.TabStop = true;
            this.radioButtonYes1.Text = "Yes";
            this.radioButtonYes1.UseVisualStyleBackColor = true;
            this.radioButtonYes1.CheckedChanged += new System.EventHandler(this.radioButtonYes1_CheckedChanged);
            // 
            // radioButtonNo1
            // 
            this.radioButtonNo1.AutoSize = true;
            this.radioButtonNo1.Location = new System.Drawing.Point(338, 52);
            this.radioButtonNo1.Name = "radioButtonNo1";
            this.radioButtonNo1.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNo1.TabIndex = 13;
            this.radioButtonNo1.Text = "No";
            this.radioButtonNo1.UseVisualStyleBackColor = true;
            this.radioButtonNo1.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Enabled = false;
            this.buttonBrowse.Location = new System.Drawing.Point(358, 106);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(24, 21);
            this.buttonBrowse.TabIndex = 4;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelpath
            // 
            this.labelpath.AutoSize = true;
            this.labelpath.Enabled = false;
            this.labelpath.Location = new System.Drawing.Point(99, 90);
            this.labelpath.Name = "labelpath";
            this.labelpath.Size = new System.Drawing.Size(153, 13);
            this.labelpath.TabIndex = 10;
            this.labelpath.Text = "Path to save the new shapefile";
            // 
            // textBoxpath
            // 
            this.textBoxpath.Enabled = false;
            this.textBoxpath.Location = new System.Drawing.Point(6, 106);
            this.textBoxpath.Name = "textBoxpath";
            this.textBoxpath.Size = new System.Drawing.Size(346, 20);
            this.textBoxpath.TabIndex = 9;
            this.textBoxpath.TextChanged += new System.EventHandler(this.textBoxpath_TextChanged);
            // 
            // labelname
            // 
            this.labelname.AutoSize = true;
            this.labelname.Location = new System.Drawing.Point(9, 26);
            this.labelname.Name = "labelname";
            this.labelname.Size = new System.Drawing.Size(139, 13);
            this.labelname.TabIndex = 8;
            this.labelname.Text = "Name for the new shapefile ";
            // 
            // textBoxname
            // 
            this.textBoxname.Location = new System.Drawing.Point(165, 23);
            this.textBoxname.Name = "textBoxname";
            this.textBoxname.Size = new System.Drawing.Size(213, 20);
            this.textBoxname.TabIndex = 7;
            this.textBoxname.TextChanged += new System.EventHandler(this.textBoxname_TextChanged);
            // 
            // CreateShapeFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 337);
            this.Controls.Add(this.panel1);
            this.Name = "CreateShapeFileForm";
            this.Text = "CreateShapeFileForm";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupboxfileinfo.ResumeLayout(false);
            this.groupboxfileinfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupboxfileinfo;
        private System.Windows.Forms.Label labelImportAll;
        private System.Windows.Forms.Label labelImport;
        internal System.Windows.Forms.ComboBox comboBox1Import;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelpath;
        internal System.Windows.Forms.TextBox textBoxpath;
        private System.Windows.Forms.Label labelname;
        internal System.Windows.Forms.TextBox textBoxname;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ComboBox comboBoxname;
        private System.Windows.Forms.Label labelnametype;
        internal System.Windows.Forms.ComboBox comboBoxtype;
        private System.Windows.Forms.Label labeltype;
        internal System.Windows.Forms.CheckBox checkBoxMainMap;
        internal System.Windows.Forms.RadioButton radioButtonNo;
        internal System.Windows.Forms.RadioButton radioButtonYes;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelOr;
        private System.Windows.Forms.Label labelSaveChoice;
        internal System.Windows.Forms.RadioButton radioButtonYes1;
        internal System.Windows.Forms.RadioButton radioButtonNo1;
    }
}