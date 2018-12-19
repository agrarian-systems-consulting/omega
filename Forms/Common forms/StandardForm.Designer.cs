namespace OMEGA.Forms
{
    partial class StandardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StandardForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelEtatSortie = new System.Windows.Forms.Panel();
            this.buttonResult = new System.Windows.Forms.Button();
            this.buttonListe2 = new System.Windows.Forms.Button();
            this.buttonCancel2 = new System.Windows.Forms.Button();
            this.buttonSave2 = new System.Windows.Forms.Button();
            this.buttonOK2 = new System.Windows.Forms.Button();
            this.buttonRemove2 = new System.Windows.Forms.Button();
            this.panelphase = new System.Windows.Forms.Panel();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonNotes = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.button123 = new System.Windows.Forms.Button();
            this.buttonreport0 = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            this.panelEtatSortie.SuspendLayout();
            this.panelphase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelEtatSortie);
            this.panel1.Controls.Add(this.panelphase);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 518);
            this.panel1.TabIndex = 0;
            // 
            // panelEtatSortie
            // 
            this.panelEtatSortie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEtatSortie.Controls.Add(this.buttonResult);
            this.panelEtatSortie.Controls.Add(this.buttonListe2);
            this.panelEtatSortie.Controls.Add(this.buttonCancel2);
            this.panelEtatSortie.Controls.Add(this.buttonSave2);
            this.panelEtatSortie.Controls.Add(this.buttonOK2);
            this.panelEtatSortie.Controls.Add(this.buttonRemove2);
            this.panelEtatSortie.Location = new System.Drawing.Point(3, 447);
            this.panelEtatSortie.Name = "panelEtatSortie";
            this.panelEtatSortie.Size = new System.Drawing.Size(834, 65);
            this.panelEtatSortie.TabIndex = 48;
            this.panelEtatSortie.Visible = false;
            // 
            // buttonResult
            // 
            this.buttonResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonResult.Location = new System.Drawing.Point(749, 3);
            this.buttonResult.Name = "buttonResult";
            this.buttonResult.Size = new System.Drawing.Size(82, 23);
            this.buttonResult.TabIndex = 33;
            this.buttonResult.Text = "See Result";
            this.buttonResult.UseVisualStyleBackColor = true;
            this.buttonResult.Click += new System.EventHandler(this.buttonResult_Click);
            // 
            // buttonListe2
            // 
            this.buttonListe2.BackgroundImage = global::OMEGA.Properties.Resources.modifier_le_tableau_icone_6063_32;
            this.buttonListe2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonListe2.Location = new System.Drawing.Point(2, 2);
            this.buttonListe2.Margin = new System.Windows.Forms.Padding(2);
            this.buttonListe2.Name = "buttonListe2";
            this.buttonListe2.Size = new System.Drawing.Size(29, 30);
            this.buttonListe2.TabIndex = 32;
            this.toolTip1.SetToolTip(this.buttonListe2, "Add Product");
            this.buttonListe2.UseVisualStyleBackColor = true;
            this.buttonListe2.Click += new System.EventHandler(this.buttonListe2_Click);
            // 
            // buttonCancel2
            // 
            this.buttonCancel2.Location = new System.Drawing.Point(603, 39);
            this.buttonCancel2.Name = "buttonCancel2";
            this.buttonCancel2.Size = new System.Drawing.Size(76, 23);
            this.buttonCancel2.TabIndex = 4;
            this.buttonCancel2.Text = "Cancel";
            this.buttonCancel2.UseVisualStyleBackColor = true;
            // 
            // buttonSave2
            // 
            this.buttonSave2.Location = new System.Drawing.Point(685, 39);
            this.buttonSave2.Name = "buttonSave2";
            this.buttonSave2.Size = new System.Drawing.Size(101, 23);
            this.buttonSave2.TabIndex = 3;
            this.buttonSave2.Text = "Save";
            this.buttonSave2.UseVisualStyleBackColor = true;
            this.buttonSave2.Click += new System.EventHandler(this.buttonSave2_Click);
            // 
            // buttonOK2
            // 
            this.buttonOK2.Location = new System.Drawing.Point(792, 39);
            this.buttonOK2.Name = "buttonOK2";
            this.buttonOK2.Size = new System.Drawing.Size(39, 23);
            this.buttonOK2.TabIndex = 2;
            this.buttonOK2.Text = "OK";
            this.buttonOK2.UseVisualStyleBackColor = true;
            this.buttonOK2.Click += new System.EventHandler(this.buttonOK2_Click);
            // 
            // buttonRemove2
            // 
            this.buttonRemove2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove2.Location = new System.Drawing.Point(36, 3);
            this.buttonRemove2.Name = "buttonRemove2";
            this.buttonRemove2.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove2.TabIndex = 1;
            this.buttonRemove2.Text = "Remove";
            this.buttonRemove2.UseVisualStyleBackColor = true;
            this.buttonRemove2.Click += new System.EventHandler(this.buttonRemove2_Click);
            // 
            // panelphase
            // 
            this.panelphase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelphase.Controls.Add(this.buttonExport);
            this.panelphase.Controls.Add(this.buttonNotes);
            this.panelphase.Controls.Add(this.buttonAdd);
            this.panelphase.Controls.Add(this.button123);
            this.panelphase.Controls.Add(this.buttonreport0);
            this.panelphase.Controls.Add(this.buttonReport);
            this.panelphase.Controls.Add(this.buttonCancel);
            this.panelphase.Controls.Add(this.buttonSave);
            this.panelphase.Controls.Add(this.buttonOK);
            this.panelphase.Controls.Add(this.buttonRemove);
            this.panelphase.Location = new System.Drawing.Point(3, 366);
            this.panelphase.Name = "panelphase";
            this.panelphase.Size = new System.Drawing.Size(834, 78);
            this.panelphase.TabIndex = 2;
            this.panelphase.Visible = false;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExport.BackgroundImage = global::OMEGA.Properties.Resources.icons8_exporter_csv_30;
            this.buttonExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExport.Location = new System.Drawing.Point(8, 36);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(33, 37);
            this.buttonExport.TabIndex = 57;
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonNotes
            // 
            this.buttonNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonNotes.BackgroundImage")));
            this.buttonNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNotes.Location = new System.Drawing.Point(45, 36);
            this.buttonNotes.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNotes.Name = "buttonNotes";
            this.buttonNotes.Size = new System.Drawing.Size(33, 37);
            this.buttonNotes.TabIndex = 56;
            this.buttonNotes.UseVisualStyleBackColor = true;
            this.buttonNotes.Click += new System.EventHandler(this.buttonNotes_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(3, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 48;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // button123
            // 
            this.button123.BackgroundImage = global::OMEGA.Properties.Resources.icons8_dernier123_50;
            this.button123.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button123.Location = new System.Drawing.Point(328, 3);
            this.button123.Name = "button123";
            this.button123.Size = new System.Drawing.Size(63, 25);
            this.button123.TabIndex = 47;
            this.toolTip1.SetToolTip(this.button123, "Put the value that correspond to the column");
            this.button123.UseVisualStyleBackColor = true;
            this.button123.Click += new System.EventHandler(this.button123_Click);
            // 
            // buttonreport0
            // 
            this.buttonreport0.BackgroundImage = global::OMEGA.Properties.Resources.icons8_dernier0_50;
            this.buttonreport0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonreport0.Location = new System.Drawing.Point(285, 3);
            this.buttonreport0.Name = "buttonreport0";
            this.buttonreport0.Size = new System.Drawing.Size(37, 25);
            this.buttonreport0.TabIndex = 46;
            this.toolTip1.SetToolTip(this.buttonreport0, "Copy value 0 on the line");
            this.buttonreport0.UseVisualStyleBackColor = true;
            this.buttonreport0.Click += new System.EventHandler(this.buttonreport0_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.BackgroundImage = global::OMEGA.Properties.Resources.icons8_dernier_50;
            this.buttonReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonReport.Location = new System.Drawing.Point(253, 3);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(26, 25);
            this.buttonReport.TabIndex = 45;
            this.toolTip1.SetToolTip(this.buttonReport, "Copy value on the line");
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(603, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(685, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(101, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(792, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(39, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(84, 4);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(837, 336);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(840, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // StandardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 518);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "StandardForm";
            this.Text = "StandardForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelEtatSortie.ResumeLayout(false);
            this.panelphase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Panel panelphase;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonreport0;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button button123;
        private System.Windows.Forms.Panel panelEtatSortie;
        private System.Windows.Forms.Button buttonListe2;
        private System.Windows.Forms.Button buttonCancel2;
        private System.Windows.Forms.Button buttonSave2;
        private System.Windows.Forms.Button buttonOK2;
        private System.Windows.Forms.Button buttonRemove2;
        private System.Windows.Forms.Button buttonResult;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonNotes;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}