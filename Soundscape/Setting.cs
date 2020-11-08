using System.Windows.Forms;

namespace Soundscape
{
    abstract class Setting
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public abstract void Render(Form form, ref int height);

        public abstract void ChangeControlValue(Form form, string value);
    }
}
