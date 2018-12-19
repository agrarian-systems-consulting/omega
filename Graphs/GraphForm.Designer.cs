namespace OMEGA.Graphs
{
    partial class GraphForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelAddExpl = new System.Windows.Forms.Panel();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.checkedListBoxExploitation = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonbars = new System.Windows.Forms.Button();
            this.buttonLine = new System.Windows.Forms.Button();
            this.buttonCurve = new System.Windows.Forms.Button();
            this.buttonPoint = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelAddExpl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(2, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chart1.Size = new System.Drawing.Size(633, 438);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panelAddExpl);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(641, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 438);
            this.panel1.TabIndex = 1;
            // 
            // panelAddExpl
            // 
            this.panelAddExpl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAddExpl.Controls.Add(this.buttonValidate);
            this.panelAddExpl.Controls.Add(this.checkedListBoxExploitation);
            this.panelAddExpl.Location = new System.Drawing.Point(3, 118);
            this.panelAddExpl.Name = "panelAddExpl";
            this.panelAddExpl.Size = new System.Drawing.Size(151, 313);
            this.panelAddExpl.TabIndex = 1;
            // 
            // buttonValidate
            // 
            this.buttonValidate.Location = new System.Drawing.Point(6, 268);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(75, 23);
            this.buttonValidate.TabIndex = 2;
            this.buttonValidate.Text = "Validate";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // checkedListBoxExploitation
            // 
            this.checkedListBoxExploitation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.checkedListBoxExploitation.FormattingEnabled = true;
            this.checkedListBoxExploitation.Location = new System.Drawing.Point(6, 3);
            this.checkedListBoxExploitation.Name = "checkedListBoxExploitation";
            this.checkedListBoxExploitation.Size = new System.Drawing.Size(139, 259);
            this.checkedListBoxExploitation.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonbars);
            this.groupBox1.Controls.Add(this.buttonLine);
            this.groupBox1.Controls.Add(this.buttonCurve);
            this.groupBox1.Controls.Add(this.buttonPoint);
            this.groupBox1.Location = new System.Drawing.Point(19, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chart Type";
            // 
            // buttonbars
            // 
            this.buttonbars.BackgroundImage = global::OMEGA.Properties.Resources.bars;
            this.buttonbars.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonbars.Location = new System.Drawing.Point(63, 59);
            this.buttonbars.Name = "buttonbars";
            this.buttonbars.Size = new System.Drawing.Size(41, 34);
            this.buttonbars.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonbars, "Column Chart");
            this.buttonbars.UseVisualStyleBackColor = true;
            this.buttonbars.Click += new System.EventHandler(this.buttonbars_Click);
            // 
            // buttonLine
            // 
            this.buttonLine.BackgroundImage = global::OMEGA.Properties.Resources.line;
            this.buttonLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonLine.Location = new System.Drawing.Point(6, 59);
            this.buttonLine.Name = "buttonLine";
            this.buttonLine.Size = new System.Drawing.Size(41, 34);
            this.buttonLine.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonLine, "Line Chart");
            this.buttonLine.UseVisualStyleBackColor = true;
            this.buttonLine.Click += new System.EventHandler(this.buttonLine_Click);
            // 
            // buttonCurve
            // 
            this.buttonCurve.BackgroundImage = global::OMEGA.Properties.Resources.curve;
            this.buttonCurve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonCurve.Location = new System.Drawing.Point(63, 19);
            this.buttonCurve.Name = "buttonCurve";
            this.buttonCurve.Size = new System.Drawing.Size(41, 34);
            this.buttonCurve.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonCurve, "Spline Chart");
            this.buttonCurve.UseVisualStyleBackColor = true;
            this.buttonCurve.Click += new System.EventHandler(this.buttonCurve_Click);
            // 
            // buttonPoint
            // 
            this.buttonPoint.BackgroundImage = global::OMEGA.Properties.Resources.point1;
            this.buttonPoint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonPoint.Location = new System.Drawing.Point(6, 19);
            this.buttonPoint.Name = "buttonPoint";
            this.buttonPoint.Size = new System.Drawing.Size(41, 34);
            this.buttonPoint.TabIndex = 0;
            this.toolTip1.SetToolTip(this.buttonPoint, "Point Chart");
            this.buttonPoint.UseVisualStyleBackColor = true;
            this.buttonPoint.Click += new System.EventHandler(this.buttonPoint_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 443);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Name = "GraphForm";
            this.Text = "GraphForm";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelAddExpl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPoint;
        private System.Windows.Forms.Button buttonCurve;
        private System.Windows.Forms.Button buttonLine;
        private System.Windows.Forms.Button buttonbars;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelAddExpl;
        private System.Windows.Forms.CheckedListBox checkedListBoxExploitation;
        private System.Windows.Forms.Button buttonValidate;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}