namespace VolnovNotificator
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyCm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.soundNotifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autorunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesReceiveTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.firstLoadAppBw = new System.ComponentModel.BackgroundWorker();
            this.notifyCm.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.notifyCm;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.BalloonTipShown += new System.EventHandler(this.notifyIcon1_BalloonTipShown);
            // 
            // notifyCm
            // 
            this.notifyCm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundNotifyToolStripMenuItem,
            this.autorunToolStripMenuItem,
            this.toolStripMenuItem1,
            this.creditsToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.notifyCm.Name = "notifyCm";
            this.notifyCm.Size = new System.Drawing.Size(209, 98);
            // 
            // soundNotifyToolStripMenuItem
            // 
            this.soundNotifyToolStripMenuItem.CheckOnClick = true;
            this.soundNotifyToolStripMenuItem.Name = "soundNotifyToolStripMenuItem";
            this.soundNotifyToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.soundNotifyToolStripMenuItem.Text = "Звуковые уведомления";
            this.soundNotifyToolStripMenuItem.CheckedChanged += new System.EventHandler(this.soundNotifyToolStripMenuItem_CheckedChanged);
            // 
            // autorunToolStripMenuItem
            // 
            this.autorunToolStripMenuItem.Checked = true;
            this.autorunToolStripMenuItem.CheckOnClick = true;
            this.autorunToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autorunToolStripMenuItem.Name = "autorunToolStripMenuItem";
            this.autorunToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.autorunToolStripMenuItem.Text = "Добавить в автозагрузку";
            this.autorunToolStripMenuItem.CheckedChanged += new System.EventHandler(this.autorunToolStripMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.creditsToolStripMenuItem.Text = "О программе";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(208, 22);
            this.exitToolStripMenuItem1.Text = "Выход";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // messagesReceiveTimer
            // 
            this.messagesReceiveTimer.Interval = 1800000;
            this.messagesReceiveTimer.Tick += new System.EventHandler(this.messagesReceiveTimer_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // firstLoadAppBw
            // 
            this.firstLoadAppBw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.firstLoadAppBw_DoWork);
            this.firstLoadAppBw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.firstLoadAppBw_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 77);
            this.Name = "Form1";
            this.Text = "Form1";
            this.notifyCm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip notifyCm;
        private System.Windows.Forms.ToolStripMenuItem soundNotifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autorunToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.Timer messagesReceiveTimer;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker firstLoadAppBw;
    }
}