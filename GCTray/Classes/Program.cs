using System;
using System.Windows.Forms;
using GCTray.Properties;
using System.Diagnostics;

namespace GCTray
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.AthleteLibrary == "" ||
                Properties.Settings.Default.AthleteLibrary == null)
            {
                Properties.Settings.Default.AthleteLibrary = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\GoldenCheetah";
            }

            if(Properties.Settings.Default.UserName == "" ||
               Properties.Settings.Default.UserName == null )
            {
                Properties.Settings.Default.UserName = Environment.UserDomainName + "." + Environment.UserName;
            }

            // Make sure no GC Instances Are Running
            Process[] gc;
            do
            {
                gc = Process.GetProcessesByName("GoldenCheetah");
                if (gc.Length != 0)
                {
                    DialogResult result = MessageBox.Show("Please Close All Instances of GoldenCheetah", "GCTask", System.Windows.Forms.MessageBoxButtons.OKCancel);
                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                }
            } while (gc.Length != 0);

            using(GCIcon gi = new GCIcon())
            {
                gi.Display();
                Application.Run();
            }
        }
    }
}
