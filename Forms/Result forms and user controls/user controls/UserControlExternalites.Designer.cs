namespace OMEGA.Forms.ResultUserControl
{
    partial class UserControlExternalites
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlExternalites));
            this.labelExter = new System.Windows.Forms.Label();
            this.buttonList = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonPlus10 = new System.Windows.Forms.Button();
            this.buttonMoins10 = new System.Windows.Forms.Button();
            this.buttonNotes = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonCalcul = new System.Windows.Forms.Button();
            this.buttonDico = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelExter
            // 
            this.labelExter.AutoSize = true;
            this.labelExter.Font = new System.Drawing.Font("Corbel", 12F);
            this.labelExter.Location = new System.Drawing.Point(1, 1);
            this.labelExter.Name = "labelExter";
            this.labelExter.Size = new System.Drawing.Size(103, 19);
            this.labelExter.TabIndex = 57;
            this.labelExter.Text = "Misc Expenses";
            // 
            // buttonList
            // 
            this.buttonList.Location = new System.Drawing.Point(134, 21);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(75, 23);
            this.buttonList.TabIndex = 56;
            this.buttonList.Text = "Misx Exp.";
            this.buttonList.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonBack);
            this.panel2.Controls.Add(this.buttonPlus10);
            this.panel2.Controls.Add(this.buttonMoins10);
            this.panel2.Controls.Add(this.buttonNotes);
            this.panel2.Controls.Add(this.buttonReport);
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonExport);
            this.panel2.Controls.Add(this.buttonSave);
            this.panel2.Controls.Add(this.buttonRemove);
            this.panel2.Controls.Add(this.buttonCalcul);
            this.panel2.Controls.Add(this.buttonDico);
            this.panel2.Controls.Add(this.labelExter);
            this.panel2.Controls.Add(this.buttonList);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 391);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.BackgroundImage")));
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.Location = new System.Drawing.Point(394, 350);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(35, 27);
            this.buttonBack.TabIndex = 83;
            this.toolTip1.SetToolTip(this.buttonBack, "retour");
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonPlus10
            // 
            this.buttonPlus10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlus10.Location = new System.Drawing.Point(755, 21);
            this.buttonPlus10.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPlus10.Name = "buttonPlus10";
            this.buttonPlus10.Size = new System.Drawing.Size(58, 27);
            this.buttonPlus10.TabIndex = 82;
            this.buttonPlus10.Text = "+ 10 >>";
            this.buttonPlus10.UseVisualStyleBackColor = true;
            this.buttonPlus10.Click += new System.EventHandler(this.buttonPlus10_Click);
            // 
            // buttonMoins10
            // 
            this.buttonMoins10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoins10.Location = new System.Drawing.Point(696, 21);
            this.buttonMoins10.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoins10.Name = "buttonMoins10";
            this.buttonMoins10.Size = new System.Drawing.Size(55, 27);
            this.buttonMoins10.TabIndex = 81;
            this.buttonMoins10.Text = "- 10 <<";
            this.buttonMoins10.UseVisualStyleBackColor = true;
            this.buttonMoins10.Click += new System.EventHandler(this.buttonMoins10_Click);
            // 
            // buttonNotes
            // 
            this.buttonNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonNotes.BackgroundImage")));
            this.buttonNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNotes.Location = new System.Drawing.Point(43, 350);
            this.buttonNotes.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNotes.Name = "buttonNotes";
            this.buttonNotes.Size = new System.Drawing.Size(34, 35);
            this.buttonNotes.TabIndex = 47;
            this.buttonNotes.UseVisualStyleBackColor = true;
            // 
            // buttonReport
            // 
            this.buttonReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReport.Location = new System.Drawing.Point(78, 319);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(115, 27);
            this.buttonReport.TabIndex = 64;
            this.buttonReport.Text = "ReportDroite";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(669, 354);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(70, 27);
            this.buttonCancel.TabIndex = 61;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExport.Image = global::OMEGA.Properties.Resources.icons8_exporter_csv_30;
            this.buttonExport.Location = new System.Drawing.Point(3, 350);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(36, 35);
            this.buttonExport.TabIndex = 63;
            this.buttonExport.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(743, 354);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 27);
            this.buttonSave.TabIndex = 62;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Location = new System.Drawing.Point(3, 319);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(70, 27);
            this.buttonRemove.TabIndex = 60;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            // 
            // buttonCalcul
            // 
            this.buttonCalcul.Location = new System.Drawing.Point(333, 21);
            this.buttonCalcul.Name = "buttonCalcul";
            this.buttonCalcul.Size = new System.Drawing.Size(75, 23);
            this.buttonCalcul.TabIndex = 59;
            this.buttonCalcul.Text = "Mot clé";
            this.buttonCalcul.UseVisualStyleBackColor = true;
            // 
            // buttonDico
            // 
            this.buttonDico.Location = new System.Drawing.Point(233, 21);
            this.buttonDico.Name = "buttonDico";
            this.buttonDico.Size = new System.Drawing.Size(75, 23);
            this.buttonDico.TabIndex = 58;
            this.buttonDico.Text = "Dico";
            this.buttonDico.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(810, 20);
            this.textBox1.TabIndex = 55;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(808, 241);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // UserControlExternalites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "UserControlExternalites";
            this.Size = new System.Drawing.Size(816, 391);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonCalcul;
        private System.Windows.Forms.Button buttonDico;
        private System.Windows.Forms.Label labelExter;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonNotes;
        private System.Windows.Forms.Button buttonPlus10;
        private System.Windows.Forms.Button buttonMoins10;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
