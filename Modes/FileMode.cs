using System;
using System.Collections.Generic;
using BasicxLogger;

namespace ReRead
{
    class FileMode
    {
        private readonly FileLogger _logger;
        private readonly FileEditor _fileEditor;
        private readonly FileHandler _fileHandler;
        private readonly MessagePrinter _messagePrinter;

        public FileMode(FileLogger logger, FileEditor fileEditor, FileHandler fileHandler, string file)
        {
            _logger = logger;
            _fileEditor = fileEditor;
            _fileHandler = fileHandler;

            _messagePrinter = new MessagePrinter(_logger);

            Run(file);
        }

        private void Run(string file)
        {
            if (_fileHandler.CheckFile(file))
            {
                //Read the content of the selected file
                List<string> fileContent = _fileHandler.ReadFile(file);

                //Read the filename from the selected file path (Select last entry of the string array)
                string fileName = file.Split('\\')[
                                        file.Split('\\').Length - 1];

                if (fileContent.Count == 0)
                {
                    //If somthing went wrong while reading the file or if the file is empty, display an error
                    _messagePrinter.FileError();
                }
                else
                {
                    //Edit the selected file content
                    List<string> editedFile = _fileEditor.Edit(fileContent);

                    //Check new file content
                    if (editedFile.Equals(new List<string>()))
                    {
                        _messagePrinter.Error();
                    }
                    else
                    {
                        //Save the new file in the 'Output' directory
                        bool saveStatus = _fileHandler.SaveFile(fileName, editedFile);

                        if (saveStatus)
                        {
                            _messagePrinter.Done();
                        }
                        else
                        {
                            _messagePrinter.SaveError();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
}
