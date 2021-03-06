﻿using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace GCTray
{
    class Profile
    {
        public EventHandler<ProfileChangedEvent> ProfileChanged;
        private LockWatcher lockFileWatcher;
        public string name {  get;  set; }
        public string athleteFullPath { get; set; }
        public string lockfile { get; set; }
        public bool enabled {  get; private set; }
        public string lockedBy;
        public string path { get; set; }
        public int pid;

        public Profile()
        {

        }

        ~Profile()
        {
            if (this.pid != 0)
            {
                // Could throw exception if PID no longer exists
                // This shouldn't be a case
                Process p = Process.GetProcessById(this.pid);
                File.Delete(this.lockfile);
                p.CloseMainWindow();
            }
        }

        public Profile(string name)
        {
            this.name = name;
        }

        public Profile(string name, bool enabled)
        {
            this.name = name;
            this.enabled = enabled;
        }

        static public List<Profile> GetLocalProfileList()
        {
            List<Profile> profiles = new List<Profile>();
            if (Properties.Settings.Default.AthleteLibrary == null)
                return null;

            if (Directory.Exists(Properties.Settings.Default.AthleteLibrary))
            {
                // Get Directory Array from WorkspaceDirectory
                string athleteLibrary = Properties.Settings.Default.AthleteLibrary;
                string username = Properties.Settings.Default.UserName;
                DirectoryInfo directory = new DirectoryInfo(Properties.Settings.Default.AthleteLibrary);
                DirectoryInfo[] directories = directory.GetDirectories();

                foreach (DirectoryInfo folder in directories)
                {
                    Profile p = new Profile(folder.Name);
                    p.athleteFullPath = athleteLibrary + '\\' + p.name;
                    DirectoryInfo athleteDir = new DirectoryInfo(p.athleteFullPath);
                    FileInfo[] files = {null};
                    try
                    {
                        files = athleteDir.GetFiles("*.GCSync.lck", SearchOption.TopDirectoryOnly);
                    }
                    catch(Exception e)
                    {
                        DialogResult d = MessageBox.Show(e.Message + "\r\n\r\nWould you like to continue running GCTray without this profile?", "Error Opening Profile Directory",MessageBoxButtons.YesNo);
                        if(d == DialogResult.Yes)
                        {
                            continue;
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }

                    p.lockfile = athleteLibrary + "\\" + p.name + "\\" + username + ".GCSync.lck";

                    p.lockFileWatcher = new LockWatcher(p);
                    p.lockFileWatcher.Path = athleteDir.FullName;
                    p.lockFileWatcher.Filter = "*.GCSync.lck";
                    p.lockFileWatcher.EnableRaisingEvents = true;

                    if(files.Length > 0)
                    {
                        p.Disable(false);
                        p.lockedBy = files[0].Name.Split(new char[] {'.'} )[0];
                    }
                    else
                    {
                        p.Enable(true);
                    }
                    
                    profiles.Add(p);
                }
            }
            return profiles;
        }

        public void Launch()
        {
            Process process = new Process();
            process.StartInfo.FileName = 
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) +
                "\\goldencheetah\\goldencheetah.exe";
            
            process.StartInfo.Arguments = "\"" + this.athleteFullPath + "\"";

            process.EnableRaisingEvents = true;
            process.Exited += process_Exited;

            process.Start();

            this.pid = process.Id;
            this.Disable();

            Console.WriteLine("Started pid {0} : window {1}", process.Id, process.MainWindowTitle);
        }

        private void process_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;
            this.pid = 0;
            this.Enable();

            Console.WriteLine("[{0}] Exited", p.Id);
        }

        public void Enable(bool deleteLockFile = true)
        {
            this.enabled = true;
            if (deleteLockFile == true)
            {
                File.Delete(this.lockfile);
            }
            if (ProfileChanged != null)
                ProfileChanged(this, new ProfileChangedEvent(this));
        }

        public void Disable(bool createLockFile = true)
        {
            this.enabled = false;
            if (this.lockedBy != "")
            {
                if (createLockFile)
                {
                    FileStream f = File.Create(this.lockfile);
                    f.Close();
                }
            }
            if(ProfileChanged != null)
                ProfileChanged(this, new ProfileChangedEvent(this));
        }
    }
}
