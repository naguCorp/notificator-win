using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace VolnovNotificator
{
    public partial class CreditsForm : Form
    {
        public CreditsForm()
        {
            InitializeComponent();       
        }

        private void volnovMailLb_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:volnov@prankota.com");
        }

        private void CreditsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void CreditsForm_VisibleChanged(object sender, EventArgs e)
        {
            if(Visible == true)
                Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void CreditsForm_Load(object sender, EventArgs e)
        {
            appVersionLaber.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}