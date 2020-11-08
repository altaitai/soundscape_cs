using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soundscape
{
    class TextSetting : Setting
    {
        public string LabelText { get; set; }

        public string DefaultValue { get; set; }

        public TextSetting(string name, string label, string defaultValue = "")
        {
            Name = name;
            Value = string.Empty;
            LabelText = label;
            DefaultValue = defaultValue;
        }

        public void SetValueCallback(object sender, EventArgs e) 
        {
            TextBox box = (TextBox)sender;
            Value = box.Text;
        }

        public override void Render(Form form, ref int height)
        {
            Label label = new Label
            {
                Left = Constants.DialogPadding,
                Top = height,
                Text = LabelText
            };
            form.Controls.Add(label);

            TextBox box = new TextBox
            {
                Name = $"{Name}_BOX",
                Left = form.Width / 2,
                Top = height,
                Width = form.Width / 2 - Constants.DialogPadding
            };
            box.TextChanged += SetValueCallback;
            form.Controls.Add(box);

            height += box.Height + Constants.DialogPadding;
        }

        public override void ChangeControlValue(Form form, string value)
        {
            form.Controls[$"{Name}_BOX"].Text = value;
        }
    }
}
