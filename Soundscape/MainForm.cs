using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.IO;
using System.Windows.Forms;

namespace Soundscape
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateStatus();
            Player = new SoundPlayer();
            SoundView.View = View.Details;
            foreach (string col in Constants.ListViewColumns)
            {
                SoundView.Columns.Add(col);
            }
        }

        LibraryFile OpenedFile = null;
        SoundPlayer Player = null;

        bool FileIsOpen 
        { 
            get
            {
                return OpenedFile != null;
            } 
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = SaveCurrentFile();
            
            if (result != DialogResult.Cancel)
            {
                OpenedFile = new LibraryFile();
                EditLibrarySettings();
                UpdateSoundList();
            }
        }

        void EditLibrarySettings()
        {
            if (FileIsOpen)
            {
                LibrarySettingsForm settings = new LibrarySettingsForm(OpenedFile.Name);
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    OpenedFile.Name = settings.LibraryName;
                    UpdateStatus();
                }
            }
        }

        void UpdateStatus()
        {
            if (!FileIsOpen)
            {
                OpenFileStatus.Text = "No active library";
                SoundCountStatus.Text = "";
            }
            else
            {
                OpenFileStatus.Text = $"Active library: {OpenedFile.Name}";
                SoundCountStatus.Text = $"Sound count: {OpenedFile.Sounds.Count}";
            }
        }

        DialogResult SaveCurrentFile()
        {
            DialogResult result = DialogResult.No;
            if (FileIsOpen)
            {
                result = MessageBox.Show($"Save {OpenedFile.Name} library?", "Confirm save", MessageBoxButtons.YesNoCancel);
            }

            if (result == DialogResult.Yes)
            {
                OpenedFile.Save(OpenedFile.Filepath);
                UpdateStatus();
            }

            return result;
        }

        private void libraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLibrarySettings();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = SaveCurrentFile();

            if (result != DialogResult.Cancel)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (OpenedFile != null)
                {
                    dialog.InitialDirectory = Path.GetDirectoryName(OpenedFile.Filepath);
                }
                dialog.Filter = "Soundscape Library files (*.slib)|*.slib|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    OpenedFile = new LibraryFile(dialog.FileName);
                    UpdateStatus();
                    UpdateSoundList();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileIsOpen)
            {
                OpenedFile.Save(OpenedFile.Filepath);
                UpdateStatus();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileIsOpen)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (OpenedFile != null)
                {
                    dialog.InitialDirectory = Path.GetDirectoryName(OpenedFile.Filepath);
                }
                dialog.Filter = "Soundscape Library files (*.slib)|*.slib|All files (*.*)|*.*";
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    OpenedFile.Save(dialog.FileName);
                    UpdateStatus();
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = SaveCurrentFile();
            if (result != DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        void UpdateSoundList()
        {
            if (FileIsOpen)
            {
                SoundView.Items.Clear();
                foreach (SoundFile file in OpenedFile.Sounds)
                {
                    SoundView.Items.Add(new ListViewItem(file.GetFields()));
                }
            }
        }

        private void SoundView_DragDrop(object sender, DragEventArgs e)
        {
            if (FileIsOpen)
            {
                string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                int i;
                for (i = 0; i < s.Length; i++)
                {
                    try
                    {
                        OpenedFile.Sounds.Add(new SoundFile(s[i]));
                    }
                    catch (Exception) { }
                }
                UpdateSoundList();
            }
        }

        private void SoundView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void SoundView_ItemActivate(object sender, EventArgs e)
        {
            if (FileIsOpen) {
                if (SoundView.SelectedIndices.Count == 1)
                {
                    try
                    {
                        Player.SoundLocation = OpenedFile.Sounds[SoundView.SelectedIndices[0]].Filepath;
                        Player.Play();
                    }
                    catch (Exception ex)
                    {
                        // TODO log in status bar
                    }
                }
            }
        }
    }
}
