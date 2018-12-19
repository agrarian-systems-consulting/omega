namespace OMEGA.Forms.Common_forms
{
    partial class InfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxCirad = new System.Windows.Forms.PictureBox();
            this.pictureBoxINRA = new System.Windows.Forms.PictureBox();
            this.pictureBoxAgroGem = new System.Windows.Forms.PictureBox();
            this.pictureBoxSupAgro = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelDev = new System.Windows.Forms.Label();
            this.labelNoVers = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelOmega = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCirad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxINRA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAgroGem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSupAgro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.pictureBoxCirad);
            this.panel1.Controls.Add(this.pictureBoxINRA);
            this.panel1.Controls.Add(this.pictureBoxAgroGem);
            this.panel1.Controls.Add(this.pictureBoxSupAgro);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.labelDev);
            this.panel1.Controls.Add(this.labelNoVers);
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Controls.Add(this.labelOmega);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pictureBoxCirad
            // 
            resources.ApplyResources(this.pictureBoxCirad, "pictureBoxCirad");
            this.pictureBoxCirad.Name = "pictureBoxCirad";
            this.pictureBoxCirad.TabStop = false;
            // 
            // pictureBoxINRA
            // 
            resources.ApplyResources(this.pictureBoxINRA, "pictureBoxINRA");
            this.pictureBoxINRA.Name = "pictureBoxINRA";
            this.pictureBoxINRA.TabStop = false;
            // 
            // pictureBoxAgroGem
            // 
            resources.ApplyResources(this.pictureBoxAgroGem, "pictureBoxAgroGem");
            this.pictureBoxAgroGem.Name = "pictureBoxAgroGem";
            this.pictureBoxAgroGem.TabStop = false;
            // 
            // pictureBoxSupAgro
            // 
            resources.ApplyResources(this.pictureBoxSupAgro, "pictureBoxSupAgro");
            this.pictureBoxSupAgro.Name = "pictureBoxSupAgro";
            this.pictureBoxSupAgro.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // labelDev
            // 
            resources.ApplyResources(this.labelDev, "labelDev");
            this.labelDev.Name = "labelDev";
            this.labelDev.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelNoVers
            // 
            resources.ApplyResources(this.labelNoVers, "labelNoVers");
            this.labelNoVers.Name = "labelNoVers";
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.Name = "labelVersion";
            // 
            // labelOmega
            // 
            resources.ApplyResources(this.labelOmega, "labelOmega");
            this.labelOmega.ForeColor = System.Drawing.Color.Green;
            this.labelOmega.Name = "labelOmega";
            // 
            // InfoForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoForm";
            this.Load += new System.EventHandler(this.InfoForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCirad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxINRA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAgroGem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSupAgro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelNoVers;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelOmega;
        private System.Windows.Forms.Label labelDev;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxCirad;
        private System.Windows.Forms.PictureBox pictureBoxINRA;
        private System.Windows.Forms.PictureBox pictureBoxAgroGem;
        private System.Windows.Forms.PictureBox pictureBoxSupAgro;
    }
}