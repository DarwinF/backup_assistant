using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Settings_Editor
{
    class riHandler
    {
        #region Controls
        private Label rarFileLocLabel;
        private Label rarFileNameLabel;
        private Label appendDate2NameLabel;
        private Label namePreviewLabel;
        private Label _namePreviewLabel;
        private TextBox rarFileLocTB;
        private TextBox rarFileNameTB;
        private CheckBox appendDate2NameCheck;
        private Button rarFileLocButton;
        private FolderBrowserDialog rarFileLocBrowser;

        private mainForm _MainForm;
        #endregion
        #region Accessors
        public TextBox RarFileLocTB
        {
            get { return rarFileLocTB; }
            set { rarFileLocTB = value; }
        }

        public TextBox RarFileNameTB
        {
            get { return rarFileNameTB; }
            set { rarFileNameTB = value; }
        }

        public CheckBox AppendDate2NameCheck
        {
            get { return appendDate2NameCheck; }
            set { appendDate2NameCheck = value; }
        }
        #endregion
        #region Public Methods
        public riHandler(Point rarInfoBoxLoc, Size rarInfoBoxSize, mainForm _mainForm)
        {
            _MainForm = _mainForm;

            InitializeControls();
            ConstructControls(rarInfoBoxLoc, rarInfoBoxSize, _mainForm);
        }

        public riHandler()
        {

        }

        public void MoveControls(int distance, ref MenuStrip mainMenu)
        {
            mainMenu.BringToFront();
            
            rarFileLocLabel.Location = new Point(rarFileLocLabel.Location.X, rarFileLocLabel.Location.Y + distance);
            rarFileNameLabel.Location = new Point(rarFileNameLabel.Location.X, rarFileNameLabel.Location.Y + distance);
            appendDate2NameLabel.Location = new Point(appendDate2NameLabel.Location.X, appendDate2NameLabel.Location.Y + distance);
            namePreviewLabel.Location = new Point(namePreviewLabel.Location.X, namePreviewLabel.Location.Y + distance);
            _namePreviewLabel.Location = new Point(_namePreviewLabel.Location.X, _namePreviewLabel.Location.Y + distance);
            rarFileLocTB.Location = new Point(rarFileLocTB.Location.X, rarFileLocTB.Location.Y + distance);
            rarFileNameTB.Location = new Point(rarFileNameTB.Location.X, rarFileNameTB.Location.Y + distance);
            appendDate2NameCheck.Location = new Point(appendDate2NameCheck.Location.X, appendDate2NameCheck.Location.Y + distance);
            rarFileLocButton.Location = new Point(rarFileLocButton.Location.X, rarFileLocButton.Location.Y + distance);
        }

        public void SetVisibility(bool value)
        {
            rarFileLocLabel.Visible = value;
            rarFileNameLabel.Visible = value;
            appendDate2NameLabel.Visible = value;
            namePreviewLabel.Visible = value;
            _namePreviewLabel.Visible = value;
            rarFileLocTB.Visible = value;
            rarFileNameTB.Visible = value;
            appendDate2NameCheck.Visible = value;
            rarFileLocButton.Visible = value;
        }

        public void ConstructControls(Point rarInfoBoxLoc, Size rarInfoBoxSize, Form _mainForm)
        {
            #region rarFileLocLabel
            rarFileLocLabel.Name = "rarFileLocLabel";
            rarFileLocLabel.Text = "Location for .rar file";
            rarFileLocLabel.Location = new Point(rarInfoBoxLoc.X + 10, rarInfoBoxLoc.Y + 18);
            rarFileLocLabel.Size = new Size((int)(rarFileLocLabel.Text.Length * (rarFileLocLabel.Font.SizeInPoints * 0.5)),
                                                (int)(rarFileLocLabel.Font.SizeInPoints * 3));
            rarFileLocLabel.BackColor = Color.OldLace;
            rarFileLocLabel.Visible = false;
            rarFileLocLabel.BringToFront();
            rarFileLocLabel.Invalidate();
            #endregion
            #region rarFileLocButton
            rarFileLocButton.Name = "rarFileLocButton";
            rarFileLocButton.Text = "...";
            rarFileLocButton.Size = new Size((int)(3 * rarFileLocButton.Font.SizeInPoints),
                                             (int)(rarFileLocButton.Font.SizeInPoints * 3));
            rarFileLocButton.Location = new Point((((rarInfoBoxLoc.X + rarInfoBoxSize.Width) - rarFileLocButton.Width) - 10),
                                                    (rarInfoBoxLoc.Y + 14));
            rarFileLocButton.Visible = false;
            rarFileLocButton.BringToFront();
            rarFileLocButton.Invalidate();
            #endregion
            #region rarFileNameLabel
            rarFileNameLabel.Name = "rarFileNameLabel";
            rarFileNameLabel.Text = ".rar File Name";
            rarFileNameLabel.Location = new Point((rarInfoBoxLoc.X + 10),
                                                  (rarFileLocLabel.Bottom + 10));
            rarFileNameLabel.Size = new Size((int)(rarFileNameLabel.Text.Length * rarFileNameLabel.Font.SizeInPoints * 0.75),
                                             (int)(rarFileNameLabel.Font.SizeInPoints * 3));
            rarFileNameLabel.BackColor = Color.OldLace;
            rarFileNameLabel.Visible = false;
            rarFileNameLabel.BringToFront();
            rarFileNameLabel.Invalidate();
            #endregion
            #region appendDate2NameLabel
            appendDate2NameLabel.Name = "appendDate2NameLabel";
            appendDate2NameLabel.Text = "Append Date to Name";
            appendDate2NameLabel.Location = new Point((rarInfoBoxLoc.X + 10),
                                                      (rarFileNameLabel.Bottom + 10));
            appendDate2NameLabel.Size = new Size((int)(appendDate2NameLabel.Text.Length * appendDate2NameLabel.Font.SizeInPoints * 0.5),
                                                 (int)(appendDate2NameLabel.Font.SizeInPoints * 3));
            appendDate2NameLabel.BackColor = Color.OldLace;
            appendDate2NameLabel.Visible = false;
            appendDate2NameLabel.BringToFront();
            appendDate2NameLabel.Invalidate();
            #endregion
            #region namePreviewLabel
            namePreviewLabel.Name = "namePreviewLabel";
            namePreviewLabel.Text = ".rar File Name Preview";
            namePreviewLabel.Location = new Point((rarInfoBoxLoc.X + 10),
                                                  (appendDate2NameLabel.Bottom + 10));
            namePreviewLabel.Size = new Size((int)(namePreviewLabel.Text.Length * namePreviewLabel.Font.SizeInPoints * 0.5),
                                             (int)(namePreviewLabel.Font.SizeInPoints * 3));
            namePreviewLabel.BackColor = Color.OldLace;
            namePreviewLabel.Visible = false;
            namePreviewLabel.BringToFront();
            namePreviewLabel.Invalidate();
            #endregion
            #region _namePreviewLabel
            _namePreviewLabel.Name = "_namePreviewLabel";
            _namePreviewLabel.Text = "null";
            _namePreviewLabel.Location = new Point(namePreviewLabel.Right + 10, appendDate2NameLabel.Bottom + 10);
            SetPreviewLabelSize();
            _namePreviewLabel.BackColor = Color.OldLace;
            _namePreviewLabel.Visible = false;
            _namePreviewLabel.BringToFront();
            _namePreviewLabel.Invalidate();
            #endregion 
            #region rarFileLocTB
            rarFileLocTB.Name = "rarFileLocTB";
            rarFileLocTB.Text = "Folder for .rar file";
            rarFileLocTB.ForeColor = Color.Gray;
            rarFileLocTB.Font = new Font(rarFileLocTB.Font, FontStyle.Italic);
            rarFileLocTB.Location = new Point(rarFileLocLabel.Right + 10, rarInfoBoxLoc.Y + 16);
            rarFileLocTB.Size = new Size((rarFileLocButton.Left - rarFileLocTB.Left - 10),
                                         (int)(rarFileLocTB.Font.SizeInPoints + 4));
            rarFileLocTB.Visible = false;
            rarFileLocTB.BringToFront();
            rarFileLocTB.Invalidate();
            #endregion
            #region rarFileNameTB
            rarFileNameTB.Name = "rarFileNameTB";
            rarFileNameTB.Text = "Enter .rar File Name";
            rarFileNameTB.ForeColor = Color.Gray;
            rarFileNameTB.Font = new Font(rarFileNameTB.Font, FontStyle.Italic);
            rarFileNameTB.Location = new Point((rarFileNameLabel.Right + 10),
                                               (rarFileLocLabel.Bottom + 10));
            rarFileNameTB.Size = new Size((((rarInfoBoxSize.Width + rarInfoBoxLoc.X) - rarFileNameLabel.Right) - 20),
                                          (int)(rarFileNameTB.Font.SizeInPoints * 3));
            rarFileNameTB.Visible = false;
            rarFileNameTB.BringToFront();
            rarFileNameTB.Invalidate();
            #endregion
            #region appendDate2NameCheck
            appendDate2NameCheck.Name = "appendDate2NameCheck";
            appendDate2NameCheck.Checked = true;
            appendDate2NameCheck.Location = new Point((appendDate2NameLabel.Right + 10),
                                                      (rarFileNameLabel.Bottom + 10));
            appendDate2NameCheck.Size = new Size(15, 15); //clickable area is roughly the same size as the box now
            appendDate2NameCheck.Visible = false;
            appendDate2NameCheck.BringToFront();
            appendDate2NameCheck.Invalidate();
            #endregion            
        }

        public void SetInitFocus()
        {
            rarFileLocTB.Focus();
        }

        public void SetPreviewLabelSize()
        {
            _namePreviewLabel.Size = new Size((int)(_namePreviewLabel.Text.Length * _namePreviewLabel.Font.SizeInPoints * 0.75),
                                              (int)(_namePreviewLabel.Font.SizeInPoints * 3));
        }
        public void InitializeControls()
        {
            rarFileLocLabel = new Label();
            _MainForm.Controls.Add(rarFileLocLabel);

            rarFileNameLabel = new Label();
            _MainForm.Controls.Add(rarFileNameLabel);

            appendDate2NameLabel = new Label();
            _MainForm.Controls.Add(appendDate2NameLabel);

            namePreviewLabel = new Label();
            _MainForm.Controls.Add(namePreviewLabel);

            _namePreviewLabel = new Label();
            _MainForm.Controls.Add(_namePreviewLabel);

            rarFileLocTB = new TextBox();
            rarFileLocTB.Click += new EventHandler(RarFileLocTB_Click);
            rarFileLocTB.TextChanged += new EventHandler(RarFileLocTB_TextChanged);
            _MainForm.Controls.Add(rarFileLocTB);

            rarFileNameTB = new TextBox();
            rarFileNameTB.Click += new EventHandler(RarFileNameTB_Click);
            rarFileNameTB.TextChanged += new EventHandler(RarFileName_TextChanged);
            _MainForm.Controls.Add(rarFileNameTB);

            appendDate2NameCheck = new CheckBox();
            appendDate2NameCheck.CheckedChanged += new EventHandler(AppendDate2NameCheck_Changed);
            _MainForm.Controls.Add(appendDate2NameCheck);

            rarFileLocButton = new Button();
            rarFileLocButton.Click += new EventHandler(RarFileLocButton_Click);
            _MainForm.Controls.Add(rarFileLocButton);

            rarFileLocBrowser = new FolderBrowserDialog();
        }
        #endregion
        #region Private Methods
        private void SetTabIndex()
        {
            rarFileLocTB.TabIndex = 0;
            rarFileLocButton.TabIndex = 1;
            rarFileNameTB.TabIndex = 2;
            appendDate2NameCheck.TabIndex = 3;
        }

        private void ClearFormatting(ref TextBox _TextBox)
        {
            Font textBoxFont = _TextBox.Font;
            textBoxFont = new Font(textBoxFont, FontStyle.Regular);
            _TextBox.ForeColor = Color.Black;
            _TextBox.Font = textBoxFont;
        }
        #endregion
        #region Event Methods
        private void LaunchExplorer()
        {
            rarFileLocBrowser.ShowDialog();
            rarFileLocTB.Text = rarFileLocBrowser.SelectedPath;
        }

        private void RarFileLocButton_Click(object sender, EventArgs e)
        {
            LaunchExplorer();
        }

        private void RarFileLocTB_Click(object sender, EventArgs e)
        {
            if (rarFileLocTB.Text == "Folder for .rar file")
            {
                rarFileLocTB.Text = "";
                ClearFormatting(ref rarFileLocTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                rarFileLocTB.SelectAll();
            }
        }

        private void RarFileLocTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref rarFileLocTB);
            _MainForm.currdatabase.RarFileLocation = rarFileLocTB.Text;
            _MainForm.IsSaved = false;
        }

        private void RarFileNameTB_Click(object sender, EventArgs e)
        {
            if (rarFileNameTB.Text == "Enter .rar File Name")
            {
                rarFileNameTB.Text = "";
                ClearFormatting(ref rarFileNameTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                rarFileNameTB.SelectAll();
            }
        }

        private void AppendDate2NameCheck_Changed(object sender, EventArgs e)
        {
            //This date is used if the dbInfo is not found/configured
            if (appendDate2NameCheck.Checked == true)
            {
                _namePreviewLabel.Text = rarFileNameTB.Text + "_" + DateTime.Today.ToString("dd_MM_yyyy");
                _MainForm.currdatabase.AppendDateToRar = "Yes";
            }
            else
            {
                _namePreviewLabel.Text = rarFileNameTB.Text;
                _MainForm.currdatabase.AppendDateToRar = "No";
            }

            SetPreviewLabelSize();
            _MainForm.IsSaved = false;
        }

        private void RarFileName_TextChanged(object sender, EventArgs e)
        {
            //The date is used if the dbInfo is not found/configured
            ClearFormatting(ref rarFileNameTB);

            if (appendDate2NameCheck.Checked == true)
            {
                _namePreviewLabel.Text = rarFileNameTB.Text + "_" + DateTime.Today.ToString("dd_MM_yyyy");
                _MainForm.currdatabase.RarFileName = rarFileNameTB.Text;
            }
            else
            {
                _namePreviewLabel.Text = rarFileNameTB.Text;
                _MainForm.currdatabase.RarFileName = rarFileNameTB.Text;
            }

            SetPreviewLabelSize();
            _MainForm.IsSaved = false;
        }
        #endregion
    }
}
