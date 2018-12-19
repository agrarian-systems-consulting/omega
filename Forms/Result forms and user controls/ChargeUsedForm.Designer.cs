namespace OMEGA.Forms
{
    partial class ChargeUsedForm
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1UsedOf = new System.Windows.Forms.Label();
            this.textBoxusedof = new System.Windows.Forms.TextBox();
            this.textBoxunity = new System.Windows.Forms.TextBox();
            this.labelunity = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.treeView1.Location = new System.Drawing.Point(2, 43);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(166, 370);
            this.treeView1.TabIndex = 0;
            // 
            // label1UsedOf
            // 
            this.label1UsedOf.AutoSize = true;
            this.label1UsedOf.Location = new System.Drawing.Point(2, 12);
            this.label1UsedOf.Name = "label1UsedOf";
            this.label1UsedOf.Size = new System.Drawing.Size(59, 17);
            this.label1UsedOf.TabIndex = 1;
            this.label1UsedOf.Text = "Used of :";
            // 
            // textBoxusedof
            // 
            this.textBoxusedof.Location = new System.Drawing.Point(63, 9);
            this.textBoxusedof.Name = "textBoxusedof";
            this.textBoxusedof.Size = new System.Drawing.Size(176, 24);
            this.textBoxusedof.TabIndex = 2;
            // 
            // textBoxunity
            // 
            this.textBoxunity.Location = new System.Drawing.Point(314, 9);
            this.textBoxunity.Name = "textBoxunity";
            this.textBoxunity.Size = new System.Drawing.Size(74, 24);
            this.textBoxunity.TabIndex = 4;
            // 
            // labelunity
            // 
            this.labelunity.AutoSize = true;
            this.labelunity.Location = new System.Drawing.Point(267, 12);
            this.labelunity.Name = "labelunity";
            this.labelunity.Size = new System.Drawing.Size(46, 17);
            this.labelunity.TabIndex = 3;
            this.labelunity.Text = "Unity :";
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(416, 12);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(41, 17);
            this.labelYear.TabIndex = 5;
            this.labelYear.Text = "Year :";
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(460, 9);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(77, 23);
            this.comboBoxYear.TabIndex = 6;
            this.comboBoxYear.SelectedIndexChanged += new System.EventHandler(this.comboBoxYear_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(174, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(583, 370);
            this.dataGridView1.TabIndex = 7;
            // 
            // ChargeUsedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(758, 416);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBoxYear);
            this.Controls.Add(this.labelYear);
            this.Controls.Add(this.textBoxunity);
            this.Controls.Add(this.labelunity);
            this.Controls.Add(this.textBoxusedof);
            this.Controls.Add(this.label1UsedOf);
            this.Controls.Add(this.treeView1);
            this.Font = new System.Drawing.Font("Corbel", 10F);
            this.Name = "ChargeUsedForm";
            this.Text = "ChargeUsedForm";
            this.Load += new System.EventHandler(this.ChargeUsedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1UsedOf;
        private System.Windows.Forms.TextBox textBoxusedof;
        private System.Windows.Forms.TextBox textBoxunity;
        private System.Windows.Forms.Label labelunity;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}