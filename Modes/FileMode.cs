using BasicxLogger;
using ReRead.Components;
using ReRead.Components.ConsoleWindow;
using System;
using System.Collections.Generic;

namespace ReRead.Modes
{
    class FileMode
    {
        private Logger logger;
        private FileEditor fileEditor;
        private FileHandler fileHandler;
        private MessagePrinter messagePrinter;

        public FileMode(Logger logger, FileEditor fileEditor, FileHandler fileHandler, string file)
        {
            this.logger = logger;
            this.fileEditor = fileEditor;
            this.fileHandler = fileHandler;

            messagePrinter = new MessagePrinter(this.logger);

            run(file);
        }

        private void run(string file)
        {
            if (fileHandler.checkFile(file))
            {
                //Read the content of the selected file
                List<string> fileContent = fileHandler.readFile(file);

                //Read the filename from the selected file path (Select last entry of the string array)
                string fileName = file.Split('\\')[
                                        file.Split('\\').Length - 1];

                if (fileContent.Count == 0)
                {
                    //If somthing went wrong while reading the file or if the file is empty, display an error
                    messagePrinter.fileError();
                }
                else
                {
                    //Edit the selected file content
                    List<string> editedFile = fileEditor.edit(fileContent);

                    //Check new file content
                    if (editedFile.Equals(new List<string>()))
                    {
                        messagePrinter.error();
                    }
                    else
                    {
                        //Save the new file in the 'Output' directory
                        bool saveStatus = fileHandler.saveFile(fileName, editedFile);

                        if (saveStatus)
                        {
                            messagePrinter.done();
                        }
                        else
                        {
                            messagePrinter.saveError();
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
