using System;
using System.Windows.Forms;

namespace VolnovNotificator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new TrayWindow();
            Application.Run();
        }
    }
}
