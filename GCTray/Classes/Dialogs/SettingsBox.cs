using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCTray
{
    public partial class SettingsBox : Form
    {
        public SettingsBox()
        {
            InitializeComponent();
            tbProfileDirectory.Text = Properties.Settings.Default.AthleteLibrary;
            tbUserName.Text = Properties.Settings.Default.UserName;
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
            tbProfileDirectory.Text = Properties.Settings.Default.AthleteLibrary;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AthleteLibrary = tbProfileDirectory.Text;
            Properties.Settings.Default.UserName = tbUserName.Text;
            Properties.Settings.Default.Save();
            this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
