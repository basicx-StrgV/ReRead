using System;
using ReRead.Components;
using ReRead.Configs;
using ReRead.Modes;
using BasicxLogger;
using BasicxLogger.Message;
using BasicxLogger.LoggerFile;
using BasicxLogger.LoggerDirectory;

namespace ReRead
{
    class Program
    {
        private static DirectoryConfig dirConfig = new DirectoryConfig(Environment.CurrentDirectory);

        private static Logger logger = new Logger(
            new LogFile("ReRead", LogFileType.log), 
            new LogDirectory(dirConfig.programFolderPath, "Logs"),
            new MessageFormat(DateFormat.year_month_day, '/'));

        private static FileEditor fileEditor;
        private static DirectoryHandler directoryHandler;
        private static FileHandler fileHandler;

        static void Main(string[] args)
        {
            initialize();
            
            if(args.Length > 0 && args.Length < 3)
            {
                switch (args[0])
                {
                    case "-file":
                        new FileMode(logger, fileEditor, fileHandler, args[1]);
                        break;
                    case "-f":
                        new FileMode(logger, fileEditor, fileHandler, args[1]);
                        break;
                    case "-directory":
                        new DirectoryMode(logger, fileEditor, fileHandler, directoryHandler, args[1]);
                        break;
                    case "-d":
                        new DirectoryMode(logger, fileEditor, fileHandler, directoryHandler, args[1]);
                        break;
                    default:
                        Console.WriteLine("unknown argument: " + args[0]);
                        break;
                }
            }
            else if (args.Length >= 3)
            {
                Console.WriteLine("to much arguments");
            }
            else
            {
                new ConsoleMode(logger, fileEditor, fileHandler);
            }
        }

        private static void initialize()
        {
            //Create objects of default program components
            fileEditor = new FileEditor(logger);
            directoryHandler = new DirectoryHandler(dirConfig, logger);
            fileHandler = new FileHandler(dirConfig, logger);

            //Create every directory needed by the program
            directoryHandler.createProgramMainDir();
            directoryHandler.createInputDir();
            directoryHandler.createOutputDir();
        }
    }
}
