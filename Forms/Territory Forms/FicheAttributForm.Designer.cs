namespace OMEGA.Forms.Territory_Forms
{
    partial class FicheAttributForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FicheAttributForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxRemarque = new System.Windows.Forms.GroupBox();
            this.textBoxRem = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxOccup = new System.Windows.Forms.GroupBox();
            this.groupBoxHabitat = new System.Windows.Forms.GroupBox();
            this.checkBoxhabitat = new System.Windows.Forms.CheckBox();
            this.textBoxMaison = new System.Windows.Forms.TextBox();
            this.textBoxOccup = new System.Windows.Forms.TextBox();
            this.labelOccup = new System.Windows.Forms.Label();
            this.labelMaison = new System.Windows.Forms.Label();
            this.groupBoxGPS = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBoxinfo = new System.Windows.Forms.GroupBox();
            this.comboBoxVillage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labeldate = new System.Windows.Forms.Label();
            this.comboBoxNom = new System.Windows.Forms.ComboBox();
            this.labelnom = new System.Windows.Forms.Label();
            this.buttonadd = new System.Windows.Forms.Button();
            this.panelOccup = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBoxRemarque.SuspendLayout();
            this.groupBoxOccup.SuspendLayout();
            this.groupBoxHabitat.SuspendLayout();
            this.groupBoxGPS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxinfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel1.Controls.Add(this.groupBoxRemarque);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.groupBoxOccup);
            this.panel1.Controls.Add(this.groupBoxHabitat);
            this.panel1.Controls.Add(this.groupBoxGPS);
            this.panel1.Controls.Add(this.groupBoxinfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Corbel", 9F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 563);
            this.panel1.TabIndex = 0;
            // 
            // groupBoxRemarque
            // 
            this.groupBoxRemarque.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRemarque.Controls.Add(this.textBoxRem);
            this.groupBoxRemarque.Location = new System.Drawing.Point(4, 469);
            this.groupBoxRemarque.Name = "groupBoxRemarque";
            this.groupBoxRemarque.Size = new System.Drawing.Size(727, 64);
            this.groupBoxRemarque.TabIndex = 10;
            this.groupBoxRemarque.TabStop = false;
            this.groupBoxRemarque.Text = "Remarque/Note :";
            // 
            // textBoxRem
            // 
            this.textBoxRem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRem.Location = new System.Drawing.Point(9, 14);
            this.textBoxRem.Multiline = true;
            this.textBoxRem.Name = "textBoxRem";
            this.textBoxRem.Size = new System.Drawing.Size(715, 45);
            this.textBoxRem.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(599, 534);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(691, 534);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(41, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBoxOccup
            // 
            this.groupBoxOccup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOccup.Controls.Add(this.buttonadd);
            this.groupBoxOccup.Controls.Add(this.panelOccup);
            this.groupBoxOccup.Location = new System.Drawing.Point(4, 295);
            this.groupBoxOccup.MinimumSize = new System.Drawing.Size(727, 143);
            this.groupBoxOccup.Name = "groupBoxOccup";
            this.groupBoxOccup.Size = new System.Drawing.Size(727, 168);
            this.groupBoxOccup.TabIndex = 9;
            this.groupBoxOccup.TabStop = false;
            this.groupBoxOccup.Text = "Occupation sur le site :";
            // 
            // groupBoxHabitat
            // 
            this.groupBoxHabitat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHabitat.Controls.Add(this.checkBoxhabitat);
            this.groupBoxHabitat.Controls.Add(this.textBoxMaison);
            this.groupBoxHabitat.Controls.Add(this.textBoxOccup);
            this.groupBoxHabitat.Controls.Add(this.labelOccup);
            this.groupBoxHabitat.Controls.Add(this.labelMaison);
            this.groupBoxHabitat.Location = new System.Drawing.Point(481, 4);
            this.groupBoxHabitat.Name = "groupBoxHabitat";
            this.groupBoxHabitat.Size = new System.Drawing.Size(250, 103);
            this.groupBoxHabitat.TabIndex = 6;
            this.groupBoxHabitat.TabStop = false;
            this.groupBoxHabitat.Text = "Présence d\'habitat ?  ";
            this.groupBoxHabitat.Enter += new System.EventHandler(this.groupBoxHabitat_Enter);
            // 
            // checkBoxhabitat
            // 
            this.checkBoxhabitat.AutoSize = true;
            this.checkBoxhabitat.Location = new System.Drawing.Point(108, 1);
            this.checkBoxhabitat.Name = "checkBoxhabitat";
            this.checkBoxhabitat.Size = new System.Drawing.Size(15, 14);
            this.checkBoxhabitat.TabIndex = 5;
            this.checkBoxhabitat.UseVisualStyleBackColor = true;
            this.checkBoxhabitat.CheckedChanged += new System.EventHandler(this.checkBoxhabitat_CheckedChanged);
            // 
            // textBoxMaison
            // 
            this.textBoxMaison.Enabled = false;
            this.textBoxMaison.Location = new System.Drawing.Point(117, 21);
            this.textBoxMaison.Name = "textBoxMaison";
            this.textBoxMaison.Size = new System.Drawing.Size(121, 22);
            this.textBoxMaison.TabIndex = 4;
            this.textBoxMaison.Text = "0";
            // 
            // textBoxOccup
            // 
            this.textBoxOccup.Enabled = false;
            this.textBoxOccup.Location = new System.Drawing.Point(117, 55);
            this.textBoxOccup.Name = "textBoxOccup";
            this.textBoxOccup.Size = new System.Drawing.Size(121, 22);
            this.textBoxOccup.TabIndex = 3;
            this.textBoxOccup.Text = "0";
            // 
            // labelOccup
            // 
            this.labelOccup.AutoSize = true;
            this.labelOccup.Location = new System.Drawing.Point(6, 57);
            this.labelOccup.Name = "labelOccup";
            this.labelOccup.Size = new System.Drawing.Size(77, 14);
            this.labelOccup.TabIndex = 2;
            this.labelOccup.Text = "Nb occupants :";
            // 
            // labelMaison
            // 
            this.labelMaison.AutoSize = true;
            this.labelMaison.Location = new System.Drawing.Point(6, 27);
            this.labelMaison.Name = "labelMaison";
            this.labelMaison.Size = new System.Drawing.Size(69, 14);
            this.labelMaison.TabIndex = 0;
            this.labelMaison.Text = "Nb maisons :";
            // 
            // groupBoxGPS
            // 
            this.groupBoxGPS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGPS.Controls.Add(this.dataGridView1);
            this.groupBoxGPS.Location = new System.Drawing.Point(4, 113);
            this.groupBoxGPS.Name = "groupBoxGPS";
            this.groupBoxGPS.Size = new System.Drawing.Size(727, 177);
            this.groupBoxGPS.TabIndex = 6;
            this.groupBoxGPS.TabStop = false;
            this.groupBoxGPS.Text = "GPS";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(721, 130);
            this.dataGridView1.TabIndex = 8;
            // 
            // groupBoxinfo
            // 
            this.groupBoxinfo.Controls.Add(this.comboBoxVillage);
            this.groupBoxinfo.Controls.Add(this.label1);
            this.groupBoxinfo.Controls.Add(this.dateTimePicker1);
            this.groupBoxinfo.Controls.Add(this.labeldate);
            this.groupBoxinfo.Controls.Add(this.comboBoxNom);
            this.groupBoxinfo.Controls.Add(this.labelnom);
            this.groupBoxinfo.Location = new System.Drawing.Point(4, 4);
            this.groupBoxinfo.Name = "groupBoxinfo";
            this.groupBoxinfo.Size = new System.Drawing.Size(311, 103);
            this.groupBoxinfo.TabIndex = 0;
            this.groupBoxinfo.TabStop = false;
            this.groupBoxinfo.Text = "Information général";
            // 
            // comboBoxVillage
            // 
            this.comboBoxVillage.FormattingEnabled = true;
            this.comboBoxVillage.Location = new System.Drawing.Point(117, 66);
            this.comboBoxVillage.Name = "comboBoxVillage";
            this.comboBoxVillage.Size = new System.Drawing.Size(188, 22);
            this.comboBoxVillage.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Village/regroupement :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(117, 40);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(188, 22);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // labeldate
            // 
            this.labeldate.AutoSize = true;
            this.labeldate.Location = new System.Drawing.Point(6, 44);
            this.labeldate.Name = "labeldate";
            this.labeldate.Size = new System.Drawing.Size(36, 14);
            this.labeldate.TabIndex = 2;
            this.labeldate.Text = "Date :";
            // 
            // comboBoxNom
            // 
            this.comboBoxNom.FormattingEnabled = true;
            this.comboBoxNom.Location = new System.Drawing.Point(117, 13);
            this.comboBoxNom.Name = "comboBoxNom";
            this.comboBoxNom.Size = new System.Drawing.Size(188, 22);
            this.comboBoxNom.TabIndex = 1;
            // 
            // labelnom
            // 
            this.labelnom.AutoSize = true;
            this.labelnom.Location = new System.Drawing.Point(4, 16);
            this.labelnom.Name = "labelnom";
            this.labelnom.Size = new System.Drawing.Size(107, 14);
            this.labelnom.TabIndex = 0;
            this.labelnom.Text = "Nom de l\'enqueteur :";
            // 
            // buttonadd
            // 
            this.buttonadd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonadd.Image = ((System.Drawing.Image)(resources.GetObject("buttonadd.Image")));
            this.buttonadd.Location = new System.Drawing.Point(351, 139);
            this.buttonadd.Name = "buttonadd";
            this.buttonadd.Size = new System.Drawing.Size(26, 23);
            this.buttonadd.TabIndex = 3;
            this.buttonadd.UseVisualStyleBackColor = true;
            this.buttonadd.Click += new System.EventHandler(this.buttonadd_Click);
            // 
            // panelOccup
            // 
            this.panelOccup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOccup.Location = new System.Drawing.Point(0, 15);
            this.panelOccup.Name = "panelOccup";
            this.panelOccup.Size = new System.Drawing.Size(724, 119);
            this.panelOccup.TabIndex = 2;
            // 
            // FicheAttributForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 563);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(592, 518);
            this.Name = "FicheAttributForm";
            this.Text = "FicheAttributForm";
            this.panel1.ResumeLayout(false);
            this.groupBoxRemarque.ResumeLayout(false);
            this.groupBoxRemarque.PerformLayout();
            this.groupBoxOccup.ResumeLayout(false);
            this.groupBoxHabitat.ResumeLayout(false);
            this.groupBoxHabitat.PerformLayout();
            this.groupBoxGPS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxinfo.ResumeLayout(false);
            this.groupBoxinfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxinfo;
        private System.Windows.Forms.GroupBox groupBoxGPS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labeldate;
        private System.Windows.Forms.Label labelnom;
        private System.Windows.Forms.GroupBox groupBoxHabitat;
        private System.Windows.Forms.CheckBox checkBoxhabitat;
        private System.Windows.Forms.Label labelOccup;
        private System.Windows.Forms.Label labelMaison;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBoxOccup;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        internal System.Windows.Forms.ComboBox comboBoxVillage;
        internal System.Windows.Forms.DateTimePicker dateTimePicker1;
        internal System.Windows.Forms.ComboBox comboBoxNom;
        internal System.Windows.Forms.TextBox textBoxMaison;
        internal System.Windows.Forms.TextBox textBoxOccup;
        private System.Windows.Forms.GroupBox groupBoxRemarque;
        internal System.Windows.Forms.TextBox textBoxRem;
        private System.Windows.Forms.Button buttonadd;
        internal System.Windows.Forms.Panel panelOccup;
    }
}