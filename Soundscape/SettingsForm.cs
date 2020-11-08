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
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowOnly;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            int totalHeight = Constants.DialogPadding;

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
                DialogResult = DialogResult.OK
            };
            Button cancelButton = new Button
            {
                Text = "Cancel",
                Left = saveButton.Width + 2*Constants.DialogPadding,
                Top = totalHeight,
                DialogResult = DialogResult.Cancel
            };
            Controls.Add(saveButton);
            Controls.Add(cancelButton);

            // adjust total height
            totalHeight += saveButton.Height + Constants.DialogPadding;
            Height = totalHeight;
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
