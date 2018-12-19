namespace OMEGA.Forms.Territory_Forms
{
    partial class AddShapeFileForm
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
            this.buttoncancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxname = new System.Windows.Forms.ComboBox();
            this.labelnametype = new System.Windows.Forms.Label();
            this.comboBoxtype = new System.Windows.Forms.ComboBox();
            this.labeltype = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxMainMap = new System.Windows.Forms.CheckBox();
            this.buttonload = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelname = new System.Windows.Forms.Label();
            this.textBoxLoad = new System.Windows.Forms.TextBox();
            this.labelLoad = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PeachPuff;
            this.panel1.Controls.Add(this.buttoncancel);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 234);
            this.panel1.TabIndex = 0;
            // 
            // buttoncancel
            // 
            this.buttoncancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttoncancel.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttoncancel.Location = new System.Drawing.Point(233, 208);
            this.buttoncancel.Name = "buttoncancel";
            this.buttoncancel.Size = new System.Drawing.Size(73, 23);
            this.buttoncancel.TabIndex = 3;
            this.buttoncancel.Text = "Cancel";
            this.buttoncancel.UseVisualStyleBackColor = true;
            this.buttoncancel.Click += new System.EventHandler(this.buttoncancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Enabled = false;
            this.buttonOK.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonOK.Location = new System.Drawing.Point(312, 208);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(48, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PeachPuff;
            this.groupBox2.Controls.Add(this.comboBoxname);
            this.groupBox2.Controls.Add(this.labelnametype);
            this.groupBox2.Controls.Add(this.comboBoxtype);
            this.groupBox2.Controls.Add(this.labeltype);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(3, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 66);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data type";
            // 
            // comboBoxname
            // 
            this.comboBoxname.FormattingEnabled = true;
            this.comboBoxname.Location = new System.Drawing.Point(122, 42);
            this.comboBoxname.Name = "comboBoxname";
            this.comboBoxname.Size = new System.Drawing.Size(224, 21);
            this.comboBoxname.TabIndex = 8;
            this.comboBoxname.SelectedIndexChanged += new System.EventHandler(this.comboBoxname_SelectedIndexChanged);
            // 
            // labelnametype
            // 
            this.labelnametype.AutoSize = true;
            this.labelnametype.Font = new System.Drawing.Font("Corbel", 9F);
            this.labelnametype.Location = new System.Drawing.Point(8, 44);
            this.labelnametype.Name = "labelnametype";
            this.labelnametype.Size = new System.Drawing.Size(42, 14);
            this.labelnametype.TabIndex = 7;
            this.labelnametype.Text = "Name :";
            // 
            // comboBoxtype
            // 
            this.comboBoxtype.FormattingEnabled = true;
            this.comboBoxtype.Location = new System.Drawing.Point(122, 15);
            this.comboBoxtype.Name = "comboBoxtype";
            this.comboBoxtype.Size = new System.Drawing.Size(224, 21);
            this.comboBoxtype.TabIndex = 6;
            this.comboBoxtype.SelectedIndexChanged += new System.EventHandler(this.comboBoxtype_SelectedIndexChanged);
            // 
            // labeltype
            // 
            this.labeltype.AutoSize = true;
            this.labeltype.Font = new System.Drawing.Font("Corbel", 9F);
            this.labeltype.Location = new System.Drawing.Point(9, 20);
            this.labeltype.Name = "labeltype";
            this.labeltype.Size = new System.Drawing.Size(36, 14);
            this.labeltype.TabIndex = 5;
            this.labeltype.Text = "Type :";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.PeachPuff;
            this.groupBox1.Controls.Add(this.checkBoxMainMap);
            this.groupBox1.Controls.Add(this.buttonload);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.labelname);
            this.groupBox1.Controls.Add(this.textBoxLoad);
            this.groupBox1.Controls.Add(this.labelLoad);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 127);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ShapeFile Info";
            // 
            // checkBoxMainMap
            // 
            this.checkBoxMainMap.AutoSize = true;
            this.checkBoxMainMap.Checked = true;
            this.checkBoxMainMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMainMap.Font = new System.Drawing.Font("Corbel", 9F);
            this.checkBoxMainMap.Location = new System.Drawing.Point(11, 90);
            this.checkBoxMainMap.Name = "checkBoxMainMap";
            this.checkBoxMainMap.Size = new System.Drawing.Size(93, 18);
            this.checkBoxMainMap.TabIndex = 5;
            this.checkBoxMainMap.Text = "Territory map";
            this.toolTip1.SetToolTip(this.checkBoxMainMap, "if checked it\'s means the shapefile loaded will be the main map for this data set" +
        ".");
            this.checkBoxMainMap.UseVisualStyleBackColor = true;
            this.checkBoxMainMap.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBoxMainMap.MouseEnter += new System.EventHandler(this.checkBox1_MouseEnter);
            this.checkBoxMainMap.MouseLeave += new System.EventHandler(this.checkBox1_MouseLeave);
            this.checkBoxMainMap.MouseHover += new System.EventHandler(this.checkBox1_MouseHover);
            // 
            // buttonload
            // 
            this.buttonload.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.buttonload.Location = new System.Drawing.Point(324, 32);
            this.buttonload.Name = "buttonload";
            this.buttonload.Size = new System.Drawing.Size(24, 21);
            this.buttonload.TabIndex = 4;
            this.buttonload.Text = "...";
            this.buttonload.UseVisualStyleBackColor = true;
            this.buttonload.Click += new System.EventHandler(this.buttonload_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(123, 64);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(224, 21);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // labelname
            // 
            this.labelname.AutoSize = true;
            this.labelname.Font = new System.Drawing.Font("Corbel", 9F);
            this.labelname.Location = new System.Drawing.Point(7, 66);
            this.labelname.Name = "labelname";
            this.labelname.Size = new System.Drawing.Size(88, 14);
            this.labelname.TabIndex = 2;
            this.labelname.Text = "Shapefile name :";
            // 
            // textBoxLoad
            // 
            this.textBoxLoad.Location = new System.Drawing.Point(123, 32);
            this.textBoxLoad.Name = "textBoxLoad";
            this.textBoxLoad.ReadOnly = true;
            this.textBoxLoad.Size = new System.Drawing.Size(198, 21);
            this.textBoxLoad.TabIndex = 1;
            this.textBoxLoad.TextChanged += new System.EventHandler(this.textBoxLoad_TextChanged);
            // 
            // labelLoad
            // 
            this.labelLoad.AutoSize = true;
            this.labelLoad.Font = new System.Drawing.Font("Corbel", 9F);
            this.labelLoad.Location = new System.Drawing.Point(8, 35);
            this.labelLoad.Name = "labelLoad";
            this.labelLoad.Size = new System.Drawing.Size(84, 14);
            this.labelLoad.TabIndex = 0;
            this.labelLoad.Text = "Load Shapefile :";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AddShapeFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 234);
            this.Controls.Add(this.panel1);
            this.Name = "AddShapeFileForm";
            this.Text = "AddShapeFileForm";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelname;
        private System.Windows.Forms.Label labelLoad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labeltype;
        internal System.Windows.Forms.Button buttoncancel;
        internal System.Windows.Forms.Button buttonOK;
        internal System.Windows.Forms.ComboBox comboBoxname;
        private System.Windows.Forms.Label labelnametype;
        internal System.Windows.Forms.ComboBox comboBoxtype;
        internal System.Windows.Forms.Button buttonload;
        internal System.Windows.Forms.TextBox textBoxName;
        internal System.Windows.Forms.TextBox textBoxLoad;
        internal System.Windows.Forms.OpenFileDialog openFileDialog1;
        internal System.Windows.Forms.CheckBox checkBoxMainMap;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}