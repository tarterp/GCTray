using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using GCTray.Properties;
using System.Reflection;

namespace GCTray
{
    class GCIcon : IDisposable
    {
        NotifyIcon ni;
        ContextMenu cm;
        
        public GCIcon()
        {
            ni = new NotifyIcon();
            cm = new ContextMenu();
        }

        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately.
            ni.Dispose();
        }

        public void Display()
        {
            ni.MouseClick += new MouseEventHandler(niMouseUp);

			ni.Icon = Resources.gc;
			ni.Text = "Golden Cheetah Syncronization Tray App";

            // Attach a context menu.
            ni.ContextMenuStrip = cm.Menu();
            
            // NotifyIcon is now active in tray
            // Baloons can't be seen until it is visible
            ni.Visible = true;

        }

        private void niMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(ni, null);
            }
        }

    }
}
