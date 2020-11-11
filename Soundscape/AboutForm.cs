using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundscape
{
    class AboutForm : SettingsForm
    {
        public override void DefineSettings()
        {
            Caption = "About";
            AvailableButtons = SettingsFormButtons.None;
            Width = 300;
            Settings.Add(new CaptionSetting("APP_NAME", $"{Constants.AppName} v{Constants.Version}"));
            Settings.Add(new CaptionSetting("CREATED_BY", $"Created by Alexandre Taillefer"));
            Settings.Add(new CaptionSetting("EMAIL", $"alexandre.taillefer0@gmail.com"));
        }
    }
}
