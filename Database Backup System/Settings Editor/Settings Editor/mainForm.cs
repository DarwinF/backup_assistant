using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Settings_Editor
{
    public partial class mainForm : Form
    {
        //GLOBAL Variables
        private static Size mFormSize = new Size();
        private enum boxState { minimized = 1, maximized = 2 };
        private enum selectedDatabase { databaseOne = 1, databaseTwo, databaseThree, databaseFour, databaseFive, databaseSix, 
                                        databaseSeven, databaseEight, databaseNine, databaseTen };
        private static boxState dbInfoState = boxState.minimized;
        private static boxState fileExtensionState = boxState.minimized;
        private static boxState rarInfoState = boxState.minimized;
        private static boxState copyInfoState = boxState.minimized;
        private static selectedDatabase selectedDB;
        private static List<Label> dbSideBarLabels = new List<Label>();
        private static int excessData;
        private static int prevScrollBarPos = 0;
        private static bool isSaved = false;
        private static bool fileSaved = false;
        private static string settingsFileSaveLocation;

        private static OpenFileDialog settingsFileBrowser;

        private dbiHandler databaseInfo;
        private feHandler fileExtHandler;
        private riHandler rarInfoHandler;
        private ciHandler copyInfoHandler;
        private database_entry nullDatabase;
        private db_entry_handler databaseListHandler;
        private database_entry[] databases = new database_entry[10];

        #region Accessors
        public mainForm()
        {
            InitializeComponent();
        }
        public bool IsSaved
        {
            get { return isSaved; }
            set { isSaved = value; }
        }
        public Label DBOneTagLabel
        {
            get { return dbOneTagLabel; }
            set { dbOneTagLabel = value; }
        }
        public Label DBTwoTagLabel
        {
            get { return dbTwoTagLabel; }
            set { dbTwoTagLabel = value; }
        }
        public Label DBThreeTagLabel
        {
            get { return dbThreeTagLabel; }
            set { dbThreeTagLabel = value; }
        }
        public Label DBFourTagLabel
        {
            get { return dbFourTagLabel; }
            set { dbFourTagLabel = value; }
        }
        public Label DBFiveTagLabel
        {
            get { return dbFiveTagLabel; }
            set { dbFiveTagLabel = value; }
        }
        public Label DBSixTagLabel
        {
            get { return dbTagSixLabel; }
            set { dbTagSixLabel = value; }
        }
        public Label DBSevenTagLabel
        {
            get { return dbTagSevenLabel; }
            set { dbTagSevenLabel = value; }
        }
        public Label DBEightTagLabel
        {
            get { return dbTagEightLabel; }
            set { dbTagEightLabel = value; }
        }
        public Label DBNineTagLabel
        {
            get { return dbTagNineLabel; }
            set { dbTagNineLabel = value; }
        }
        public Label DBTenTagLabel
        {
            get { return dbTagTenLabel; }
            set { dbTagTenLabel = value; }
        }
        public Label DBInfoBox
        {
            get { return dbInfoBox; }
        }
        public Label FileExtensionBox
        {
            get { return fileExtensionBox; }
        }
        public Label RarInfoBox
        {
            get { return rarInfoBox; }
        }
        public Label CopyInfoBox
        {
            get { return copyInfoBox; }
        }

        public database_entry currdatabase
        {
            get { return databases[GetDBNumber()]; }
        }
        #endregion
        #region GUI_Interaction
        private void mainForm_Load(object sender, EventArgs e)
        {
            //Load the size of the form to the previously set size
            formSizeHandler();

            //Set Initial Position and Sizes corresponding to the size of the form
            databaseEntryHandler();
            selectionHandler(dbOneTagLabel);
            scrollBarHandler();
            sidebarHandler();
            sideBarDBHandler();
            InitializeDatabase();

            //call the constructors for the boxes of data
            databaseInfo = new dbiHandler(dbInfoBox.Location, dbInfoBox.Size, this);
            fileExtHandler = new feHandler(fileExtensionBox.Location, fileExtensionBox.Size, this);
            rarInfoHandler = new riHandler(rarInfoBox.Location, rarInfoBox.Size, this);
            copyInfoHandler = new ciHandler(copyInfoBox.Location, copyInfoBox.Size, this);
            databaseListHandler = new db_entry_handler(ref databases, this);
            nullDatabase = new database_entry(this);
            databaseInfo.AddDateFormatData();
            copyInfoHandler.LoadComboBox();
            
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save the new size of the for to the properties file
            mFormSize = this.Size;
            Properties.Settings.Default.size = mFormSize;
            Properties.Settings.Default.Save();

            if (!fileSaved)
            {
                DialogResult dr = MessageBox.Show("You're file has not been saved, would you like to do so now?", "Unsaved File", MessageBoxButtons.YesNo);

                if (dr == DialogResult.No)
                    Environment.Exit(0);
                else if (dr == DialogResult.Yes)
                {
                    SaveData();

                    if (fileSaved)
                        MessageBox.Show("File was saved.", "File Saved", MessageBoxButtons.OK);
                    else if (!fileSaved && settingsFileSaveLocation != null)
                        SaveData();
                    else
                        MessageBox.Show("File was not saved.", "File Not Saved", MessageBoxButtons.OK);
                }
            }
        }

        private void dbInfoHeader_Click(object sender, EventArgs e)
        {
            if (dbInfoState == boxState.minimized)
            {
                //call the maximizeBoxHandler function
                //0 = dbInfoBox was clicked, 215 = expand distance, 0 = which box we are expanding
                //Since we are using "Case Fall Through" we need to seperate the two parts of each case
                boxMaximizeHandler(0, 215, 0);
                //Change the state to maximized
                dbInfoState = boxState.maximized;
                //Tell the controls to be displayed
                databaseInfo.SetVisibility(true);
                //Set the focus to the database location entry text box
                databaseInfo.SetFocus_dbLocTB();
            }
            else
            {
                //call the boxMinimizeHandler function
                //0 = dbInfoBox was clicked, -215 = expand distance (negative to go 'up'), 0 = which box we are expanding
                //Since we are using "Case Fall Through" we need to seperate the two parts of each case
                boxMinimizeHandler(0, -215, 0);
                //Change the state to minimized
                dbInfoState = boxState.minimized;
                //Tell the controls to stop being displayed
                databaseInfo.SetVisibility(false);
            }
            //Call the scrollBarHandler
            scrollBarHandler();
        }

        private void fileExtensionHeader_Click(object sender, EventArgs e)
        {
            if (fileExtensionState == boxState.minimized)
            {
                //call the maximizeBoxHandler function
                //0 = dbInfoBox was clicked, 30 = expand distance, 0 = which box we are expanding
                //Since we are using "Case Fall Through" we need to seperate the two parts of each case
                boxMaximizeHandler(1, 30, 1);
                //Change the state to maximized
                fileExtensionState = boxState.maximized;
                fileExtHandler.SetVisibility(true);
                fileExtHandler.SetInitFocus();
            }
            else
            {
                //call the boxMinimizeHandler function
                //0 = dbInfoBox was clicked, -30 = expand distance (negative to go 'up'), 0 = which box we are expanding
                //Since we are using "Case Fall Through" we need to seperate the two parts of each case
                boxMinimizeHandler(1, -30, 1);
                //Change the state to minimized
                fileExtensionState = boxState.minimized;
                fileExtHandler.SetVisibility(false);
            }
            //Call the scrollBarHandler
            scrollBarHandler();
        }

        private void rarInfoHeader_Click(object sender, EventArgs e)
        {
            if (rarInfoState == boxState.minimized)
            {
                //call the maximizeBoxHandler function
                //0 = dbInfoBox was clicked, 130 = expand distance, 0 = which box we are expanding
                //Since we are using "Case Fall Through" we need to seperate the two parts of each case
                boxMaximizeHandler(2, 130, 2);
                //Change the state to maximized
                rarInfoState = boxState.maximized;
                rarInfoHandler.SetVisibility(true);
                rarInfoHandler.SetInitFocus();
            }
            else
            {
                //call the boxMinimizeHandler function
                //0 = dbInfoBox was clicked, -200 = expand distance (negative to go 'up'), 0 = which box we are expanding
                //Since we are using "Case Fall Through" we need to seperate the two parts of each case
                boxMinimizeHandler(2, -130, 2);
                //Change the state to minimized
                rarInfoState = boxState.minimized;
                rarInfoHandler.SetVisibility(false);
            }
            //Call the scrollBarHandler
            scrollBarHandler();
        }

        private void copyInfoHeader_Click(object sender, EventArgs e)
        {
            if (copyInfoState == boxState.minimized)
            {
                //call the maximizeBoxHandler function
                //0 = dbInfoBox was clicked, 200 = expand distance (temp), 0 = which box we are expanding
                //Since we are using "Case Fall Through" we need to seperate the two parts of each case
                boxMaximizeHandler(3, 200, 3);
                //Change the state to maximized
                copyInfoState = boxState.maximized;
                copyInfoHandler.SetVisibility(true);
                copyInfoHandler.SetInitFocus();
            }
            else
            {
                //call the boxMinimizeHandler function
                //0 = dbInfoBox was clicked, -200 = expand distance (temp; negative to go 'up'), 0 = which box we are expanding
                //Since we are using "Case Fall Through" we need to seperate the two parts of each case
                boxMinimizeHandler(3, -200, 3);
                //Change the state to minimized
                copyInfoState = boxState.minimized;
                copyInfoHandler.SetVisibility(false);
            }
            //Call the scrollBarHandler
            scrollBarHandler();
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            //Call the scrollBarHandler after resizing to make sure that everything is loaded right
            scrollBarHandler();
            sidebarHandler();
            sideBarDBHandler();
            boxesHandler();
            headerHandler();
            screenStateHandler();

            databaseInfo.ConstructControls(dbInfoBox.Location, dbInfoBox.Size, this);
            fileExtHandler.ConstructControls(fileExtensionBox.Location, fileExtensionBox.Size, this);
            rarInfoHandler.ConstructControls(rarInfoBox.Location, rarInfoBox.Size, this);
            copyInfoHandler.ConstructControls(copyInfoBox.Location, copyInfoBox.Size, this);
            
            //Fill the Controls with the selected Databases Information
            FillSelectedDB();

            #region Visibility Check
            if (dbInfoState == boxState.maximized)
                databaseInfo.SetVisibility(true);
            else
                databaseInfo.SetVisibility(false);

            if (fileExtensionState == boxState.maximized)
                fileExtHandler.SetVisibility(true);
            else
                fileExtHandler.SetVisibility(false);

            if (rarInfoState == boxState.maximized)
                rarInfoHandler.SetVisibility(true);
            else
                rarInfoHandler.SetVisibility(false);
            if (copyInfoState == boxState.maximized)
                copyInfoHandler.SetVisibility(true);
            else
                copyInfoHandler.SetVisibility(false);
            #endregion
        }

        private void scrollBar_ValueChanged(object sender, EventArgs e)
        {
            //local variables
            int currScrollBarPos = scrollBar.Value;
            int scrollableDistance = scrollBar.Maximum - scrollBar.LargeChange;
            int scrollDistance = (excessData / scrollableDistance) + 1;

            if (currScrollBarPos < prevScrollBarPos)
            {                
                boxScrollHandler(scrollDistance * (prevScrollBarPos - currScrollBarPos));
            }
            else if (currScrollBarPos > prevScrollBarPos)
            {
                boxScrollHandler(scrollDistance * -1 * (currScrollBarPos - prevScrollBarPos));
            }
            Console.WriteLine(currScrollBarPos);
            prevScrollBarPos = currScrollBarPos;
        }

        private void addDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dbSideBarLabels.Count; i++)
            {
                if (!dbSideBarLabels[i].Visible)
                {
                    AddDatabaseEntry(dbSideBarLabels[i]);
                    break;
                }
            }
        }

        private void removeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult promptResponse = MessageBox.Show("Are you sure you want to remove " +
                                                            currDBStrip.Text + "?", "Removing Database",
                                                            MessageBoxButtons.YesNo);

            if (promptResponse == System.Windows.Forms.DialogResult.Yes)
            {
                switch ((int)selectedDB)
                {
                    case 1:
                        RemoveDatabase(databases[0]);
                        ClearData(databases[0]);
                        break;
                    case 2:
                        RemoveDatabase(databases[1]);
                        ClearData(databases[1]);
                        break;
                    case 3:
                        RemoveDatabase(databases[2]);
                        ClearData(databases[2]);
                        break;
                    case 4:
                        RemoveDatabase(databases[3]);
                        ClearData(databases[3]);
                        break;
                    case 5:
                        RemoveDatabase(databases[4]);
                        ClearData(databases[4]);
                        break;
                    case 6:
                        RemoveDatabase(databases[5]);
                        ClearData(databases[5]);
                        break;
                    case 7:
                        RemoveDatabase(databases[6]);
                        ClearData(databases[6]);
                        break;
                    case 8:
                        RemoveDatabase(databases[7]);
                        ClearData(databases[7]);
                        break;
                    case 9:
                        RemoveDatabase(databases[8]);
                        ClearData(databases[8]);
                        break;
                    case 10:
                        RemoveDatabase(databases[9]);
                        ClearData(databases[9]);
                        break;
                }
            }
        }

        private void clearDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch ((int)selectedDB)
            {
                case 0:
                    ClearData(databases[0]);
                    break;
                case 1:
                    ClearData(databases[1]);
                    break;
                case 2:
                    ClearData(databases[2]);
                    break;
                case 3:
                    ClearData(databases[3]);
                    break;
                case 4:
                    ClearData(databases[4]);
                    break;
                case 5:
                    ClearData(databases[5]);
                    break;
                case 6:
                    ClearData(databases[6]);
                    break;
                case 7:
                    ClearData(databases[7]);
                    break;
                case 8:
                    ClearData(databases[8]);
                    break;
                case 9:
                    ClearData(databases[9]);
                    break;
            }
        }

        private void dbTwoTagLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbTwoTagLabel);
            dbLoadedPrompt(dbTwoTagLabel.Text);
            selectedDB = selectedDatabase.databaseTwo;
        }

        private void dbOneTagLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbOneTagLabel);
            dbLoadedPrompt(dbOneTagLabel.Text);
            selectedDB = selectedDatabase.databaseOne;
        }

        private void dbThreeTagLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbThreeTagLabel);
            dbLoadedPrompt(dbThreeTagLabel.Text);
            selectedDB = selectedDatabase.databaseThree;
        }

        private void dbFourTagLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbFourTagLabel);
            dbLoadedPrompt(dbFourTagLabel.Text);
            selectedDB = selectedDatabase.databaseFour;
        }

        private void dbFiveTagLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbFiveTagLabel);
            dbLoadedPrompt(dbFiveTagLabel.Text);
            selectedDB = selectedDatabase.databaseFive;
        }

        private void dbTagSixLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbTagSixLabel);
            dbLoadedPrompt(dbTagSixLabel.Text);
            selectedDB = selectedDatabase.databaseSix;
        }

        private void dbTagSevenLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbTagSevenLabel);
            dbLoadedPrompt(dbTagSevenLabel.Text);
            selectedDB = selectedDatabase.databaseSeven;
        }

        private void dbTagEightLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbTagEightLabel);
            dbLoadedPrompt(dbTagEightLabel.Text);
            selectedDB = selectedDatabase.databaseEight;
        }

        private void dbTagNineLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbTagNineLabel);
            dbLoadedPrompt(dbTagNineLabel.Text);
            selectedDB = selectedDatabase.databaseNine;
        }

        private void dbTagTenLabel_Click(object sender, EventArgs e)
        {
            selectionHandler(dbTagTenLabel);
            dbLoadedPrompt(dbTagNineLabel.Text);
            selectedDB = selectedDatabase.databaseTen;
        }

        private void loadFile_Click(object sender, EventArgs e)
        {
            string settingsFilePath = "temp";

            settingsFileBrowser = new OpenFileDialog();
            settingsFileBrowser.Filter = "Configuration Files (*.conf) | *.conf";
            settingsFileBrowser.ShowDialog();
            settingsFilePath = settingsFileBrowser.FileName;

            if (settingsFilePath == "temp")
            {
                DialogResult dr = MessageBox.Show("Warning:\nA settings.conf file was not selected. Would you like to continue\nwithout selecting one?\n(This will cancel the load file option.)", "Warning",
                    MessageBoxButtons.YesNo);

                if (dr == DialogResult.No)
                {
                    loadFile_Click(sender, e);
                }
            }
            else
            {
                databaseListHandler.GetDataFromFile(settingsFilePath);
                selectionHandler(dbOneTagLabel);
                MessageBox.Show("The settings.conf file has been loaded!", "Settings File Loaded", MessageBoxButtons.OK);
                selectedDB = selectedDatabase.databaseOne;
                databases[0].FillControlFields();
            }
        }

        private void newFile_Click(object sender, EventArgs e)
        {
            CreateFile();   
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion
        #region Handlers
        private void scrollBarHandler()
        {
            //Local Variables
            int copyBoxBottom = copyInfoBox.Location.Y + copyInfoBox.Size.Height;
            int screenBottom = this.ClientRectangle.Height - (infoStrip.Size.Height + 5); // +5 is for a cushion under the box
            int screenTop = mainMenu.Height + 5; //+5 is for a cushion at the top of the screen.
            int dataBoxHeight = copyBoxBottom + dbInfoHeader.Location.Y;
            int safeViewBoxHeight = screenBottom + screenTop;
            excessData = dataBoxHeight - safeViewBoxHeight;
            
            //see if the distance is greater than the "viewport" distance and the scrollbar is off
            if ((dataBoxHeight > safeViewBoxHeight) && !scrollBar.Visible)
            {
                //"reset" the scroll bar
                scrollBar.Visible = true;
                scrollBar.Value = 0;
                //resize the scrollbar for the current window size
                scrollBar.Height = infoStrip.Location.Y - mainMenu.Size.Height;
                scrollBar.Location = new Point((this.ClientRectangle.Width - scrollBar.Width), mainMenu.Size.Height);
            }
            //see if the distance is greater than the "viewport" distance and the scrollbar is on
            else if ((dataBoxHeight > safeViewBoxHeight) && scrollBar.Visible)
            {
                //resize the scrollbar for the current window size
                scrollBar.Height = infoStrip.Location.Y - mainMenu.Size.Height;
                scrollBar.Location = new Point((this.ClientRectangle.Width - scrollBar.Width), mainMenu.Size.Height);
                //reset all the values for boxHeights
                dataBoxHeight = copyBoxBottom + dbInfoHeader.Location.Y;
                safeViewBoxHeight = screenBottom + screenTop;
                excessData = dataBoxHeight - safeViewBoxHeight;
            }
            //The data is all on screen again so we can turn the scrollBar off
            else
            {
                scrollBar.Visible = false;
            }
        }

        private void boxesHandler()
        {
            //local variables
            int leftEdge = (this.ClientRectangle.Width / 4) + 10;
            int rightEdge = this.ClientRectangle.Width - scrollBar.Width - 10;
            int boxWidth = rightEdge - leftEdge;

            //move the boxes to line up with the new window size
            dbInfoBox.Location = new Point(leftEdge, dbInfoBox.Location.Y);
            fileExtensionBox.Location = new Point(leftEdge, fileExtensionBox.Location.Y);
            rarInfoBox.Location = new Point(leftEdge, rarInfoBox.Location.Y);
            copyInfoBox.Location = new Point(leftEdge, copyInfoBox.Location.Y);

            //resize the boxes to fit in the new window
            dbInfoBox.Width = boxWidth;
            fileExtensionBox.Width = boxWidth;
            rarInfoBox.Width = boxWidth;
            copyInfoBox.Width = boxWidth;
        }

        private void headerHandler()
        {
            //local variables
            int leftEdge = (this.ClientRectangle.Width / 4) + 25;

            //size all the headers to fit the windows size

            //move all the headers to line up with the new window size
            dbInfoHeader.Location = new Point(leftEdge, dbInfoHeader.Location.Y);
            fileExtensionHeader.Location = new Point(leftEdge, fileExtensionHeader.Location.Y);
            rarInfoHeader.Location = new Point(leftEdge, rarInfoHeader.Location.Y);
            copyInfoHeader.Location = new Point(leftEdge, copyInfoHeader.Location.Y);
        }

        private void databaseEntryHandler()
        {
            dbSideBarLabels.Add(dbOneTagLabel);
            dbSideBarLabels.Add(dbTwoTagLabel);
            dbTwoTagLabel.Visible = false;
            dbSideBarLabels.Add(dbThreeTagLabel);
            dbThreeTagLabel.Visible = false;
            dbSideBarLabels.Add(dbFourTagLabel);
            dbFourTagLabel.Visible = false;
            dbSideBarLabels.Add(dbFiveTagLabel);
            dbFiveTagLabel.Visible = false;
            dbSideBarLabels.Add(dbTagSixLabel);
            dbTagSixLabel.Visible = false;
            dbSideBarLabels.Add(dbTagSevenLabel);
            dbTagSevenLabel.Visible = false;
            dbSideBarLabels.Add(dbTagEightLabel);
            dbTagEightLabel.Visible = false;
            dbSideBarLabels.Add(dbTagNineLabel);
            dbTagNineLabel.Visible = false;
            dbSideBarLabels.Add(dbTagTenLabel);
            dbTagTenLabel.Visible = false;

            selectedDB = selectedDatabase.databaseOne;
        }

        private void sidebarHandler()
        {
            //local variables
            int formHeight = this.ClientRectangle.Height - (mainMenu.Height + infoStrip.Height);
            int formWidth = this.ClientRectangle.Width;

            //draw the side bar to be the height of the form and a quarter of it's width
            sideMenuBox.Size = new Size((formWidth / 4), formHeight);
            sideMenuBox.Location = new Point(0,mainMenu.Height);
        }

        private void sideBarDBHandler()
        {
            //local variables
            int databaseTagWidth = this.ClientRectangle.Width / 4 - 15;
            const int databaseTagHeight = 25;
            Label previousLabel = null;

            //go through the labels in the List of database labels
            //foreach label, resize it to be 10 pixels narrower than the sidebar width
            foreach (Label currLabel in dbSideBarLabels)
            {
                currLabel.Width = databaseTagWidth;
                currLabel.Height = databaseTagHeight;
                //if this is not the first label, we need to position it below the previous one
                if (previousLabel != null)
                {
                    //Height - 1 is to get the borders to line up and look better
                    currLabel.Location = new Point(0, previousLabel.Location.Y + (databaseTagHeight - 1));
                }
                else
                {
                    currLabel.Location = new Point(0, mainMenu.Height);
                }
                previousLabel = currLabel;
            }
        }

        private void selectionHandler(Label selectedLabel)
        {
            //When the label is selected, return all the previous databases to the original
            //background color and set the selected one to the selected backcolor
            //Set the infoStrip to display the currently selected database

            foreach (Label currLabel in dbSideBarLabels)
            {
                if (currLabel == selectedLabel)
                {
                    currLabel.BackColor = Color.LightSkyBlue;
                    currDBStrip.Text = currLabel.Text;
                }
                else
                {
                    currLabel.BackColor = SystemColors.Control;
                }
            }
        }

        private void screenStateHandler()
        {
            //set the screen state to maximized so it will be maximized on load next time
            if (this.WindowState == FormWindowState.Maximized)
                Properties.Settings.Default.maximized = true;
            else
                Properties.Settings.Default.maximized = false;
        }

        private void formSizeHandler()
        {
            if (!Properties.Settings.Default.maximized)
            {
                mFormSize = Properties.Settings.Default.size;
                this.Size = mFormSize;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void boxScrollHandler(int distance)
        {
            //move all the headers and boxes the passed distance
            //Do this movement additively so that the a negative number can be passed to go up
            //and a positive number can be passed to go down

            dbInfoBox.Location = new Point(dbInfoBox.Location.X, dbInfoBox.Location.Y + distance);
            dbInfoHeader.Location = new Point(dbInfoHeader.Location.X, dbInfoHeader.Location.Y + distance);
            databaseInfo.MoveControls(distance, ref mainMenu);
            fileExtHandler.MoveControls(distance, ref mainMenu);
            rarInfoHandler.MoveControls(distance, ref mainMenu);
            copyInfoHandler.MoveControls(distance, ref mainMenu);
            fileExtensionBox.Location = new Point(fileExtensionBox.Location.X, fileExtensionBox.Location.Y + distance);
            fileExtensionHeader.Location = new Point(fileExtensionHeader.Location.X, fileExtensionHeader.Location.Y + distance);
            rarInfoBox.Location = new Point(rarInfoBox.Location.X, rarInfoBox.Location.Y + distance);
            rarInfoHeader.Location = new Point(rarInfoHeader.Location.X, rarInfoHeader.Location.Y + distance);
            copyInfoBox.Location = new Point(copyInfoBox.Location.X, copyInfoBox.Location.Y + distance);
            copyInfoHeader.Location = new Point(copyInfoHeader.Location.X, copyInfoHeader.Location.Y + distance);
        }

        private void boxMaximizeHandler(int boxMaximized, int expandDistance, int expand)
        {
            //dbInfoBox = 0, fileExtensionBox = 1, rarInfoBox = 2, copyInfoBox = 3
            switch (boxMaximized)
            {
                case 0:
                    if (expand == 0)
                    {
                        dbInfoBox.Height += expandDistance;
                        fileExtHandler.MoveControls(expandDistance, ref mainMenu);
                        rarInfoHandler.MoveControls(expandDistance, ref mainMenu);
                        copyInfoHandler.MoveControls(expandDistance, ref mainMenu);
                    }                        
                    goto case 1;
                case 1:
                    if (expand == 1)
                    {
                        fileExtensionBox.Height += expandDistance;
                        rarInfoHandler.MoveControls(expandDistance, ref mainMenu);
                        copyInfoHandler.MoveControls(expandDistance, ref mainMenu);
                    }
                    else
                    {
                        fileExtensionBox.Location = new Point(fileExtensionBox.Location.X, fileExtensionBox.Location.Y + expandDistance);
                        fileExtensionHeader.Location = new Point(fileExtensionHeader.Location.X, fileExtensionHeader.Location.Y + expandDistance);
                    }
                    goto case 2;
                case 2:
                    if (expand == 2)
                    {
                        rarInfoBox.Height += expandDistance;
                        copyInfoHandler.MoveControls(expandDistance, ref mainMenu);
                    }
                    else
                    {
                        rarInfoBox.Location = new Point(rarInfoBox.Location.X, rarInfoBox.Location.Y + expandDistance);
                        rarInfoHeader.Location = new Point(rarInfoHeader.Location.X, rarInfoHeader.Location.Y + expandDistance);
                    }
                    goto case 3;
                case 3:
                    if (expand == 3) copyInfoBox.Height += expandDistance;
                    else
                    {
                        copyInfoBox.Location = new Point(copyInfoBox.Location.X, copyInfoBox.Location.Y + expandDistance);
                        copyInfoHeader.Location = new Point(copyInfoHeader.Location.X, copyInfoHeader.Location.Y + expandDistance);
                    }
                    break;
            }
        }

        private void boxMinimizeHandler(int boxMinimized, int expandDistance, int expand)
        {
            //dbInfoBox = 0, fileExtensionBox = 1, rarInfoBox = 2, copyInfoBox = 3
            switch (boxMinimized)
            {
                case 0:
                    if (expand == 0)
                    {
                        dbInfoBox.Height += expandDistance;
                        fileExtHandler.MoveControls(expandDistance, ref mainMenu);
                        rarInfoHandler.MoveControls(expandDistance, ref mainMenu);
                        copyInfoHandler.MoveControls(expandDistance, ref mainMenu);
                    }
                    goto case 1;
                case 1:
                    if (expand == 1)
                    {
                        fileExtensionBox.Height += expandDistance;
                        rarInfoHandler.MoveControls(expandDistance, ref mainMenu);
                        copyInfoHandler.MoveControls(expandDistance, ref mainMenu);
                    }
                    else
                    {
                        fileExtensionBox.Location = new Point(fileExtensionBox.Location.X, fileExtensionBox.Location.Y + expandDistance);
                        fileExtensionHeader.Location = new Point(fileExtensionHeader.Location.X, fileExtensionHeader.Location.Y + expandDistance);
                    }
                    goto case 2;
                case 2:
                    if (expand == 2)
                    {
                        rarInfoBox.Height += expandDistance;
                        copyInfoHandler.MoveControls(expandDistance, ref mainMenu);
                    }
                    else
                    {
                        rarInfoBox.Location = new Point(rarInfoBox.Location.X, rarInfoBox.Location.Y + expandDistance);
                        rarInfoHeader.Location = new Point(rarInfoHeader.Location.X, rarInfoHeader.Location.Y + expandDistance);
                    }
                    goto case 3;
                case 3:
                    if (expand == 3) copyInfoBox.Height += expandDistance;
                    else
                    {
                        copyInfoBox.Location = new Point(copyInfoBox.Location.X, copyInfoBox.Location.Y + expandDistance);
                        copyInfoHeader.Location = new Point(copyInfoHeader.Location.X, copyInfoHeader.Location.Y + expandDistance);
                    }
                    break;
            }
        }
        #endregion           
        #region Functional Methods
        public void AddDatabaseEntry(Label dbTag)
        {
            dbTag.Visible = true;
            selectionHandler(dbTag);

            if (dbInfoState != boxState.maximized)
            {
                boxMaximizeHandler(0, 215, 0);
                dbInfoState = boxState.maximized;
                databaseInfo.SetVisibility(true);
                databaseInfo.SetFocus_dbLocTB();
            }
        }

        public int GetDBNumber()
        {
            switch ((int)selectedDB)
            {
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 3:
                    return 2;
                case 4:
                    return 3;
                case 5:
                    return 4;
                case 6:
                    return 5;
                case 7:
                    return 6;
                case 8:
                    return 7;
                case 9:
                    return 8;
                case 10:
                    return 9;
            }

            return -1;
        }

        private void dbLoadedPrompt(string dbName)
        {
            MessageBox.Show(dbName + " has been loaded.", "Database Loaded", MessageBoxButtons.OK);
        }

        private void InitializeDatabase()
        {
            switch ((int)selectedDB)
            {
                case 1:
                    if (databases[0] == null)
                        databases[0] = new database_entry(1);
                    else
                        LoadInfo(databases[0]);
                    break;
                case 2:
                    if (databases[1] == null)
                        databases[1] = new database_entry(2);
                    else
                        LoadInfo(databases[1]);
                    break;
                case 3:
                    if (databases[2] == null)
                        databases[2] = new database_entry(3);
                    else
                        LoadInfo(databases[2]);
                    break;
                case 4:
                    if (databases[3] == null)
                        databases[3] = new database_entry(4);
                    else
                        LoadInfo(databases[3]);
                    break;
                case 5:
                    if (databases[4] == null)
                        databases[4] = new database_entry(5);
                    else
                        LoadInfo(databases[4]);
                    break;
                case 6:
                    if (databases[5] == null)
                        databases[5] = new database_entry(6);
                    else
                        LoadInfo(databases[5]);
                    break;
                case 7:
                    if (databases[6] == null)
                        databases[6] = new database_entry(7);
                    else
                        LoadInfo(databases[6]);
                    break;
                case 8:
                    if (databases[7] == null)
                        databases[7] = new database_entry(8);
                    else
                        LoadInfo(databases[7]);
                    break;
                case 9:
                    if (databases[8] == null)
                        databases[8] = new database_entry(9);
                    else
                        LoadInfo(databases[8]);
                    break;
                case 10:
                    if (databases[9] == null)
                        databases[9] = new database_entry(10);
                    else
                        LoadInfo(databases[9]);
                    break;
            }
        }

        private void LoadInfo(database_entry currentDatabase)
        {

        }

        private void SaveData()
        {
            int dbCount = 1;

            if (!File.Exists(settingsFileSaveLocation + "\\settings.conf") || settingsFileSaveLocation == null)
                CreateFile();
            else
            {
                foreach (database_entry database in databases)
                {
                    if (database != null)
                        database.SaveData(settingsFileSaveLocation + "\\settings.conf", dbCount);

                    dbCount++;
                }

                fileSaved = true;
                MessageBox.Show("File has been saved.", "File Saved", MessageBoxButtons.OK);

                isSaved = true;
            }
        }

        private void ClearData(database_entry currentDatabase)
        {
            //call construct controls
            databaseInfo.ConstructControls(dbInfoBox.Location, dbInfoBox.Size, this);
            fileExtHandler.ConstructControls(fileExtensionBox.Location, fileExtensionBox.Size, this);
            rarInfoHandler.ConstructControls(rarInfoBox.Location, rarInfoBox.Size, this);
            copyInfoHandler.ConstructControls(copyInfoBox.Location, copyInfoBox.Size, this);
            //restore all minimized/maximized boxes
            #region Visibility Reset
            if (dbInfoState == boxState.maximized)
            {
                databaseInfo.SetVisibility(true);
                databaseInfo.SetFocus_dbLocTB();
            }
            else
                databaseInfo.SetVisibility(false);

            if (fileExtensionState == boxState.maximized)
            {
                fileExtHandler.SetVisibility(true);
                fileExtHandler.SetInitFocus();
            }
            else
                fileExtHandler.SetVisibility(false);

            if (rarInfoState == boxState.maximized)
            {
                rarInfoHandler.SetVisibility(true);
                rarInfoHandler.SetInitFocus();
            }
            else
                rarInfoHandler.SetVisibility(false);
            if (copyInfoState == boxState.maximized)
            {
                copyInfoHandler.SetVisibility(true);
                copyInfoHandler.SetInitFocus();
            }
            else
                copyInfoHandler.SetVisibility(false);
            #endregion
            //clone the nullDatabase to the currentDatabase
            currentDatabase = nullDatabase;
        }

        private void RemoveDatabase(database_entry currentDatabase)
        {
            switch ((int)selectedDB)
            {
                case 1:
                    MessageBox.Show("You cannot remove the first Database!\nPlease use the clear option instead.", "ALERT!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 2:
                    dbSideBarLabels[1].Visible = false;
                    selectionHandler(dbSideBarLabels[0]);
                    break;
                case 3:
                    dbSideBarLabels[2].Visible = false;
                    SelectPrevious(2);
                    break;
                case 4:
                    dbSideBarLabels[3].Visible = false;
                    SelectPrevious(3);
                    break;
                case 5:
                    dbSideBarLabels[4].Visible = false;
                    SelectPrevious(4);
                    break;
                case 6:
                    dbSideBarLabels[5].Visible = false;
                    SelectPrevious(5);
                    break;
                case 7:
                    dbSideBarLabels[6].Visible = false;
                    SelectPrevious(6);
                    break;
                case 8:
                    dbSideBarLabels[7].Visible = false;
                    SelectPrevious(7);
                    break;
                case 9:
                    dbSideBarLabels[8].Visible = false;
                    SelectPrevious(8);
                    break;
                case 10:
                    dbSideBarLabels[9].Visible = false;
                    SelectPrevious(9);
                    break;
            }
        }

        private void SelectPrevious(int current)
        {
            for (int count = (current - 1); current >= 0; current++)
            {
                if (dbSideBarLabels[current].Visible)
                {
                    selectionHandler(dbSideBarLabels[current]);
                    break;
                }
            }
        }

        private void FillSelectedDB()
        {
            switch ((int)selectedDB)
            {
                case 0:
                    databases[0].FillControlFields();
                    break;
                case 1:
                    databases[1].FillControlFields();
                    break;
                case 2:
                    databases[2].FillControlFields();
                    break;
                case 3:
                    databases[3].FillControlFields();
                    break;
                case 4:
                    databases[4].FillControlFields();
                    break;
                case 5:
                    databases[5].FillControlFields();
                    break;
                case 6:
                    databases[6].FillControlFields();
                    break;
                case 7:
                    databases[7].FillControlFields();
                    break;
                case 8:
                    databases[8].FillControlFields();
                    break;
                case 9:
                    databases[9].FillControlFields();
                    break;
            }
        }

        private void CreateFile()
        {
            DialogResult dr;

            //prompt the user for where they want to save the file
            if (saveLocationBrowser.ShowDialog() == DialogResult.OK)
            {
                dr = MessageBox.Show("Are you sure you want to save your initial file to:\n" + saveLocationBrowser.SelectedPath, "Save Location", MessageBoxButtons.YesNo);
                //if yes, store it in settingsFileSaveLocation
                if (dr == DialogResult.Yes)
                {
                    settingsFileSaveLocation = saveLocationBrowser.SelectedPath;
                    //Console.WriteLine(settingsFileSaveLocation);
                    using (File.Create(settingsFileSaveLocation + "\\settings.conf")) { }
                }
                //if no, reload the location browser
                else if (dr == DialogResult.No)
                {
                    saveLocationBrowser.Dispose();
                    saveLocationBrowser.ShowDialog();
                }
            }

        }
        #endregion
    }
}
