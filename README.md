# GCTray
Share and Sync GoldenCheetah with friends

# Description
GCTray came from some friends who wanted to share their GoldenCheetah database and even be able to make changes in each others profiles remotely. GoogleDrive allowed up to put our profiles on a cloud storage for others to load, but then we started to notice the issue of database corruption; thus the need for syncronization. GCTray provides the capability to share your GoldenCheetah profile among friends and not have to worry about database corruption

# Setup
GCTray requires that you are using a cloud based storage. During testing we found dropbox not to be as reliable as GoogleDrive therefore I will provide instructions for setting up with GoogleDrive

## Install GoogleDrive and Setup your Local Profiles
1. Install [GoogleDrive Anywhere](https://tools.google.com/dlpage/drive)
2. Sync GoogleDrive to your local system
3. Create a folder in your google drive, I call mine "GoldenCheetah Profiles"
4. Copy your existing profiles to your google drive 
  1. Generally the path is: %LocalAppData%\GoldenCheetah
5. Now that you are intending on using these profiles it is wise (but not required if GCTray is to be used exclusively) to go into GoldenCheetah settings and point your Athlete Library here.
  1. Tools -> Options
  2. General -> Athlete Library
  
## Setup and use GCTray
- GCTray will run as a taskbar item and can be run by double clicking or setting to start when you login.
- You can set GCTray to run automatically from it's settings menu
- GCTray also has a settings that needs to point to your GoogleDrive Profiles folder, by default it looks at GoldenCheetah's Default directory (not what you just changed it to)

## Sharing Profiles
1. From GoogleDrive you can choose who you share your profile with
2. I find this better to manage from the web version of GoogleDrive
3. Right-Click on the profile you desire to share and choose whom you would like to share it with
  1. You do have the option to share your profile read-only
  2. This allows others to open your profile with GCTray, but they can't edit
  3. You are for sure not going to get any database problems, no one can wipe out your profile, but no one can make changes if you desire this.
  4. be careful who gets write privileges... do they need it?

## Using Shared Profiles
1. This is important that you don't overwrite someones data, this is why you make a copy of your own, and maybe even periodically back it up.
2. Go to [Shared With Me](https://drive.google.com/drive/shared-with-me)
3. Right Click the desired profile that has recently been shared with you
4. Choose "Add to My Drive"
5. A dialog will ask you where you would like to put this folder
6. Choose the folder you created earlier for your profiles

## Deleting a Shared Profile
1. If you no longer want to view a shared profile simple delete the folder from your profile directory
2. This will not delete the sharer's data

# Running GCTray
- GCtray runs as a taskbar item
- you may click on it and view the profiles available to launch
- if a profile is grayed out, that means someone else has that profile open currently
  - future updates will allow you to see who has an open instance and even remotely close it down.
- You can change where GCTray is pulling it's profile list within the settings
- If you have open instances upon starting GCTray it will ask you to close them
- If you exit GCTray it will close down your instances of GoldenCheetah and unlock them for other user

# Rules of using GCTray
1. **If you use GCTray, use it exclusively**
  1. GCTray runs outside of GoldenCheetah therefore you can *cheat* and open a profile with GoldenCheetah even though someone else has it open
  2. This rule is to protect yours and others data in the way it has been intended to be used
2. Do **NOT** use the shortcut from within GoldenCheetah to launch another profile, only use GCTray.
