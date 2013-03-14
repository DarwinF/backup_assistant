using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Settings_Editor
{
    public class database_entry
    {
        #region Global Variables
        string header;
        const string databaseLocation = "DatabaseLocation: ";
        string _databaseLocation;
        const string numFilesToRar = "NumberOfFilesToRar: ";
        string _numFilesToRar;
        List<string> extensionList = new List<string>();
        const string databaseExtension = "DatabaseBackupExtension: ";
        const string compressAsOne = "CompressAllAsOne: ";
        string _compressAsOne;
        const string rarFileLocation = "RARFileLocation: ";
        string _rarFileLocation;
        const string rarFileName = "RARFileName: ";
        string _rarFileName;
        const string appendDateToRar = "AppendDateToRARName: ";
        string _appendDateToRar;
        const string dateLocationInBackup = "DateLocationInBackup: ";
        string _dateLocationInBackup;
        const string dateLengthInBackup = "DateLengthInBackup: ";
        string _dateLengthInBackup;
        const string dateFormat = "DateFormat: ";
        string _dateFormat;
        const string dateSeperated = "DateSeperated: ";
        string _dateSeperated;
        const string copyToLocation = "CopyToLocation: ";
        string _copyToLocation;
        const string overwriteFiles = "OverwriteFiles: ";
        string _overwriteFiles;
        const string createNewFolder = "CreateNewFolderOnArrival: ";
        string _createNewFolder;
        const string newFolderName = "NewFolderName: ";
        string _newFolderName;
        const string appendDateToFolderName = "AppendDateToFolderName: ";
        string _appendDateToFolderName;
        const string versionsToCopy = "VersionsToCopy: ";
        string _versionsToCopy;
        const string amntToCopy = "AmountToCopy: ";
        string _amntToCopy;
        string footer;

        List<string> settingsFileLines = new List<string>();
        mainForm _mainForm;
        #endregion
        #region Constructors
        public database_entry(int databaseNumber)
        {
            header = "## Database " + databaseNumber + " Information";
            footer = "## End Database " + databaseNumber + " Information";
        }
        public database_entry(mainForm _MainForm)
        {
            if (extensionList.Count != 2)
            {
                extensionList.Add(null);
                extensionList.Add(null);
            }

            _mainForm = _MainForm;
        }
        #endregion
        #region Accessors
        public string DatabaseLocation
        {
            get { return _databaseLocation; }
            set { _databaseLocation = value; }
        }
        public string NumFilesToRar
        {
            get { return _numFilesToRar; }
            set { _numFilesToRar = value; }
        }
        public List<string> ExtensionList
        {
            get { return extensionList; }
            set { extensionList = value; }
        }
        public string CompressAsOne
        {
            get { return _compressAsOne; }
            set { _compressAsOne = value; }
        }
        public string RarFileLocation
        {
            get { return _rarFileLocation; }
            set { _rarFileLocation = value; }
        }
        public string RarFileName
        {
            get { return _rarFileName; }
            set { _rarFileName = value; }
        }
        public string AppendDateToRar
        {
            get { return _appendDateToRar; }
            set { _appendDateToRar = value; }
        }
        public string DateLocationInBackup
        {
            get { return _dateLocationInBackup; }
            set { _dateLocationInBackup = value; }
        }
        public string DateLengthInBackup
        {
            get { return _dateLengthInBackup; }
            set { _dateLengthInBackup = value; }
        }
        public string DateFormat
        {
            get { return _dateFormat; }
            set { _dateFormat = value; }
        }
        public string DateSeperated
        {
            get { return _dateSeperated; }
            set { _dateSeperated = value; }
        }
        public string CopyToLocation
        {
            get { return _copyToLocation; }
            set { _copyToLocation = value; }
        }
        public string OverwriteFiles
        {
            get { return _overwriteFiles; }
            set { _overwriteFiles = value; }
        }
        public string CreateNewFolder
        {
            get { return _createNewFolder; }
            set { _createNewFolder = value; }
        }
        public string NewFolderName
        {
            get { return _newFolderName; }
            set { _newFolderName = value; }
        }
        public string AppendDateToFolderName
        {
            get { return _appendDateToFolderName; }
            set { _appendDateToFolderName = value; }
        }
        public string VersionsToCopy
        {
            get { return _versionsToCopy; }
            set { _versionsToCopy = value; }
        }
        public string AmntToCopy
        {
            get { return _amntToCopy; }
            set { _amntToCopy = value; }
        }
        #endregion
        #region Public Methods
        public void SaveData(string fileLocation, int currDB)
        {
            using (StreamWriter saveWriter = new StreamWriter(fileLocation))
            {
                saveWriter.WriteLine("## Database " + currDB + " Information");
                saveWriter.WriteLine();
                saveWriter.WriteLine(databaseLocation + _databaseLocation);
                saveWriter.WriteLine(numFilesToRar + _numFilesToRar);
                for (int count = 0; count < extensionList.Count; count++)
                    saveWriter.WriteLine(databaseExtension + extensionList[count]);
                if (_compressAsOne != null)
                    saveWriter.WriteLine(compressAsOne + _compressAsOne);
                else if (_compressAsOne == null)
                    saveWriter.WriteLine(compressAsOne + "No");
                saveWriter.WriteLine(rarFileLocation + _rarFileLocation);
                saveWriter.WriteLine(rarFileName + _rarFileName);
                if (_appendDateToRar != null)
                    saveWriter.WriteLine(appendDateToRar + _appendDateToRar);
                else if (_appendDateToRar == null)
                    saveWriter.WriteLine(appendDateToRar + "No");
                saveWriter.WriteLine(dateLocationInBackup + _dateLocationInBackup);
                saveWriter.WriteLine(dateLengthInBackup + _dateLengthInBackup);
                saveWriter.WriteLine(dateFormat + _dateFormat);
                if (_dateSeperated != null)
                    saveWriter.WriteLine(dateSeperated + _dateSeperated);
                else if (_dateSeperated == null)
                    saveWriter.WriteLine(dateSeperated + "No");
                saveWriter.WriteLine(copyToLocation + _copyToLocation);
                if (_createNewFolder != null)
                    saveWriter.WriteLine(createNewFolder + _createNewFolder);
                else if (_createNewFolder == null)
                    saveWriter.WriteLine(createNewFolder + "No");
                if (_appendDateToFolderName != null)
                    saveWriter.WriteLine(appendDateToFolderName + _appendDateToFolderName);
                else if (_appendDateToFolderName == null)
                    saveWriter.WriteLine(appendDateToFolderName + _appendDateToFolderName);
                saveWriter.WriteLine(versionsToCopy + _versionsToCopy);
                saveWriter.WriteLine(amntToCopy + _amntToCopy);
                saveWriter.WriteLine();
                saveWriter.WriteLine("## End Database " + currDB + " Information");
            }
        }
        public int GetNumberOfDatabases(string settingsFileLoc)
        {
            int numberOfDatabases = 0;
            string numDBLine = "NumberOfDatabases: ";
            foreach (string line in settingsFileLines)
            {
                if (line.Contains(numDBLine))
                {
                    try
                    {
                        numberOfDatabases = Convert.ToInt32(line.Substring(numDBLine.Length));
                    }
                    catch (FormatException fe)
                    {
                        MessageBox.Show("Error: " + fe.Message, "Error getting number of databases!\nProgram will exit.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Environment.Exit(0);
                    }
                }
            }

            return numberOfDatabases;
        }
        public void MatchLine(string currentLine)
        {
            if (currentLine.Contains(databaseLocation))
                _databaseLocation = currentLine.Substring(databaseLocation.Length);
            else if (currentLine.Contains(numFilesToRar))
                _numFilesToRar = currentLine.Substring(numFilesToRar.Length);
            else if (currentLine.Contains(databaseExtension))
                extensionList.Add(currentLine.Substring(databaseExtension.Length));
            else if (currentLine.Contains(compressAsOne))
                _compressAsOne = currentLine.Substring(compressAsOne.Length);
            else if (currentLine.Contains(rarFileLocation))
                _rarFileLocation = currentLine.Substring(rarFileLocation.Length);
            else if (currentLine.Contains(rarFileName))
                _rarFileName = currentLine.Substring(rarFileName.Length);
            else if (currentLine.Contains(appendDateToRar))
                _appendDateToRar = currentLine.Substring(appendDateToRar.Length);
            else if (currentLine.Contains(dateLocationInBackup))
                _dateLocationInBackup = currentLine.Substring(dateLocationInBackup.Length);
            else if (currentLine.Contains(dateLengthInBackup))
                _dateLengthInBackup = currentLine.Substring(dateLengthInBackup.Length);
            else if (currentLine.Contains(dateFormat))
                _dateFormat = currentLine.Substring(dateFormat.Length);
            else if (currentLine.Contains(dateSeperated))
                _dateSeperated = currentLine.Substring(dateSeperated.Length);
            else if (currentLine.Contains(copyToLocation))
                _copyToLocation = currentLine.Substring(copyToLocation.Length);
            else if (currentLine.Contains(overwriteFiles))
                _overwriteFiles = currentLine.Substring(overwriteFiles.Length);
            else if (currentLine.Contains(createNewFolder))
                _createNewFolder = currentLine.Substring(createNewFolder.Length);
            else if (currentLine.Contains(newFolderName))
                _newFolderName = currentLine.Substring(newFolderName.Length);
            else if (currentLine.Contains(appendDateToFolderName))
                _appendDateToFolderName = currentLine.Substring(appendDateToFolderName.Length);
            else if (currentLine.Contains(versionsToCopy))
                _versionsToCopy = currentLine.Substring(versionsToCopy.Length);
            else if (currentLine.Contains(amntToCopy))
                _amntToCopy = currentLine.Substring(amntToCopy.Length);
        }
        public void FillControlFields()
        {
            //Database Info Controls
            foreach (TextBox formTB in _mainForm.Controls.OfType<TextBox>())
            {
                if (formTB.Name == "dbLocTB")
                    formTB.Text = _databaseLocation;

                else if (formTB.Name == "numFileExtTB")
                    formTB.Text = _numFilesToRar;

                else if (formTB.Name == "fileDateStartTB")
                    formTB.Text = _dateLocationInBackup;

                else if (formTB.Name == "fileDateLengthTB")
                    formTB.Text = _dateLengthInBackup;

                else if (formTB.Name == "extOneTB")
                    formTB.Text = extensionList[0];

                else if (formTB.Name == "extTwoTB")
                    formTB.Text = extensionList[1];

                else if (formTB.Name == "rarFileLocTB")
                    formTB.Text = _rarFileLocation;

                else if (formTB.Name == "rarFileNameTB")
                    formTB.Text = _rarFileName;

                else if (formTB.Name == "copyToLocTB")
                    formTB.Text = _copyToLocation;

                else if (formTB.Name == "newFolderNameTB")
                    formTB.Text = _newFolderName;

                else if (formTB.Name == "numFilesToCopyTB")
                    formTB.Text = _amntToCopy;
            }

            foreach (CheckBox formCheck in _mainForm.Controls.OfType<CheckBox>())
            {
                if (formCheck.Name == "compressAsOneCheck")
                {
                    if (_compressAsOne == "YES" || _compressAsOne == "yes" || _compressAsOne == "Yes")
                        formCheck.Checked = true;
                    else
                        formCheck.Checked = false;
                }

                else if (formCheck.Name == "fileDateSepCheck")
                {
                    if (_dateSeperated == "YES" || _dateSeperated == "yes" || _compressAsOne == "Yes")
                        formCheck.Checked = true;
                    else
                        formCheck.Checked = false;
                }

                else if (formCheck.Name == "appendDate2NameCheck")
                {
                    if (_appendDateToRar == "YES" || _appendDateToRar == "yes" || _appendDateToRar == "Yes")
                        formCheck.Checked = true;
                    else
                        formCheck.Checked = false;
                }

                else if (formCheck.Name == "createFolderCheck")
                {
                    if (_createNewFolder == "YES" || _createNewFolder == "yes" || _createNewFolder == "Yes")
                        formCheck.Checked = true;
                    else
                        formCheck.Checked = false;
                }

                else if (formCheck.Name == "appendDateCheck")
                {
                    if (_appendDateToFolderName == "YES" || _appendDateToFolderName == "yes" || _appendDateToFolderName == "Yes")
                        formCheck.Checked = true;
                    else
                        formCheck.Checked = false;
                }

                else if (formCheck.Name == "overwriteCheck")
                {
                    if (_overwriteFiles == "YES" || _overwriteFiles == "yes" || _overwriteFiles == "Yes")
                        formCheck.Checked = true;
                    else
                        formCheck.Checked = false;
                }
            }
        }
        #endregion
    }
}
