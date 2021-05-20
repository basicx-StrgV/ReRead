using BasicxLogger;
using ReRead.Components;
using ReRead.Components.ConsoleWindow;
using System;
using System.Collections.Generic;

namespace ReRead.Modes
{
    class DirectoryMode
    {
        private Logger logger;
        private FileEditor fileEditor;
        private FileHandler fileHandler;
        private DirectoryHandler directoryHandler;
        private MessagePrinter messagePrinter;

        public DirectoryMode(Logger logger, FileEditor fileEditor, FileHandler fileHandler,
                                DirectoryHandler directoryHandler, string directory)
        {
            this.logger = logger;
            this.fileEditor = fileEditor;
            this.fileHandler = fileHandler;
            this.directoryHandler = directoryHandler;

            messagePrinter = new MessagePrinter(this.logger);

            run(directory);
        }

        private void run(string directory)
        {
            if (directoryHandler.checkDirectory(directory))
            {
                List<string> files = directoryHandler.getFiles(directory);

                if(files.Count > 0)
                {
                    List<string> doneList = new List<string>();
                    List<string> failedList = new List<string>();

                    foreach (string file in files)
                    {
                        //Read the content of the file
                        List<string> fileContent = fileHandler.readFile(file);

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
                            List<string> editedFile = fileEditor.edit(fileContent);

                            //Check new file content
                            if (editedFile.Equals(new List<string>()))
                            {
                                //If the content is empty add the file to the failed list
                                failedList.Add(file);
                            }
                            else
                            {
                                //Save the new file in the 'Output' directory
                                bool saveStatus = fileHandler.saveFile(fileName, editedFile);

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

                    messagePrinter.allStatus(doneList, failedList);
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
