using System;
using System.Collections.Generic;
using BasicxLogger;

namespace ReRead
{
    class DirectoryMode
    {
        private readonly FileLogger _logger;
        private readonly FileEditor _fileEditor;
        private readonly FileHandler _fileHandler;
        private readonly DirectoryHandler _directoryHandler;
        private readonly MessagePrinter _messagePrinter;

        public DirectoryMode(FileLogger logger, FileEditor fileEditor, FileHandler fileHandler,
                                DirectoryHandler directoryHandler, string directory)
        {
            _logger = logger;
            _fileEditor = fileEditor;
            _fileHandler = fileHandler;
            _directoryHandler = directoryHandler;

            _messagePrinter = new MessagePrinter(_logger);

            Run(directory);
        }

        private void Run(string directory)
        {
            if (_directoryHandler.CheckDirectory(directory))
            {
                List<string> files = _directoryHandler.GetFiles(directory);

                if(files.Count > 0)
                {
                    List<string> doneList = new List<string>();
                    List<string> failedList = new List<string>();

                    foreach (string file in files)
                    {
                        //Read the content of the file
                        List<string> fileContent = _fileHandler.ReadFile(file);

                        //Read the filename from the selected file path (Select last entry of the string array)
                        string fileName = file.Split('\\')[
                                                file.Split('\\').Length - 1];

                        if (fileContent.Count == 0)
                        {
                            //If somthing went wrong, add the file to the failed list
                            failedList.Add(file);
                        }
                        else
                        {
                            //Edit the selected file content
                            List<string> editedFile = _fileEditor.Edit(fileContent);

                            //Check new file content
                            if (editedFile.Equals(new List<string>()))
                            {
                                //If the content is empty add the file to the failed list
                                failedList.Add(file);
                            }
                            else
                            {
                                //Save the new file in the 'Output' directory
                                bool saveStatus = _fileHandler.SaveFile(fileName, editedFile);

                                if (saveStatus)
                                {
                                    doneList.Add(file);
                                }
                                else
                                {
                                    failedList.Add(file);
                                }
                            }
                        }
                    }

                    _messagePrinter.AllStatus(doneList, failedList);
                }
                else
                {
                    Console.WriteLine("Directory is empty");
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist");
            }
        }
    }
}
