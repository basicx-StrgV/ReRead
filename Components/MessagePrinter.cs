using System;
using EasyLogger;

namespace ReRead.Components
{
    class MessagePrinter
    {
        Logger logger;
        
        public MessagePrinter(Logger logger)
        {
            this.logger = logger;
        }

        public void startMessage()
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
                logger.write(e.Message);
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
            catch(Exception e)
            {
                logger.write(e.Message);
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
                logger.write(e.Message);
            }
        }
    }
}
