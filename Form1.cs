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
    public partial class Form1 : Form
    {
        private CreditsForm _crefitsForm = new CreditsForm();
        private AppSettingsWrapper _appSettingsWrapper = new AppSettingsWrapper("settings.cnf");
        KeyValuePair<string, string> _lastMessagesValuePair;

        public Form1()
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
            if (SetAppAutorunValue(true))
                _appSettingsWrapper.EditRecord("autorun", "on");
            else
                _appSettingsWrapper.EditRecord("autorun", "off");
            firstLoadAppBw.RunWorkerAsync();
        }

        private void LoadAppSettings()
        {
            var settingsValues = _appSettingsWrapper.GetAllConfigData();
            foreach(KeyValuePair<string, string> keyValueSetting in settingsValues)
            {
                switch(keyValueSetting.Key)
                {
                    case "sound":
                        if (keyValueSetting.Value == "on")
                            soundNotifyToolStripMenuItem.Checked = true;
                        else
                            soundNotifyToolStripMenuItem.Checked = false;
                        break;
                    case "autorun":
                        if (keyValueSetting.Value == "on")
                        {
                            if(CheckAutorun())
                            autorunToolStripMenuItem.Checked = true;
                            else
                            {
                                if(SetAppAutorunValue(true))
                                    autorunToolStripMenuItem.Checked = true;
                                else
                                    autorunToolStripMenuItem.Checked = false;
                            }
                        }
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
            double siteAppVersion;

            try
            {
                using (var webClient = new WebClient())
                    siteAppVersion = Convert.ToDouble(webClient.DownloadString("http://prankota.com/version.txt").Replace(".", ""));


                if (currentAppVersion < siteAppVersion)
                {
                    var result = MessageBox.Show("Обнаружена новая версия приложения Prankota Notificator! Обновить приложение?", "Prankota Notificator",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (var webClient = new WebClient())
                                webClient.DownloadFile("http://prankota.com/notificator.exe", "notificator_new.exe");
                            MessageBox.Show("Готово, обновленная версия приложения находится рядом с запущенным файлом.");
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка обновления приложения!");
                        }

                    }
                }
            }
            catch { }
        }

        #region Autorun
        private bool SetAppAutorunValue(bool autorun)
        {
            const string registryKeyName = "PrankotaNf";
            string exePath = Application.ExecutablePath;
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    regKey.SetValue(registryKeyName, exePath);
                else
                {
                    if (regKey.GetValue(registryKeyName) != null)
                        regKey.DeleteValue(registryKeyName);
                }

                regKey.Close();
            }
            catch
            {
                if (autorun)
                    MessageBox.Show("Ошибка добавления программы в автозагрузку! Возможно, программе не хватает прав.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Ошибка при удалении программы из автозагрузки! Возможно, программе не хватает прав.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool CheckAutorun()
        {
            const string registryKeyName = "PrankotaNf";
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            string[] allKeys = regKey.GetValueNames();
            bool inAutorun = false;
            foreach (string key in allKeys)
            {
                if (key == registryKeyName)
                {
                    if (regKey.GetValue(registryKeyName).ToString() == Application.ExecutablePath)
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
            if (soundNotifyToolStripMenuItem.Checked)
            {
                SoundPlayer player = new System.Media.SoundPlayer(Resource1.lolNotifySound1);
                player.Play();
            }
        }

        private void soundNotifyToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (soundNotifyToolStripMenuItem.Checked)
                _appSettingsWrapper.EditRecord("sound", "on");
            else
                _appSettingsWrapper.EditRecord("sound", "off");
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

        string link;
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(link))
            {
                Process.Start(link);
                link = null;
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                var newMessageValuePair = MessagesReceive.GetLastMessages();
                
                if(newMessageValuePair.Key != _lastMessagesValuePair.Key)
                {
                    _lastMessagesValuePair = newMessageValuePair;
                    if (!string.IsNullOrEmpty(newMessageValuePair.Value))
                        link = newMessageValuePair.Value;
                    notifyIcon1.BalloonTipTitle = "Евгений Вольнов";
                    notifyIcon1.BalloonTipText = newMessageValuePair.Key;
                    notifyIcon1.ShowBalloonTip(10000);
                    Thread.Sleep(10000);
                }
            }
            catch 
            { 

            }
        }

        private void messagesReceiveTimer_Tick(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void ShowBalloonMessage(string text, int timeout)
        {
            notifyIcon1.BalloonTipTitle = "Евгений Вольнов";
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
            link = "http://prankota.com/prankota-notificator";
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