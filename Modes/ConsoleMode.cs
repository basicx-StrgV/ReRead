using System;
using System.Collections.Generic;
using BasicxLogger;

namespace ReRead
{
    class ConsoleMode
    {
        private readonly FileLogger _logger;
        private readonly WindowHandler _windowHandler;
        private readonly FileEditor _fileEditor;
        private readonly FileHandler _fileHandler;
        private readonly InputHandler _inputHandler;
        private readonly MessagePrinter _messagePrinter;
        private readonly ErrorHandler _errorHandler;


        public ConsoleMode(FileLogger logger, FileEditor fileEditor, FileHandler fileHandler)
        {
            _logger = logger;
            _fileEditor = fileEditor;
            _fileHandler = fileHandler;

            _windowHandler = new WindowHandler(_logger);
            _inputHandler = new InputHandler(_logger);
            _messagePrinter = new MessagePrinter(_logger);
            _errorHandler = new ErrorHandler(_logger, _windowHandler, _messagePrinter, _inputHandler);

            Startup();
            Run();
        }

        private void Startup()
        {
            //First message
            _windowHandler.ClearWindow();
            _messagePrinter.Start();
            _inputHandler.PressEnterToContinue();
        }

        public  void Run()
        {
            while (true)
            {
                _windowHandler.ClearWindow();

                //Get a list of every file in the 'ReRead_Input' directory
                List<string> files = _fileHandler.GetFileList();

                //Open thefile select menu and save the selection to the input string
                string input = _inputHandler.FileSelectMenu(files);

                //Process input
                if (input.Equals("EXIT"))
                {
                    //Open the exit menu if the user selected 'EXIT'
                    Exit();
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

                    _windowHandler.ClearWindow();
                    _messagePrinter.AllStatus(doneList, failedList);
                    _inputHandler.PressEnterToContinue();
                }
                else if (input.Equals(""))
                {
                    //Display an error if somthing went wrong
                    _errorHandler.Error(ErrorType.normal);
                }
                else if (!input.Equals("EXIT") && !input.Equals("RELOAD") && !input.Equals("ALL") && !input.Equals(""))
                {
                    //If a file is selected
                    //Read the content of the selected file
                    List<string> fileContent = _fileHandler.ReadFile(input);

                    //Read the filename from the selected file path (Select last entry of the string array)
                    string fileName = input.Split('\\')[
                                            input.Split('\\').Length - 1];

                    if (fileContent.Count == 0)
                    {
                        //If somthing went wrong while reading the file or if the file is empty, display an error
                        _errorHandler.Error(ErrorType.file);
                    }
                    else
                    {
                        //Edit the selected file content
                        List<string> editedFile = _fileEditor.Edit(fileContent);

                        //Check new file content
                        if (editedFile.Equals(new List<string>()))
                        {
                            _errorHandler.Error(ErrorType.normal);
                        }
                        else
                        {
                            //Save the new file in the 'Output' directory
                            bool saveStatus = _fileHandler.SaveFile(fileName, editedFile);

                            if (saveStatus)
                            {
                                _windowHandler.ClearWindow();
                                _messagePrinter.Done();
                                _inputHandler.PressEnterToContinue();
                            }
                            else
                            {
                                _errorHandler.Error(ErrorType.save);
                            }
                        }
                    }
                }
            }
        }

        private void Exit()
        {
            _windowHandler.ClearWindow();
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
