using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundscape
{
    class LibrarySettingsForm : SettingsForm
    {
        public override void DefineSettings()
        {
            Caption = "Library Settings";
            Settings.Add(new TextSetting("LIBRARY_NAME", "Name"));
        }

        public string LibraryName
        {
            get
            {
                return GetSettingValue("LIBRARY_NAME");
            }
            set
            {
                SetSettingValue("LIBRARY_NAME", value);
            }
        }

        public LibrarySettingsForm(string name)
        {
            LibraryName = name;
        }
    }
}
