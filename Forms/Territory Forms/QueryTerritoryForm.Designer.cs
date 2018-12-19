namespace OMEGA.Forms.Territory_Forms
{
    partial class QueryTerritoryForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonAND = new System.Windows.Forms.Button();
            this.buttonOR = new System.Windows.Forms.Button();
            this.textBoxMot = new System.Windows.Forms.TextBox();
            this.comboBoxFiltre = new System.Windows.Forms.ComboBox();
            this.comboBoxSecondType = new System.Windows.Forms.ComboBox();
            this.comboBoxMainType = new System.Windows.Forms.ComboBox();
            this.comboBoxdisplayhide = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelrequete = new System.Windows.Forms.Label();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.BurlyWood;
            this.panel1.Controls.Add(this.ButtonAND);
            this.panel1.Controls.Add(this.buttonOR);
            this.panel1.Controls.Add(this.textBoxMot);
            this.panel1.Controls.Add(this.comboBoxFiltre);
            this.panel1.Controls.Add(this.comboBoxSecondType);
            this.panel1.Controls.Add(this.comboBoxMainType);
            this.panel1.Controls.Add(this.comboBoxdisplayhide);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(842, 68);
            this.panel1.TabIndex = 0;
            // 
            // ButtonAND
            // 
            this.ButtonAND.Enabled = false;
            this.ButtonAND.Location = new System.Drawing.Point(12, 40);
            this.ButtonAND.Name = "ButtonAND";
            this.ButtonAND.Size = new System.Drawing.Size(47, 25);
            this.ButtonAND.TabIndex = 11;
            this.ButtonAND.Text = "AND";
            this.ButtonAND.UseVisualStyleBackColor = true;
            // 
            // buttonOR
            // 
            this.buttonOR.Enabled = false;
            this.buttonOR.Location = new System.Drawing.Point(65, 40);
            this.buttonOR.Name = "buttonOR";
            this.buttonOR.Size = new System.Drawing.Size(43, 25);
            this.buttonOR.TabIndex = 10;
            this.buttonOR.Text = "OR";
            this.buttonOR.UseVisualStyleBackColor = true;
            // 
            // textBoxMot
            // 
            this.textBoxMot.Location = new System.Drawing.Point(659, 17);
            this.textBoxMot.Name = "textBoxMot";
            this.textBoxMot.Size = new System.Drawing.Size(168, 20);
            this.textBoxMot.TabIndex = 6;
            this.textBoxMot.TextChanged += new System.EventHandler(this.textBoxMot_TextChanged);
            // 
            // comboBoxFiltre
            // 
            this.comboBoxFiltre.FormattingEnabled = true;
            this.comboBoxFiltre.Location = new System.Drawing.Point(459, 16);
            this.comboBoxFiltre.Name = "comboBoxFiltre";
            this.comboBoxFiltre.Size = new System.Drawing.Size(194, 21);
            this.comboBoxFiltre.TabIndex = 5;
            this.comboBoxFiltre.SelectedIndexChanged += new System.EventHandler(this.comboBoxFiltre_SelectedIndexChanged);
            // 
            // comboBoxSecondType
            // 
            this.comboBoxSecondType.FormattingEnabled = true;
            this.comboBoxSecondType.Location = new System.Drawing.Point(266, 16);
            this.comboBoxSecondType.Name = "comboBoxSecondType";
            this.comboBoxSecondType.Size = new System.Drawing.Size(187, 21);
            this.comboBoxSecondType.TabIndex = 2;
            this.comboBoxSecondType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSecondType_SelectedIndexChanged);
            // 
            // comboBoxMainType
            // 
            this.comboBoxMainType.FormattingEnabled = true;
            this.comboBoxMainType.Location = new System.Drawing.Point(139, 16);
            this.comboBoxMainType.Name = "comboBoxMainType";
            this.comboBoxMainType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMainType.TabIndex = 1;
            this.comboBoxMainType.SelectedIndexChanged += new System.EventHandler(this.comboBoxMainType_SelectedIndexChanged);
            // 
            // comboBoxdisplayhide
            // 
            this.comboBoxdisplayhide.FormattingEnabled = true;
            this.comboBoxdisplayhide.Location = new System.Drawing.Point(12, 16);
            this.comboBoxdisplayhide.Name = "comboBoxdisplayhide";
            this.comboBoxdisplayhide.Size = new System.Drawing.Size(121, 21);
            this.comboBoxdisplayhide.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(712, 127);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 25);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(799, 127);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(43, 25);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelrequete
            // 
            this.labelrequete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelrequete.AutoSize = true;
            this.labelrequete.Location = new System.Drawing.Point(9, 116);
            this.labelrequete.Name = "labelrequete";
            this.labelrequete.Size = new System.Drawing.Size(66, 13);
            this.labelrequete.TabIndex = 12;
            this.labelrequete.Text = "Your Query :";
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.BackColor = System.Drawing.Color.BurlyWood;
            this.textBoxQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxQuery.Location = new System.Drawing.Point(74, 116);
            this.textBoxQuery.Multiline = true;
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.Size = new System.Drawing.Size(611, 20);
            this.textBoxQuery.TabIndex = 12;
            this.textBoxQuery.Text = "none";
            // 
            // QueryTerritoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(842, 154);
            this.Controls.Add(this.textBoxQuery);
            this.Controls.Add(this.labelrequete);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panel1);
            this.Name = "QueryTerritoryForm";
            this.Text = "QueryTerritoryForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ButtonAND;
        private System.Windows.Forms.Button buttonOR;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelrequete;
        private System.Windows.Forms.TextBox textBoxQuery;
        internal System.Windows.Forms.ComboBox comboBoxdisplayhide;
        internal System.Windows.Forms.TextBox textBoxMot;
        internal System.Windows.Forms.ComboBox comboBoxFiltre;
        internal System.Windows.Forms.ComboBox comboBoxSecondType;
        internal System.Windows.Forms.ComboBox comboBoxMainType;
    }
}