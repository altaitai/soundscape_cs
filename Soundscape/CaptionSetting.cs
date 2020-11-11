using System.Windows.Forms;

namespace Soundscape
{
    class CaptionSetting : Setting
    {

        public string CaptionText { get; set; }

        public CaptionSetting(string name, string caption)
        {
            Name = name;
            CaptionText = caption;
        }

        public override void ChangeControlValue(Form form, string value)
        {
            form.Controls[$"{Name}_LABEL"].Text = value;
        }

        public override void Render(Form form, ref int totalHeight)
        {
            Label label = new Label
            {
                Name = $"{Name}_LABEL",
                Left = Constants.DialogPadding,
                Top = totalHeight,
                Text = CaptionText,
                Width = form.Width - 2*Constants.DialogPadding,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            form.Controls.Add(label);

            totalHeight += Constants.BoxHeight + Constants.DialogPadding;
        }
    }
}
