using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MessCleaner
{
    class Program
    {
        private static string logFile = "..\\..\\Logs\\MessCleaner_" + DateTime.Today.Day.ToString() + "_" +
            DateTime.Today.Month.ToString() + "_" + DateTime.Today.Year.ToString() + ".log";
        private static string debugFile = "..\\..\\Logs\\DEBUG_LOG_" + DateTime.Today.Day.ToString() + "_" +
            DateTime.Today.Month.ToString() + "_" + DateTime.Today.Year.ToString() + ".debug";
        
        const int maxSingleRarList = 75;

        private static string[] compressedFiles;
        private static string[] rarFiles;
        private static string[,] multiFileList;
        private static int[] monthList;
        private static List<string> dbInformation = new List<string>();
        private static List<string> filesToCompress = new List<string>();
        private static List<string> fileExtensions = new List<string>();
        private static string fileLocation, databaseLocation, fileDate;
        private static string listPath = null;

        private static bool dateSeperated = true;
        private static bool debugging = false;
        private static bool testing = false;

        private static int compressedFilesSize;
        private static int rarFileArraySize, dateLocation, dateLength;
        private static int numDB, numCompressedFiles = 0, numFilesToCompress, numRarFiles;

        [STAThread]
        static void Main(string[] args)
        {
            using (File.Create(logFile)) { }
            using (StreamWriter logWriter = new StreamWriter(logFile, true)) { logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Starting Program."); }

            fileLocation = "..\\..\\settings.conf";

            if (args.Length > 0)
            {
                if (args[0] == "--reset-only")
                {
                    if (File.Exists("rarFiles.txt"))
                        File.Delete("rarFiles.txt");

                    foreach (string file in Directory.EnumerateFiles("..\\..\\file_lists\\", "*.txt"))
                    {
                        File.Delete(file);
                    }

                    using (StreamWriter logWriter = new StreamWriter(logFile, true)) { logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Resetting the program."); }
                    Environment.Exit(0);
                }
                if (args[0] == "--reset-before")
                {
                    if (File.Exists("rarFiles.txt"))
                        File.Delete("rarFiles.txt");

                    foreach (string file in Directory.EnumerateFiles("..\\..\\file_lists\\", "*.txt"))
                    {
                        File.Delete(file);
                    }

                    using (StreamWriter logWriter = new StreamWriter(logFile, true)) { logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Resetting before running."); }
                }

                if (args[0] == "--update-settings")
                {
                    using (StreamWriter logWriter = new StreamWriter(logFile, true)) { logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Updating the settings file."); }
                    File.Delete(fileLocation);
                    Environment.Exit(0);
                }

                if (args[0] == "--debug-mode")
                {
                    using (StreamWriter logWriter = new StreamWriter(logFile, true))
                    {
                        logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Running in debug mode. Please refer to the debug log for more information.");
                        debugging = true;
                        using (File.Create(debugFile)) { }
                    }
                }

                if (args[0] == "--testing")
                {
                    using (StreamWriter logWriter = new StreamWriter(logFile, true))
                    {
                        logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Running in testing mode.");
                        testing = true;
                    }
                }
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Running in Debug Mode.");
                }
            }

            if (!File.Exists(fileLocation))
            {
                    configBrowser cbForm = new configBrowser();

                    cbForm.ShowDialog();
            }

            GetNumDB();
            rarFileArraySize = numDB * maxSingleRarList + (1 * numDB);
            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": The rarFileArraySize variable has been set to " + rarFileArraySize);
                }
            }
            compressedFilesSize = 25; //two weeks of files at one per day + extra

            compressedFiles = new string[compressedFilesSize];
            rarFiles = new string[rarFileArraySize];
            monthList = new int[rarFileArraySize];

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": The compressedFiles, rarFiles, and monthList arrays have been initialized.");
                }
            }

            FillRarArray();
            for (int currDB = 1; currDB <= numDB; currDB++)  //start at one because the database numbering starts at one
            {
                LoadCompressedFiles(currDB);
                LoadFilesToCompress(currDB);
                CompressFiles(currDB);
                WriteCompressedFiles();   

                dbInformation.Clear();
                Array.Clear(compressedFiles, 0, compressedFiles.Length);
                filesToCompress.Clear();
                fileExtensions.Clear();
            }

            //RemoveOldRarFiles();
            WriteRarList();

            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Finished running the program.");
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Program completed successfully!");
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Finished running the program; it completed successfully.");
                }

                Console.WriteLine("Program Completed Successfully.");
                Console.ReadKey();
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
            StreamReader fileReader = new StreamReader(fileLocation);
            string tempLine, number;

            tempLine = fileReader.ReadLine();

            while (tempLine != null)
            {
                if (tempLine.Contains("NumberOfDatabases: "))
                {
                    number = tempLine.Substring("NumberOfDatabases: ".Length);
                    numDB = Convert.ToInt32(number);
                }

                tempLine = fileReader.ReadLine();
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There are " + numDB + " databases.");
                }
            }

            using (StreamWriter logWriter = new StreamWriter(logFile, true)) 
            { 
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting number of Databases."); 
            }
        }

        private static void LoadCompressedFiles(int currDB)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true)) { logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting Compressed Files."); }
            if (currDB < 10)
                listPath = "..\\..\\file_lists\\00" + currDB.ToString() + " - files_list.txt";
            else if (currDB >= 10 && currDB < 100)
                listPath = "..\\..\\file_lists\\0" + currDB.ToString() + " - files_list.txt";
            else if (currDB >= 100)
                listPath = "..\\..\\file_lists\\" + currDB.ToString() + " - files_list.txt";
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true)) 
                { 
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Error while loading the compressed files."); 
                }
                PrintError("0x0005");
            }

            if (!File.Exists(listPath))
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true)) 
                { 
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": The file had to be created for the compressed files."); 
                }
                using (File.Create(listPath)) { }
            }

            else if (File.Exists(listPath))
            {
                using (StreamReader fileReader = new StreamReader(listPath))
                {
                    string tempLine;

                    for (int i = 0; i < compressedFilesSize; i++)
                    {
                        tempLine = fileReader.ReadLine();
                        if (tempLine != null)
                        {
                            compressedFiles[i] = tempLine;
                            numCompressedFiles++;
                        }
                        else
                            break;
                    }
                }

                if (debugging)
                {
                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Compressed Files were loaded from the File List and put into the array.");

                        for (int i = 0; i < compressedFiles.Length; i++)
                        {
                            debugWriter.WriteLine("\t\tcompressedFiles[" + i + "] = " + compressedFiles[i]);
                        }
                    }
                }

                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": The file existed and was read from.");
                }
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error in the LoadCompressedFiles function.");
                }
                PrintError("0x0055");
            }
        }

        private static void LoadFilesToCompress(int currDB)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true)) 
            { 
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Loading the information."); 
            }

            StreamReader fileReader = new StreamReader(fileLocation);
            string tempLine = fileReader.ReadLine();
            string currDBHeader = "## Database " + currDB.ToString() + " Information";
            string currDBFooter = "## End Database " + currDB.ToString() + " Information";
            string dbLocationKey = "DatabaseLocation: ";
            string numFilesToCompressKey = "NumberOfFilesToRar: ";
            string fileExtensionKey = "DatabaseBackupExtension: ";
            int count = 0;
            int searchLocation, extensionStartPos;

            #region LoadInfo
            while (tempLine != currDBHeader)
            {
                tempLine = fileReader.ReadLine();
            }

            if (tempLine == currDBHeader)
                //remove the whitespace after the header
                tempLine = fileReader.ReadLine();

            while (tempLine != currDBFooter)
            {
                tempLine = fileReader.ReadLine();
                dbInformation.Add(tempLine);
                count++;
            }

            if (tempLine == currDBFooter)
            {
                //remove the footer and blankspace
                dbInformation.RemoveRange(dbInformation.Count - 2, 2);
            }
            #endregion

            //get database location
            if ((searchLocation = SearchInfo(dbLocationKey)) != -1)
            {
                //included +1 because it adds an unknown space at the beginning of the file path
                databaseLocation = dbInformation[searchLocation].Substring(dbInformation.IndexOf(dbLocationKey) + dbLocationKey.Length + 1);
            }
            else
            {
                ThrowFileError();
            }

            //get number of files to RAR
            if ((extensionStartPos = SearchInfo(numFilesToCompressKey)) != -1)
            {
                string temp;
                temp = dbInformation[extensionStartPos].Substring(dbInformation.IndexOf(numFilesToCompressKey) + numFilesToCompressKey.Length);
                numFilesToCompress = Convert.ToInt32(temp);

                //compressedFiles = new string[numFilesToCompress * 14];
            }
            else
            {
                ThrowFileError();
            }
            //read next lines (equal to number of files to RAR) for file extensions to look for
            for (int i = 1; i <= numFilesToCompress; i++)
            {
                //included the +1 because it adds an unknown space at the beginning of the file extension
                fileExtensions.Add(dbInformation[extensionStartPos + i].Substring(dbInformation.IndexOf(fileExtensionKey) + fileExtensionKey.Length + 1));
            }           

            //load all files to list
            for (int j = 0; j < fileExtensions.Count; j++)
            {
                foreach (string backupFile in Directory.EnumerateFiles(databaseLocation, "*" + fileExtensions[j], SearchOption.TopDirectoryOnly))
                {
                    filesToCompress.Add(backupFile);
                }
            }

            fileReader.Close();

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": FileExtensions have been loaded into a list.");

                    for (int i = 0; i < fileExtensions.Count; i++)
                    {
                        debugWriter.WriteLine("\t\tfileExtentsions[" + i + "] = " + fileExtensions[i]);
                    }
                }
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": FilesToCompress have been loaded into a list.");

                    for (int i = 0; i < filesToCompress.Count; i++)
                    {
                        debugWriter.WriteLine("\t\tfilesToCompress[" + i + "] = " + filesToCompress[i]);
                    }
                }
            }
        }

        private static void CompressFiles(int currDB)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true)) 
            { 
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting ready to compress the file."); 
            }
            bool appendDate = false;

            if (dbInformation[SearchInfo("AppendDateToRARName")].Contains("Yes"))
            {
                appendDate = true;
            }
            else if (dbInformation[SearchInfo("AppendDateToRARName")].Contains("No"))
            {
                appendDate = false;
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error in the CompressFiles function.");
                } 
                PrintError("0x0007");
            }

            if (numFilesToCompress == 1)
            {
                foreach (string file in filesToCompress)
                {
                    if (SearchCompressedFiles(file) == 1)
                    {
                            if (appendDate)
                            {
                                RemoveDate(file);
                                GetDateFormat();
                                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                                {
                                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Performing date operations.");
                                }
                                WriteBatFile(file, true);
                            }
                            else if (!appendDate)
                            {
                                WriteBatFile(file, false);
                            }
                            else
                            {
                                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                                {
                                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error in CompressFiles trying to append date.");
                                }
                                PrintError("0x0006");
                            }
                    }
               }
            }
            else if (numFilesToCompress > 1)
            {
                MultiExtensionCompress();
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error getting the number of file types.");
                }
                PrintError("0x000A");
            }
        }

        private static int SearchInfo(string key)
        {
            /*
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Searching through the information for " + key + ".");
            }
            */
            int count, keyLocation = -1;

            for (count = 0; count < dbInformation.Count; count++)
            {
                if (dbInformation[count].Contains(key))
                {
                    keyLocation = count;
                    break;
                }
            }

            return keyLocation;
        }

        private static void ThrowFileError()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was a File Error.");
            }

            DialogResult dr;
            dr = MessageBox.Show("There was an error reading your \"Settings\" file.\nWould you like to open the editor to fix it?",
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

        private static int SearchCompressedFiles(string currentFile)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Searching for a compressed files.");
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Searching through the compressedFiles list for this file " + currentFile + ".\n");
                }
            }

            for (int i = 0; i < compressedFilesSize; i++)
            {
                if (debugging)
                {
                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine("\t\tCurrent File - " + currentFile);
                    }
                }

                if (compressedFiles[i] == currentFile)
                {
                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Found! Compressed File: " + compressedFiles[i] + " .:. currentFile: " + currentFile);
                        }
                    }
                    return 0;                    
                }
            }
            return 1;
        }

        private static void RemoveDate(string currentFile)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Removing the date from the current file.");
            }

            if (currentFile != null)
            {
                string fileName = currentFile.Substring(databaseLocation.Length);
                string tempLocation = null, tempLength = null;
                foreach (string info in dbInformation)
                {
                    if (info.Contains("DateLocationInBackup: "))
                    {
                        tempLocation = info;
                    }
                    else if (info.Contains("DateLengthInBackup: "))
                    {
                        tempLength = info;
                    }
                }
                
                tempLocation = tempLocation.Substring("DateLocationInBackup: ".Length);
                tempLength = tempLength.Substring("DateLengthInBackup: ".Length);

                dateLocation = Convert.ToInt32(tempLocation);
                dateLength = Convert.ToInt32(tempLength);
                fileDate = fileName.Substring((dateLocation - 1), dateLength);
            }
            else if (currentFile == null)
            {
                string tempLocation = null, tempLength = null;

                foreach (string info in dbInformation)
                {
                    if (info.Contains("DateLocationInBackup: "))
                    {
                        tempLocation = info;
                    }
                    else if (info.Contains("DateLengthInBackup: "))
                    {
                        tempLength = info;
                    }
                }

                tempLocation = tempLocation.Substring("DateLocationInBackup: ".Length);
                tempLength = tempLength.Substring("DateLengthInBackup: ".Length);
                dateLocation = Convert.ToInt32(tempLocation);
                dateLength = Convert.ToInt32(tempLength);
            }
        }

        private static void GetDateFormat()
        {
            DateSeperated();
            string dateFormat = "DateFormat: ";

            foreach (string info in dbInformation)
            {
                if (info.Contains(dateFormat))
                {
                    if (dateSeperated)
                    {
                        int monthLoc = GetMonthLoc(info.Substring(dateFormat.Length));
                        ReadDate(8, 10, monthLoc);
                    }
                    else if (!dateSeperated)
                    {
                        int monthLoc = GetMonthLoc(info.Substring(dateFormat.Length));
                        ReadDate(6, 8, monthLoc);
                    }
                    else
                    {
                        using (StreamWriter logWriter = new StreamWriter(logFile, true))
                        {
                            logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error getting the date format.");
                        }
                        PrintError("0x0015");
                    }
                }
            }
        }

        private static void PrintError(string errorCode)
        {
            Console.WriteLine("Error: " + errorCode);
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void WriteBatFile(string currentFile, bool appendDate)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Writing the Batch file.");
            }

            string rarLocation = null;
            string fileName = null;
            string rarName = null;
            foreach (string info in dbInformation)
            {
                if (info.Contains("RARFileLocation: "))
                {
                    rarLocation = info.Substring("RARFileLocation: ".Length);
                }
                else if (info.Contains("RARFileName: "))
                {
                    fileName = info.Substring("RARFileName: ".Length);
                }
            }

            StreamWriter batchWriter = new StreamWriter("zipFiles.bat");

            if (appendDate)
            {
                rarName = rarLocation + fileName + "-" + fileDate + ".rar";
                batchWriter.WriteLine("SET RAR_PATH=" + rarName);
            }
            else if (!appendDate)
            {
                rarName = rarLocation + fileName + ".rar";
                batchWriter.WriteLine("SET RAR_PATH=" + rarName);
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error attaching the date to the .rar file.");
                }
                PrintError("0x0008");
            }

            batchWriter.WriteLine("\"C:\\Program Files\\7-Zip\\7z.exe\" a \"%RAR_PATH%\" " + currentFile + " -mx5");
            //batchWriter.WriteLine("PAUSE");
            batchWriter.Close();
            
            //write file to the correct file list
            if (numCompressedFiles < 25)
            {
                if (debugging)
                {
                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Adding file " + currentFile + " to the compressedFiles array.");
                    }
                }
                compressedFiles[numCompressedFiles] = currentFile;
                numCompressedFiles++;
            }
            else if (numCompressedFiles >= 25)
            {
                PushCurrentFiles(currentFile);
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error writing the file to the list of compressed files.");
                }
                PrintError("0x0009");
            }

            AddRarFile(rarName);
            Process batch = Process.Start("zipFiles.bat");
            
            batch.WaitForExit();

            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": " + rarName + " was compressed from file: " + currentFile);
            }
        }

        private static void WriteCompressedFiles()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Writing the compressed files to a text file.");
            }

            StreamWriter fileWriter = new StreamWriter(listPath);

            for (int i = 0; i < compressedFilesSize; i++)
            {
                if (compressedFiles[i] != null)
                {
                    fileWriter.WriteLine(compressedFiles[i]);
                }
            }

            fileWriter.Close();

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Writing all the compressedFles to the Files_List file.");
                }
            }
        }

        private static void PushCurrentFiles(string fileToAdd)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Pushing the oldest file out of the current file list.");
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Removing " + compressedFiles[0] + " from the compressedFiles array and " +
                                                                    "and adding in " + fileToAdd);
                }
            }

            for (int i = 0; i < compressedFilesSize-1; i++)
            {
                compressedFiles[i] = compressedFiles[i + 1];
            }

            compressedFiles[compressedFilesSize - 1] = fileToAdd;
        }

        private static void DateSeperated()
        {
            int location = SearchInfo("DateSeperated: ");
            string dateSep = dbInformation[location].Substring("DateSeperated: ".Length);

            if (dateSep == "Yes")
                dateSeperated = true;
            else if (dateSep == "No")
                dateSeperated = false;
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error determining whether the date is seperated.");
                }
                PrintError("0x0014");
            }
        }

        private static void ReadDate(int lengthShort, int lengthLong, int monthLoc)
        {
            /*
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Reading the date.");
            }
            */
            if (fileDate.Length == lengthShort)
            {
                AddMonth(monthLoc, true);
            }
            else if (fileDate.Length == lengthLong)
            {
                AddMonth(monthLoc, false);
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error reading the date.");
                }
                PrintError("0x0020");
            }
        }

        private static void FillRarArray()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Filling an array with the files that have been compressed.");
            }

            if (!File.Exists("rarFiles.txt"))
            {
                using (StreamWriter writer = new StreamWriter("rarFiles.txt"))
                {
                    writer.WriteLine("0");
                    numRarFiles = 0;
                }
            }

            else if (File.Exists("rarFiles.txt"))
            {
                int count = 0;
                using (StreamReader reader = new StreamReader("rarFiles.txt"))
                {                    
                    string temp;
                    numRarFiles = Convert.ToInt32(reader.ReadLine());
                    
                    temp = reader.ReadLine();
                    while (temp != null)
                    {
                        string[] splitLine = temp.Split(' ');

                        if (testing)
                        {
                            if (count > 0)
                            {
                                Console.WriteLine("Count: " + count + "; monthList.Length: " + monthList.Length + "; rarFiles.Length: " + rarFiles.Length +
                                        "; splitLine.Length: " + splitLine.Length + "; splitLine[0]: " + splitLine[0] + "; splitLine[1]: " + splitLine[1] +
                                        "; monthList[count-1]: " + monthList[count - 1] + "; rarFiles[count-1]: " + rarFiles[count - 1]);
                            }
                        }

                        AddRarFile(splitLine[0]);
                        try
                        {
                           AddMonthFile(Convert.ToInt32(splitLine[1]));
                        }
                        catch (FormatException feExcep)
                        {
                            Console.WriteLine(feExcep.Message);
                            Console.WriteLine(feExcep.StackTrace);
                            Console.ReadKey();
                        }
                        count++;
                        temp = reader.ReadLine();
                    }
                }
                if (debugging)
                {
                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": The rarFiles and monthList arrays were filled. These are there contents.");
                        for (int i = 0; i < count; i++)
                        {
                            debugWriter.WriteLine("\t\trarFiles[" + i + "] = " + rarFiles[i]);
                        }
                        debugWriter.WriteLine();
                        for (int i = 0; i < count; i++)
                        {
                            debugWriter.WriteLine("\t\tmonthList[" + i + "] = " + monthList[i]);
                        }
                    }
                }
            }            
        }

        private static void AddRarFile(string fileName)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Adding files to the array of .rar files.");
            }

            if (numRarFiles < rarFileArraySize)
            {
                if (debugging)
                {
                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Adding the file " + fileName + " to the rarFiles array.");
                    }
                }
                rarFiles[numRarFiles] = fileName;
                numRarFiles++;
            }
            else if (numRarFiles >= rarFileArraySize)
            {
                PushRarFiles(fileName);
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error adding the .rar file.");
                }
                PrintError("0x0018");
            }
        }

        private static void PushRarFiles(string fileToAdd)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Pushing out the oldest .rar file from the array of .rar files.");
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Removing the file " + rarFiles[0] + "from the array and adding " + fileToAdd);
                }
            }

            for (int i = 0; i < (rarFileArraySize-1); i++)
            {
                rarFiles[i] = rarFiles[i + 1];
            }

            rarFiles[rarFileArraySize - 1] = fileToAdd;
        }

        private static void WriteRarList()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Writing the array of .rar files to a text file.");
            }

            StreamWriter writer = new StreamWriter("rarFiles.txt");

            writer.WriteLine(numRarFiles);
            for (int i = 0; i < rarFileArraySize; i++)
            {
                if (rarFiles[i] != null)
                {
                    writer.WriteLine(rarFiles[i] + " " + monthList[i]);
                }
            }

            writer.Close();

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Writing the rarFiles list to the rarFiles text file.");

                    for (int i = 0; i < rarFiles.Length; i++)
                    {
                        debugWriter.WriteLine("\t\trarFiles[" + i + "] = " + rarFiles[i]);
                    }
                }
            }
        }

        private static int GetMonthLoc(string dateFormat)
        {
            /*
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Getting the location of the \"month\" of the file.");
            }
            */
            int monthPos = -1;
            foreach (char letter in dateFormat)
            {
                if (letter.CompareTo('M') == 0)
                {
                    if (monthPos == -1)
                        monthPos = 0;
                    else
                        break;
                }
                else
                    monthPos++;
            }

            if (monthPos == -1)
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error getting the position of the \"month\" of the file.");
                }
                PrintError("0x0019");
                return monthPos;
            }
            else
                return monthPos+1;
        }

        private static void AddMonth(int monthLoc, bool shortDate)
        {
            /*
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Adding the month the file was created.");
            }
            */
            if (!dateSeperated)
                monthList[numRarFiles] = Convert.ToInt32(fileDate.Substring(monthLoc, 2));
            else if (dateSeperated)
            {
                if (shortDate)
                {
                    switch (monthLoc)
                    {
                        case 0:
                            monthList[numRarFiles] = Convert.ToInt32(fileDate.Substring(monthLoc, 2));
                            break;
                        case 2:
                            monthList[numRarFiles] = Convert.ToInt32(fileDate.Substring(monthLoc + 1, 2));
                            break;
                        case 4:
                            monthList[numRarFiles] = Convert.ToInt32(fileDate.Substring(monthLoc + 2, 2));
                            break;
                    }
                }
                else if (!shortDate)
                {
                    switch (monthLoc)
                    {
                        case 0:
                            monthList[numRarFiles] = Convert.ToInt32(fileDate.Substring(monthLoc, 2));
                            break;
                        case 2:
                        case 4:
                            monthList[numRarFiles] = Convert.ToInt32(fileDate.Substring(monthLoc + 1, 2));
                            break;
                        case 6:
                            monthList[numRarFiles] = Convert.ToInt32(fileDate.Substring(monthLoc + 2, 2));
                            break;
                    }
                }
                else
                {
                 
                    using (StreamWriter logWriter = new StreamWriter(logFile, true))
                    {
                        logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error getting the month.");
                    }
                    PrintError("0x0023");
                }
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error determining the month format.");
                }
                PrintError("0x0022");
            }
        }
        
        private static void RemoveOldRarFiles()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Removing the old .rar files.");
            }
            int currentLocation = 0;
            int currentMonth = System.DateTime.Today.Month;
            int monthToDelete = -1;

            switch (currentMonth)
            {
                case 0:
                    break;
                case 1:
                    monthToDelete = 11;
                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Remove files from the " + monthToDelete + " month.");
                        }
                    }
                    break;
                case 2:
                    monthToDelete = 12;
                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Remove files from the " + monthToDelete + " month.");
                        }
                    }
                    break;
                default:
                    monthToDelete = currentMonth - 2;
                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Remove files from the " + monthToDelete + " month.");
                        }
                    }
                    break;
            }

            if (monthToDelete != -1)
            {
                foreach (int month in monthList)
                {
                    if (month != 0)
                    {
                        if (month == monthToDelete)
                        {
                            if (debugging)
                            {
                                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                                {
                                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Removing rarFiles[" + currentLocation + "], " + rarFiles[currentLocation]);
                                }
                            }
                            File.Delete(rarFiles[currentLocation]);
                            numRarFiles--;
                            rarFiles[currentLocation] = null;
                            monthList[currentLocation] = 0;
                        }

                        currentLocation++;
                    }
                }
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error removing the old .rar files.");
                }
                PrintError("0x0021");
            }
        }

        private static void MultiExtensionCompress()
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Working in the Multi Extension Compress function.");
            }

            List<string> files2Compress = new List<string>();
            List<string> localCompressedFiles = new List<string>();
            List<string> dates = new List<string>();

            bool appendDate = true;
            bool found = false;

            RemoveDate(null); // to get dateLocation and dateLength

            int arrSize;

            if (filesToCompress.Count % numFilesToCompress == 0)
            {
                arrSize = filesToCompress.Count / numFilesToCompress;
            }
            else
            {
                arrSize = (filesToCompress.Count / numFilesToCompress) + 1;
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": arrSize variable set to " + arrSize + " by dividing filesToCompress.Count(" + 
                                            filesToCompress.Count + ") numFilesToCompress(" + numFilesToCompress + ")");
                }
            }

            multiFileList = new string[arrSize, numFilesToCompress];
            int xCount = 0, yCount = 1;
            int xPos = 1, yPos = 0;

            for (; xCount < filesToCompress.Count - 1; xCount++)
            {
                for (; yCount < filesToCompress.Count; yCount++)
                {
                    string temp1 = filesToCompress[xCount].Substring(databaseLocation.Length);
                    string temp2 = filesToCompress[yCount].Substring(databaseLocation.Length);

                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": temp1 set to: " + temp1 + " and temp2 set to: " + temp2);
                            debugWriter.WriteLine("\t\t\tThe above variables are being compared.");
                        }
                    }

                    if (temp1.Substring(temp1.IndexOf('.')) != ".trn")
                    {
                        if (temp1.Substring(0, dateLocation + dateLength) == temp2.Substring(0, dateLocation + dateLength))
                        {
                            multiFileList[yPos, xPos - 1] = temp1;
                            multiFileList[yPos, xPos] = temp2;

                            if (debugging)
                            {
                                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                                {
                                    debugWriter.WriteLine("\tmultiFileList[" + yPos + ", " + (xPos - 1) + "] set to: " + multiFileList[yPos, (xPos - 1)]);
                                    debugWriter.WriteLine("\tmultiFileList[" + yPos + ", " + xPos + "] set to: " + multiFileList[yPos, xPos]);
                                }
                            }

                            yPos++;
                            break;
                        }
                    }
                }
                yCount = xCount + 2;
            }

            using (StreamReader reader = new StreamReader(listPath))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    localCompressedFiles.Add(line);
                    line = reader.ReadLine();
                }
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Looking through the files to see which files need to be compressed.");
                }
            }

            for (int y = 0; y < yPos; y++)
            {
                string temp = multiFileList[y, 0] + " " + multiFileList[y, 1];
                string tempCheck = databaseLocation + multiFileList[y, 0] + " " + databaseLocation + multiFileList[y, 1];

                foreach (string fileLine in localCompressedFiles)
                {
                    if (debugging)
                    {
                        using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                        {
                            debugWriter.WriteLine("\tfileLine = " + fileLine);
                            debugWriter.WriteLine("\ttempCheck = " + tempCheck);
                        }
                    }

                    if (tempCheck == fileLine)
                    {
                        found = true;
                        break;
                    }
                    else
                        found = false;
                }

                if (!found)
                {
                    files2Compress.Add(temp);
                    fileDate = temp;
                    fileDate = fileDate.Substring(dateLocation - 1, dateLength);
                    string tempPass = databaseLocation + multiFileList[y, 0] + " " + databaseLocation + multiFileList[y, 1];
                    GetDateFormat();                        
                    WriteBatFile(tempPass, appendDate);
                }
            }

            foreach (string info in dbInformation)
            {
                if (info.Contains("AppendDateToRARName: "))
                {
                    if (info.Substring("AppendDateToRARName: ".Length) == "Yes")
                    {
                        appendDate = true;
                    }
                    else
                        appendDate = false;
                }
            }

            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Finished in the Multi Extension Compress function.");
            }
        }

        private static void AddMonthFile(int month)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Adding files to the array of .rar files.");
            }

            if (numRarFiles < rarFileArraySize)
            {
                if (debugging)
                {
                    using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                    {
                        debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Adding the month " + month + " to the monthLIst array.");
                    }
                }
                monthList[numRarFiles] = month;
            }
            else if (numRarFiles >= rarFileArraySize)
            {
                PushMonthFiles(month);
            }
            else
            {
                using (StreamWriter logWriter = new StreamWriter(logFile, true))
                {
                    logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": There was an error adding the month to the list.");
                }
                PrintError("0x0018");
            }
        }

        private static void PushMonthFiles(int month)
        {
            using (StreamWriter logWriter = new StreamWriter(logFile, true))
            {
                logWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Pushing out the oldest month from the array of file months");
            }

            if (debugging)
            {
                using (StreamWriter debugWriter = new StreamWriter(debugFile, true))
                {
                    debugWriter.WriteLine(DateTime.Now.ToLocalTime() + ": Removing the month " + monthList[0] + "from the array and adding " + month + " to the month list.");
                }
            }

            for (int i = 0; i < (rarFileArraySize - 1); i++)
            {
                monthList[i] = monthList[i + 1];
            }

            monthList[rarFileArraySize - 1] = month;
        }
    }
}
