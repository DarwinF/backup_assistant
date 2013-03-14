using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Settings_Editor
{
    class db_entry_handler
    {
        #region Variables
        private database_entry[] dbEntries;
        private mainForm _mainForm;
        #endregion
        #region Constructor
        public db_entry_handler(ref database_entry[] databaseList, mainForm MainForm)
        {
            dbEntries = databaseList;
            _mainForm = MainForm;
        }
        #endregion
        #region Public Methods
        public void GetDataFromFile(string pathToSettingsFile)
        {
            string line;

            for (int i = 0; i < 10; i++)
            {
                using (StreamReader reader = new StreamReader(pathToSettingsFile))
                {
                    int currDB = 0, returnVal = 0;

                    line = reader.ReadLine();
                    while (line != null)
                    {
                        returnVal = GetDB(line);

                        if (returnVal != 0)
                            currDB = returnVal;

                        if (currDB != 0)
                        {
                            if (dbEntries[currDB - 1] == null)
                            {
                                dbEntries[currDB - 1] = new database_entry(_mainForm);
                            }

                            dbEntries[currDB - 1].MatchLine(line);
                            AddDatabaseTag(currDB);
                        }
                        line = reader.ReadLine();
                    }
                }
            }


        }
        #endregion
        #region Private Methods
        private int GetDB(string line)
        {
            if (line == "## Database 1 Information")
                return 1;
            else if (line == "## Database 2 Information")
                return 2;
            else if (line == "## Database 3 Information")
                return 3;
            else if (line == "## Database 4 Information")
                return 4;
            else if (line == "## Database 5 Information")
                return 5;
            else if (line == "## Database 6 Information")
                return 6;
            else if (line == "## Database 7 Information")
                return 7;
            else if (line == "## Database 8 Information")
                return 8;
            else if (line == "## Database 9 Information")
                return 9;
            else if (line == "## Database 10 Information")
                return 10;
            else
                return 0;
        }
        private void AddDatabaseTag(int currDB)
        {
            switch (currDB)
            {
                case 1:
                    _mainForm.AddDatabaseEntry(_mainForm.DBOneTagLabel);
                    break;
                case 2:
                    _mainForm.AddDatabaseEntry(_mainForm.DBTwoTagLabel);
                    break;
                case 3:
                    _mainForm.AddDatabaseEntry(_mainForm.DBThreeTagLabel);
                    break;
                case 4:
                    _mainForm.AddDatabaseEntry(_mainForm.DBFourTagLabel);
                    break;
                case 5:
                    _mainForm.AddDatabaseEntry(_mainForm.DBFiveTagLabel);
                    break;
                case 6:
                    _mainForm.AddDatabaseEntry(_mainForm.DBSixTagLabel);
                    break;
                case 7:
                    _mainForm.AddDatabaseEntry(_mainForm.DBSevenTagLabel);
                    break;
                case 8:
                    _mainForm.AddDatabaseEntry(_mainForm.DBEightTagLabel);
                    break;
                case 9:
                    _mainForm.AddDatabaseEntry(_mainForm.DBNineTagLabel);
                    break;
                case 10:
                    _mainForm.AddDatabaseEntry(_mainForm.DBTenTagLabel);
                    break;
            }
        }
        #endregion
    }
}
