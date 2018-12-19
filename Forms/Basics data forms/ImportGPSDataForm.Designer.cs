namespace OMEGA.Forms
{
    partial class ImportGPSDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportGPSDataForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonImportPoint = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonline = new System.Windows.Forms.RadioButton();
            this.radioButtonpoint = new System.Windows.Forms.RadioButton();
            this.buttonGPSdata = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonImportGPX = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel1.Controls.Add(this.buttonImportGPX);
            this.panel1.Controls.Add(this.buttonImportPoint);
            this.panel1.Controls.Add(this.checkedListBox1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 398);
            this.panel1.TabIndex = 0;
            // 
            // buttonImportPoint
            // 
            this.buttonImportPoint.Enabled = false;
            this.buttonImportPoint.Location = new System.Drawing.Point(210, 12);
            this.buttonImportPoint.Name = "buttonImportPoint";
            this.buttonImportPoint.Size = new System.Drawing.Size(147, 43);
            this.buttonImportPoint.TabIndex = 5;
            this.buttonImportPoint.Text = "Import Selected Points";
            this.buttonImportPoint.UseVisualStyleBackColor = true;
            this.buttonImportPoint.Click += new System.EventHandler(this.buttonImportPoint_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.LavenderBlush;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(4, 85);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(384, 304);
            this.checkedListBox1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonline);
            this.groupBox1.Controls.Add(this.radioButtonpoint);
            this.groupBox1.Controls.Add(this.buttonGPSdata);
            this.groupBox1.Location = new System.Drawing.Point(103, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 75);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // radioButtonline
            // 
            this.radioButtonline.AutoSize = true;
            this.radioButtonline.Location = new System.Drawing.Point(76, 47);
            this.radioButtonline.Name = "radioButtonline";
            this.radioButtonline.Size = new System.Drawing.Size(58, 17);
            this.radioButtonline.TabIndex = 3;
            this.radioButtonline.TabStop = true;
            this.radioButtonline.Text = "Tracés";
            this.radioButtonline.UseVisualStyleBackColor = true;
            // 
            // radioButtonpoint
            // 
            this.radioButtonpoint.AutoSize = true;
            this.radioButtonpoint.Location = new System.Drawing.Point(6, 47);
            this.radioButtonpoint.Name = "radioButtonpoint";
            this.radioButtonpoint.Size = new System.Drawing.Size(54, 17);
            this.radioButtonpoint.TabIndex = 2;
            this.radioButtonpoint.TabStop = true;
            this.radioButtonpoint.Text = "Points";
            this.radioButtonpoint.UseVisualStyleBackColor = true;
            // 
            // buttonGPSdata
            // 
            this.buttonGPSdata.Location = new System.Drawing.Point(6, 9);
            this.buttonGPSdata.Name = "buttonGPSdata";
            this.buttonGPSdata.Size = new System.Drawing.Size(128, 32);
            this.buttonGPSdata.TabIndex = 1;
            this.buttonGPSdata.Text = "Import GPS USB Data";
            this.buttonGPSdata.UseVisualStyleBackColor = true;
            this.buttonGPSdata.Click += new System.EventHandler(this.buttonGPSdata_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 61);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonImportGPX
            // 
            this.buttonImportGPX.Location = new System.Drawing.Point(25, 12);
            this.buttonImportGPX.Name = "buttonImportGPX";
            this.buttonImportGPX.Size = new System.Drawing.Size(129, 43);
            this.buttonImportGPX.TabIndex = 0;
            this.buttonImportGPX.Text = "Import GPX FIle";
            this.buttonImportGPX.UseVisualStyleBackColor = true;
            this.buttonImportGPX.Click += new System.EventHandler(this.buttonImportGPX_Click);
            // 
            // ImportGPSDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 398);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(407, 437);
            this.MinimumSize = new System.Drawing.Size(407, 437);
            this.Name = "ImportGPSDataForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ImportGPSDataForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ImportGPSDataForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGPSdata;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonImportGPX;
        private System.Windows.Forms.RadioButton radioButtonline;
        private System.Windows.Forms.RadioButton radioButtonpoint;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button buttonImportPoint;
    }
}