using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Settings_Editor
{
    class dbiHandler
    {
        ///
        ///Here we handle all the database information stuff. 
        ///This is the stuff that will appear in the database information box.
        ///

        #region Global Control Variables
        //Global variables, this is where all of our items in the box are going to be declared
        //so that we can deconstruct and reconstruct them when this class is instantiated

        private Button dbFolderLocButton;
        private Button sampleFileLocButton;
        private CheckBox compressAsOneCheck;
        private CheckBox fileDateSepCheck;
        private ComboBox dateFormatCombo;
        private FolderBrowserDialog dbFolderBrowser;
        private Label dbLocLabel;
        private Label numFileExtLabel;
        private Label compressAsOneLabel;
        private Label dateFormatLabel;
        private Label sampleFilePathLabel;
        private Label fileDateStartLabel;
        private Label fileDateLengthLabel;
        private Label fileDateSepLabel;
        private Label monthLocLabel;
        private Label monthLabel;
        private Label dayLocLabel;
        private Label dayLabel;
        private Label yearLocLabel;
        private Label yearLabel;
        private OpenFileDialog sampleFileBrowser;
        private TextBox dbLocTB;
        private TextBox numFileExtTB;
        private TextBox sampleFilePathTB;
        private TextBox fileDateStartTB;
        private TextBox fileDateLengthTB;
        private TextBox monthTB;
        private TextBox dayTB;
        private TextBox yearTB;

        private mainForm _MainForm;

        #endregion
        #region Accessors       
        
        public TextBox DBLocTB
        {
            get { return dbLocTB; } 
            set { dbLocTB = value; }
        }

        public CheckBox CompressAsOneCheck
        {
            get { return compressAsOneCheck; }
            set { compressAsOneCheck = value; }
        }

        public CheckBox FileDateSepCheck
        {
            get { return fileDateSepCheck; }
            set { fileDateSepCheck = value; }
        }

        public TextBox NumFileExtTB
        {
            get { return numFileExtTB; }
            set { numFileExtTB = value; } 
        }

        public TextBox SampleFilePathTB
        {
            get { return sampleFilePathTB; }
            set { sampleFilePathTB = value; }
        }

        public TextBox FileDateStartTB
        {
            get { return fileDateStartTB; }
            set { fileDateStartTB = value; }
        }

        public TextBox FileDateLengthTB
        {
            get { return fileDateLengthTB; }
            set { fileDateLengthTB = value; }
        }

        public TextBox MonthTB
        {
            get { return monthTB; }
            set { monthTB = value; }
        }

        public TextBox DayTB
        {
            get { return dayTB; }
            set { dayTB = value; }
        }

        public TextBox YearTB
        {
            get { return yearTB; }
            set { yearTB = value; }
        }
        #endregion
        #region Public Methods
        public dbiHandler(Point dbBoxLocation, Size dbBoxSize, mainForm mainForm)
        {
            _MainForm = mainForm;
            InitializeControls();
            SetTabIndex();
            ConstructControls(dbBoxLocation, dbBoxSize, mainForm);
        }
        public dbiHandler()
        {
            
        }
        public void SetVisibility(bool value)
        {
            dbLocLabel.Visible = value;
            dbLocTB.Visible = value;
            dbFolderLocButton.Visible = value;
            sampleFileLocButton.Visible = value;
            compressAsOneCheck.Visible = value;
            fileDateSepCheck.Visible = value;
            dateFormatCombo.Visible = value;
            dbLocLabel.Visible = value;
            numFileExtLabel.Visible = value;
            compressAsOneLabel.Visible = value;
            dateFormatLabel.Visible = value;
            sampleFilePathLabel.Visible = value;
            fileDateStartLabel.Visible = value;
            fileDateLengthLabel.Visible = value;
            fileDateSepLabel.Visible = value;
            monthLocLabel.Visible = value;
            monthLabel.Visible = value;
            dayLocLabel.Visible = value;
            dayLabel.Visible = value;
            yearLocLabel.Visible = value;
            yearLabel.Visible = value;
            numFileExtTB.Visible = value;
            sampleFilePathTB.Visible = value;
            fileDateStartTB.Visible = value;
            fileDateLengthTB.Visible = value;
            monthTB.Visible = value;
            dayTB.Visible = value;
            yearTB.Visible = value;
        }
        public void SetFocus_dbLocTB()
        {
            dbLocTB.Focus();
        }
        public void ConstructControls(Point dbBoxLoc, Size dbBoxSize, Form mainForm)
        {
            //Initialize the controls with their basic data:
            //Name, Size, Location, Text/Data (if applicable) and any Restraints (TextBoxes)
            #region dbLocLabel
            dbLocLabel.Name = "dbLocLabel";
            dbLocLabel.Text = "Database Location:";
            dbLocLabel.Location = new Point(dbBoxLoc.X + 10, dbBoxLoc.Y + 18); //10 below header, 18 to the right of the border
            dbLocLabel.Size = new Size((int)(dbLocLabel.Text.Length * (dbLocLabel.Font.SizeInPoints * 0.75)), (int)dbLocLabel.Font.Size + 4);
            dbLocLabel.BackColor = Color.OldLace;
            dbLocLabel.Visible = false;
            dbLocLabel.BringToFront();
            dbLocLabel.Invalidate();
            #endregion
            #region dbFolderLocButton
            dbFolderLocButton.Name = "dbFolderLocButton";
            dbFolderLocButton.Text = "...";
            dbFolderLocButton.Size = new Size((int)(dbFolderLocButton.Text.Length * (dbFolderLocButton.Font.Size)),
                                              (int)(dbFolderLocButton.Font.SizeInPoints * 3));
            dbFolderLocButton.Location = new Point(((dbBoxSize.Width + dbBoxLoc.X) - dbFolderLocButton.Width) - 10,
                                                    dbBoxLoc.Y + 14);
            dbFolderLocButton.Visible = false;
            dbFolderLocButton.BringToFront();
            dbFolderLocButton.Invalidate();            
            #endregion
            #region dbLocTB
            dbLocTB.Name = "dbLocTB";
            dbLocTB.Text = "Path to Database";
            dbLocTB.Font = new Font(dbLocTB.Font, FontStyle.Italic);
            dbLocTB.ForeColor = Color.Gray;
            dbLocTB.Location = new Point((dbLocLabel.Width + dbLocLabel.Location.X), dbBoxLoc.Y + 16); //16 to line up text with the label
            dbLocTB.Size = new Size((dbFolderLocButton.Location.X - dbLocTB.Location.X) - 10, (int)dbLocTB.Font.SizeInPoints + 4);
            dbLocTB.Visible = false;
            dbLocTB.BringToFront();
            dbLocTB.Invalidate();
            #endregion
            #region numFileExtLabel
            numFileExtLabel.Name = "numFileExtLabel";
            numFileExtLabel.Text = "Number of File Extensions:";
            numFileExtLabel.Size = new Size((int)(numFileExtLabel.Text.Length * (numFileExtLabel.Font.SizeInPoints * 0.65)),
                                            (int)(numFileExtLabel.Font.SizeInPoints + 4));
            numFileExtLabel.Location = new Point(dbBoxLoc.X + 10, (dbLocTB.Bottom + 10));
            numFileExtLabel.BackColor = Color.OldLace;
            numFileExtLabel.Visible = false;
            numFileExtLabel.BringToFront();
            numFileExtLabel.Invalidate();
            #endregion
            #region numFileExtTB
            numFileExtTB.Name = "numFileExtTB";
            numFileExtTB.Text = "Number of File Extensions";
            numFileExtTB.Font = new Font(numFileExtTB.Font, FontStyle.Italic);
            numFileExtTB.ForeColor = Color.Gray;
            numFileExtTB.Size = new Size((int)(numFileExtTB.Text.Length * (dbLocTB.Font.SizeInPoints * 0.75)), (int)(dbLocTB.Font.SizeInPoints + 4)); //Display Text
            numFileExtTB.Location = new Point((numFileExtLabel.Right), (dbLocTB.Bottom + 8));
            numFileExtTB.Visible = false;
            numFileExtTB.BringToFront();
            numFileExtTB.Invalidate();
            #endregion
            #region compressAsOneLabel
            compressAsOneLabel.Name = "compressAsOneLabel";
            compressAsOneLabel.Text = "Compress Files As One:";
            compressAsOneLabel.Size = new Size((int)(compressAsOneLabel.Font.SizeInPoints * (compressAsOneLabel.Text.Length * 0.75)),
                                                (int)(compressAsOneLabel.Font.SizeInPoints + 4));
            compressAsOneLabel.Location = new Point((numFileExtTB.Right + 11), (dbLocTB.Bottom + 11));
            compressAsOneLabel.Visible = false;
            compressAsOneLabel.BackColor = Color.OldLace;
            compressAsOneLabel.BringToFront();
            compressAsOneLabel.Invalidate();
            #endregion
            #region compressAsOneCheck
            compressAsOneCheck.Name = "compressAsOneCheck";
            compressAsOneCheck.Checked = true;
            compressAsOneCheck.Location = new Point(compressAsOneLabel.Right, dbLocTB.Bottom + 11);
            compressAsOneCheck.Size = new Size(15, 15); //15,15 to make the "clickable area" roughly the same size as the box
            compressAsOneCheck.BackColor = Color.OldLace;
            compressAsOneCheck.Visible = false;
            compressAsOneCheck.BringToFront();
            compressAsOneCheck.Invalidate();
            #endregion
            #region dateFormatLabel
            dateFormatLabel.Name = "dateFormatLabel";
            dateFormatLabel.Text = "Date Format:";
            dateFormatLabel.Size = new Size((int)(dateFormatLabel.Font.SizeInPoints * (dateFormatLabel.Text.Length * 0.75)),
                                            (int)(dateFormatLabel.Font.SizeInPoints + 4));
            dateFormatLabel.Location = new Point(dbBoxLoc.X + 10, numFileExtTB.Bottom + 10);
            dateFormatLabel.BackColor = Color.OldLace;
            dateFormatLabel.Visible = false;
            dateFormatLabel.BringToFront();
            dateFormatLabel.Invalidate();
            #endregion
            #region dateFormatCombo
            dateFormatCombo.Name = "dateFormatCombo";
            dateFormatCombo.Location = new Point(dateFormatLabel.Right + 10, numFileExtTB.Bottom + 8);
            dateFormatCombo.Text = "Select Format";
            dateFormatCombo.Visible = false;
            dateFormatCombo.BringToFront();
            dateFormatCombo.Invalidate();
            #endregion
            #region sampleFilePathLabel
            sampleFilePathLabel.Name = "sampleFilePathLabel";
            sampleFilePathLabel.Text = "Sample File Path:";
            sampleFilePathLabel.Size = new Size((int)(sampleFilePathLabel.Font.SizeInPoints * dateFormatLabel.Text.Length),
                                                (int)(sampleFilePathLabel.Font.SizeInPoints + 4));
            sampleFilePathLabel.Location = new Point(dbBoxLoc.X + 10, dateFormatCombo.Bottom + 10);
            sampleFilePathLabel.BackColor = Color.OldLace;
            sampleFilePathLabel.Visible = false;
            sampleFilePathLabel.BringToFront();
            sampleFilePathLabel.Invalidate();
            #endregion
            #region sampleFileLocButton
            sampleFileLocButton.Name = "sampleFileLocButton";
            sampleFileLocButton.Text = "...";
            sampleFileLocButton.Size = new Size((int)(sampleFileLocButton.Text.Length * (sampleFileLocButton.Font.Size)),
                                              (int)(sampleFileLocButton.Font.SizeInPoints * 3));
            sampleFileLocButton.Location = new Point(((dbBoxLoc.X + dbBoxSize.Width) - (sampleFileLocButton.Width + 10)), dateFormatCombo.Bottom + 8);
            sampleFileLocButton.Visible = false;
            sampleFileLocButton.BringToFront();
            sampleFileLocButton.Invalidate();
            #endregion
            #region sampleFilePathTB
            sampleFilePathTB.Name = "sampleFilePathTB";
            sampleFilePathTB.Text = "Sample File Path";
            sampleFilePathTB.Font = new Font(sampleFilePathTB.Font, FontStyle.Italic);
            sampleFilePathTB.ForeColor = Color.Gray;
            sampleFilePathTB.Location = new Point(sampleFilePathLabel.Right + 10, dateFormatCombo.Bottom + 10);
            sampleFilePathTB.Size = new Size((sampleFileLocButton.Location.X - sampleFilePathTB.Location.X) - 10, (int)sampleFilePathTB.Font.SizeInPoints + 4);
            sampleFilePathTB.Visible = false;
            sampleFilePathTB.BringToFront();
            sampleFilePathTB.Invalidate();
            #endregion
            #region fileDateStartLabel
            fileDateStartLabel.Name = "fileDateStartLabel";
            fileDateStartLabel.Text = "File Date Start Location:";
            fileDateStartLabel.Location = new Point(dbBoxLoc.X + 10, sampleFilePathTB.Bottom + 10);
            fileDateStartLabel.Size = new Size(((int)(fileDateStartLabel.Font.SizeInPoints * 0.65) * fileDateStartLabel.Text.Length),
                                                (int)fileDateStartLabel.Font.SizeInPoints + 4);
            fileDateStartLabel.BackColor = Color.OldLace;
            fileDateStartLabel.Visible = false;
            fileDateStartLabel.BringToFront();
            fileDateStartLabel.Invalidate();
            #endregion
            #region fileDateStartTB
            fileDateStartTB.Name = "fileDateStartTB";
            fileDateStartTB.Text = "00";
            fileDateStartTB.Location = new Point(fileDateStartLabel.Right + 10, sampleFilePathTB.Bottom + 10);
            fileDateStartTB.Size = new Size((int)(fileDateStartTB.Text.Length * (fileDateStartTB.Font.SizeInPoints * 2)),
                                            (int)(fileDateStartTB.Font.SizeInPoints + 4));
            fileDateStartTB.Font = new Font(fileDateStartTB.Font, FontStyle.Italic);
            fileDateStartTB.ForeColor = Color.Gray;
            fileDateStartTB.Visible = false;
            fileDateStartTB.BringToFront();
            fileDateStartTB.Invalidate();
            #endregion
            #region monthTB
            monthTB.Name = "monthTB";
            monthTB.Text = "MM";
            monthTB.Size = new Size((int)(monthTB.Font.SizeInPoints * monthTB.Text.Length * 2.25), (int)(monthTB.Font.SizeInPoints + 4));
            monthTB.Location = new Point(((dbBoxSize.Width + dbBoxLoc.X) - monthTB.Width) - 10, sampleFilePathTB.Bottom + 10);
            monthTB.Font = new Font(monthTB.Font, FontStyle.Italic);
            monthTB.ForeColor = Color.Gray;
            monthTB.Visible = false;
            monthTB.BringToFront();
            monthTB.Invalidate();
            #endregion
            #region monthLabel
            monthLabel.Name = "monthLabel";
            monthLabel.Text = "Month";
            monthLabel.Size = new Size((int)monthLabel.Font.SizeInPoints * monthLabel.Text.Length,
                                        (int)monthLabel.Font.SizeInPoints + 4);
            monthLabel.Location = new Point(monthTB.Left - monthLabel.Width - 10, sampleFilePathTB.Bottom + 10);
            monthLabel.BackColor = Color.OldLace;
            monthLabel.Visible = false;
            monthLabel.BringToFront();
            monthLabel.Invalidate();
            #endregion
            #region fileDateLengthLabel
            fileDateLengthLabel.Name = "fileDateLengthLabel";
            fileDateLengthLabel.Text = "File Date Length";
            fileDateLengthLabel.Location = new Point(dbBoxLoc.X + 10, fileDateStartTB.Bottom + 10);
            fileDateLengthLabel.Size = new Size(((int)(fileDateLengthLabel.Font.SizeInPoints * fileDateLengthLabel.Text.Length * 0.75)),
                                                (int)fileDateLengthLabel.Font.SizeInPoints + 8);
            fileDateLengthLabel.BackColor = Color.OldLace;
            fileDateLengthLabel.Visible = false;
            fileDateLengthLabel.BringToFront();
            fileDateLengthLabel.Invalidate();
            #endregion
            #region fileDateLengthTB
            fileDateLengthTB.Name = "fileDateLengthTB";
            fileDateLengthTB.Text = "00";
            fileDateLengthTB.Location = new Point(fileDateLengthLabel.Right + 10, fileDateStartTB.Bottom + 10);
            fileDateLengthTB.Size = new Size((int)(fileDateLengthTB.Text.Length * (fileDateLengthTB.Font.SizeInPoints * 2)),
                                             (int)(fileDateLengthTB.Font.SizeInPoints + 4));
            fileDateLengthTB.Font = new Font(fileDateLengthTB.Font, FontStyle.Italic);
            fileDateLengthTB.ForeColor = Color.Gray;
            fileDateLengthTB.Visible = false;
            fileDateLengthTB.BringToFront();
            fileDateLengthTB.Invalidate();
            #endregion
            #region dayTB
            dayTB.Name = "dayTB";
            dayTB.Text = "DD";
            dayTB.Size = new Size((int)(dayTB.Text.Length * (dayTB.Font.SizeInPoints) * 2.25), (int)(dayTB.Font.SizeInPoints + 4));
            dayTB.Location = new Point(((dbBoxSize.Width + dbBoxLoc.X) - dayTB.Width) - 10, fileDateStartTB.Bottom + 10);
            dayTB.Font = new Font(dayTB.Font, FontStyle.Italic);
            dayTB.ForeColor = Color.Gray;
            dayTB.Visible = false;
            dayTB.BringToFront();
            dayTB.Invalidate();
            #endregion
            #region dayLabel
            dayLabel.Name = "dayLabel";
            dayLabel.Text = "Day";
            dayLabel.Size = new Size((int)(dayLabel.Font.SizeInPoints * dayLabel.Text.Length * 1.5),
                                     (int)dayLabel.Font.SizeInPoints + 8);
            dayLabel.Location = new Point(monthTB.Left - monthLabel.Width, fileDateStartTB.Bottom + 10);
            dayLabel.BackColor = Color.OldLace;
            dayLabel.Visible = false;
            dayLabel.BringToFront();
            dayLabel.Invalidate();
            #endregion
            #region fileDateSepLabel
            fileDateSepLabel.Name = "fileDateSepLabel";
            fileDateSepLabel.Text = "Is the date seperated?";
            fileDateSepLabel.Location = new Point(dbBoxLoc.X + 10, fileDateLengthTB.Bottom + 10);
            fileDateSepLabel.Size = new Size(((int)(fileDateSepLabel.Font.SizeInPoints * fileDateSepLabel.Text.Length * 0.75)),
                                             (int)fileDateSepLabel.Font.SizeInPoints + 4);
            fileDateSepLabel.BackColor = Color.OldLace;
            fileDateSepLabel.Visible = false;
            fileDateSepLabel.BringToFront();
            fileDateSepLabel.Invalidate();
            #endregion
            #region fileDateSepCheck
            fileDateSepCheck.Name = "fileDateSepCheck";
            fileDateSepCheck.Checked = true;
            fileDateSepCheck.Location = new Point(fileDateSepLabel.Right + 10, fileDateLengthTB.Bottom + 10);
            fileDateSepCheck.Size = new Size(15, 15); //15,15 to make the "clickable area" roughly the same size as the box
            fileDateSepCheck.Visible = false;
            fileDateSepCheck.BringToFront();
            fileDateSepCheck.Invalidate();
            #endregion
            #region yearTB
            yearTB.Name = "yearTB";
            yearTB.Text = "YYYY";
            yearTB.Size = new Size((int)(yearTB.Font.SizeInPoints * 4 * 1.15), (int)(yearTB.Font.SizeInPoints + 4));    //we multiply by four so that the box is not resized if the text is changed to "YY"
            yearTB.Location = new Point(((dbBoxSize.Width + dbBoxLoc.X) - yearTB.Width) - 10,
                                                    fileDateLengthTB.Bottom + 10);
            yearTB.Font = new Font(yearTB.Font, FontStyle.Italic);
            yearTB.ForeColor = Color.Gray;
            yearTB.Visible = false;
            yearTB.BringToFront();
            yearTB.Invalidate();
            #endregion
            #region yearLabel
            yearLabel.Name = "yearLabel";
            yearLabel.Text = "Year";
            yearLabel.Size = new Size((int)yearLabel.Font.SizeInPoints * yearLabel.Text.Length,
                                      (int)yearLabel.Font.SizeInPoints + 4);
            yearLabel.Location = new Point(monthTB.Left - monthLabel.Width, fileDateLengthTB.Bottom + 10);
            yearLabel.BackColor = Color.OldLace;
            yearLabel.Visible = false;
            yearLabel.BringToFront();
            yearLabel.Invalidate();
            #endregion
        }
        public void MoveControls(int distance, ref MenuStrip menuStrip)
        {
            menuStrip.BringToFront();

            dbFolderLocButton.Location = new Point(dbFolderLocButton.Location.X, dbFolderLocButton.Location.Y + distance);
            sampleFileLocButton.Location = new Point(sampleFileLocButton.Location.X, sampleFileLocButton.Location.Y + distance);
            compressAsOneCheck.Location = new Point(compressAsOneCheck.Location.X, compressAsOneCheck.Location.Y + distance);
            fileDateSepCheck.Location = new Point(fileDateSepCheck.Location.X, fileDateSepCheck.Location.Y + distance);
            dateFormatCombo.Location = new Point(dateFormatCombo.Location.X, dateFormatCombo.Location.Y + distance);
            dbLocLabel.Location = new Point(dbLocLabel.Location.X, dbLocLabel.Location.Y + distance);
            numFileExtLabel.Location = new Point(numFileExtLabel.Location.X, numFileExtLabel.Location.Y + distance);
            compressAsOneLabel.Location = new Point(compressAsOneLabel.Location.X, compressAsOneLabel.Location.Y + distance);
            dateFormatLabel.Location = new Point(dateFormatLabel.Location.X, dateFormatLabel.Location.Y + distance);
            sampleFilePathLabel.Location = new Point(sampleFilePathLabel.Location.X, sampleFilePathLabel.Location.Y + distance);
            fileDateStartLabel.Location = new Point(fileDateStartLabel.Location.X, fileDateStartLabel.Location.Y + distance);
            fileDateLengthLabel.Location = new Point(fileDateLengthLabel.Location.X, fileDateLengthLabel.Location.Y + distance);
            fileDateSepLabel.Location = new Point(fileDateSepLabel.Location.X, fileDateSepLabel.Location.Y + distance);
            monthLocLabel.Location = new Point(monthLocLabel.Location.X, monthLocLabel.Location.Y + distance);
            monthLabel.Location = new Point(monthLabel.Location.X, monthLabel.Location.Y + distance);
            dayLocLabel.Location = new Point(dayLocLabel.Location.X, dayLocLabel.Location.Y + distance);
            dayLabel.Location = new Point(dayLabel.Location.X, dayLabel.Location.Y + distance);
            yearLocLabel.Location = new Point(yearLocLabel.Location.X, yearLocLabel.Location.Y + distance);
            yearLabel.Location = new Point(yearLabel.Location.X, yearLabel.Location.Y + distance);
            dbLocTB.Location = new Point(dbLocTB.Location.X, dbLocTB.Location.Y + distance);
            numFileExtTB.Location = new Point(numFileExtTB.Location.X, numFileExtTB.Location.Y + distance);
            sampleFilePathTB.Location = new Point(sampleFilePathTB.Location.X, sampleFilePathTB.Location.Y + distance);
            fileDateStartTB.Location = new Point(fileDateStartTB.Location.X, fileDateStartTB.Location.Y + distance);
            fileDateLengthTB.Location = new Point(fileDateLengthTB.Location.X, fileDateLengthTB.Location.Y + distance);
            monthTB.Location = new Point(monthTB.Location.X, monthTB.Location.Y + distance);
            dayTB.Location = new Point(dayTB.Location.X, dayTB.Location.Y + distance);
            yearTB.Location = new Point(yearTB.Location.X, yearTB.Location.Y + distance);
        }
        public void AddDateFormatData()
        {
            //Month First -- Long
            dateFormatCombo.Items.Add("MM/DD/YYYY");
            dateFormatCombo.Items.Add("MM/YYYY/DD");
            //Month First -- Short
            dateFormatCombo.Items.Add("MM/DD/YY");
            dateFormatCombo.Items.Add("MM/YY/DD");
            //Day First -- Long
            dateFormatCombo.Items.Add("DD/MM/YYYY");
            dateFormatCombo.Items.Add("DD/YYYY/MM");
            //Day First -- Short
            dateFormatCombo.Items.Add("DD/MM/YY");
            dateFormatCombo.Items.Add("DD/YY/MM");
            //Year First -- Long
            dateFormatCombo.Items.Add("YYYY/DD/MM");
            dateFormatCombo.Items.Add("YYYY/MM/DD");
            //Year First -- Short
            dateFormatCombo.Items.Add("YY/DD/MM");
            dateFormatCombo.Items.Add("YY/MM/DD");
        }
        public void InitializeControls()
        {
            dbFolderLocButton = new Button();
            dbFolderLocButton.Click += new EventHandler(dbLocButton_Click);
            _MainForm.Controls.Add(dbFolderLocButton);

            sampleFileLocButton = new Button();
            sampleFileLocButton.Click += new EventHandler(sampleFileLocButton_Click);
            _MainForm.Controls.Add(sampleFileLocButton);

            compressAsOneCheck = new CheckBox();
            _MainForm.Controls.Add(compressAsOneCheck);

            fileDateSepCheck = new CheckBox();
            _MainForm.Controls.Add(fileDateSepCheck);

            dateFormatCombo = new ComboBox();
            _MainForm.Controls.Add(dateFormatCombo);

            dbFolderBrowser = new FolderBrowserDialog();

            dbLocLabel = new Label();
            _MainForm.Controls.Add(dbLocLabel);

            numFileExtLabel = new Label();
            _MainForm.Controls.Add(numFileExtLabel);

            compressAsOneLabel = new Label();
            _MainForm.Controls.Add(compressAsOneLabel);

            dateFormatLabel = new Label();
            _MainForm.Controls.Add(dateFormatLabel);

            sampleFilePathLabel = new Label();
            _MainForm.Controls.Add(sampleFilePathLabel);

            fileDateStartLabel = new Label();
            _MainForm.Controls.Add(fileDateStartLabel);

            fileDateLengthLabel = new Label();
            _MainForm.Controls.Add(fileDateLengthLabel);

            fileDateSepLabel = new Label();
            _MainForm.Controls.Add(fileDateSepLabel);

            monthLocLabel = new Label();
            _MainForm.Controls.Add(monthLocLabel);

            monthLabel = new Label();
            _MainForm.Controls.Add(monthLabel);

            dayLocLabel = new Label();
            _MainForm.Controls.Add(dayLocLabel);

            dayLabel = new Label();
            _MainForm.Controls.Add(dayLabel);

            yearLocLabel = new Label();
            _MainForm.Controls.Add(yearLocLabel);

            yearLabel = new Label();
            _MainForm.Controls.Add(yearLabel);

            sampleFileBrowser = new OpenFileDialog();

            dbLocTB = new TextBox();
            dbLocTB.Click += new EventHandler(dbLocTB_Click);
            dbLocTB.TextChanged += new EventHandler(dbLocTB_TextChanged);
            _MainForm.Controls.Add(dbLocTB);

            numFileExtTB = new TextBox();
            numFileExtTB.Click += new EventHandler(numFileExtTB_Click);
            numFileExtTB.TextChanged += new EventHandler(numFileExtTB_TextChanged);
            _MainForm.Controls.Add(numFileExtTB);

            sampleFilePathTB = new TextBox();
            sampleFilePathTB.Click += new EventHandler(sampleFilePathTB_Click);
            sampleFilePathTB.TextChanged += new EventHandler(sampleFilePathTB_TextChanged);
            _MainForm.Controls.Add(sampleFilePathTB);

            fileDateStartTB = new TextBox();
            fileDateStartTB.Click += new EventHandler(fileDateStartTB_Click);
            fileDateStartTB.TextChanged += new EventHandler(fileDateStartTB_TextChanged);
            _MainForm.Controls.Add(fileDateStartTB);

            fileDateLengthTB = new TextBox();
            fileDateLengthTB.Click += new EventHandler(fileDateLengthTB_Click);
            fileDateLengthTB.TextChanged += new EventHandler(fileDateLengthTB_TextChanged);
            _MainForm.Controls.Add(fileDateLengthTB);

            monthTB = new TextBox();
            monthTB.Click += new EventHandler(monthTB_Click);
            monthTB.TextChanged += new EventHandler(monthTB_TextChanged);
            _MainForm.Controls.Add(monthTB);

            dayTB = new TextBox();
            dayTB.Click += new EventHandler(dayTB_Click);
            dayTB.TextChanged += new EventHandler(dayTB_TextChanged);
            _MainForm.Controls.Add(dayTB);

            yearTB = new TextBox();
            yearTB.Click += new EventHandler(yearTB_Click);
            yearTB.TextChanged += new EventHandler(yearTB_Changed);
            _MainForm.Controls.Add(yearTB);
        }
        #endregion
        #region Private Methods
        private void SetTabIndex()
        {
            dbLocTB.TabIndex = 0;
            dbFolderLocButton.TabIndex = 1;
            numFileExtTB.TabIndex = 2;
            compressAsOneCheck.TabIndex = 3;
            dateFormatCombo.TabIndex = 4;
            sampleFilePathTB.TabIndex = 5;
            sampleFileLocButton.TabIndex = 6;
            fileDateStartTB.TabIndex = 7;
            monthTB.TabIndex = 8;
            fileDateLengthTB.TabIndex = 9;
            dayTB.TabIndex = 10;
            fileDateSepCheck.TabIndex = 11;
            yearTB.TabIndex = 12;
        }
        #endregion
        #region Event Handler Methods
        private void dbLocButton_Click(object sender, EventArgs e)
        {
            LaunchFileExplorer();
        }

        private void dbLocTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref dbLocTB);
            _MainForm.currdatabase.DatabaseLocation = dbLocTB.Text;
            _MainForm.IsSaved = false;
        }

        private void dbLocTB_Click(object sender, EventArgs e)
        {
            if (dbLocTB.Text == "Path to Database" && dbLocTB.SelectedText == "")
            {
                dbLocTB.Text = null;
                ClearFormatting(ref dbLocTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                dbLocTB.SelectAll();
            }
        }

        private void numFileExtTB_Click(object sender, EventArgs e)
        {
            if (numFileExtTB.Text == "Number of File Extensions" && numFileExtTB.SelectedText == "")
            {
                numFileExtTB.Text = null;
                ClearFormatting(ref numFileExtTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                numFileExtTB.SelectAll();
            }
        }

        private void numFileExtTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref numFileExtTB);
            _MainForm.currdatabase.NumFilesToRar = numFileExtTB.Text;
            _MainForm.IsSaved = false;
        }

        private void compressAsOneCheck_Changed(object sender, EventArgs e)
        {
            if (compressAsOneCheck.Checked)
                _MainForm.currdatabase.CompressAsOne = "Yes";
            else if (!compressAsOneCheck.Checked)
                _MainForm.currdatabase.CompressAsOne = "No";
            _MainForm.IsSaved = false;
        }

        private void dateFormatCombo_Changed(object sender, EventArgs e)
        {
            _MainForm.currdatabase.DateFormat = dateFormatCombo.SelectedValue.ToString();
            _MainForm.IsSaved = false;
        }

        private void sampleFileLocButton_Click(object sender, EventArgs e)
        {
            sampleFileBrowser.ShowDialog();
            sampleFilePathTB.Text = sampleFileBrowser.FileName;
        }

        private void sampleFilePathTB_Click(object sender, EventArgs e)
        {
            if (sampleFilePathTB.Text == "Sample File Path" && sampleFilePathTB.SelectedText == "")
            {
                sampleFilePathTB.Text = null;
                ClearFormatting(ref sampleFilePathTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                sampleFilePathTB.SelectAll();
            }
        }

        private void sampleFilePathTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref sampleFilePathTB);
            _MainForm.IsSaved = false;
        }

        private void fileDateStartTB_Click(object sender, EventArgs e)
        {
            if (fileDateStartTB.Text == "00" && fileDateStartTB.SelectedText == "")
            {
                fileDateStartTB.Text = null;
                ClearFormatting(ref fileDateStartTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                fileDateStartTB.SelectAll();
            }
        }

        private void fileDateStartTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref fileDateStartTB);
            _MainForm.currdatabase.DateLocationInBackup = fileDateStartTB.Text;
            _MainForm.IsSaved = false;
        }

        private void monthTB_Click(object sender, EventArgs e)
        {
            if (monthTB.Text == "MM" && monthTB.SelectedText == "")
            {
                monthTB.Text = null;
                ClearFormatting(ref monthTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                monthTB.SelectAll();
            }
        }

        private void monthTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref monthTB);
            _MainForm.IsSaved = false;
        }

        private void fileDateLengthTB_Click(object sender, EventArgs e)
        {
            if (fileDateLengthTB.Text == "00" && fileDateLengthTB.SelectedText == "")
            {
                fileDateLengthTB.Text = "";
                ClearFormatting(ref fileDateLengthTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                fileDateLengthTB.SelectAll();
            }
        }

        private void fileDateLengthTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref fileDateLengthTB);
            _MainForm.currdatabase.DateLengthInBackup = fileDateLengthTB.Text;
            _MainForm.IsSaved = false;
        }

        private void dayTB_Click(object sender, EventArgs e)
        {
            if (dayTB.Text == "DD" && dayTB.SelectedText == "")
            {
                dayTB.Text = "";
                ClearFormatting(ref dayTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                dayTB.SelectAll();
            }
        }

        private void dayTB_TextChanged(object sender, EventArgs e)
        {
            ClearFormatting(ref dayTB);
            _MainForm.IsSaved = false;
        }

        private void fileDateSepCheck_Changed(object sender, EventArgs e)
        {
            if (fileDateSepCheck.Checked)
                _MainForm.currdatabase.DateSeperated = "Yes";
            else if (!fileDateSepCheck.Checked)
                _MainForm.currdatabase.DateSeperated = "No";
                
            _MainForm.IsSaved = false;
        }

        private void yearTB_Click(object sender, EventArgs e)
        {
            if ((yearTB.Text == "YYYY" || yearTB.Text == "YY") && yearTB.SelectedText == "")
            {
                yearTB.Text = "";
                ClearFormatting(ref yearTB);
                _MainForm.IsSaved = false;
            }
            else
            {
                yearTB.SelectAll();
            }
        }

        private void yearTB_Changed(object sender, EventArgs e)
        {
            ClearFormatting(ref yearTB);
            _MainForm.IsSaved = false;
        }

        private void LaunchFileExplorer()
        {
            dbFolderBrowser.ShowDialog();

            //save the file path to the dbLocTB.Text field and clear the formatting of the textbox
            ClearFormatting(ref dbLocTB);
            dbLocTB.Text = dbFolderBrowser.SelectedPath;
        }

        private void ClearFormatting(ref TextBox _TextBox)
        {
            Font textBoxFont = _TextBox.Font;                                       //create dummy variable to store font
            textBoxFont = new Font(textBoxFont, FontStyle.Regular);
            _TextBox.ForeColor = Color.Black;                                       //modify font and textbox font color
            _TextBox.Font = textBoxFont;                                            //set textbox font to modified font
        }
        #endregion
    }
}
