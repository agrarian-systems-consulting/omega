namespace OMEGA.Forms
{
    partial class ExitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExitForm));
            this.buttonsavequit = new System.Windows.Forms.Button();
            this.buttonquitnosave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonsavequit
            // 
            this.buttonsavequit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonsavequit.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonsavequit.Location = new System.Drawing.Point(19, 37);
            this.buttonsavequit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonsavequit.Name = "buttonsavequit";
            this.buttonsavequit.Size = new System.Drawing.Size(92, 68);
            this.buttonsavequit.TabIndex = 0;
            this.buttonsavequit.Text = "Sauvegarder et quitter";
            this.buttonsavequit.UseVisualStyleBackColor = false;
            this.buttonsavequit.Click += new System.EventHandler(this.buttonsavequit_Click);
            // 
            // buttonquitnosave
            // 
            this.buttonquitnosave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonquitnosave.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonquitnosave.Location = new System.Drawing.Point(128, 37);
            this.buttonquitnosave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonquitnosave.Name = "buttonquitnosave";
            this.buttonquitnosave.Size = new System.Drawing.Size(89, 68);
            this.buttonquitnosave.TabIndex = 1;
            this.buttonquitnosave.Text = "Quitter sans sauvegarder";
            this.buttonquitnosave.UseVisualStyleBackColor = false;
            this.buttonquitnosave.Click += new System.EventHandler(this.buttonquitnosave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonCancel.Font = new System.Drawing.Font("Corbel", 10F);
            this.buttonCancel.Location = new System.Drawing.Point(232, 37);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 68);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 11F);
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Attention, des données ont été modifiées. Que faire ?";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ExitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(356, 116);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonquitnosave);
            this.Controls.Add(this.buttonsavequit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(372, 155);
            this.MinimumSize = new System.Drawing.Size(372, 155);
            this.Name = "ExitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExitForm";
            this.Load += new System.EventHandler(this.ExitForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonsavequit;
        private System.Windows.Forms.Button buttonquitnosave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
    }
}