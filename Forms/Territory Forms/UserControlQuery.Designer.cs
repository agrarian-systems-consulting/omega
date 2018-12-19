namespace OMEGA.Forms.Territory_Forms
{
    partial class UserControlQuery
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonAND = new System.Windows.Forms.Button();
            this.buttonOR = new System.Windows.Forms.Button();
            this.textBoxMot = new System.Windows.Forms.TextBox();
            this.comboBoxFiltre = new System.Windows.Forms.ComboBox();
            this.comboBoxSecondType = new System.Windows.Forms.ComboBox();
            this.comboBoxMainType = new System.Windows.Forms.ComboBox();
            this.comboBoxdisplayhide = new System.Windows.Forms.ComboBox();
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Corbel", 10F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(841, 78);
            this.panel1.TabIndex = 1;
            // 
            // ButtonAND
            // 
            this.ButtonAND.Location = new System.Drawing.Point(12, 45);
            this.ButtonAND.Name = "ButtonAND";
            this.ButtonAND.Size = new System.Drawing.Size(47, 25);
            this.ButtonAND.TabIndex = 9;
            this.ButtonAND.Text = "AND";
            this.ButtonAND.UseVisualStyleBackColor = true;
            // 
            // buttonOR
            // 
            this.buttonOR.Location = new System.Drawing.Point(65, 45);
            this.buttonOR.Name = "buttonOR";
            this.buttonOR.Size = new System.Drawing.Size(43, 25);
            this.buttonOR.TabIndex = 8;
            this.buttonOR.Text = "OR";
            this.buttonOR.UseVisualStyleBackColor = true;
            // 
            // textBoxMot
            // 
            this.textBoxMot.Location = new System.Drawing.Point(659, 15);
            this.textBoxMot.Name = "textBoxMot";
            this.textBoxMot.Size = new System.Drawing.Size(168, 24);
            this.textBoxMot.TabIndex = 6;
            // 
            // comboBoxFiltre
            // 
            this.comboBoxFiltre.FormattingEnabled = true;
            this.comboBoxFiltre.Location = new System.Drawing.Point(459, 16);
            this.comboBoxFiltre.Name = "comboBoxFiltre";
            this.comboBoxFiltre.Size = new System.Drawing.Size(194, 23);
            this.comboBoxFiltre.TabIndex = 5;
            // 
            // comboBoxSecondType
            // 
            this.comboBoxSecondType.FormattingEnabled = true;
            this.comboBoxSecondType.Location = new System.Drawing.Point(266, 16);
            this.comboBoxSecondType.Name = "comboBoxSecondType";
            this.comboBoxSecondType.Size = new System.Drawing.Size(187, 23);
            this.comboBoxSecondType.TabIndex = 2;
            // 
            // comboBoxMainType
            // 
            this.comboBoxMainType.FormattingEnabled = true;
            this.comboBoxMainType.Location = new System.Drawing.Point(139, 16);
            this.comboBoxMainType.Name = "comboBoxMainType";
            this.comboBoxMainType.Size = new System.Drawing.Size(121, 23);
            this.comboBoxMainType.TabIndex = 1;
            // 
            // comboBoxdisplayhide
            // 
            this.comboBoxdisplayhide.FormattingEnabled = true;
            this.comboBoxdisplayhide.Location = new System.Drawing.Point(12, 16);
            this.comboBoxdisplayhide.Name = "comboBoxdisplayhide";
            this.comboBoxdisplayhide.Size = new System.Drawing.Size(121, 23);
            this.comboBoxdisplayhide.TabIndex = 0;
            // 
            // UserControlQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UserControlQuery";
            this.Size = new System.Drawing.Size(841, 78);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ButtonAND;
        private System.Windows.Forms.Button buttonOR;
        internal System.Windows.Forms.TextBox textBoxMot;
        internal System.Windows.Forms.ComboBox comboBoxFiltre;
        internal System.Windows.Forms.ComboBox comboBoxSecondType;
        internal System.Windows.Forms.ComboBox comboBoxMainType;
        internal System.Windows.Forms.ComboBox comboBoxdisplayhide;
    }
}
