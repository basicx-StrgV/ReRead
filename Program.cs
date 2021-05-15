using System;
using System.Collections.Generic;
using ReRead.Components;
using ReRead.Configs;
using BasicxLogger;
using BasicxLogger.LoggerFile;
using BasicxLogger.LoggerDirectory;

namespace ReRead
{
    class Program
    {
        static private DirectoryConfig dirConfig = new DirectoryConfig(Environment.CurrentDirectory);

        private Logger logger = new Logger(
            new LogFile("ReRead", LogFileType.log), 
            new LogDirectory(dirConfig.programFolderPath, "Logs"));

        private WindowHandler windowHandler;
        private FileEditor fileEditor;
        private DirectoryHandler directoryHandler;
        private FileHandler fileHandler;
        private InputHandler inputHandler;
        private MessagePrinter messagePrinter;
        private ErrorHandler errorHandler;

        static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            initialize();
            startup();
        }

        private void initialize()
        {
            //Create objects of every program component
            windowHandler = new WindowHandler(logger);
            fileEditor = new FileEditor(logger);
            inputHandler = new InputHandler(logger);
            messagePrinter = new MessagePrinter(logger);
            directoryHandler = new DirectoryHandler(dirConfig, logger);
            fileHandler = new FileHandler(dirConfig, logger);
            errorHandler = new ErrorHandler(logger, windowHandler, messagePrinter, inputHandler);
        }

        private void startup()
        {
            //Create every directory needed by the program
            directoryHandler.createProgramMainDir();
            directoryHandler.createInputDir();
            directoryHandler.createOutputDir();

            //First message
            windowHandler.clearWindow();
            messagePrinter.start();
            inputHandler.pressEnterToContinue();

            //Start main process
            run();
        }

        private void run()
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
                        string fileContent = fileHandler.readFile(file);

                        //Read the filename from the selected file path (Select last entry of the string array)
                        string fileName = file.Split('\\')[
                                                file.Split('\\').Length - 1];

                        if (fileContent == "")
                        {
                            //If somthing went wrong, add the file to the failed list
                            failedList.Add(file);
                        }
                        else
                        {
                            //Edit the selected file content
                            string editedFile = fileEditor.edit(fileContent);

                            //Check new file content
                            if (editedFile.Equals(""))
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
                    string fileContent = fileHandler.readFile(input);

                    //Read the filename from the selected file path (Select last entry of the string array)
                    string fileName = input.Split('\\')[
                                            input.Split('\\').Length - 1];

                    if (fileContent == "")
                    {
                        //If somthing went wrong while reading the file or if the file is empty, display an error
                        errorHandler.error(ErrorType.file);
                    }
                    else 
                    {
                        //Edit the selected file content
                        string editedFile = fileEditor.edit(fileContent);

                        //Check new file content
                        if(editedFile.Equals(""))
                        {
                            errorHandler.error(ErrorType.normal);
                        }
                        else
                        {
                            //Save the new file in the 'Output' directory
                            bool saveStatus = fileHandler.saveFile(fileName, editedFile);
                            
                            if(saveStatus)
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
            while(true)
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
