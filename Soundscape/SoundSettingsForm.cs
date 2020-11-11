using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundscape
{
    class SoundSettingsForm : SettingsForm
    {
        public override void DefineSettings()
        {
            Caption = "Sound Settings";
            Settings.Add(new TextSetting("SOUND_NAME", "Name"));
            Settings.Add(new TextSetting("SOUND_DESC", "Description"));
            Settings.Add(new TextSetting("SOUND_PATH", "Path"));
        }

        public string SoundName
        {
            get
            {
                return GetSettingValue("SOUND_NAME");
            }
            set
            {
                SetSettingValue("SOUND_NAME", value);
            }
        }

        public string SoundDescription
        {
            get
            {
                return GetSettingValue("SOUND_DESC");
            }
            set
            {
                SetSettingValue("SOUND_DESC", value);
            }
        }

        public string SoundPath
        {
            get
            {
                return GetSettingValue("SOUND_PATH");
            }
            set
            {
                SetSettingValue("SOUND_PATH", value);
            }
        }

        public SoundSettingsForm(SoundFile file)
        {
            SoundName = file.Name;
            SoundDescription = file.Description;
            SoundPath = file.Filepath;
        }

        public void UpdateSound(SoundFile file)
        {
            file.Name = SoundName;
            file.Description = SoundDescription;
            file.Filepath = SoundPath;
        }
    }
}
