using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Soundscape
{
    abstract class SettingsForm : Form
    {
        public string Caption { get; set; }

        public List<Setting> Settings { get; set; }

        public SettingsFormButtons AvailableButtons { get; set; }

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

            // add buttons
            if (AvailableButtons == SettingsFormButtons.OKCancel ||
                AvailableButtons == SettingsFormButtons.OK)
            {
                Button okButton = new Button
                {
                    Text = "OK",
                    Left = Constants.DialogPadding,
                    Top = totalHeight,
                    Height = Constants.BoxHeight,
                    DialogResult = DialogResult.OK
                };
                Controls.Add(okButton);

                if (AvailableButtons == SettingsFormButtons.OKCancel)
                {
                    Button cancelButton = new Button
                    {
                        Text = "Cancel",
                        Left = okButton.Width + 2 * Constants.DialogPadding,
                        Top = totalHeight,
                        Height = Constants.BoxHeight,
                        DialogResult = DialogResult.Cancel
                    };
                    Controls.Add(cancelButton);
                }

                totalHeight += Constants.BoxHeight + Constants.DialogPadding;
            }
            
            // adjust total height
            Height = topBarHeight + totalHeight;
        }

        public SettingsForm()
        {
            AvailableButtons = SettingsFormButtons.OKCancel;
            Caption = "Settings";
            Width = Constants.DialogWidth;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Settings = new List<Setting>();
            DefineSettings();
            RenderSettings();
        }
    }
}
