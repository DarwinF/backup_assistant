using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MessCleaner
{
    public partial class configBrowser : Form
    {
        private string fileNotFoundMessage = "File does not exist in the currently selected location.Would you \nlike " +
                                "to keep the current location and create a default file?";

        private string editorPath = "C:\\Documents and Settings\\u3149\\My Documents\\Visual Studio 2010\\Projects\\MessCleanerSetup_v1.0.0\\MessCleanerSetup_v1.0.0\\bin\\Debug\\";
        private string editorName = "MessCleanerSetup_v1.0.0.exe";

        public configBrowser()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            File.Copy(fileLocationTextBox.Text.ToString(), "..\\..\\settings.conf");

            this.Hide();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            configBrowserDialog.ShowDialog();

            if (!File.Exists(configBrowserDialog.SelectedPath.ToString() + "\\settings.conf"))
            {
                ResetTextBox();

                DialogResult alertResponse;
                alertResponse = MessageBox.Show(fileNotFoundMessage, "File Not Found!", MessageBoxButtons.YesNo);

                switch (alertResponse)
                {
                    case DialogResult.Yes:
                        MessageBox.Show("File created! Please note that this file will be unusable and you" +
                                        " will need to open the editor to configure your settings file.", "WARNING!");
                        CreateDefaultFile(configBrowserDialog.SelectedPath.ToString());
                        SetTextBox(configBrowserDialog.SelectedPath.ToString());
                        acceptButton.Enabled = true;
                        break;
                    case DialogResult.No:
                        browseButton_Click(sender, e);
                        break;
                    default:
                        Console.WriteLine("Error: 0x0002 -- Press any key to exit.");
                        Console.ReadKey();
                        Application.Exit();
                        break;
                }
            }

            else
            {
                SetTextBox(configBrowserDialog.SelectedPath.ToString());
                acceptButton.Enabled = true;
            }
        }

        private void ResetTextBox()
        {
            fileLocationTextBox.Text = "Configuration File Loctaion";
            fileLocationTextBox.ForeColor = SystemColors.ScrollBar;
            fileLocationTextBox.Font = new Font(fileLocationTextBox.Font, FontStyle.Italic);
        }

        private void SetTextBox(string filePath)
        {
            fileLocationTextBox.Text = filePath + "\\settings.conf";
            fileLocationTextBox.ForeColor = Color.Black;
            fileLocationTextBox.Font = new Font(fileLocationTextBox.Font, FontStyle.Regular);
        }

        private void CreateDefaultFile(string path)
        {
            List<string> defaultFile = new List<string>();
            string tempLine;

            StreamReader fileReader = new StreamReader("..\\..\\settings.conf");
            tempLine = fileReader.ReadLine();

            while (tempLine != null)
            {
                defaultFile.Add(tempLine);
                tempLine = fileReader.ReadLine();
            }

            fileReader.Close();

            StreamWriter fileWriter = new StreamWriter(path + "\\settings.conf");
            for (int i = 0; i < defaultFile.Count; i++)
            {
                fileWriter.WriteLine(defaultFile[i]);
            }
            fileWriter.Close();
        }

        private void launchEditorButton_Click(object sender, EventArgs e)
        {
            Process editor = Process.Start(editorPath + editorName);
            editor.WaitForExit();            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
