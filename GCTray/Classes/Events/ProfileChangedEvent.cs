using System;

namespace GCTray
{
    class ProfileChangedEvent : EventArgs
    {
        private readonly Profile ProfileObject;

        public ProfileChangedEvent(Profile profile)
        {
            ProfileObject = profile;
        }

        public Profile profile
        {
            get { return ProfileObject; }
        }
    }
}
