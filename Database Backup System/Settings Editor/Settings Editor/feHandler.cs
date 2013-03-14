using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Settings_Editor
{
    class feHandler
    {
        #region Controls
        private Label extOneLabel;
        private Label extTwoLabel;
        private TextBox extOneTB;
        private TextBox extTwoTB;

        private mainForm _MainForm;
        #endregion
        #region Accessors
        public TextBox ExtOneTB
        {
            get { return extOneTB; }
            set { extOneTB = value; }
        }

        public TextBox ExtTwoTB
        {
            get { return extTwoTB; }
            set { extTwoTB = value; }
        }
        #endregion
        #region Constructors
        public feHandler(Point feBoxLoc, Size feBoxSize, mainForm _mainForm)
        {
            _MainForm = _mainForm;
            InitializeControls(); 
            ConstructControls(feBoxLoc, feBoxSize, _mainForm);                 
        }

        public feHandler()
        {

        }
        #endregion
        #region Public Methods
        public void ConstructControls(Point feBoxLoc, Size feBoxSize, Form mainForm)
        {
            #region extOneLabel
            extOneLabel.Name = "extOneLabel";
            extOneLabel.Text = "Extension One";
            extOneLabel.Location = new Point(feBoxLoc.X + 10, feBoxLoc.Y + 18);
            extOneLabel.Size = new Size((int)(extOneLabel.Text.Length * (extOneLabel.Font.SizeInPoints * 0.75)),
                                        (int)(extOneLabel.Font.SizeInPoints * 3));
            extOneLabel.BackColor = Color.OldLace;
            extOneLabel.Visible = false;
            extOneLabel.BringToFront();
            extOneLabel.Invalidate();
            #endregion
            #region extOneTB
            extOneTB.Name = "extOneTB";
            extOneTB.Text = ".ext";
            extOneTB.Location = new Point(extOneLabel.Right + 10, feBoxLoc.Y + 16);
            extOneTB.Size = new Size((int)(extOneTB.Font.SizeInPoints * 4), (int)(extOneTB.Font.SizeInPoints * 2));
            extOneTB.ForeColor = Color.Gray;
            extOneTB.Font = new Font(extOneTB.Font, FontStyle.Italic);
            extOneTB.Visible = false;
            extOneTB.BringToFront();
            extOneTB.Invalidate();
            #endregion
            #region extTwoLabel
            extTwoLabel.Name = "extTwoLabel";
            extTwoLabel.Text = "Extension Two";
            extTwoLabel.Location = new Point(extOneTB.Right + 10, feBoxLoc.Y + 18);
            extTwoLabel.Size = new Size((int)(extTwoLabel.Text.Length * (extTwoLabel.Font.SizeInPoints * 0.75)),
                                        (int)(extTwoLabel.Font.SizeInPoints * 3));
            extTwoLabel.BackColor = Color.OldLace;
            extTwoLabel.Visible = false;
            extTwoLabel.BringToFront();
            extTwoLabel.Invalidate();
            #endregion
            #region extTwoTB
            extTwoTB.Name = "extTwoTB";
            extTwoTB.Text = ".ext";
            extTwoTB.Location = new Point(extTwoLabel.Right + 10, feBoxLoc.Y + 16);
            extTwoTB.Size = new Size((int)(extTwoTB.Font.SizeInPoints * 4), (int)(extTwoTB.Font.SizeInPoints * 2));
            extTwoTB.ForeColor = Color.Gray;
            extTwoTB.Font = new Font(extTwoTB.Font, FontStyle.Italic);
            extTwoTB.Visible = false;
            extTwoTB.BringToFront();
            extTwoTB.Invalidate();
            #endregion

            SetTabIndex();       
        }

        public void MoveControls(int distance, ref MenuStrip menuStrip)
        {
            menuStrip.BringToFront();

            extOneLabel.Location = new Point(extOneLabel.Location.X, extOneLabel.Location.Y + distance);
            extTwoLabel.Location = new Point(extTwoLabel.Location.X, extTwoLabel.Location.Y + distance);
            extOneTB.Location = new Point(extOneTB.Location.X, extOneTB.Location.Y + distance);
            extTwoTB.Location = new Point(extTwoTB.Location.X, extTwoTB.Location.Y + distance);
        }

        public void SetVisibility(bool value)
        {
            extOneLabel.Visible = value;
            extTwoLabel.Visible = value;
            extOneTB.Visible = value;
            extTwoTB.Visible = value;
        }

        public void SetInitFocus()
        {
            extOneTB.Focus();
        }
        public void InitializeControls()
        {
            extOneLabel = new Label();
            _MainForm.Controls.Add(extOneLabel);

            extTwoLabel = new Label();
            _MainForm.Controls.Add(extTwoLabel);

            extOneTB = new TextBox();
            extOneTB.Click += new EventHandler(ExtOneTB_Click);
            extOneTB.TextChanged += new EventHandler(ExtOneTB_TextChanged);
            _MainForm.Controls.Add(extOneTB);

            extTwoTB = new TextBox();
            extTwoTB.Click += new EventHandler(ExtTwoTB_Click);
            extTwoTB.TextChanged += new EventHandler(ExtTwoTB_TextChanged);
            _MainForm.Controls.Add(extTwoTB);
        }
        #endregion
        #region Private Methods
        private void SetTabIndex()
        {
            extOneTB.TabIndex = 0;
            extTwoTB.TabIndex = 1;
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
        private void ExtOneTB_Click(object sender, EventArgs e)
        {
            if (extOneTB.Text == ".ext")
            {
                extOneTB.Text = "";
                ClearFormatting(ref extOneTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                extOneTB.SelectAll();
            }
        }

        private void ExtOneTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref extOneTB);
            if (_MainForm.currdatabase.ExtensionList.Count > 0)
                _MainForm.currdatabase.ExtensionList[0] = extOneTB.Text;
            else if (_MainForm.currdatabase.ExtensionList.Count < 1)
                _MainForm.currdatabase.ExtensionList.Add(extOneTB.Text);
            _MainForm.IsSaved = false;
        }

        private void ExtTwoTB_Click(object sender, EventArgs e)
        {
            if (extTwoTB.Text == ".ext")
            {
                extTwoTB.Text = "";
                ClearFormatting(ref extTwoTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                extTwoTB.SelectAll();
            }
        }

        private void ExtTwoTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref extTwoTB);
            if (_MainForm.currdatabase.ExtensionList.Count > 1)
                _MainForm.currdatabase.ExtensionList[1] = extTwoTB.Text;
            else if (_MainForm.currdatabase.ExtensionList.Count < 2)
                _MainForm.currdatabase.ExtensionList.Add(extTwoTB.Text);
            _MainForm.IsSaved = false;
        }
        #endregion
    }
}
