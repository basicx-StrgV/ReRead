using System;
using System.Collections.Generic;
using ReRead.Components;
using ReRead.Components.ConsoleWindow;
using BasicxLogger;

namespace ReRead.Modes
{
    class ConsoleMode
    {
        private Logger logger;
        private WindowHandler windowHandler;
        private FileEditor fileEditor;
        private FileHandler fileHandler;
        private InputHandler inputHandler;
        private MessagePrinter messagePrinter;
        private ErrorHandler errorHandler;


        public ConsoleMode(Logger logger, FileEditor fileEditor, FileHandler fileHandler)
        {
            this.logger = logger;
            this.fileEditor = fileEditor;
            this.fileHandler = fileHandler;

            windowHandler = new WindowHandler(this.logger);
            inputHandler = new InputHandler(this.logger);
            messagePrinter = new MessagePrinter(this.logger);
            errorHandler = new ErrorHandler(this.logger, windowHandler, messagePrinter, inputHandler);

            startup();
            run();
        }

        private void startup()
        {
            //First message
            windowHandler.clearWindow();
            messagePrinter.start();
            inputHandler.pressEnterToContinue();
        }

        public  void run()
        {
            while (true)
            {
                windowHandler.clearWindow();

                //Get a list of every file in the 'ReRead_Input' directory
                List<string> files = fileHandler.getFileList();

                //Open thefile select menu and save the selection to the input string
                string input = inputHandler.fileSelectMenu(files);

                //Process input
                if (input.Equals("EXIT"))
                {
                    //Open the exit menu if the user selected 'EXIT'
                    exit();
                }
                else if (input.Equals("RELOAD"))
                {
                    //Placeholder
                }
                else if (input.Equals("ALL"))
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

                    windowHandler.clearWindow();
                    messagePrinter.allStatus(doneList, failedList);
                    inputHandler.pressEnterToContinue();
                }
                else if (input.Equals(""))
                {
                    //Display an error if somthing went wrong
                    errorHandler.error(ErrorType.normal);
                }
                else if (!input.Equals("EXIT") && !input.Equals("RELOAD") && !input.Equals("ALL") && !input.Equals(""))
                {
                    //If a file is selected
                    //Read the content of the selected file
                    List<string> fileContent = fileHandler.readFile(input);

                    //Read the filename from the selected file path (Select last entry of the string array)
                    string fileName = input.Split('\\')[
                                            input.Split('\\').Length - 1];

                    if (fileContent.Count == 0)
                    {
                        //If somthing went wrong while reading the file or if the file is empty, display an error
                        errorHandler.error(ErrorType.file);
                    }
                    else
                    {
                        //Edit the selected file content
                        List<string> editedFile = fileEditor.edit(fileContent);

                        //Check new file content
                        if (editedFile.Equals(new List<string>()))
                        {
                            errorHandler.error(ErrorType.normal);
                        }
                        else
                        {
                            //Save the new file in the 'Output' directory
                            bool saveStatus = fileHandler.saveFile(fileName, editedFile);

                            if (saveStatus)
                            {
                                windowHandler.clearWindow();
                                messagePrinter.done();
                                inputHandler.pressEnterToContinue();
                            }
                            else
                            {
                                errorHandler.error(ErrorType.save);
                            }
                        }
                    }
                }
            }
        }

        private void exit()
        {
            windowHandler.clearWindow();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nAre you sure thet you want to exit?");
            Console.ResetColor();
            Console.WriteLine("Press ENTER to continue or ESC to cancel");

            //Return to the main process if 'ESC' gets pressed and close the program if 'ENTER' gets pressed
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Environment.Exit(0);
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }
    }
}
