namespace OMEGA.Forms
{
    partial class DictionnaryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictionnaryForm));
            this.groupBoxDictionnaries = new System.Windows.Forms.GroupBox();
            this.buttonVillagedico = new System.Windows.Forms.Button();
            this.buttonsoustypeoccupation = new System.Windows.Forms.Button();
            this.buttonTypeOccupation = new System.Windows.Forms.Button();
            this.datagridviewSSoccup = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.dataGridViewOccup = new System.Windows.Forms.DataGridView();
            this.dataGridViewVillage = new System.Windows.Forms.DataGridView();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxDictionnaries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridviewSSoccup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOccup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVillage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxDictionnaries
            // 
            this.groupBoxDictionnaries.BackColor = System.Drawing.Color.PeachPuff;
            this.groupBoxDictionnaries.Controls.Add(this.buttonVillagedico);
            this.groupBoxDictionnaries.Controls.Add(this.buttonsoustypeoccupation);
            this.groupBoxDictionnaries.Controls.Add(this.buttonTypeOccupation);
            this.groupBoxDictionnaries.Location = new System.Drawing.Point(9, 10);
            this.groupBoxDictionnaries.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxDictionnaries.Name = "groupBoxDictionnaries";
            this.groupBoxDictionnaries.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxDictionnaries.Size = new System.Drawing.Size(222, 409);
            this.groupBoxDictionnaries.TabIndex = 0;
            this.groupBoxDictionnaries.TabStop = false;
            this.groupBoxDictionnaries.Text = "Dictionnaries";
            // 
            // buttonVillagedico
            // 
            this.buttonVillagedico.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonVillagedico.Location = new System.Drawing.Point(20, 158);
            this.buttonVillagedico.Margin = new System.Windows.Forms.Padding(2);
            this.buttonVillagedico.Name = "buttonVillagedico";
            this.buttonVillagedico.Size = new System.Drawing.Size(189, 43);
            this.buttonVillagedico.TabIndex = 2;
            this.buttonVillagedico.Text = "Dictionnary of Villages";
            this.buttonVillagedico.UseVisualStyleBackColor = true;
            this.buttonVillagedico.Click += new System.EventHandler(this.buttonVillagedico_Click);
            // 
            // buttonsoustypeoccupation
            // 
            this.buttonsoustypeoccupation.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonsoustypeoccupation.Location = new System.Drawing.Point(20, 92);
            this.buttonsoustypeoccupation.Margin = new System.Windows.Forms.Padding(2);
            this.buttonsoustypeoccupation.Name = "buttonsoustypeoccupation";
            this.buttonsoustypeoccupation.Size = new System.Drawing.Size(189, 43);
            this.buttonsoustypeoccupation.TabIndex = 1;
            this.buttonsoustypeoccupation.Text = "Dictionnary of sub  occupation site";
            this.buttonsoustypeoccupation.UseVisualStyleBackColor = true;
            // 
            // buttonTypeOccupation
            // 
            this.buttonTypeOccupation.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonTypeOccupation.Location = new System.Drawing.Point(20, 28);
            this.buttonTypeOccupation.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTypeOccupation.Name = "buttonTypeOccupation";
            this.buttonTypeOccupation.Size = new System.Drawing.Size(189, 43);
            this.buttonTypeOccupation.TabIndex = 0;
            this.buttonTypeOccupation.Text = "Dictionnary of site occupation";
            this.buttonTypeOccupation.UseVisualStyleBackColor = true;
            this.buttonTypeOccupation.Click += new System.EventHandler(this.buttonTypeOccupation_Click);
            // 
            // datagridviewSSoccup
            // 
            this.datagridviewSSoccup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridviewSSoccup.Location = new System.Drawing.Point(257, 18);
            this.datagridviewSSoccup.Margin = new System.Windows.Forms.Padding(2);
            this.datagridviewSSoccup.Name = "datagridviewSSoccup";
            this.datagridviewSSoccup.RowTemplate.Height = 24;
            this.datagridviewSSoccup.Size = new System.Drawing.Size(530, 401);
            this.datagridviewSSoccup.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonAdd.Location = new System.Drawing.Point(236, 430);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(86, 28);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonRemove.Location = new System.Drawing.Point(326, 430);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(77, 28);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonOk.Location = new System.Drawing.Point(712, 430);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 28);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonDuplicate
            // 
            this.buttonDuplicate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDuplicate.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonDuplicate.Location = new System.Drawing.Point(408, 430);
            this.buttonDuplicate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDuplicate.Name = "buttonDuplicate";
            this.buttonDuplicate.Size = new System.Drawing.Size(77, 28);
            this.buttonDuplicate.TabIndex = 6;
            this.buttonDuplicate.Text = "Duplicate";
            this.buttonDuplicate.UseVisualStyleBackColor = true;
            this.buttonDuplicate.Click += new System.EventHandler(this.buttonDuplicate_Click);
            // 
            // buttonValidate
            // 
            this.buttonValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonValidate.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonValidate.Location = new System.Drawing.Point(624, 430);
            this.buttonValidate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(84, 28);
            this.buttonValidate.TabIndex = 7;
            this.buttonValidate.Text = "Validate";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.BackgroundImage = global::OMEGA.Properties.Resources.icons8_exporter_csv_30;
            this.buttonExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExport.Location = new System.Drawing.Point(9, 423);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(35, 36);
            this.buttonExport.TabIndex = 20;
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // dataGridViewOccup
            // 
            this.dataGridViewOccup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOccup.Location = new System.Drawing.Point(236, 18);
            this.dataGridViewOccup.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewOccup.Name = "dataGridViewOccup";
            this.dataGridViewOccup.RowTemplate.Height = 24;
            this.dataGridViewOccup.Size = new System.Drawing.Size(551, 401);
            this.dataGridViewOccup.TabIndex = 23;
            this.dataGridViewOccup.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOccup_CellValueChanged);
            // 
            // dataGridViewVillage
            // 
            this.dataGridViewVillage.AllowUserToAddRows = false;
            this.dataGridViewVillage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewVillage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVillage.Location = new System.Drawing.Point(236, 18);
            this.dataGridViewVillage.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewVillage.Name = "dataGridViewVillage";
            this.dataGridViewVillage.RowTemplate.Height = 24;
            this.dataGridViewVillage.Size = new System.Drawing.Size(551, 401);
            this.dataGridViewVillage.TabIndex = 24;
            this.dataGridViewVillage.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVillage_CellValueChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonCancel.Location = new System.Drawing.Point(544, 430);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 28);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PeachPuff;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 468);
            this.panel1.TabIndex = 26;
            // 
            // DictionnaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(796, 468);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dataGridViewVillage);
            this.Controls.Add(this.dataGridViewOccup);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonValidate);
            this.Controls.Add(this.buttonDuplicate);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.datagridviewSSoccup);
            this.Controls.Add(this.groupBoxDictionnaries);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DictionnaryForm";
            this.Text = "DictionnaryForm";
            this.groupBoxDictionnaries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridviewSSoccup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOccup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVillage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDictionnaries;
        private System.Windows.Forms.Button buttonVillagedico;
        private System.Windows.Forms.Button buttonsoustypeoccupation;
        private System.Windows.Forms.Button buttonTypeOccupation;
        private System.Windows.Forms.DataGridView datagridviewSSoccup;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.Button buttonValidate;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.DataGridView dataGridViewOccup;
        private System.Windows.Forms.DataGridView dataGridViewVillage;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
    }
}