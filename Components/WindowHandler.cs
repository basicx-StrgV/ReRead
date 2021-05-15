using System;
using BasicxLogger;

namespace ReRead.Components
{
    class WindowHandler
    {
        Logger logger;

        public WindowHandler(Logger logger)
        {
            this.logger = logger;
            configureWindow();
        }

        public void clearWindow()
        {
            try
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine("______    ______               _\n" +
                                    "| ___ \\   | ___ \\             | |\n" +
                                    "| |_/ /___| |_/ /___  __ _  __| |\n" +
                                    "|    // _ \\    // _ \\/ _` |/ _` |\n" +
                                    "| |\\ \\  __/ |\\ \\  __/ (_| | (_| |\n" +
                                    "\\_| \\_\\___\\_| \\_\\___|\\__,_|\\__,_|\n" +
                                    "by basicx-StrgV");
            }
            catch (Exception e)
            {
                logger.log(e.Message);
            }
        }

        public void configureWindow()
        {
            try
            {
                //Configure the console window
                Console.Title = "ReRead";
                Console.CursorVisible = false;
            }
            catch (Exception e)
            {
                logger.log(e.Message);
            }
        }
    }
}
