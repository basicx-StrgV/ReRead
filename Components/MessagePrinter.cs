using System;
using System.Collections.Generic;
using BasicxLogger;
using BasicxLogger.Message;

namespace ReRead.Components
{
    class MessagePrinter
    {
        private Logger logger;

        public MessagePrinter(Logger logger)
        {
            this.logger = logger;
        }

        public void start()
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
                logger.log(Tag.EXCEPTION, e.Message);
            }
        }

        public void done()
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
                logger.log(Tag.EXCEPTION, e.Message);
            }
        }

        public void error()
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
                logger.log(Tag.EXCEPTION, e.Message);
            }
        }

        public void fileError()
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
                logger.log(Tag.EXCEPTION, e.Message);
            }
        }

        public void saveError()
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
                logger.log(Tag.EXCEPTION, e.Message);
            }
        }

        public void allStatus(List<string> doneList, List<string> failedList)
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
                logger.log(Tag.EXCEPTION, e.Message);
            }
        }
    }
}


