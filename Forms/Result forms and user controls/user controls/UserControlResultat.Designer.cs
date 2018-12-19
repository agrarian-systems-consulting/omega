namespace OMEGA.Forms.ResultUserControl
{
    partial class UserControlResultat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlResultat));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panelCalendars = new System.Windows.Forms.Panel();
            this.panelGridCalendar = new System.Windows.Forms.Panel();
            this.dataGridViewValues = new System.Windows.Forms.DataGridView();
            this.panelgraphecalendar = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.buttonValues = new System.Windows.Forms.Button();
            this.buttongraphe = new System.Windows.Forms.Button();
            this.groupBoxcalendrier = new System.Windows.Forms.GroupBox();
            this.textBoxnbworker = new System.Windows.Forms.TextBox();
            this.labelemployee = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.comboBoxyear = new System.Windows.Forms.ComboBox();
            this.labelcalendrier = new System.Windows.Forms.Label();
            this.dataGridViewListeCalendar = new System.Windows.Forms.DataGridView();
            this.panelmonnaie = new System.Windows.Forms.Panel();
            this.buttonAutremonnaie = new System.Windows.Forms.Button();
            this.textBoxCurrency = new System.Windows.Forms.TextBox();
            this.Results = new System.Windows.Forms.Label();
            this.panelGraphique = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panelStandard = new System.Windows.Forms.Panel();
            this.dataGridViewStd = new System.Windows.Forms.DataGridView();
            this.panelbouton = new System.Windows.Forms.Panel();
            this.buttonCalendar = new System.Windows.Forms.Button();
            this.buttonDico = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonGraphique = new System.Windows.Forms.Button();
            this.buttonStandard = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelCalendars.SuspendLayout();
            this.panelGridCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).BeginInit();
            this.panelgraphecalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBoxResults.SuspendLayout();
            this.groupBoxcalendrier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListeCalendar)).BeginInit();
            this.panelmonnaie.SuspendLayout();
            this.panelGraphique.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panelStandard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStd)).BeginInit();
            this.panelbouton.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Controls.Add(this.panelCalendars);
            this.panel1.Controls.Add(this.panelmonnaie);
            this.panel1.Controls.Add(this.Results);
            this.panel1.Controls.Add(this.panelGraphique);
            this.panel1.Controls.Add(this.panelStandard);
            this.panel1.Controls.Add(this.panelbouton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1887, 433);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.BackgroundImage")));
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.Location = new System.Drawing.Point(65, 312);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(39, 31);
            this.buttonBack.TabIndex = 86;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // panelCalendars
            // 
            this.panelCalendars.BackColor = System.Drawing.Color.Transparent;
            this.panelCalendars.Controls.Add(this.panelGridCalendar);
            this.panelCalendars.Controls.Add(this.panelgraphecalendar);
            this.panelCalendars.Controls.Add(this.groupBoxResults);
            this.panelCalendars.Controls.Add(this.groupBoxcalendrier);
            this.panelCalendars.Location = new System.Drawing.Point(522, 39);
            this.panelCalendars.Name = "panelCalendars";
            this.panelCalendars.Size = new System.Drawing.Size(927, 398);
            this.panelCalendars.TabIndex = 4;
            // 
            // panelGridCalendar
            // 
            this.panelGridCalendar.Controls.Add(this.dataGridViewValues);
            this.panelGridCalendar.Location = new System.Drawing.Point(219, 184);
            this.panelGridCalendar.Name = "panelGridCalendar";
            this.panelGridCalendar.Size = new System.Drawing.Size(307, 175);
            this.panelGridCalendar.TabIndex = 8;
            // 
            // dataGridViewValues
            // 
            this.dataGridViewValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewValues.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewValues.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewValues.Name = "dataGridViewValues";
            this.dataGridViewValues.Size = new System.Drawing.Size(307, 175);
            this.dataGridViewValues.TabIndex = 1;
            this.dataGridViewValues.Visible = false;
            // 
            // panelgraphecalendar
            // 
            this.panelgraphecalendar.Controls.Add(this.chart1);
            this.panelgraphecalendar.Location = new System.Drawing.Point(219, 3);
            this.panelgraphecalendar.Name = "panelgraphecalendar";
            this.panelgraphecalendar.Size = new System.Drawing.Size(307, 175);
            this.panelgraphecalendar.TabIndex = 7;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(4, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(288, 169);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Visible = false;
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.buttonValues);
            this.groupBoxResults.Controls.Add(this.buttongraphe);
            this.groupBoxResults.Location = new System.Drawing.Point(59, 204);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(116, 79);
            this.groupBoxResults.TabIndex = 6;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxResults_Paint);
            // 
            // buttonValues
            // 
            this.buttonValues.Location = new System.Drawing.Point(6, 51);
            this.buttonValues.Name = "buttonValues";
            this.buttonValues.Size = new System.Drawing.Size(102, 23);
            this.buttonValues.TabIndex = 1;
            this.buttonValues.Text = "Valeurs";
            this.buttonValues.UseVisualStyleBackColor = true;
            // 
            // buttongraphe
            // 
            this.buttongraphe.Location = new System.Drawing.Point(5, 25);
            this.buttongraphe.Name = "buttongraphe";
            this.buttongraphe.Size = new System.Drawing.Size(102, 23);
            this.buttongraphe.TabIndex = 0;
            this.buttongraphe.Text = "Graphe";
            this.buttongraphe.UseVisualStyleBackColor = true;
            // 
            // groupBoxcalendrier
            // 
            this.groupBoxcalendrier.Controls.Add(this.textBoxnbworker);
            this.groupBoxcalendrier.Controls.Add(this.labelemployee);
            this.groupBoxcalendrier.Controls.Add(this.labelYear);
            this.groupBoxcalendrier.Controls.Add(this.comboBoxyear);
            this.groupBoxcalendrier.Controls.Add(this.labelcalendrier);
            this.groupBoxcalendrier.Controls.Add(this.dataGridViewListeCalendar);
            this.groupBoxcalendrier.Location = new System.Drawing.Point(3, 2);
            this.groupBoxcalendrier.Name = "groupBoxcalendrier";
            this.groupBoxcalendrier.Size = new System.Drawing.Size(210, 191);
            this.groupBoxcalendrier.TabIndex = 0;
            this.groupBoxcalendrier.TabStop = false;
            this.groupBoxcalendrier.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxcalendrier_Paint);
            this.groupBoxcalendrier.Enter += new System.EventHandler(this.groupBoxcalendrier_Enter);
            // 
            // textBoxnbworker
            // 
            this.textBoxnbworker.Location = new System.Drawing.Point(114, 119);
            this.textBoxnbworker.Name = "textBoxnbworker";
            this.textBoxnbworker.Size = new System.Drawing.Size(86, 20);
            this.textBoxnbworker.TabIndex = 5;
            // 
            // labelemployee
            // 
            this.labelemployee.AutoSize = true;
            this.labelemployee.Location = new System.Drawing.Point(112, 102);
            this.labelemployee.Name = "labelemployee";
            this.labelemployee.Size = new System.Drawing.Size(74, 13);
            this.labelemployee.TabIndex = 4;
            this.labelemployee.Text = "Nb travailleurs";
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(111, 33);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(38, 13);
            this.labelYear.TabIndex = 3;
            this.labelYear.Text = "Année";
            // 
            // comboBoxyear
            // 
            this.comboBoxyear.FormattingEnabled = true;
            this.comboBoxyear.Location = new System.Drawing.Point(114, 49);
            this.comboBoxyear.Name = "comboBoxyear";
            this.comboBoxyear.Size = new System.Drawing.Size(87, 21);
            this.comboBoxyear.TabIndex = 2;
            this.comboBoxyear.SelectedIndexChanged += new System.EventHandler(this.comboBoxyear_SelectedIndexChanged);
            // 
            // labelcalendrier
            // 
            this.labelcalendrier.AutoSize = true;
            this.labelcalendrier.Location = new System.Drawing.Point(6, 33);
            this.labelcalendrier.Name = "labelcalendrier";
            this.labelcalendrier.Size = new System.Drawing.Size(54, 13);
            this.labelcalendrier.TabIndex = 1;
            this.labelcalendrier.Text = "Calendrier";
            this.labelcalendrier.Paint += new System.Windows.Forms.PaintEventHandler(this.labelcalendrier_Paint);
            // 
            // dataGridViewListeCalendar
            // 
            this.dataGridViewListeCalendar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridViewListeCalendar.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewListeCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewListeCalendar.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewListeCalendar.Location = new System.Drawing.Point(6, 49);
            this.dataGridViewListeCalendar.Name = "dataGridViewListeCalendar";
            this.dataGridViewListeCalendar.Size = new System.Drawing.Size(102, 132);
            this.dataGridViewListeCalendar.TabIndex = 0;
            // 
            // panelmonnaie
            // 
            this.panelmonnaie.BackColor = System.Drawing.Color.Transparent;
            this.panelmonnaie.Controls.Add(this.buttonAutremonnaie);
            this.panelmonnaie.Controls.Add(this.textBoxCurrency);
            this.panelmonnaie.Location = new System.Drawing.Point(12, 39);
            this.panelmonnaie.Name = "panelmonnaie";
            this.panelmonnaie.Size = new System.Drawing.Size(95, 81);
            this.panelmonnaie.TabIndex = 0;
            this.panelmonnaie.Paint += new System.Windows.Forms.PaintEventHandler(this.panelmonnaie_Paint);
            // 
            // buttonAutremonnaie
            // 
            this.buttonAutremonnaie.Enabled = false;
            this.buttonAutremonnaie.Font = new System.Drawing.Font("Corbel", 8.5F);
            this.buttonAutremonnaie.Location = new System.Drawing.Point(44, 55);
            this.buttonAutremonnaie.Name = "buttonAutremonnaie";
            this.buttonAutremonnaie.Size = new System.Drawing.Size(48, 23);
            this.buttonAutremonnaie.TabIndex = 13;
            this.buttonAutremonnaie.Text = "Other";
            this.buttonAutremonnaie.UseVisualStyleBackColor = true;
            // 
            // textBoxCurrency
            // 
            this.textBoxCurrency.Font = new System.Drawing.Font("Corbel", 9F);
            this.textBoxCurrency.Location = new System.Drawing.Point(3, 27);
            this.textBoxCurrency.Name = "textBoxCurrency";
            this.textBoxCurrency.Size = new System.Drawing.Size(89, 22);
            this.textBoxCurrency.TabIndex = 14;
            // 
            // Results
            // 
            this.Results.AutoSize = true;
            this.Results.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(226)))), ((int)(((byte)(165)))));
            this.Results.Font = new System.Drawing.Font("Corbel", 12F);
            this.Results.Location = new System.Drawing.Point(4, 3);
            this.Results.Name = "Results";
            this.Results.Size = new System.Drawing.Size(50, 19);
            this.Results.TabIndex = 3;
            this.Results.Text = "Result";
            // 
            // panelGraphique
            // 
            this.panelGraphique.BackColor = System.Drawing.Color.Transparent;
            this.panelGraphique.Controls.Add(this.dataGridView2);
            this.panelGraphique.Font = new System.Drawing.Font("Corbel", 10F);
            this.panelGraphique.Location = new System.Drawing.Point(320, 39);
            this.panelGraphique.Name = "panelGraphique";
            this.panelGraphique.Size = new System.Drawing.Size(196, 304);
            this.panelGraphique.TabIndex = 2;
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Corbel", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.Location = new System.Drawing.Point(3, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(190, 295);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // panelStandard
            // 
            this.panelStandard.Controls.Add(this.dataGridViewStd);
            this.panelStandard.Font = new System.Drawing.Font("Corbel", 10F);
            this.panelStandard.Location = new System.Drawing.Point(113, 39);
            this.panelStandard.Name = "panelStandard";
            this.panelStandard.Size = new System.Drawing.Size(190, 375);
            this.panelStandard.TabIndex = 2;
            // 
            // dataGridViewStd
            // 
            this.dataGridViewStd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridViewStd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewStd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Corbel", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewStd.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewStd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewStd.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewStd.Name = "dataGridViewStd";
            this.dataGridViewStd.Size = new System.Drawing.Size(190, 375);
            this.dataGridViewStd.TabIndex = 0;
            this.dataGridViewStd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStd_CellClick);
            // 
            // panelbouton
            // 
            this.panelbouton.BackColor = System.Drawing.Color.Transparent;
            this.panelbouton.Controls.Add(this.buttonCalendar);
            this.panelbouton.Controls.Add(this.buttonDico);
            this.panelbouton.Controls.Add(this.buttonHelp);
            this.panelbouton.Controls.Add(this.buttonExport);
            this.panelbouton.Controls.Add(this.buttonGraphique);
            this.panelbouton.Controls.Add(this.buttonStandard);
            this.panelbouton.Font = new System.Drawing.Font("Corbel", 10F);
            this.panelbouton.Location = new System.Drawing.Point(113, 3);
            this.panelbouton.Name = "panelbouton";
            this.panelbouton.Size = new System.Drawing.Size(678, 30);
            this.panelbouton.TabIndex = 1;
            // 
            // buttonCalendar
            // 
            this.buttonCalendar.Location = new System.Drawing.Point(165, 3);
            this.buttonCalendar.Name = "buttonCalendar";
            this.buttonCalendar.Size = new System.Drawing.Size(75, 23);
            this.buttonCalendar.TabIndex = 6;
            this.buttonCalendar.Text = "Travail";
            this.buttonCalendar.UseVisualStyleBackColor = true;
            this.buttonCalendar.Click += new System.EventHandler(this.buttonCalendar_Click);
            // 
            // buttonDico
            // 
            this.buttonDico.Location = new System.Drawing.Point(526, 3);
            this.buttonDico.Name = "buttonDico";
            this.buttonDico.Size = new System.Drawing.Size(75, 23);
            this.buttonDico.TabIndex = 5;
            this.buttonDico.Text = "Dico";
            this.buttonDico.UseVisualStyleBackColor = true;
            this.buttonDico.Visible = false;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(603, 3);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Visible = false;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(349, 3);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 3;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Visible = false;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonGraphique
            // 
            this.buttonGraphique.Location = new System.Drawing.Point(84, 3);
            this.buttonGraphique.Name = "buttonGraphique";
            this.buttonGraphique.Size = new System.Drawing.Size(75, 23);
            this.buttonGraphique.TabIndex = 1;
            this.buttonGraphique.Text = "Graphique";
            this.buttonGraphique.UseVisualStyleBackColor = true;
            // 
            // buttonStandard
            // 
            this.buttonStandard.Location = new System.Drawing.Point(3, 3);
            this.buttonStandard.Name = "buttonStandard";
            this.buttonStandard.Size = new System.Drawing.Size(75, 23);
            this.buttonStandard.TabIndex = 0;
            this.buttonStandard.Text = "Standard";
            this.buttonStandard.UseVisualStyleBackColor = true;
            // 
            // UserControlResultat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UserControlResultat";
            this.Size = new System.Drawing.Size(1887, 433);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCalendars.ResumeLayout(false);
            this.panelGridCalendar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewValues)).EndInit();
            this.panelgraphecalendar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxcalendrier.ResumeLayout(false);
            this.groupBoxcalendrier.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListeCalendar)).EndInit();
            this.panelmonnaie.ResumeLayout(false);
            this.panelmonnaie.PerformLayout();
            this.panelGraphique.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panelStandard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStd)).EndInit();
            this.panelbouton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelStandard;
        private System.Windows.Forms.DataGridView dataGridViewStd;
        private System.Windows.Forms.Panel panelbouton;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonGraphique;
        private System.Windows.Forms.Button buttonStandard;
        private System.Windows.Forms.Panel panelmonnaie;
        private System.Windows.Forms.Label Results;
        private System.Windows.Forms.Button buttonDico;
        private System.Windows.Forms.TextBox textBoxCurrency;
        private System.Windows.Forms.Button buttonAutremonnaie;
        private System.Windows.Forms.Panel panelCalendars;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Button buttonValues;
        private System.Windows.Forms.Button buttongraphe;
        private System.Windows.Forms.GroupBox groupBoxcalendrier;
        private System.Windows.Forms.TextBox textBoxnbworker;
        private System.Windows.Forms.Label labelemployee;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.ComboBox comboBoxyear;
        private System.Windows.Forms.Label labelcalendrier;
        private System.Windows.Forms.DataGridView dataGridViewListeCalendar;
        private System.Windows.Forms.Button buttonCalendar;
        private System.Windows.Forms.Panel panelGridCalendar;
        private System.Windows.Forms.DataGridView dataGridViewValues;
        private System.Windows.Forms.Panel panelgraphecalendar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panelGraphique;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button buttonBack;
    }
}
