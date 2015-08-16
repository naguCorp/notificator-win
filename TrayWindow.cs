using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace VolnovNotificator
{
    public partial class TrayWindow : Form
    {
        private readonly CreditsForm _crefitsForm = new CreditsForm();
        private readonly AppSettingsWrapper _appSettingsWrapper = new AppSettingsWrapper("settings.cnf");
        private KeyValuePair<string, string> _lastMessagesValuePair;
        const string RegistryKeyName = "PrankotaNf";


        public TrayWindow()
        {
            InitializeComponent();
            CheckNewAppVersion();
            if (File.Exists("settings.cnf"))
                LoadAppSettings();
            else
                FirstLoadApp();
        }

        private void FirstLoadApp()
        {
            _appSettingsWrapper.EditRecord("sound", "off");
            _appSettingsWrapper.EditRecord("autorun", SetAppAutorunValue(true) ? "on":"off");

            firstLoadAppBw.RunWorkerAsync();
        }

        private void LoadAppSettings()
        {
            var settingsValues = _appSettingsWrapper.GetAllConfigData();
            foreach(var keyValueSetting in settingsValues)
            {
                switch(keyValueSetting.Key)
                {
                    case "sound": soundNotifyToolStripMenuItem.Checked = keyValueSetting.Value == "on";
                        break;
                    case "autorun":
                        if (keyValueSetting.Value == "on")
                            autorunToolStripMenuItem.Checked = CheckAutorun() ? true : SetAppAutorunValue(true);
                        else
                            autorunToolStripMenuItem.Checked = false;
                        break;
                }
            }
            backgroundWorker1.RunWorkerAsync();
            messagesReceiveTimer.Enabled = true;
            messagesReceiveTimer.Start();
        }

        private void CheckNewAppVersion()
        {
            var currentAppVersion = Convert.ToDouble(Assembly.GetExecutingAssembly().GetName().Version.ToString().Replace(".", ""));

            try
            {
                double siteAppVersion;
                using (var webClient = new WebClient())
                    siteAppVersion =
                        Convert.ToDouble(webClient.DownloadString("http://prankota.com/version.txt").Replace(".", ""));


                if (!(currentAppVersion < siteAppVersion)) return;

                var result =
                    MessageBox.Show(@"Обнаружена новая версия приложения Prankota Notificator! Обновить приложение?",
                        @"Prankota Notificator",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;

                try
                {
                    using (var webClient = new WebClient())
                        webClient.DownloadFile("http://prankota.com/notificator.exe", "notificator_new.exe");
                    MessageBox.Show(@"Готово, обновленная версия приложения находится рядом с запущенным файлом.");
                }
                catch
                {
                    MessageBox.Show(@"Ошибка обновления приложения!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Autorun
        private bool SetAppAutorunValue(bool autorun)
        {
            
            var exePath = Application.ExecutablePath;
            var regKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            if (regKey == null) return false;
            try
            {
                if (autorun)
                    regKey.SetValue(RegistryKeyName, exePath);
                else
                {
                    if (regKey.GetValue(RegistryKeyName) != null)
                        regKey.DeleteValue(RegistryKeyName);
                }

                regKey.Close();
            }
            catch
            {
                 MessageBox.Show(
                     autorun ? 
                     "Ошибка добавления программы в автозагрузку! Возможно, программе не хватает прав." : 
                     "Ошибка при удалении программы из автозагрузки! Возможно, программе не хватает прав.",
                     @"Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool CheckAutorun()
        {
            var regKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            if (regKey == null) return false;
            var allKeys = regKey.GetValueNames();
            var inAutorun = false;
            foreach (var key in allKeys)
            {
                if (key == RegistryKeyName)
                {
                    if (regKey.GetValue(RegistryKeyName).ToString() == Application.ExecutablePath)
                        inAutorun = true;
                }
                else
                    inAutorun = false;
            }
            return inAutorun;
        }
        #endregion

        #region GUI
        private void notifyIcon1_BalloonTipShown(object sender, EventArgs e)
        {
            if (!soundNotifyToolStripMenuItem.Checked) return;

            var player = new SoundPlayer(Resource1.lolNotifySound1);
            player.Play();
        }

        private void soundNotifyToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            _appSettingsWrapper.EditRecord("sound", soundNotifyToolStripMenuItem.Checked ? "on": "off");
        }

        private void autorunToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (autorunToolStripMenuItem.Checked)
            {
                if (!SetAppAutorunValue(true))
                    autorunToolStripMenuItem.Checked = true;
                _appSettingsWrapper.EditRecord("autorun", "on");
            }
            else
            {
                if (!SetAppAutorunValue(false))
                    autorunToolStripMenuItem.Checked = false;
                _appSettingsWrapper.EditRecord("autorun", "off");
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _crefitsForm.Show();
        }
        #endregion

        string _link;
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_link)) return;

            Process.Start(_link);
            _link = null;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                var newMessageValuePair = MessagesReceive.GetLastMessages();

                if (newMessageValuePair.Key == _lastMessagesValuePair.Key) return;

                _lastMessagesValuePair = newMessageValuePair;
                if (!string.IsNullOrEmpty(newMessageValuePair.Value))
                    _link = newMessageValuePair.Value;
                notifyIcon1.BalloonTipTitle = @"Евгений Вольнов";
                notifyIcon1.BalloonTipText = newMessageValuePair.Key;
                notifyIcon1.ShowBalloonTip(10000);
                Thread.Sleep(10000);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,
                                @"Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void messagesReceiveTimer_Tick(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void ShowBalloonMessage(string text, int timeout)
        {
            notifyIcon1.BalloonTipTitle = @"Евгений Вольнов";
            notifyIcon1.BalloonTipText = text;
            notifyIcon1.ShowBalloonTip(timeout);
        }

        private void firstLoadAppBw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ShowBalloonMessage("Вот так работают уведомления в Prankota Notificator.", 10000);
            Thread.Sleep(10000);
            soundNotifyToolStripMenuItem.Checked = true;
            ShowBalloonMessage("А так работает уведомление со звуковым сигналом.", 10000);
            soundNotifyToolStripMenuItem.Checked = false;
            Thread.Sleep(10000);
            ShowBalloonMessage("Как только появится новый пранк Prankota Notificator уведомит тебя.", 10000);
            Thread.Sleep(10000);
            _link = "http://prankota.com/prankota-notificator";
            ShowBalloonMessage("Для перехода к новости просто нажми на уведомление.", 10000);
            Thread.Sleep(10000);
        }

        private void firstLoadAppBw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            messagesReceiveTimer.Enabled = true;
            messagesReceiveTimer.Start();
        }
    }
}