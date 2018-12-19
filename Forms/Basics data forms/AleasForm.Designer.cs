namespace OMEGA.Forms
{
    partial class AleasForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AleasForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelDatagrid = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonNote = new System.Windows.Forms.Button();
            this.buttonCopyIdentique = new System.Windows.Forms.Button();
            this.buttonCopy123 = new System.Windows.Forms.Button();
            this.buttonCopy0 = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonProdChar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.panelDatagrid.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.panelDatagrid);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Corbel", 10F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 457);
            this.panel1.TabIndex = 0;
            // 
            // panelDatagrid
            // 
            this.panelDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDatagrid.Controls.Add(this.panel2);
            this.panelDatagrid.Controls.Add(this.dataGridView1);
            this.panelDatagrid.Location = new System.Drawing.Point(217, 3);
            this.panelDatagrid.Name = "panelDatagrid";
            this.panelDatagrid.Size = new System.Drawing.Size(645, 451);
            this.panelDatagrid.TabIndex = 1;
            this.panelDatagrid.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.buttonNote);
            this.panel2.Controls.Add(this.buttonCopyIdentique);
            this.panel2.Controls.Add(this.buttonCopy123);
            this.panel2.Controls.Add(this.buttonCopy0);
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonSave);
            this.panel2.Controls.Add(this.buttonOk);
            this.panel2.Controls.Add(this.buttonRemove);
            this.panel2.Controls.Add(this.buttonProdChar);
            this.panel2.Location = new System.Drawing.Point(0, 350);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(645, 101);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::OMEGA.Properties.Resources.icons8_exporter_csv_30;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 31);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // buttonNote
            // 
            this.buttonNote.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonNote.BackgroundImage")));
            this.buttonNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNote.Location = new System.Drawing.Point(43, 61);
            this.buttonNote.Name = "buttonNote";
            this.buttonNote.Size = new System.Drawing.Size(32, 34);
            this.buttonNote.TabIndex = 8;
            this.buttonNote.UseVisualStyleBackColor = true;
            this.buttonNote.Click += new System.EventHandler(this.buttonNote_Click);
            // 
            // buttonCopyIdentique
            // 
            this.buttonCopyIdentique.BackgroundImage = global::OMEGA.Properties.Resources.icons8_dernier_50;
            this.buttonCopyIdentique.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCopyIdentique.Location = new System.Drawing.Point(229, 3);
            this.buttonCopyIdentique.Name = "buttonCopyIdentique";
            this.buttonCopyIdentique.Size = new System.Drawing.Size(40, 29);
            this.buttonCopyIdentique.TabIndex = 7;
            this.buttonCopyIdentique.UseVisualStyleBackColor = true;
            this.buttonCopyIdentique.Click += new System.EventHandler(this.buttonCopyIdentique_Click);
            // 
            // buttonCopy123
            // 
            this.buttonCopy123.BackgroundImage = global::OMEGA.Properties.Resources.icons8_dernier123_50;
            this.buttonCopy123.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCopy123.Location = new System.Drawing.Point(275, 3);
            this.buttonCopy123.Name = "buttonCopy123";
            this.buttonCopy123.Size = new System.Drawing.Size(71, 29);
            this.buttonCopy123.TabIndex = 6;
            this.buttonCopy123.UseVisualStyleBackColor = true;
            this.buttonCopy123.Click += new System.EventHandler(this.buttonCopy123_Click);
            // 
            // buttonCopy0
            // 
            this.buttonCopy0.BackgroundImage = global::OMEGA.Properties.Resources.icons8_dernier0_50;
            this.buttonCopy0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCopy0.Location = new System.Drawing.Point(352, 3);
            this.buttonCopy0.Name = "buttonCopy0";
            this.buttonCopy0.Size = new System.Drawing.Size(50, 29);
            this.buttonCopy0.TabIndex = 5;
            this.buttonCopy0.UseVisualStyleBackColor = true;
            this.buttonCopy0.Click += new System.EventHandler(this.buttonCopy0_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(386, 72);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(467, 72);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(99, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(572, 72);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(64, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(84, 3);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonProdChar
            // 
            this.buttonProdChar.Location = new System.Drawing.Point(3, 3);
            this.buttonProdChar.Name = "buttonProdChar";
            this.buttonProdChar.Size = new System.Drawing.Size(75, 23);
            this.buttonProdChar.TabIndex = 0;
            this.buttonProdChar.Text = "Produit/Charge";
            this.buttonProdChar.UseVisualStyleBackColor = true;
            this.buttonProdChar.Click += new System.EventHandler(this.buttonProdChar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(645, 344);
            this.dataGridView1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(208, 451);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // AleasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 457);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AleasForm";
            this.Text = "AleasForm";
            this.panel1.ResumeLayout(false);
            this.panelDatagrid.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panelDatagrid;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonNote;
        private System.Windows.Forms.Button buttonCopyIdentique;
        private System.Windows.Forms.Button buttonCopy123;
        private System.Windows.Forms.Button buttonCopy0;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonProdChar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}