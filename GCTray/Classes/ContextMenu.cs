using System;
using System.Diagnostics;
using System.Windows.Forms;
using GCTray.Properties;
using System.Collections.Generic;

namespace GCTray 
{
    class ContextMenu : Form
    {
        private bool                isAboutLoaded = false;
        private List<Profile>       profiles;
        private ContextMenuStrip    menu;
        private ToolStripMenuItem   aboutItem;
        private ToolStripMenuItem   exitItem;
        private ToolStripMenuItem   configItem;

        public ContextMenu()
        {
   
            CreateHandle();
                        
            menu = new ContextMenuStrip();

            // Index 0 Title
            menu.Items.Add(new ToolStripStatusLabel("Golden Cheetah Sync"));

            // Index 1 Separator
            menu.Items.Add(new ToolStripSeparator());

            // Index 2+n Users
            PopulateProfiles();

            // Separator
            menu.Items.Add(new ToolStripSeparator());

            // Configure
            configItem = new ToolStripMenuItem("Settings");
            configItem.Click += new EventHandler(Config_Click);
            configItem.Image = Resources.Preferences;
            menu.Items.Add(configItem);

            // About.
            aboutItem = new ToolStripMenuItem("About");
            aboutItem.Click += new EventHandler(About_Click);
            aboutItem.Image = Resources.About;
            menu.Items.Add(aboutItem);

            // Exit.
            exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += new System.EventHandler(Exit_Click);
            exitItem.Image = Resources.Exit;
            menu.Items.Add(exitItem);

        }

        private void ClearProfileList()
        {
            for(int i = 0; i < profiles.Count; i++)
            {
                menu.Items.RemoveAt(2);
            }
        }

        private void PopulateProfiles()
        {
            profiles = Profile.GetLocalProfileList();
            foreach (Profile p in profiles)
            {
                p.ProfileChanged += ProfileChanged;
                ToolStripMenuItem item = new ToolStripMenuItem(p.name);
                item.Enabled = p.enabled;
                item.Name = p.name;
                item.Click += new EventHandler(LaunchProfile);
                menu.Items.Insert(2, item);
            }
        }

        private void LaunchProfile(object sender, EventArgs e)
        {
            Profile profile = profiles.Find(p => p.name == sender.ToString());
            profile.Launch();
        }

        internal void UpdateProfileList()
        {
            ClearProfileList();
            PopulateProfiles();
        }

        public new ContextMenuStrip Menu()
        {
            return menu;
        }

        private void Config_Click(object sender, EventArgs e)
        {
            SettingsBox sb = new SettingsBox();
            sb.ShowDialog();
            UpdateProfileList();
            sb.Dispose();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void About_Click(object sender, EventArgs e)
        {
            if (!isAboutLoaded)
            {
                isAboutLoaded = true;
                new AboutBox().ShowDialog();
                isAboutLoaded = false;
            }
        }

        delegate void DelegateSetMenuEnable(ToolStripMenuItem menu, bool enabled);
        void SetMenuEnable(ToolStripItem item, bool enabled)
        {
            item.Enabled = enabled;
        }

        public void ProfileChanged(object sender, ProfileChangedEvent e)
        {
            Profile p = sender as Profile;
            ToolStripItem[] items = menu.Items.Find(p.name, false);
            ToolStripMenuItem item = (ToolStripMenuItem)items[0];
            
            if (this.menu.InvokeRequired)
            {
                DelegateSetMenuEnable d = new DelegateSetMenuEnable(SetMenuEnable);
                this.Invoke(d, new object[] {item, p.enabled});
            }
            else
            {        
                SetMenuEnable(item, p.enabled);
            }
            
        }

    }
}
