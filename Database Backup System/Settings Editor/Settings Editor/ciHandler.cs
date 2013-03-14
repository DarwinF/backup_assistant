using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Settings_Editor
{
    class ciHandler
    {
        #region Controls
        private Label copyToLocLabel;
        private Label createFolderLabel;
        private Label appendDateLabel;
        private Label newFolderNameLabel;
        private Label namePreviewLabel;
        private Label _namePreviewLabel;
        private Label amntToCopyLabel;
        private Label overwriteLabel;
        private Label versionToCopyLabel;
        private CheckBox createFolderCheck;
        private CheckBox appendDateCheck;
        private CheckBox overwriteCheck;
        private TextBox copyToLocTB;
        private TextBox newFolderNameTB;
        private TextBox numFilesToCopyTB;
        private Button copyToLocButton;
        private ComboBox versionToCopyCombo;
        private FolderBrowserDialog copyToLocBrowser;

        private mainForm _MainForm;
        #endregion
        #region Accessors
        public CheckBox CreateFolderCheck
        {
            get { return createFolderCheck; }
            set { createFolderCheck = value; }
        }

        public CheckBox AppendDateCheck
        {
            get { return appendDateCheck; }
            set { appendDateCheck = value; }
        }

        public CheckBox OverwriteCheck
        {
            get { return overwriteCheck; }
            set { overwriteCheck = value; }
        }

        public TextBox CopyToLocTB
        {
            get { return copyToLocTB; }
            set { copyToLocTB = value; }
        }

        public TextBox NewFolderNameTB
        {
            get { return newFolderNameTB; }
            set { newFolderNameTB = value; }
        }

        public TextBox NumFilesToCopyTB
        {
            get { return numFilesToCopyTB; }
            set { numFilesToCopyTB = value; }
        }
        #endregion
        #region Public Methods
        public ciHandler(Point copyInfoBoxLoc, Size copyInfoBoxSize, mainForm _mainForm)
        {
            _MainForm = _mainForm;

            InitializeControls();
            ConstructControls(copyInfoBoxLoc, copyInfoBoxSize, _mainForm);
        }

        public ciHandler()
        {

        }

        public void MoveControls(int distance, ref MenuStrip mainMenu)
        {
            mainMenu.BringToFront();

            copyToLocLabel.Location = new Point(copyToLocLabel.Location.X, copyToLocLabel.Location.Y + distance);
            createFolderLabel.Location = new Point(createFolderLabel.Location.X, createFolderLabel.Location.Y + distance);
            appendDateLabel.Location = new Point(appendDateLabel.Location.X, appendDateLabel.Location.Y + distance);
            newFolderNameLabel.Location = new Point(newFolderNameLabel.Location.X, newFolderNameLabel.Location.Y + distance);
            namePreviewLabel.Location = new Point(namePreviewLabel.Location.X, namePreviewLabel.Location.Y + distance);
            _namePreviewLabel.Location = new Point(_namePreviewLabel.Location.X, _namePreviewLabel.Location.Y + distance);
            amntToCopyLabel.Location = new Point(amntToCopyLabel.Location.X, amntToCopyLabel.Location.Y + distance);
            overwriteLabel.Location = new Point(overwriteLabel.Location.X, overwriteLabel.Location.Y + distance);
            versionToCopyLabel.Location = new Point(versionToCopyLabel.Location.X, versionToCopyLabel.Location.Y + distance);
            createFolderCheck.Location = new Point(createFolderCheck.Location.X, createFolderCheck.Location.Y + distance);
            appendDateCheck.Location = new Point(appendDateCheck.Location.X, appendDateCheck.Location.Y + distance);
            overwriteCheck.Location = new Point(overwriteCheck.Location.X, overwriteCheck.Location.Y + distance);
            copyToLocTB.Location = new Point(copyToLocTB.Location.X, copyToLocTB.Location.Y + distance);
            copyToLocButton.Location = new Point(copyToLocButton.Location.X, copyToLocButton.Location.Y + distance);
            newFolderNameTB.Location = new Point(newFolderNameTB.Location.X, newFolderNameTB.Location.Y + distance);
            numFilesToCopyTB.Location = new Point(numFilesToCopyTB.Location.X, numFilesToCopyTB.Location.Y + distance);
            versionToCopyCombo.Location = new Point(versionToCopyCombo.Location.X, versionToCopyCombo.Location.Y + distance);
        }

        public void SetVisibility(bool value)
        {
            copyToLocLabel.Visible = value;
            createFolderLabel.Visible = value;
            appendDateLabel.Visible = value;
            newFolderNameLabel.Visible = value;
            namePreviewLabel.Visible = value;
            _namePreviewLabel.Visible = value;
            amntToCopyLabel.Visible = value;
            overwriteLabel.Visible = value;
            versionToCopyLabel.Visible = value;
            createFolderCheck.Visible = value;
            appendDateCheck.Visible = value;
            overwriteCheck.Visible = value;
            copyToLocTB.Visible = value;
            newFolderNameTB.Visible = value;
            numFilesToCopyTB.Visible = value;
            versionToCopyCombo.Visible = value;
            copyToLocButton.Visible = value;
        }

        public void ConstructControls(Point copyInfoBoxLoc, Size copyInfoBoxSize, Form _mainForm)
        {
            #region copyToLocLabel
            copyToLocLabel.Name = "copyToLocLabel";
            copyToLocLabel.Text = "Copy To Location";
            copyToLocLabel.Location = new Point(copyInfoBoxLoc.X + 10, copyInfoBoxLoc.Y + 18);
            copyToLocLabel.Size = new Size((int)(copyToLocLabel.Text.Length * copyToLocLabel.Font.SizeInPoints * 0.75),
                                           (int)(copyToLocLabel.Font.SizeInPoints * 2));
            copyToLocLabel.BackColor = Color.OldLace;
            copyToLocLabel.Visible = false;
            copyToLocLabel.BringToFront();
            copyToLocLabel.Invalidate();
            #endregion
            #region createFolderLabel
            createFolderLabel.Name = "createFolderLabel";
            createFolderLabel.Text = "Create New Folder";
            createFolderLabel.Location = new Point(copyInfoBoxLoc.X + 10, copyToLocLabel.Bottom + 10);
            createFolderLabel.Size = new Size((int)(createFolderLabel.Text.Length * createFolderLabel.Font.SizeInPoints * 0.75),
                                              (int)(createFolderLabel.Font.SizeInPoints * 2));
            createFolderLabel.BackColor = Color.OldLace;
            createFolderLabel.Visible = false;
            createFolderLabel.BringToFront();
            createFolderLabel.Invalidate();
            #endregion
            #region createFolderCheck
            createFolderCheck.Name = "createFolderCheck";
            createFolderCheck.Checked = true;
            createFolderCheck.Location = new Point(createFolderLabel.Right + 10, copyToLocLabel.Bottom + 10);
            createFolderCheck.Size = new Size(15, 15);      //sets the "clickable" area to about the size of the box
            createFolderCheck.Visible = false;
            createFolderCheck.BringToFront();
            createFolderCheck.Invalidate();
            #endregion
            #region appendDateLabel
            appendDateLabel.Name = "appendDateLabel";
            appendDateLabel.Text = "Append Date To Folder Name";
            appendDateLabel.Location = new Point(createFolderCheck.Right + 10, copyToLocLabel.Bottom + 10);
            appendDateLabel.Size = new Size((int)(appendDateLabel.Text.Length * appendDateLabel.Font.SizeInPoints * 0.75),
                                            (int)(appendDateLabel.Font.SizeInPoints * 2));
            appendDateLabel.BackColor = Color.OldLace;
            appendDateLabel.Visible = false;
            appendDateLabel.BringToFront();
            appendDateLabel.Invalidate();
            #endregion
            #region newFolderNameLabel
            newFolderNameLabel.Name = "newFolderNameLabel";
            newFolderNameLabel.Text = "New Folder Name";
            newFolderNameLabel.Location = new Point(copyInfoBoxLoc.X + 10, appendDateLabel.Bottom + 12);
            newFolderNameLabel.Size = new Size((int)(newFolderNameLabel.Text.Length * newFolderNameLabel.Font.SizeInPoints * 0.75),
                                               (int)(newFolderNameLabel.Font.SizeInPoints * 2));
            newFolderNameLabel.BackColor = Color.OldLace;
            newFolderNameLabel.Visible = false;
            newFolderNameLabel.BringToFront();
            newFolderNameLabel.Invalidate();
            #endregion
            #region namePreviewLabel
            namePreviewLabel.Name = "namePreviewLabel";
            namePreviewLabel.Text = "Folder Name Preview";
            namePreviewLabel.Size = new Size((int)(namePreviewLabel.Text.Length * namePreviewLabel.Font.SizeInPoints * 0.5),
                                             (int)(namePreviewLabel.Font.SizeInPoints * 2));
            namePreviewLabel.Location = new Point(copyInfoBoxLoc.X + 200, newFolderNameLabel.Bottom + 12);
            namePreviewLabel.BackColor = Color.OldLace;
            namePreviewLabel.Visible = false;
            namePreviewLabel.BringToFront();
            namePreviewLabel.Invalidate();
            #endregion
            #region _namePreviewLabel
            _namePreviewLabel.Name = "_namePreviewLabel";
            _namePreviewLabel.Text = "null";
            _namePreviewLabel.Location = new Point(namePreviewLabel.Right + 10, newFolderNameLabel.Bottom + 12);
            SetPreviewLabelSize();
            _namePreviewLabel.BackColor = Color.OldLace;
            _namePreviewLabel.Visible = false;
            _namePreviewLabel.BringToFront();
            _namePreviewLabel.Invalidate();
            #endregion
            #region VersionToCopyLabel
            versionToCopyLabel.Name = "versionToCopyLabel";
            versionToCopyLabel.Text = "File Version to Copy";
            versionToCopyLabel.Location = new Point(copyInfoBoxLoc.X + 10, namePreviewLabel.Bottom + 13);
            versionToCopyLabel.Size = new Size((int)(versionToCopyLabel.Text.Length * versionToCopyLabel.Font.SizeInPoints * 0.75),
                                               (int)(versionToCopyLabel.Font.SizeInPoints * 2));
            versionToCopyLabel.BackColor = Color.OldLace;
            versionToCopyLabel.Visible = false;
            versionToCopyLabel.BringToFront();
            versionToCopyLabel.Invalidate();
            #endregion
            #region copyToLocButton
            copyToLocButton.Name = "copyToLocButton";
            copyToLocButton.Text = "...";
            copyToLocButton.Location = new Point((((copyInfoBoxLoc.X + copyInfoBoxSize.Width) - copyToLocButton.Width) - 10),
                                                 (copyInfoBoxLoc.Y + 12));
            copyToLocButton.Size = new Size((int)(copyToLocButton.Text.Length * copyToLocButton.Font.SizeInPoints),
                                            (int)(copyToLocButton.Font.SizeInPoints * 3));
            copyToLocButton.Visible = false;
            copyToLocButton.BringToFront();
            copyToLocButton.Invalidate();
            #endregion
            #region versionToCopyCombo
            versionToCopyCombo.Name = "versionToCopyCombo";
            versionToCopyCombo.Text = "Select Versions to Copy";
            versionToCopyCombo.Location = new Point(versionToCopyLabel.Right + 10, namePreviewLabel.Bottom + 10);
            versionToCopyCombo.Size = new Size((int)(versionToCopyCombo.Text.Length * versionToCopyCombo.Font.SizeInPoints * 0.5),
                                                (int)(versionToCopyCombo.Font.SizeInPoints * 2));
            versionToCopyCombo.Visible = false;
            versionToCopyCombo.BringToFront();
            versionToCopyCombo.Invalidate();
            #endregion
            #region amntToCopyLabel
            amntToCopyLabel.Name = "amntToCopyLabel";
            amntToCopyLabel.Text = "Number of files to Copy";
            amntToCopyLabel.Location = new Point(versionToCopyCombo.Right + 10, namePreviewLabel.Bottom + 13);
            amntToCopyLabel.Size = new Size((int)(amntToCopyLabel.Text.Length * amntToCopyLabel.Font.SizeInPoints * 0.75),
                                             (int)(amntToCopyLabel.Font.SizeInPoints * 2));
            amntToCopyLabel.BackColor = Color.OldLace;
            amntToCopyLabel.Visible = false;
            amntToCopyLabel.BringToFront();
            amntToCopyLabel.Invalidate();
            #endregion
            #region appendDateCheck
            appendDateCheck.Name = "appendDateCheck";
            appendDateCheck.Checked = true;
            appendDateCheck.Location = new Point(appendDateLabel.Right + 10, copyToLocLabel.Bottom + 10);
            appendDateCheck.Size = new Size(15, 15);        //sets the "clickable" area to about the size of the box
            appendDateCheck.Visible = false;
            appendDateCheck.BringToFront();
            appendDateCheck.Invalidate();
            #endregion
            #region copyToLocTB
            copyToLocTB.Name = "copyToLocTB";
            copyToLocTB.Text = "Copy-To Location";
            copyToLocTB.Location = new Point(copyToLocLabel.Right + 10, copyInfoBoxLoc.Y + 14);
            copyToLocTB.Size = new Size((copyToLocButton.Left - 10) - (copyToLocLabel.Right + 10), (int)(copyToLocTB.Font.SizeInPoints * 2));
            copyToLocTB.ForeColor = Color.Gray;
            copyToLocTB.Font = new Font(copyToLocTB.Font, FontStyle.Italic);
            copyToLocTB.Visible = false;
            copyToLocTB.BringToFront();
            copyToLocTB.Invalidate();
            #endregion
            #region newFolderNameTB
            newFolderNameTB.Name = "newFolderNameTB";
            newFolderNameTB.Text = "New Folder Name";
            newFolderNameTB.Location = new Point(newFolderNameLabel.Right + 10, appendDateLabel.Bottom + 10);
            newFolderNameTB.Size = new Size(((copyInfoBoxSize.Width + copyInfoBoxLoc.X) - 10) - (newFolderNameLabel.Right + 10), 
                                            (int)(newFolderNameTB.Font.SizeInPoints * 2));
            newFolderNameTB.ForeColor = Color.Gray;
            newFolderNameTB.Font = new Font(newFolderNameTB.Font, FontStyle.Italic);
            newFolderNameTB.Visible = false;
            newFolderNameTB.BringToFront();
            newFolderNameTB.Invalidate();
            #endregion
            #region numFilesToCopyTB
            numFilesToCopyTB.Name = "numFilesToCopy";
            numFilesToCopyTB.Text = "Number of Files to Copy";
            numFilesToCopyTB.Location = new Point(amntToCopyLabel.Right + 10, namePreviewLabel.Bottom + 10);
            numFilesToCopyTB.Size = new Size(((copyInfoBoxSize.Width + copyInfoBoxLoc.X) - 10) - (amntToCopyLabel.Right + 10),
                                             (int)(numFilesToCopyTB.Font.SizeInPoints * 2));
            numFilesToCopyTB.ForeColor = Color.Gray;
            numFilesToCopyTB.Font = new Font(numFilesToCopyTB.Font, FontStyle.Italic);
            numFilesToCopyTB.Visible = false;
            numFilesToCopyTB.BringToFront();
            numFilesToCopyTB.Invalidate();
            #endregion
            #region overwriteLabel
            overwriteLabel.Name = "overwriteLabel";
            overwriteLabel.Text = "Overwrite Existing Files";
            overwriteLabel.Location = new Point(copyInfoBoxLoc.X + 10, versionToCopyLabel.Bottom + 12);
            overwriteLabel.Size = new Size((int)(overwriteLabel.Text.Length * overwriteLabel.Font.SizeInPoints * 0.65),
                                           (int)(overwriteLabel.Font.SizeInPoints * 2));
            overwriteLabel.BackColor = Color.OldLace;
            overwriteLabel.Visible = false;
            overwriteLabel.BringToFront();
            overwriteLabel.Invalidate();
            #endregion
            #region overwriteCheck
            overwriteCheck.Name = "overwriteCheck";
            overwriteCheck.Checked = false;
            overwriteCheck.Location = new Point(overwriteLabel.Right + 10, versionToCopyLabel.Bottom + 12);
            overwriteCheck.Size = new Size(15, 15);         //makes the "clickable" area the size of the box
            overwriteCheck.Visible = false;
            overwriteCheck.BringToFront();
            overwriteCheck.Invalidate();
            #endregion

            SetTabIndex();
        }

        public void SetInitFocus()
        {
            copyToLocTB.Focus();
        }

        public void SetPreviewLabelSize()
        {            
            _namePreviewLabel.Size = new Size((int)(_namePreviewLabel.Text.Length * _namePreviewLabel.Font.SizeInPoints * 0.75),
                                              (int)(_namePreviewLabel.Font.SizeInPoints * 2));
        }

        public void LoadComboBox()
        {
            versionToCopyCombo.Items.Add("Newest");
            versionToCopyCombo.Items.Add("Oldest");
            versionToCopyCombo.Items.Add("All");
        }

        public void InitializeControls()
        {
            copyToLocLabel = new Label();
            _MainForm.Controls.Add(copyToLocLabel);

            createFolderLabel = new Label();
            _MainForm.Controls.Add(createFolderLabel);

            appendDateLabel = new Label();
            _MainForm.Controls.Add(appendDateLabel);

            newFolderNameLabel = new Label();
            _MainForm.Controls.Add(newFolderNameLabel);

            namePreviewLabel = new Label();
            _MainForm.Controls.Add(namePreviewLabel);

            _namePreviewLabel = new Label();
            _MainForm.Controls.Add(_namePreviewLabel);

            amntToCopyLabel = new Label();
            _MainForm.Controls.Add(amntToCopyLabel);

            overwriteLabel = new Label();
            _MainForm.Controls.Add(overwriteLabel);

            versionToCopyLabel = new Label();
            _MainForm.Controls.Add(versionToCopyLabel);

            createFolderCheck = new CheckBox();
            createFolderCheck.CheckedChanged += new EventHandler(CreateFolderCheck_CheckChanged);
            _MainForm.Controls.Add(createFolderCheck);

            appendDateCheck = new CheckBox();
            appendDateCheck.CheckedChanged += new EventHandler(AppendDateCheck_CheckChanged);
            _MainForm.Controls.Add(appendDateCheck);

            overwriteCheck = new CheckBox();
            overwriteCheck.CheckedChanged += new EventHandler(OverwriteExistingCheck_CheckChanged);
            _MainForm.Controls.Add(overwriteCheck);

            copyToLocTB = new TextBox();
            copyToLocTB.Click += new EventHandler(CopyToLocTB_Click);
            copyToLocTB.TextChanged += new EventHandler(CopyToLocTB_TextChanged);
            _MainForm.Controls.Add(copyToLocTB);

            newFolderNameTB = new TextBox();
            newFolderNameTB.Click += new EventHandler(NewFolderNameTB_Click);
            newFolderNameTB.TextChanged += new EventHandler(NewFolderNameTB_TextChanged);
            _MainForm.Controls.Add(newFolderNameTB);

            numFilesToCopyTB = new TextBox();
            numFilesToCopyTB.Click += new EventHandler(NumFilesToCopyTB_Click);
            numFilesToCopyTB.TextChanged += new EventHandler(NumFilesToCopyTB_TextChanged);
            _MainForm.Controls.Add(numFilesToCopyTB);

            copyToLocButton = new Button();
            copyToLocButton.Click += new EventHandler(CopyToLocButton_Click);
            _MainForm.Controls.Add(copyToLocButton);

            versionToCopyCombo = new ComboBox();
            versionToCopyCombo.SelectedValueChanged += new EventHandler(VersionToCopyCombo_ValueChanged);
            _MainForm.Controls.Add(versionToCopyCombo);

            copyToLocBrowser = new FolderBrowserDialog();
        }
        #endregion
        #region Private Methods
        private void SetTabIndex()
        {
            copyToLocTB.TabIndex = 0;
            copyToLocButton.TabIndex = 1;
            createFolderCheck.TabIndex = 2;
            appendDateCheck.TabIndex = 3;
            newFolderNameTB.TabIndex = 4;
            versionToCopyCombo.TabIndex = 5;
            numFilesToCopyTB.TabIndex = 6;
            overwriteCheck.TabIndex = 7;
        }

        private void ClearFormatting(ref TextBox _TextBox)
        {
            Font textBoxFont = _TextBox.Font;
            textBoxFont = new Font(textBoxFont, FontStyle.Regular);
            _TextBox.ForeColor = Color.Black;
            _TextBox.Font = textBoxFont;
        }        

        private void UpdatePreviewText()
        {
            if (createFolderCheck.Checked)
            {
                if (appendDateCheck.Checked)
                {
                    _namePreviewLabel.Text = newFolderNameTB.Text + "_" + DateTime.Today.ToString("dd_MM_yyyy");
                    SetPreviewLabelSize();
                }
                else
                {
                    _namePreviewLabel.Text = newFolderNameTB.Text;
                    SetPreviewLabelSize();
                }
            }
            else
            {
                _namePreviewLabel.Text = "Folder not being created.";
                SetPreviewLabelSize();
            }
        }

        private void LaunchExplorer()
        {
            copyToLocBrowser.ShowDialog();
            ClearFormatting(ref copyToLocTB);
            copyToLocTB.Text = copyToLocBrowser.SelectedPath;
        }
        #endregion
        #region Event Methods
        private void CopyToLocTB_Click(object sender, EventArgs e)
        {
            if (copyToLocTB.Text == "Copy-To Location")
            {
                copyToLocTB.Text = "";
                ClearFormatting(ref copyToLocTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                copyToLocTB.SelectAll();
            }
        }

        private void CopyToLocTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref copyToLocTB);
            _MainForm.currdatabase.CopyToLocation = copyToLocTB.Text;
            _MainForm.IsSaved = false;
        }

        private void CopyToLocButton_Click(object sender, EventArgs e)
        {
            LaunchExplorer();
        }

        private void CreateFolderCheck_CheckChanged(object sender, EventArgs e)
        {
            if (createFolderCheck.Checked == false)
            {
                newFolderNameTB.Enabled = false;
                _MainForm.currdatabase.CreateNewFolder = "Yes";
            }
            else
            {
                newFolderNameTB.Enabled = true;
                _MainForm.currdatabase.CreateNewFolder = "No";
            }

            UpdatePreviewText();
            _MainForm.IsSaved = false;
        }

        private void AppendDateCheck_CheckChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
            if (appendDateCheck.Checked)
                _MainForm.currdatabase.AppendDateToFolderName = "Yes";
            else if (!appendDateCheck.Checked)
                _MainForm.currdatabase.AppendDateToFolderName = "No";
            _MainForm.IsSaved = false;
        }

        private void NewFolderNameTB_Click(object sender, EventArgs e)
        {
            if (newFolderNameTB.Text == "New Folder Name")
            {
                newFolderNameTB.Text = "";
                ClearFormatting(ref newFolderNameTB);
            }
            else
            {
                newFolderNameTB.SelectAll();
            }
            _MainForm.IsSaved = false;
        }

        private void NewFolderNameTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref newFolderNameTB);
            _MainForm.currdatabase.NewFolderName = newFolderNameTB.Text;
            UpdatePreviewText();
            _MainForm.IsSaved = false;
        }

        private void VersionToCopyCombo_ValueChanged(object sender, EventArgs e)
        {
            if (versionToCopyCombo.SelectedItem.ToString() == "All")
            {
                numFilesToCopyTB.Enabled = false;
            }
            else
            {
                numFilesToCopyTB.Enabled = true;
            }
            _MainForm.currdatabase.VersionsToCopy = versionToCopyCombo.SelectedValue.ToString();
            _MainForm.IsSaved = false;
        }

        private void NumFilesToCopyTB_Click(object sender, EventArgs e)
        {
            if (numFilesToCopyTB.Text == "Number of Files to Copy")
            {
                numFilesToCopyTB.Text = "";
                ClearFormatting(ref numFilesToCopyTB);
            }
            else
            {
                numFilesToCopyTB.SelectAll();
            }
            _MainForm.IsSaved = false;
        }

        private void NumFilesToCopyTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref numFilesToCopyTB);
            _MainForm.currdatabase.AmntToCopy = numFilesToCopyTB.Text;
            _MainForm.IsSaved = false;
        }

        private void OverwriteExistingCheck_CheckChanged(object sender, EventArgs e)
        {
            if (overwriteCheck.Checked)
                _MainForm.currdatabase.OverwriteFiles = "Yes";
            else if (!overwriteCheck.Checked)
                _MainForm.currdatabase.OverwriteFiles = "No";
            _MainForm.IsSaved = false;
        }
        #endregion
    }
}
