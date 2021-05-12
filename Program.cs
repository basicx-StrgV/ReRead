using System;
using System.Collections.Generic;
using System.IO;

namespace ReRead
{
    class Program
    {
        Config config = new Config();

        FileEditor fileEditor;
        DirectoryHandler directoryHandler;
        FileHandler fileHandler;
        InputHandler inputHandler;

        static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            initialize();
            startup();
            run();
        }

        private void initialize()
        {
            config.runningDirectory = Environment.CurrentDirectory;
            
            fileEditor = new FileEditor();
            inputHandler = new InputHandler();
            directoryHandler = new DirectoryHandler(config);
            fileHandler = new FileHandler(config);
        }

        private void startup()
        {
            directoryHandler.createInputDir();
            directoryHandler.createOutputDir();

            Console.Title = "ReRead";
            Console.CursorVisible = false;
        }

        private void run()
        {
            renderWindow();

            //First massage
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nPut your file/s in the 'ReRead_Input' folder an press ENTER to continue\n");
            Console.ResetColor();

            Console.ReadKey(true);

            //Main process
            while (true)
            {
                renderWindow();

                //Get a list of every file in the 'ReRead_Input' directory
                List<string> files = fileHandler.getFileList();
                if(files.Count == 0)
                {
                    files.Add("");
                }

                //Open thefile select menu and save the selection to the input string
                string input = inputHandler.fileSelectMenu(files);

                if (input == "EXIT")
                {
                    renderWindow();
                    exit();
                }
                if (input == "RELOAD")
                {
                    //Placeholder
                }
                else if(input == "")
                {
                    renderWindow();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nERROR\n");
                    Console.ResetColor();
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadKey(true);
                }
                else
                {
                    string file = "";
                    file = fileHandler.readFile(input);
                    
                    if(file == "")
                    {
                        Console.WriteLine("\n The file is empty or could not be found!\n");
                    }
                    else {
                        string currentFileName = input;

                        List<string>editedFile = fileEditor.splitToLines(file);

                        fileHandler.saveFile(editedFile, "ReRead_" + currentFileName);
                    }
                }
            }
        }

        private void renderWindow()
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
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nAre you sure thet you want to exit?");
            Console.ResetColor();
            Console.WriteLine("Press ENTER to continue or ESC to cancel");
            
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
