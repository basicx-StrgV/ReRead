using System;
using System.Collections.Generic;
using BasicxLogger;

namespace ReRead
{
    class MessagePrinter
    {
        private readonly FileLogger _logger;

        public MessagePrinter(FileLogger logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nPlease put your file/s in the 'Input' folder.\n");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }

        public void Done()
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nDONE\n");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }

        public void Error()
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nERROR\n");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }

        public void FileError()
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nThe file is empty or could not be found!\n");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }

        public void SaveError()
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nThe file can not be saved!\n");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }

        public void AllStatus(List<string> doneList, List<string> failedList)
        {
            try
            {
                //DONE
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n" + doneList.Count + " : DONE");
                Console.ResetColor();

                foreach(string file in doneList)
                {
                    Console.WriteLine(file.Split('\\')[
                                        file.Split('\\').Length - 1]);
                }

                //FAILED
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n" + failedList.Count + " : FAILED");
                Console.ResetColor();

                foreach (string file in failedList)
                {
                    Console.WriteLine(file.Split('\\')[
                                        file.Split('\\').Length - 1]);
                }

                //Empty line
                Console.WriteLine("\n");

            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }
    }
}


