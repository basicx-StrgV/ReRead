using System;
using System.Collections.Generic;
using EasyLogger;

namespace ReRead
{
    class Program
    {
        private Config config = new Config();
        private Logger logger = new Logger();

        private FileEditor fileEditor;
        private DirectoryHandler directoryHandler;
        private FileHandler fileHandler;
        private InputHandler inputHandler;
        private MessagePrinter messagePrinter;

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
            //Set the directory in witch the program runs
            config.runningDirectory = Environment.CurrentDirectory;

            //Configure the logger
            configureLogger();

            //Create objects of every program component
            fileEditor = new FileEditor(logger);
            inputHandler = new InputHandler(logger);
            messagePrinter = new MessagePrinter(logger);
            directoryHandler = new DirectoryHandler(config, logger);
            fileHandler = new FileHandler(config, logger);

            //Configure the console window
            Console.Title = "ReRead";
            Console.CursorVisible = false;
        }

        private void startup()
        {
            //Create every directory needed by the program
            directoryHandler.createProgramMainDir();
            directoryHandler.createInputDir();
            directoryHandler.createOutputDir();

            //First message
            clearWindow();
            messagePrinter.startMessage();
            inputHandler.pressEnterToContinue();

            //Start main process
            run();
        }

        private void run()
        {
            while (true)
            {
                clearWindow();

                //Get a list of every file in the 'ReRead_Input' directory
                List<string> files = fileHandler.getFileList();
                if(files.Count == 0)
                {
                    files.Add("");
                }

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
                else if (input.Equals(""))
                {
                    //Display an error if somthing went wrong
                    clearWindow();
                    messagePrinter.error();
                    inputHandler.pressEnterToContinue();
                }
                else if (!input.Equals("EXIT") && !input.Equals("RELOAD") && !input.Equals(""))
                {
                    //If a file is selected
                    //Read the content of the selected file
                    string fileContent = fileHandler.readFile(input);

                    //Read the filename from the selected file path
                    string fileName = input.Split('\\')[input.Split('\\').Length - 1];

                    if (fileContent == "")
                    {
                        //If somthing went wrong while reading the file or if the file is empty, display an error
                        clearWindow();
                        messagePrinter.fileError();
                        inputHandler.pressEnterToContinue();
                    }
                    else {
                        //Edit the selected file content
                        List<string>editedFile = fileEditor.edit(fileContent);

                        //Save the new file in the 'Output' directory
                        fileHandler.saveFile(fileName, editedFile);
                    }
                }
            }
        }

        private void clearWindow()
        {
            Console.Clear();
            Console.WriteLine("______    ______               _\n" +
                                "| ___ \\   | ___ \\             | |\n" +
                                "| |_/ /___| |_/ /___  __ _  __| |\n" +
                                "|    // _ \\    // _ \\/ _` |/ _` |\n" +
                                "| |\\ \\  __/ |\\ \\  __/ (_| | (_| |\n" +
                                "\\_| \\_\\___\\_| \\_\\___|\\__,_|\\__,_|");
        }

        private void exit()
        {
            clearWindow();
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

        private void configureLogger()
        {
            logger.setCustomLogDirectoryPath(config.runningDirectory + config.programFolder);
            logger.setCustomLogDirectoryName("Logs");
        }
    }
}
