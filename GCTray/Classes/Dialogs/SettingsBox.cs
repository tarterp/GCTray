using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace GCTray
{
    public partial class SettingsBox : Form
    {
        public SettingsBox()
        {
            InitializeComponent();
            tbProfileDirectory.Text = Properties.Settings.Default.AthleteLibrary;
            tbUserName.Text = Properties.Settings.Default.UserName;
            cbAutorun.Checked = Properties.Settings.Default.Autorun;
        }

        public void SetAthleteLibrary()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Choose folder that contains profiles you would like to monitor";
            folderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowser.SelectedPath = tbProfileDirectory.Text;
            folderBrowser.ShowNewFolderButton = true;

            if (folderBrowser.ShowDialog(new Form()) == DialogResult.OK)
            {
                tbProfileDirectory.Text = folderBrowser.SelectedPath;
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            SetAthleteLibrary();
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AthleteLibrary = tbProfileDirectory.Text;
            Properties.Settings.Default.UserName = tbUserName.Text;
            if(cbAutorun.Checked == true &&
               Properties.Settings.Default.Autorun == false)
            {
                CreateAutorun();
                Properties.Settings.Default.Autorun = true;
            }
            else if(cbAutorun.Checked == false &&
                    Properties.Settings.Default.Autorun == true)
            {
                DeleteAutoRun();
                Properties.Settings.Default.Autorun = false;
            }
            Properties.Settings.Default.Save();
            this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CreateAutorun()
        {
            string pname = Process.GetCurrentProcess().ProcessName;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string fullpath = path + pname + ".exe";

            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")); //Windows Script Host Shell Object
            dynamic shell = Activator.CreateInstance(t);
            try
            {
                var lnk = shell.CreateShortcut("GCTray.lnk");
                string gcTrayLinkName = "\\GCTray.lnk";
                string linkFullPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + gcTrayLinkName;

                if(File.Exists(linkFullPath))
                {
                    File.Delete(linkFullPath);
                }

                try
                {
                    lnk.TargetPath = fullpath;
                    lnk.Save();
                    File.Move("GCTray.lnk", linkFullPath);
                }
                finally
                {
                    Marshal.FinalReleaseComObject(lnk);
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(shell);

            }
        }

        private void DeleteAutoRun()
        {
            string gcTrayLinkName = "\\GCTray.lnk";
            string linkFullPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + gcTrayLinkName;

            if (File.Exists(linkFullPath))
            {
                File.Delete(linkFullPath);
            }
        }




    }
}
