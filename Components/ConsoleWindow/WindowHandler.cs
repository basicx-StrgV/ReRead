using System;
using BasicxLogger;

namespace ReRead
{
    class WindowHandler
    {
        private readonly FileLogger _logger;

        public WindowHandler(FileLogger logger)
        {
            _logger = logger;
            ConfigureWindow();
        }

        public void ClearWindow()
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
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }

        public void ConfigureWindow()
        {
            try
            {
                //Configure the console window
                Console.Title = "ReRead";
                Console.CursorVisible = false;
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }
    }
}
