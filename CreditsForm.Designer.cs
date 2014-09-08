namespace VolnovNotificator
{
    partial class CreditsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.volnovMailLb = new System.Windows.Forms.LinkLabel();
            this.appVersionLaber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Prankota Notificator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Будьте всегда в курсе обновлений Пранкоты.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Вопросы и пожелания на";
            // 
            // volnovMailLb
            // 
            this.volnovMailLb.AutoSize = true;
            this.volnovMailLb.Location = new System.Drawing.Point(153, 55);
            this.volnovMailLb.Name = "volnovMailLb";
            this.volnovMailLb.Size = new System.Drawing.Size(115, 13);
            this.volnovMailLb.TabIndex = 3;
            this.volnovMailLb.TabStop = true;
            this.volnovMailLb.Text = "volnov@prankota.com";
            this.volnovMailLb.Click += new System.EventHandler(this.volnovMailLb_Click);
            // 
            // appVersionLaber
            // 
            this.appVersionLaber.AutoSize = true;
            this.appVersionLaber.Location = new System.Drawing.Point(119, 9);
            this.appVersionLaber.Name = "appVersionLaber";
            this.appVersionLaber.Size = new System.Drawing.Size(0, 13);
            this.appVersionLaber.TabIndex = 4;
            // 
            // CreditsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 79);
            this.Controls.Add(this.appVersionLaber);
            this.Controls.Add(this.volnovMailLb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreditsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prankota Notificator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreditsForm_FormClosing);
            this.Load += new System.EventHandler(this.CreditsForm_Load);
            this.VisibleChanged += new System.EventHandler(this.CreditsForm_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel volnovMailLb;
        private System.Windows.Forms.Label appVersionLaber;
    }
}