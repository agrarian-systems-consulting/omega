namespace OMEGA.Forms
{
    partial class PeriodeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeriodeForm));
            this.panelPeriode = new System.Windows.Forms.Panel();
            this.buttonDuplicatePeriode = new System.Windows.Forms.Button();
            this.buttonAddPeriode = new System.Windows.Forms.Button();
            this.buttonRemovePeriode = new System.Windows.Forms.Button();
            this.dataGridViewPeriode = new System.Windows.Forms.DataGridView();
            this.pictureBoxExport = new System.Windows.Forms.PictureBox();
            this.labelCopyBas = new System.Windows.Forms.Label();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.dataGridViewDetailPeriode = new System.Windows.Forms.DataGridView();
            this.groupBoxTemplate = new System.Windows.Forms.GroupBox();
            this.radioButtonWeek = new System.Windows.Forms.RadioButton();
            this.radioButton2weeks = new System.Windows.Forms.RadioButton();
            this.radioBttnMois = new System.Windows.Forms.RadioButton();
            this.buttonRemovePeriodeDetail = new System.Windows.Forms.Button();
            this.buttonAddPeriodeDetail = new System.Windows.Forms.Button();
            this.buttonOKPeriode = new System.Windows.Forms.Button();
            this.buttonCancelPeriode = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelPeriode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPeriode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetailPeriode)).BeginInit();
            this.groupBoxTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPeriode
            // 
            this.panelPeriode.Controls.Add(this.buttonDuplicatePeriode);
            this.panelPeriode.Controls.Add(this.buttonAddPeriode);
            this.panelPeriode.Controls.Add(this.buttonRemovePeriode);
            this.panelPeriode.Controls.Add(this.dataGridViewPeriode);
            this.panelPeriode.Location = new System.Drawing.Point(3, 3);
            this.panelPeriode.Margin = new System.Windows.Forms.Padding(2);
            this.panelPeriode.Name = "panelPeriode";
            this.panelPeriode.Size = new System.Drawing.Size(216, 363);
            this.panelPeriode.TabIndex = 1;
            // 
            // buttonDuplicatePeriode
            // 
            this.buttonDuplicatePeriode.Enabled = false;
            this.buttonDuplicatePeriode.Location = new System.Drawing.Point(3, 79);
            this.buttonDuplicatePeriode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDuplicatePeriode.Name = "buttonDuplicatePeriode";
            this.buttonDuplicatePeriode.Size = new System.Drawing.Size(90, 27);
            this.buttonDuplicatePeriode.TabIndex = 32;
            this.buttonDuplicatePeriode.Text = "Validate";
            this.buttonDuplicatePeriode.UseVisualStyleBackColor = true;
            // 
            // buttonAddPeriode
            // 
            this.buttonAddPeriode.Location = new System.Drawing.Point(3, 15);
            this.buttonAddPeriode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddPeriode.Name = "buttonAddPeriode";
            this.buttonAddPeriode.Size = new System.Drawing.Size(90, 27);
            this.buttonAddPeriode.TabIndex = 30;
            this.buttonAddPeriode.Text = "Add";
            this.buttonAddPeriode.UseVisualStyleBackColor = true;
            // 
            // buttonRemovePeriode
            // 
            this.buttonRemovePeriode.Location = new System.Drawing.Point(3, 47);
            this.buttonRemovePeriode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemovePeriode.Name = "buttonRemovePeriode";
            this.buttonRemovePeriode.Size = new System.Drawing.Size(90, 27);
            this.buttonRemovePeriode.TabIndex = 31;
            this.buttonRemovePeriode.Text = "Remove";
            this.buttonRemovePeriode.UseVisualStyleBackColor = true;
            // 
            // dataGridViewPeriode
            // 
            this.dataGridViewPeriode.AllowUserToAddRows = false;
            this.dataGridViewPeriode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPeriode.Location = new System.Drawing.Point(96, 2);
            this.dataGridViewPeriode.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewPeriode.Name = "dataGridViewPeriode";
            this.dataGridViewPeriode.ReadOnly = true;
            this.dataGridViewPeriode.RowHeadersWidth = 10;
            this.dataGridViewPeriode.RowTemplate.Height = 24;
            this.dataGridViewPeriode.Size = new System.Drawing.Size(118, 359);
            this.dataGridViewPeriode.TabIndex = 1;
            // 
            // pictureBoxExport
            // 
            this.pictureBoxExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxExport.Image = global::OMEGA.Properties.Resources.icons8_exporter_csv_30;
            this.pictureBoxExport.Location = new System.Drawing.Point(222, 334);
            this.pictureBoxExport.Name = "pictureBoxExport";
            this.pictureBoxExport.Size = new System.Drawing.Size(30, 30);
            this.pictureBoxExport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxExport.TabIndex = 44;
            this.pictureBoxExport.TabStop = false;
            this.pictureBoxExport.Click += new System.EventHandler(this.pictureBoxExport_Click);
            // 
            // labelCopyBas
            // 
            this.labelCopyBas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyBas.AutoSize = true;
            this.labelCopyBas.Location = new System.Drawing.Point(580, 268);
            this.labelCopyBas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCopyBas.Name = "labelCopyBas";
            this.labelCopyBas.Size = new System.Drawing.Size(130, 13);
            this.labelCopyBas.TabIndex = 38;
            this.labelCopyBas.Text = "Copier cellule vers le bas :";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopy.Image")));
            this.buttonCopy.Location = new System.Drawing.Point(714, 257);
            this.buttonCopy.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(33, 31);
            this.buttonCopy.TabIndex = 37;
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // dataGridViewDetailPeriode
            // 
            this.dataGridViewDetailPeriode.AllowUserToAddRows = false;
            this.dataGridViewDetailPeriode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDetailPeriode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetailPeriode.Location = new System.Drawing.Point(223, 5);
            this.dataGridViewDetailPeriode.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewDetailPeriode.Name = "dataGridViewDetailPeriode";
            this.dataGridViewDetailPeriode.RowTemplate.Height = 24;
            this.dataGridViewDetailPeriode.Size = new System.Drawing.Size(524, 252);
            this.dataGridViewDetailPeriode.TabIndex = 2;
            // 
            // groupBoxTemplate
            // 
            this.groupBoxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxTemplate.Controls.Add(this.radioButtonWeek);
            this.groupBoxTemplate.Controls.Add(this.radioButton2weeks);
            this.groupBoxTemplate.Controls.Add(this.radioBttnMois);
            this.groupBoxTemplate.Location = new System.Drawing.Point(389, 261);
            this.groupBoxTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxTemplate.Name = "groupBoxTemplate";
            this.groupBoxTemplate.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxTemplate.Size = new System.Drawing.Size(181, 76);
            this.groupBoxTemplate.TabIndex = 36;
            this.groupBoxTemplate.TabStop = false;
            this.groupBoxTemplate.Text = "Template Calendars";
            // 
            // radioButtonWeek
            // 
            this.radioButtonWeek.AutoSize = true;
            this.radioButtonWeek.Font = new System.Drawing.Font("Corbel", 8F);
            this.radioButtonWeek.Location = new System.Drawing.Point(4, 46);
            this.radioButtonWeek.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonWeek.Name = "radioButtonWeek";
            this.radioButtonWeek.Size = new System.Drawing.Size(50, 17);
            this.radioButtonWeek.TabIndex = 2;
            this.radioButtonWeek.TabStop = true;
            this.radioButtonWeek.Text = "Week";
            this.radioButtonWeek.UseVisualStyleBackColor = true;
            // 
            // radioButton2weeks
            // 
            this.radioButton2weeks.AutoSize = true;
            this.radioButton2weeks.Font = new System.Drawing.Font("Corbel", 8F);
            this.radioButton2weeks.Location = new System.Drawing.Point(68, 17);
            this.radioButton2weeks.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton2weeks.Name = "radioButton2weeks";
            this.radioButton2weeks.Size = new System.Drawing.Size(60, 17);
            this.radioButton2weeks.TabIndex = 1;
            this.radioButton2weeks.TabStop = true;
            this.radioButton2weeks.Text = "2 weeks";
            this.radioButton2weeks.UseVisualStyleBackColor = true;
            // 
            // radioBttnMois
            // 
            this.radioBttnMois.AutoSize = true;
            this.radioBttnMois.Font = new System.Drawing.Font("Corbel", 8F);
            this.radioBttnMois.Location = new System.Drawing.Point(4, 17);
            this.radioBttnMois.Margin = new System.Windows.Forms.Padding(2);
            this.radioBttnMois.Name = "radioBttnMois";
            this.radioBttnMois.Size = new System.Drawing.Size(60, 17);
            this.radioBttnMois.TabIndex = 0;
            this.radioBttnMois.TabStop = true;
            this.radioBttnMois.Text = "Months";
            this.radioBttnMois.UseVisualStyleBackColor = true;
            // 
            // buttonRemovePeriodeDetail
            // 
            this.buttonRemovePeriodeDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemovePeriodeDetail.Enabled = false;
            this.buttonRemovePeriodeDetail.Location = new System.Drawing.Point(301, 261);
            this.buttonRemovePeriodeDetail.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemovePeriodeDetail.Name = "buttonRemovePeriodeDetail";
            this.buttonRemovePeriodeDetail.Size = new System.Drawing.Size(84, 27);
            this.buttonRemovePeriodeDetail.TabIndex = 33;
            this.buttonRemovePeriodeDetail.Text = "Remove";
            this.buttonRemovePeriodeDetail.UseVisualStyleBackColor = true;
            // 
            // buttonAddPeriodeDetail
            // 
            this.buttonAddPeriodeDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddPeriodeDetail.Enabled = false;
            this.buttonAddPeriodeDetail.Location = new System.Drawing.Point(221, 261);
            this.buttonAddPeriodeDetail.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddPeriodeDetail.Name = "buttonAddPeriodeDetail";
            this.buttonAddPeriodeDetail.Size = new System.Drawing.Size(76, 27);
            this.buttonAddPeriodeDetail.TabIndex = 33;
            this.buttonAddPeriodeDetail.Text = "Add";
            this.buttonAddPeriodeDetail.UseVisualStyleBackColor = true;
            // 
            // buttonOKPeriode
            // 
            this.buttonOKPeriode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOKPeriode.CausesValidation = false;
            this.buttonOKPeriode.Location = new System.Drawing.Point(669, 339);
            this.buttonOKPeriode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOKPeriode.Name = "buttonOKPeriode";
            this.buttonOKPeriode.Size = new System.Drawing.Size(78, 27);
            this.buttonOKPeriode.TabIndex = 34;
            this.buttonOKPeriode.Text = "OK";
            this.buttonOKPeriode.UseVisualStyleBackColor = true;
            // 
            // buttonCancelPeriode
            // 
            this.buttonCancelPeriode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancelPeriode.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancelPeriode.Location = new System.Drawing.Point(576, 339);
            this.buttonCancelPeriode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancelPeriode.Name = "buttonCancelPeriode";
            this.buttonCancelPeriode.Size = new System.Drawing.Size(87, 27);
            this.buttonCancelPeriode.TabIndex = 32;
            this.buttonCancelPeriode.Text = "Cancel";
            this.buttonCancelPeriode.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 377);
            this.panel1.TabIndex = 45;
            // 
            // PeriodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 377);
            this.Controls.Add(this.dataGridViewDetailPeriode);
            this.Controls.Add(this.labelCopyBas);
            this.Controls.Add(this.pictureBoxExport);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.panelPeriode);
            this.Controls.Add(this.buttonOKPeriode);
            this.Controls.Add(this.groupBoxTemplate);
            this.Controls.Add(this.buttonCancelPeriode);
            this.Controls.Add(this.buttonAddPeriodeDetail);
            this.Controls.Add(this.buttonRemovePeriodeDetail);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(772, 416);
            this.Name = "PeriodeForm";
            this.Text = "PeriodeForm";
            this.Load += new System.EventHandler(this.PeriodeForm_Load);
            this.panelPeriode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPeriode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetailPeriode)).EndInit();
            this.groupBoxTemplate.ResumeLayout(false);
            this.groupBoxTemplate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelPeriode;
        private System.Windows.Forms.Button buttonDuplicatePeriode;
        private System.Windows.Forms.Button buttonAddPeriode;
        private System.Windows.Forms.DataGridView dataGridViewPeriode;
        private System.Windows.Forms.Label labelCopyBas;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.DataGridView dataGridViewDetailPeriode;
        private System.Windows.Forms.GroupBox groupBoxTemplate;
        private System.Windows.Forms.RadioButton radioButtonWeek;
        private System.Windows.Forms.RadioButton radioButton2weeks;
        private System.Windows.Forms.RadioButton radioBttnMois;
        private System.Windows.Forms.Button buttonRemovePeriodeDetail;
        private System.Windows.Forms.Button buttonAddPeriodeDetail;
        private System.Windows.Forms.Button buttonOKPeriode;
        private System.Windows.Forms.Button buttonCancelPeriode;
        private System.Windows.Forms.PictureBox pictureBoxExport;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button buttonRemovePeriode;
    }
}