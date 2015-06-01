using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GCTray
{
    class LockWatcher : FileSystemWatcher
    {
        private Profile p;

        public LockWatcher()
        {
            Deleted += OnDeleted;
            Created += OnCreated;
        }

        public LockWatcher(Profile p)
        {
            Created += OnCreated;
            Deleted += OnDeleted;
            this.p = p;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            p.Disable(false);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            
            p.Enable(false);
        }


    }
}
