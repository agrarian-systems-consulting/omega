namespace OMEGA.Forms
{
    partial class ColorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorForm));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonColor1 = new System.Windows.Forms.Button();
            this.buttoncolor2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelPreview = new System.Windows.Forms.Label();
            this.buttonHeader = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonColor1
            // 
            this.buttonColor1.Location = new System.Drawing.Point(144, 9);
            this.buttonColor1.Name = "buttonColor1";
            this.buttonColor1.Size = new System.Drawing.Size(75, 23);
            this.buttonColor1.TabIndex = 0;
            this.buttonColor1.Text = "Color row 1";
            this.buttonColor1.UseVisualStyleBackColor = true;
            this.buttonColor1.Click += new System.EventHandler(this.buttonColor1_Click);
            // 
            // buttoncolor2
            // 
            this.buttoncolor2.Location = new System.Drawing.Point(240, 9);
            this.buttoncolor2.Name = "buttoncolor2";
            this.buttoncolor2.Size = new System.Drawing.Size(75, 23);
            this.buttoncolor2.TabIndex = 1;
            this.buttoncolor2.Text = "Color row 2";
            this.buttoncolor2.UseVisualStyleBackColor = true;
            this.buttoncolor2.Click += new System.EventHandler(this.buttoncolor2_Click);
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(333, 269);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // labelPreview
            // 
            this.labelPreview.AutoSize = true;
            this.labelPreview.Location = new System.Drawing.Point(1, 35);
            this.labelPreview.Name = "labelPreview";
            this.labelPreview.Size = new System.Drawing.Size(45, 13);
            this.labelPreview.TabIndex = 3;
            this.labelPreview.Text = "Preview";
            // 
            // buttonHeader
            // 
            this.buttonHeader.Location = new System.Drawing.Point(27, 9);
            this.buttonHeader.Name = "buttonHeader";
            this.buttonHeader.Size = new System.Drawing.Size(95, 23);
            this.buttonHeader.TabIndex = 4;
            this.buttonHeader.Text = "Color Header";
            this.buttonHeader.UseVisualStyleBackColor = true;
            this.buttonHeader.Click += new System.EventHandler(this.buttonHeader_Click);
            // 
            // ColorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 344);
            this.Controls.Add(this.buttonHeader);
            this.Controls.Add(this.labelPreview);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttoncolor2);
            this.Controls.Add(this.buttonColor1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(325, 337);
            this.Name = "ColorForm";
            this.Text = "ColorForm";
            this.Load += new System.EventHandler(this.ColorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonColor1;
        private System.Windows.Forms.Button buttoncolor2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelPreview;
        private System.Windows.Forms.Button buttonHeader;
    }
}