using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Soundscape
{
    abstract class SettingsForm : Form
    {
        public string Caption { get; set; }

        public List<Setting> Settings { get; set; }

        public string GetSettingValue(string name)
        {
            foreach (Setting setting in Settings)
            {
                if (setting.Name == name)
                {
                    return setting.Value;
                }
            }
            return string.Empty;
        }

        public void SetSettingValue(string name, string value)
        {
            foreach (Setting setting in Settings)
            {
                if (setting.Name == name)
                {
                    setting.Value = value;
                    setting.ChangeControlValue(this, value);
                    return;
                }
            }
        }

        public abstract void DefineSettings();

        void RenderSettings()
        {
            Text = Caption;
            Width = Constants.DialogWidth;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            int totalHeight = Constants.DialogPadding;

            // weird bug workaround
            Height = 0;
            int topBarHeight = Height;

            // render custom settings
            if (Settings != null)
            {
                foreach (Setting setting in Settings)
                {
                    setting.Render(this, ref totalHeight);
                }
            }

            // add OK and Cancel buttons
            Button saveButton = new Button
            {
                Text = "Save",
                Left = Constants.DialogPadding,
                Top = totalHeight,
                Height = Constants.BoxHeight,
                DialogResult = DialogResult.OK
            };
            Button cancelButton = new Button
            {
                Text = "Cancel",
                Left = saveButton.Width + 2*Constants.DialogPadding,
                Top = totalHeight,
                Height = Constants.BoxHeight,
                DialogResult = DialogResult.Cancel
            };
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            totalHeight += Constants.BoxHeight + Constants.DialogPadding;
            Height = topBarHeight + totalHeight;
        }

        public SettingsForm()
        {
            Caption = "Settings";
            Settings = new List<Setting>();
            DefineSettings();
            RenderSettings();
        }
    }
}
