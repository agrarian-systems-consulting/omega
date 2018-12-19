namespace OMEGA.Forms.ResultUserControl
{
    partial class UserControlExpenseIncome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlExpenseIncome));
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.labelExpInc = new System.Windows.Forms.Label();
            this.buttonList = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Controls.Add(this.buttonPlus10);
            this.panel1.Controls.Add(this.buttonMoins10);
            this.panel1.Controls.Add(this.buttonNotes);
            this.panel1.Controls.Add(this.buttonReport);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonExport);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonRemove);
            this.panel1.Controls.Add(this.buttonCalcul);
            this.panel1.Controls.Add(this.buttonDico);
            this.panel1.Controls.Add(this.labelExpInc);
            this.panel1.Controls.Add(this.buttonList);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 390);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.BackgroundImage")));
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.Location = new System.Drawing.Point(480, 336);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(35, 27);
            this.buttonBack.TabIndex = 81;
            this.toolTip1.SetToolTip(this.buttonBack, "Retour");
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // buttonPlus10
            // 
            this.buttonPlus10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlus10.Location = new System.Drawing.Point(939, 21);
            this.buttonPlus10.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPlus10.Name = "buttonPlus10";
            this.buttonPlus10.Size = new System.Drawing.Size(58, 27);
            this.buttonPlus10.TabIndex = 80;
            this.buttonPlus10.Text = "+ 10 >>";
            this.buttonPlus10.UseVisualStyleBackColor = true;
            this.buttonPlus10.Click += new System.EventHandler(this.buttonPlus10_Click);
            // 
            // buttonMoins10
            // 
            this.buttonMoins10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoins10.Location = new System.Drawing.Point(880, 21);
            this.buttonMoins10.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMoins10.Name = "buttonMoins10";
            this.buttonMoins10.Size = new System.Drawing.Size(55, 27);
            this.buttonMoins10.TabIndex = 79;
            this.buttonMoins10.Text = "- 10 <<";
            this.buttonMoins10.UseVisualStyleBackColor = true;
            this.buttonMoins10.Click += new System.EventHandler(this.buttonMoins10_Click);
            // 
            // buttonNotes
            // 
            this.buttonNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonNotes.BackgroundImage")));
            this.buttonNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNotes.Location = new System.Drawing.Point(43, 333);
            this.buttonNotes.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNotes.Name = "buttonNotes";
            this.buttonNotes.Size = new System.Drawing.Size(34, 35);
            this.buttonNotes.TabIndex = 47;
            this.buttonNotes.UseVisualStyleBackColor = true;
            // 
            // buttonReport
            // 
            this.buttonReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReport.BackgroundImage = global::OMEGA.Properties.Resources.icons8_dernier_50;
            this.buttonReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonReport.Location = new System.Drawing.Point(89, 301);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(26, 27);
            this.buttonReport.TabIndex = 64;
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(826, 301);
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
            this.buttonExport.Location = new System.Drawing.Point(3, 333);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(36, 35);
            this.buttonExport.TabIndex = 63;
            this.buttonExport.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(900, 301);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(97, 27);
            this.buttonSave.TabIndex = 62;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Location = new System.Drawing.Point(3, 302);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(81, 27);
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
            // labelExpInc
            // 
            this.labelExpInc.AutoSize = true;
            this.labelExpInc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(226)))), ((int)(((byte)(165)))));
            this.labelExpInc.Font = new System.Drawing.Font("Corbel", 12F);
            this.labelExpInc.Location = new System.Drawing.Point(1, 1);
            this.labelExpInc.Name = "labelExpInc";
            this.labelExpInc.Size = new System.Drawing.Size(103, 19);
            this.labelExpInc.TabIndex = 57;
            this.labelExpInc.Text = "Misc Expenses";
            // 
            // buttonList
            // 
            this.buttonList.Location = new System.Drawing.Point(134, 21);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(75, 23);
            this.buttonList.TabIndex = 56;
            this.buttonList.Text = "Misx Exp.";
            this.buttonList.UseVisualStyleBackColor = true;
            this.buttonList.Click += new System.EventHandler(this.buttonList_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(994, 20);
            this.textBox1.TabIndex = 55;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(994, 223);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // UserControlExpenseIncome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UserControlExpenseIncome";
            this.Size = new System.Drawing.Size(1000, 390);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonCalcul;
        private System.Windows.Forms.Button buttonDico;
        private System.Windows.Forms.Label labelExpInc;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonNotes;
        private System.Windows.Forms.Button buttonPlus10;
        private System.Windows.Forms.Button buttonMoins10;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
