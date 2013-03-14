using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace BackYourMessUp
{
    class Program
    {
        private static string logFile = "..\\..\\Logs\\BackYourMessUp_" + DateTime.Today.Day.ToString() + "_" +
            DateTime.Today.Month.ToString() + "_" + DateTime.Today.Year.ToString() + ".log";
        private static string debugFile;

        private static string[] fileArray;
        private static List<string> dbInformation = new List<string>();
        private static List<string> files = new List<string>();
        private static string fileLocation, databaseLocation;
        private static string copyLocation, FolderName, fileName;
        private static bool createFolder = false, appendDate = true, overwriteFiles = false, debugging = false;
        private static int numDB, numFilesToCP;

        private enum versionToCP
        {
            Newest,
            Oldest,
            All
        };
        private static versionToCP cpVersion = versionToCP.Newest;

        [STAThread]
        static void Main(string[] args)
        {
            using (File.Create(logFile)) { }
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Starting the program.");
            }
            fileLocation = "..\\..\\settings.conf";

            if (args.Length > 0)
            {
                if (args[0] == "--update-settings")
                {
                    File.Delete(fileLocation);
                    Environment.Exit(0);
                }

                if (args[0] == "--debug-mode")
                {
                    debugFile = ("..\\..\\Logs\\BackYourMessUp_" + DateTime.Today.Day.ToString() + "_" +
            DateTime.Today.Month.ToString() + "_" + DateTime.Today.Year.ToString() + ".DEBUG");

                    using (File.Create(debugFile)) { }

                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Starting BackYourMessUp in DEBUG mode.");
                    }
                    using (StreamWriter logWriter = new StreamWriter(logFile, true))
                    {
                        logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Starting BackYourMessUp in DEBUG mode.");
                    }

                    debugging = true;
                }
            }
            if (!File.Exists(fileLocation))
            {
                configBrowser cbForm = new configBrowser();

                cbForm.ShowDialog();
            }

            GetNumDB();

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Number of databases found: " + numDB);
                }
            }

            for (int currDB = 0; currDB < numDB; currDB++)
            {                
                LoadFileInfo(currDB + 1);
                PrepareFolder();
                GetFilesToCopy();
                CopyListToArray();
                SortArray();

                MoveFiles();

                ClearArrays();
            }

            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Finished executing the program.");
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Program completed Successfully.");
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Program completed successfully.");
                    Console.WriteLine("Program Completed Successfully.");
                    Console.ReadKey();
                }
            }
        }

        private static int IntegrityCheck(string path)
        {
            if (File.Exists(path))
                return 0;
            else if (!File.Exists(path))
                return 1;
            else
                return -1;
        }

        private static void GetNumDB()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting the number of databases.");
            }
            StreamReader fileReader = new StreamReader(fileLocation);
            string tempLine, number;

            tempLine = fileReader.ReadLine();

            while (tempLine != null)
            {
                if (tempLine.Contains("NumberOfDatabases: "))
                {
                    number = tempLine.Substring("NumberOfDatabases: ".Length);
                    //TODO: error checking
                    numDB = Convert.ToInt32(number);
                }

                tempLine = fileReader.ReadLine();
            }
        }

        private static void LoadFileInfo(int currDB)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Loading the file information.");
            }
            StreamReader fileReader = new StreamReader(fileLocation);
            string tempLine = fileReader.ReadLine();

            string currDBHeader = "## Database " + currDB.ToString() + " Information";
            string currDBFooter = "## End Database " + currDB.ToString() + " Information";

            while (tempLine != currDBHeader)
            {
                tempLine = fileReader.ReadLine();
            }

            if (tempLine == currDBHeader)
                tempLine = fileReader.ReadLine();

            while (tempLine != currDBFooter)
            {
                tempLine = fileReader.ReadLine();

                dbInformation.Add(tempLine);
            }

            if (tempLine == currDBFooter)
            {
                dbInformation.RemoveRange(dbInformation.Count - 2, 2);
            }

            fileReader.Close();

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Settings file information loaded.");
                    for (int i = 0; i < dbInformation.Count; i++)
                    {
                        debugWriter.WriteLine("\t\tDatabaseInfo[" + i + "] = " + dbInformation[i]);
                    }
                }
            }
        }

        private static void ThrowFileError()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was a file error.");
            }

            DialogResult dr;
            dr = MessageBox.Show("There was an error reading your \"Settings\" file.\nWould you liketo open the editor to fix it?",
                                    "Error!", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                //execute editor
            }
            else if (dr == DialogResult.No)
                Environment.Exit(0);
            else
            {
                PrintError("0x0001");
            }
        }

        private static void PrintError(string errorCode)
        {
            Console.WriteLine("Error: " + errorCode);
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void PrepareFolder()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Preparing the folder for the files to be copied to.");
            }

            string folderLocation = null;
            LoadData();

            if (createFolder)
            {
                if (appendDate)
                    folderLocation = copyLocation + FolderName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year;
                else if (!appendDate)
                    folderLocation = copyLocation + FolderName;
                else
                {
                    using (StreamWriter logWriter = new StreamWriter(logFile, true))
                    {
                        logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error attaching the date to the folder.");
                    }
                    PrintError("0x0033");
                }

                Directory.CreateDirectory(folderLocation);

                if (debugging)
                {
                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": A new folder needs to be created.");
                        debugWriter.WriteLine("\t\t\tfolderLocation = " + folderLocation);
                        debugWriter.WriteLine("\t\t\tcopyLocation = " + copyLocation);
                    }
                }

                copyLocation = folderLocation;

                if (debugging)
                {
                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": copyLocation changed to folderLocation. (" + copyLocation + ")");
                    }
                }
            }
        }

        private static void GetFilesToCopy()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting the location of the .rar files.");
            }

            foreach (string info in dbInformation)
            {
                if (info.Contains("RARFileLocation: "))
                {
                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting the RARFileLocation from this location: " + info);
                        }
                    }
                    databaseLocation = info.Substring("RARFileLocation: ".Length);
                }
            }
            GetFiles();
        }

        private static void GetFiles()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting all the .rar files from the directory.");
            }
            foreach (string file in Directory.EnumerateFiles(databaseLocation, "*.rar", SearchOption.TopDirectoryOnly))
            {
                files.Add(file);
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": The following files have been added to the file list.");
                    for (int count = 0; count < files.Count; count++)
                    {
                        debugWriter.WriteLine("\t\t\tfiles[" + count + "] = " + files[count]);
                    }
                }
            }
        }

        private static void CopyListToArray()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Copying the list to an array.");
            }

            fileArray = new string[files.Count];

            for (int i = 0; i < files.Count; i++)
            {
                fileArray[i] = files[i];
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Copying the files list to the fileArray list. The complete fileArary list:");
                    for (int count = 0; count < fileArray.Length; count++)
                    {
                        debugWriter.WriteLine("\t\t\tfileArray[" + count + "] = " + fileArray[count]);
                    }
                }
            }
        }

        private static void SortArray()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Sorting the array of .rar files.");
            }

            string temp;
            int round, i;

            for (round = fileArray.Length; round > 0; round--)
            {
                for (i = 1; i < round; i++)
                {
                    if (DateTime.Compare(File.GetLastWriteTime(fileArray[i - 1]), File.GetLastWriteTime(fileArray[i])) > 0)
                    {
                        temp = fileArray[i];
                        fileArray[i] = fileArray[i - 1];
                        fileArray[i - 1] = temp;
                    }
                }
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": The array has been sorted into this order:");
                    for (int count = 0; count < fileArray.Length; count++)
                    {
                        debugWriter.WriteLine("\t\t\tfileArray[" + count + "] = " + fileArray[count]);
                    }
                }
            }
        }

        private static void MoveFiles()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Moving all the files to new folder.");
            }

            int numFiles = fileArray.Length-1;

            if (numFiles != -1)
            {
                if (cpVersion == versionToCP.Newest)
                {
                    for (int i = numFiles; i > (numFiles - numFilesToCP); i--)
                    {
                        if (File.Exists(copyLocation + "\\" + fileName) && overwriteFiles ||
                            !File.Exists(copyLocation + "\\" + fileName))
                        {
                            GetFileName(fileArray[i]);
                            File.Copy(fileArray[i], copyLocation + "\\" + fileName, true);

                            if (debugging)
                            {
                                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                                {
                                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Copying file(" + fileName + ") to the location(" + copyLocation + ")");
                                }
                            }
                        }
                    }
                }
                else if (cpVersion == versionToCP.Oldest)
                {
                    for (int i = 0; i < numFilesToCP; i++)
                    {
                        if (File.Exists(copyLocation + "\\" + fileName) && overwriteFiles ||
                            !File.Exists(copyLocation + "\\" + fileName))
                        {
                            GetFileName(fileArray[i]);
                            File.Copy(fileArray[i], copyLocation + "\\" + fileName, true);

                            if (debugging)
                            {
                                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                                {
                                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Copying file(" + fileName + ") to the location (" + copyLocation + ")");
                                }
                            }
                        }
                    }
                }
                else if (cpVersion == versionToCP.All)
                {
                    for (int i = 0; i < fileArray.Length; i++)
                    {
                        if (File.Exists(copyLocation + "\\" + fileName) && overwriteFiles ||
                            !File.Exists(copyLocation + "\\" + fileName))
                        {
                            GetFileName(fileArray[i]);
                            File.Copy(fileArray[i], copyLocation + "\\" + fileName, true);

                            if (debugging)
                            {
                                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                                {
                                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Copying file(" + fileName + ") to the location (" + copyLocation + ")");
                                }
                            }
                        }
                    }
                }
                else
                {
                    using (StreamWriter logWriter = new StreamWriter(logFile, true))
                    {
                        logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error determining which version of the file to copy.");
                    }
                    PrintError("0x0035");
                }
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error getting the files to move.");
                }
                PrintError("0x000F");
            }
        }

        private static void LoadData()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Parsing all the data from the \"settings.conf\" file.");
            }

            foreach (string info in dbInformation)
            {
                if (info.Contains("RARFileLocation: "))
                {
                    databaseLocation = info.Substring("RARFileLocation: ".Length);

                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": RARFileLocation set to: " + databaseLocation);
                        }
                    }
                }
                else if (info.Contains("CopyToLocation: "))
                {
                    copyLocation = info.Substring("CopyToLocation: ".Length);

                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": CopyLocation set to: " + copyLocation);
                        }
                    }
                }
                else if (info.Contains("CreateNewFolderOnArrival: "))
                {
                    if (info.Substring("CreateNewFolderOnArrival: ".Length) == "Yes")
                        createFolder = true;
                    else if (info.Substring("CreateNewFolderOnArrival: ".Length) == "No")
                        createFolder = false;
                    else
                    {
                        using (StreamWriter logWriter = new StreamWriter(logFile, true))
                        {
                            logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error determining if a new file should be made.");
                        }
                        PrintError("0x0030");
                    }

                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": CreateNewFolderOnArrival set to: " + createFolder);
                        }
                    }
                }
                else if (info.Contains("NewFolderName: "))
                {
                    FolderName = info.Substring("NewFolderName: ".Length);
                }
                else if (info.Contains("AppendDateToFolderName: "))
                {
                    if (info.Substring("AppendDateToFolderName: ".Length) == "Yes")
                        appendDate = true;
                    else if (info.Substring("AppendDateToFolderName: ".Length) == "No")
                        appendDate = false;
                    else
                    {
                        using (StreamWriter logWriter = new StreamWriter(logFile, true))
                        {
                            logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error determining if the date should be appended to the folder.");
                        }
                        PrintError("0x0031");
                    }

                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": AppendDateToFolderName set to: " + appendDate);
                        }
                    }
                }
                else if (info.Contains("VersionsToCopy: "))
                {
                    if (info.Substring("VersionsToCopy: ".Length) == "Newest")
                        cpVersion = versionToCP.Newest;
                    else if (info.Substring("VersionsToCopy: ".Length) == "Oldest")
                        cpVersion = versionToCP.Oldest;
                    else if (info.Substring("VersionsToCopy: ".Length) == "All")
                        cpVersion = versionToCP.All;
                    else
                    {
                        using (StreamWriter logWriter = new StreamWriter(logFile, true))
                        {
                            logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error determining which version of the file to copy.");
                        }
                        PrintError("0x0032");
                    }

                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": VersionsToCopy set to: " + cpVersion);
                        }
                    }
                }
                else if (info.Contains("AmountToCopy: "))
                {
                    numFilesToCP = Convert.ToInt32(info.Substring("AmountToCopy: ".Length));

                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": AmountToCopy set to: " + numFilesToCP);
                        }
                    }
                }
                else if (info.Contains("OverwriteFiles: "))
                {
                    if (info.Substring("OverwriteFiles: ".Length) == "Yes")
                    {
                        overwriteFiles = true;
                    }
                    else if (info.Substring("OverwriteFiles: ".Length) == "No")
                    {
                        overwriteFiles = false;
                    }
                    else
                    {
                        using (StreamWriter logWriter = new StreamWriter(logFile, true))
                        {
                            logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error determining if the destination files should be overwritten.");
                        }
                        PrintError("0x0066");
                    }

                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": OverwriteFiles set to: " + overwriteFiles);
                        }
                    }
                }
            }
        }

        private static void GetFileName(string fileLocation)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting the files name.");
            }
            fileName = fileLocation.Substring(databaseLocation.Length);

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Got fileName(" + fileName + ") from fileLocation (" + fileLocation + ")");
                }
            }
        }

        private static void ClearArrays()
        {
            Array.Clear(fileArray, 0, fileArray.Length);
            dbInformation.Clear();
            files.Clear();
        }
    }
}
