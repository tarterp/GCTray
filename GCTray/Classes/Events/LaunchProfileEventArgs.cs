using System;

namespace GCTray
{
    class LaunchProfileEventArgs : EventArgs
    {
        private readonly Profile ProfileObject;

        public LaunchProfileEventArgs(Profile profile)
        {
            ProfileObject = profile;
        }

        public Profile profile
        {
            get { return ProfileObject; }
        }
    }
}
