namespace OMEGA.Forms.ResultUserControl
{
    partial class UserControlDefinition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlDefinition));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBoxMonth = new System.Windows.Forms.ComboBox();
            this.groupBoxAléa = new System.Windows.Forms.GroupBox();
            this.radioButtonWithout = new System.Windows.Forms.RadioButton();
            this.radioButtonWith = new System.Windows.Forms.RadioButton();
            this.textBoxYearBegin = new System.Windows.Forms.TextBox();
            this.labelMonth = new System.Windows.Forms.Label();
            this.panelAlea = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCostPrice2 = new System.Windows.Forms.ComboBox();
            this.comboBoxCostPrice = new System.Windows.Forms.ComboBox();
            this.comboBoxProdPrice2 = new System.Windows.Forms.ComboBox();
            this.LabelChargePrice = new System.Windows.Forms.Label();
            this.LabelProductPrice = new System.Windows.Forms.Label();
            this.comboBoxProductPrice = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxExtQte2 = new System.Windows.Forms.ComboBox();
            this.comboBoxExterQte = new System.Windows.Forms.ComboBox();
            this.LabelExternaliteQte = new System.Windows.Forms.Label();
            this.comboBoxCostQte2 = new System.Windows.Forms.ComboBox();
            this.LabelProductQte = new System.Windows.Forms.Label();
            this.comboBoxCostQte = new System.Windows.Forms.ComboBox();
            this.comboBoxProductQte = new System.Windows.Forms.ComboBox();
            this.comboBoxProdQte2 = new System.Windows.Forms.ComboBox();
            this.LabelCostQte = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.textBoxVar = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelClassif = new System.Windows.Forms.Label();
            this.labelVariante = new System.Windows.Forms.Label();
            this.labelNom = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxAléa.SuspendLayout();
            this.panelAlea.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonQuit);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.comboBoxMonth);
            this.panel1.Controls.Add(this.groupBoxAléa);
            this.panel1.Controls.Add(this.textBoxYearBegin);
            this.panel1.Controls.Add(this.labelMonth);
            this.panel1.Controls.Add(this.panelAlea);
            this.panel1.Controls.Add(this.labelYear);
            this.panel1.Controls.Add(this.textBoxVar);
            this.panel1.Controls.Add(this.textBoxName);
            this.panel1.Controls.Add(this.labelClassif);
            this.panel1.Controls.Add(this.labelVariante);
            this.panel1.Controls.Add(this.labelNom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Corbel", 10F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 396);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonBack
            // 
            this.buttonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.BackgroundImage")));
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.Location = new System.Drawing.Point(7, 278);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(35, 27);
            this.buttonBack.TabIndex = 80;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(574, 240);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(102, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "button1";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click_1);
            // 
            // buttonQuit
            // 
            this.buttonQuit.Location = new System.Drawing.Point(504, 240);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(66, 23);
            this.buttonQuit.TabIndex = 15;
            this.buttonQuit.Text = "button1";
            this.buttonQuit.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(108, 115);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(559, 64);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.FormattingEnabled = true;
            this.comboBoxMonth.Location = new System.Drawing.Point(349, 81);
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.Size = new System.Drawing.Size(146, 23);
            this.comboBoxMonth.TabIndex = 9;
            this.comboBoxMonth.SelectedIndexChanged += new System.EventHandler(this.comboBoxMonth_SelectedIndexChanged);
            // 
            // groupBoxAléa
            // 
            this.groupBoxAléa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(226)))), ((int)(((byte)(165)))));
            this.groupBoxAléa.Controls.Add(this.radioButtonWithout);
            this.groupBoxAléa.Controls.Add(this.radioButtonWith);
            this.groupBoxAléa.Location = new System.Drawing.Point(7, 190);
            this.groupBoxAléa.Name = "groupBoxAléa";
            this.groupBoxAléa.Size = new System.Drawing.Size(104, 68);
            this.groupBoxAléa.TabIndex = 0;
            this.groupBoxAléa.TabStop = false;
            this.groupBoxAléa.Text = "Alea";
            // 
            // radioButtonWithout
            // 
            this.radioButtonWithout.AutoSize = true;
            this.radioButtonWithout.Location = new System.Drawing.Point(14, 42);
            this.radioButtonWithout.Name = "radioButtonWithout";
            this.radioButtonWithout.Size = new System.Drawing.Size(72, 21);
            this.radioButtonWithout.TabIndex = 1;
            this.radioButtonWithout.TabStop = true;
            this.radioButtonWithout.Text = "Without";
            this.radioButtonWithout.UseVisualStyleBackColor = true;
            this.radioButtonWithout.CheckedChanged += new System.EventHandler(this.radioButtonWithout_CheckedChanged);
            // 
            // radioButtonWith
            // 
            this.radioButtonWith.AutoSize = true;
            this.radioButtonWith.Location = new System.Drawing.Point(14, 19);
            this.radioButtonWith.Name = "radioButtonWith";
            this.radioButtonWith.Size = new System.Drawing.Size(53, 21);
            this.radioButtonWith.TabIndex = 0;
            this.radioButtonWith.TabStop = true;
            this.radioButtonWith.Text = "With";
            this.radioButtonWith.UseVisualStyleBackColor = true;
            this.radioButtonWith.CheckedChanged += new System.EventHandler(this.radioButtonWith_CheckedChanged);
            // 
            // textBoxYearBegin
            // 
            this.textBoxYearBegin.Location = new System.Drawing.Point(349, 55);
            this.textBoxYearBegin.Name = "textBoxYearBegin";
            this.textBoxYearBegin.Size = new System.Drawing.Size(146, 24);
            this.textBoxYearBegin.TabIndex = 8;
            this.textBoxYearBegin.TextChanged += new System.EventHandler(this.textBoxYearBegin_TextChanged);
            // 
            // labelMonth
            // 
            this.labelMonth.AutoSize = true;
            this.labelMonth.Location = new System.Drawing.Point(246, 84);
            this.labelMonth.Name = "labelMonth";
            this.labelMonth.Size = new System.Drawing.Size(102, 17);
            this.labelMonth.TabIndex = 6;
            this.labelMonth.Text = "Mois ouverture :";
            // 
            // panelAlea
            // 
            this.panelAlea.BackColor = System.Drawing.Color.Transparent;
            this.panelAlea.Controls.Add(this.groupBox1);
            this.panelAlea.Controls.Add(this.groupBox2);
            this.panelAlea.Font = new System.Drawing.Font("Corbel", 10F);
            this.panelAlea.Location = new System.Drawing.Point(114, 185);
            this.panelAlea.Name = "panelAlea";
            this.panelAlea.Size = new System.Drawing.Size(387, 207);
            this.panelAlea.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxCostPrice2);
            this.groupBox1.Controls.Add(this.comboBoxCostPrice);
            this.groupBox1.Controls.Add(this.comboBoxProdPrice2);
            this.groupBox1.Controls.Add(this.LabelChargePrice);
            this.groupBox1.Controls.Add(this.LabelProductPrice);
            this.groupBox1.Controls.Add(this.comboBoxProductPrice);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 87);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PRICE";
            // 
            // comboBoxCostPrice2
            // 
            this.comboBoxCostPrice2.FormattingEnabled = true;
            this.comboBoxCostPrice2.Location = new System.Drawing.Point(187, 50);
            this.comboBoxCostPrice2.Name = "comboBoxCostPrice2";
            this.comboBoxCostPrice2.Size = new System.Drawing.Size(190, 23);
            this.comboBoxCostPrice2.TabIndex = 18;
            // 
            // comboBoxCostPrice
            // 
            this.comboBoxCostPrice.FormattingEnabled = true;
            this.comboBoxCostPrice.Location = new System.Drawing.Point(70, 50);
            this.comboBoxCostPrice.Name = "comboBoxCostPrice";
            this.comboBoxCostPrice.Size = new System.Drawing.Size(111, 23);
            this.comboBoxCostPrice.TabIndex = 17;
            // 
            // comboBoxProdPrice2
            // 
            this.comboBoxProdPrice2.FormattingEnabled = true;
            this.comboBoxProdPrice2.Location = new System.Drawing.Point(187, 23);
            this.comboBoxProdPrice2.Name = "comboBoxProdPrice2";
            this.comboBoxProdPrice2.Size = new System.Drawing.Size(190, 23);
            this.comboBoxProdPrice2.TabIndex = 16;
            // 
            // LabelChargePrice
            // 
            this.LabelChargePrice.AutoSize = true;
            this.LabelChargePrice.Location = new System.Drawing.Point(0, 53);
            this.LabelChargePrice.Name = "LabelChargePrice";
            this.LabelChargePrice.Size = new System.Drawing.Size(34, 17);
            this.LabelChargePrice.TabIndex = 15;
            this.LabelChargePrice.Text = "Cost";
            // 
            // LabelProductPrice
            // 
            this.LabelProductPrice.AutoSize = true;
            this.LabelProductPrice.Location = new System.Drawing.Point(0, 26);
            this.LabelProductPrice.Name = "LabelProductPrice";
            this.LabelProductPrice.Size = new System.Drawing.Size(54, 17);
            this.LabelProductPrice.TabIndex = 14;
            this.LabelProductPrice.Text = "Product";
            // 
            // comboBoxProductPrice
            // 
            this.comboBoxProductPrice.FormattingEnabled = true;
            this.comboBoxProductPrice.Location = new System.Drawing.Point(70, 23);
            this.comboBoxProductPrice.Name = "comboBoxProductPrice";
            this.comboBoxProductPrice.Size = new System.Drawing.Size(111, 23);
            this.comboBoxProductPrice.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxExtQte2);
            this.groupBox2.Controls.Add(this.comboBoxExterQte);
            this.groupBox2.Controls.Add(this.LabelExternaliteQte);
            this.groupBox2.Controls.Add(this.comboBoxCostQte2);
            this.groupBox2.Controls.Add(this.LabelProductQte);
            this.groupBox2.Controls.Add(this.comboBoxCostQte);
            this.groupBox2.Controls.Add(this.comboBoxProductQte);
            this.groupBox2.Controls.Add(this.comboBoxProdQte2);
            this.groupBox2.Controls.Add(this.LabelCostQte);
            this.groupBox2.Location = new System.Drawing.Point(3, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 112);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "QUANTITY";
            // 
            // comboBoxExtQte2
            // 
            this.comboBoxExtQte2.FormattingEnabled = true;
            this.comboBoxExtQte2.Location = new System.Drawing.Point(187, 81);
            this.comboBoxExtQte2.Name = "comboBoxExtQte2";
            this.comboBoxExtQte2.Size = new System.Drawing.Size(190, 23);
            this.comboBoxExtQte2.TabIndex = 27;
            // 
            // comboBoxExterQte
            // 
            this.comboBoxExterQte.FormattingEnabled = true;
            this.comboBoxExterQte.Location = new System.Drawing.Point(70, 81);
            this.comboBoxExterQte.Name = "comboBoxExterQte";
            this.comboBoxExterQte.Size = new System.Drawing.Size(111, 23);
            this.comboBoxExterQte.TabIndex = 26;
            // 
            // LabelExternaliteQte
            // 
            this.LabelExternaliteQte.AutoSize = true;
            this.LabelExternaliteQte.Location = new System.Drawing.Point(0, 84);
            this.LabelExternaliteQte.Name = "LabelExternaliteQte";
            this.LabelExternaliteQte.Size = new System.Drawing.Size(71, 17);
            this.LabelExternaliteQte.TabIndex = 25;
            this.LabelExternaliteQte.Text = "Externality";
            // 
            // comboBoxCostQte2
            // 
            this.comboBoxCostQte2.FormattingEnabled = true;
            this.comboBoxCostQte2.Location = new System.Drawing.Point(187, 51);
            this.comboBoxCostQte2.Name = "comboBoxCostQte2";
            this.comboBoxCostQte2.Size = new System.Drawing.Size(190, 23);
            this.comboBoxCostQte2.TabIndex = 24;
            // 
            // LabelProductQte
            // 
            this.LabelProductQte.AutoSize = true;
            this.LabelProductQte.Location = new System.Drawing.Point(0, 24);
            this.LabelProductQte.Name = "LabelProductQte";
            this.LabelProductQte.Size = new System.Drawing.Size(54, 17);
            this.LabelProductQte.TabIndex = 20;
            this.LabelProductQte.Text = "Product";
            // 
            // comboBoxCostQte
            // 
            this.comboBoxCostQte.FormattingEnabled = true;
            this.comboBoxCostQte.Location = new System.Drawing.Point(70, 51);
            this.comboBoxCostQte.Name = "comboBoxCostQte";
            this.comboBoxCostQte.Size = new System.Drawing.Size(111, 23);
            this.comboBoxCostQte.TabIndex = 23;
            // 
            // comboBoxProductQte
            // 
            this.comboBoxProductQte.FormattingEnabled = true;
            this.comboBoxProductQte.Location = new System.Drawing.Point(70, 21);
            this.comboBoxProductQte.Name = "comboBoxProductQte";
            this.comboBoxProductQte.Size = new System.Drawing.Size(111, 23);
            this.comboBoxProductQte.TabIndex = 19;
            // 
            // comboBoxProdQte2
            // 
            this.comboBoxProdQte2.FormattingEnabled = true;
            this.comboBoxProdQte2.Location = new System.Drawing.Point(187, 21);
            this.comboBoxProdQte2.Name = "comboBoxProdQte2";
            this.comboBoxProdQte2.Size = new System.Drawing.Size(190, 23);
            this.comboBoxProdQte2.TabIndex = 22;
            // 
            // LabelCostQte
            // 
            this.LabelCostQte.AutoSize = true;
            this.LabelCostQte.Location = new System.Drawing.Point(3, 54);
            this.LabelCostQte.Name = "LabelCostQte";
            this.LabelCostQte.Size = new System.Drawing.Size(34, 17);
            this.LabelCostQte.TabIndex = 21;
            this.LabelCostQte.Text = "Cost";
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(246, 58);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(90, 17);
            this.labelYear.TabIndex = 5;
            this.labelYear.Text = "Année début :";
            // 
            // textBoxVar
            // 
            this.textBoxVar.Location = new System.Drawing.Point(108, 55);
            this.textBoxVar.Name = "textBoxVar";
            this.textBoxVar.Size = new System.Drawing.Size(101, 24);
            this.textBoxVar.TabIndex = 4;
            this.textBoxVar.TextChanged += new System.EventHandler(this.textBoxVar_TextChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(108, 25);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(559, 24);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // labelClassif
            // 
            this.labelClassif.AutoSize = true;
            this.labelClassif.Location = new System.Drawing.Point(14, 115);
            this.labelClassif.Name = "labelClassif";
            this.labelClassif.Size = new System.Drawing.Size(94, 17);
            this.labelClassif.TabIndex = 2;
            this.labelClassif.Text = "Classification : ";
            // 
            // labelVariante
            // 
            this.labelVariante.AutoSize = true;
            this.labelVariante.Location = new System.Drawing.Point(14, 58);
            this.labelVariante.Name = "labelVariante";
            this.labelVariante.Size = new System.Drawing.Size(68, 17);
            this.labelVariante.TabIndex = 1;
            this.labelVariante.Text = "Variante : ";
            // 
            // labelNom
            // 
            this.labelNom.AutoSize = true;
            this.labelNom.Location = new System.Drawing.Point(14, 28);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(54, 17);
            this.labelNom.TabIndex = 0;
            this.labelNom.Text = "Name : ";
            // 
            // UserControlDefinition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(690, 390);
            this.Name = "UserControlDefinition";
            this.Size = new System.Drawing.Size(701, 396);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxAléa.ResumeLayout(false);
            this.groupBoxAléa.PerformLayout();
            this.panelAlea.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxVar;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelClassif;
        private System.Windows.Forms.Label labelVariante;
        private System.Windows.Forms.Label labelNom;
        private System.Windows.Forms.Label labelMonth;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.TextBox textBoxYearBegin;
        private System.Windows.Forms.ComboBox comboBoxMonth;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBoxAléa;
        private System.Windows.Forms.RadioButton radioButtonWithout;
        private System.Windows.Forms.RadioButton radioButtonWith;
        private System.Windows.Forms.Panel panelAlea;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxExtQte2;
        private System.Windows.Forms.ComboBox comboBoxExterQte;
        private System.Windows.Forms.Label LabelExternaliteQte;
        private System.Windows.Forms.ComboBox comboBoxCostQte2;
        private System.Windows.Forms.Label LabelProductQte;
        private System.Windows.Forms.ComboBox comboBoxCostQte;
        private System.Windows.Forms.ComboBox comboBoxProductQte;
        private System.Windows.Forms.ComboBox comboBoxProdQte2;
        private System.Windows.Forms.Label LabelCostQte;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxCostPrice2;
        private System.Windows.Forms.ComboBox comboBoxCostPrice;
        private System.Windows.Forms.ComboBox comboBoxProdPrice2;
        private System.Windows.Forms.Label LabelChargePrice;
        private System.Windows.Forms.Label LabelProductPrice;
        private System.Windows.Forms.ComboBox comboBoxProductPrice;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Button buttonBack;
    }
}
