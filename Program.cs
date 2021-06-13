using System;
using ReRead.Configs;
using BasicxLogger;
using BasicxLogger.Files;


namespace ReRead
{
    class Program
    {
        private static DirectoryConfig s_dirConfig = new DirectoryConfig(Environment.CurrentDirectory);

        private static FileLogger s_logger = new FileLogger(
            new TxtLogFile(s_dirConfig.ProgramFolderPath + "Logs", "Log"));
        

        private static FileEditor s_fileEditor;
        private static DirectoryHandler s_directoryHandler;
        private static FileHandler s_fileHandler;

        static void Main(string[] args)
        {
            Initialize();
            
            if(args.Length > 0 && args.Length < 3)
            {
                switch (args[0])
                {
                    case "-file":
                        new FileMode(s_logger, s_fileEditor, s_fileHandler, args[1]);
                        break;
                    case "-f":
                        new FileMode(s_logger, s_fileEditor, s_fileHandler, args[1]);
                        break;
                    case "-directory":
                        new DirectoryMode(s_logger, s_fileEditor, s_fileHandler, s_directoryHandler, args[1]);
                        break;
                    case "-d":
                        new DirectoryMode(s_logger, s_fileEditor, s_fileHandler, s_directoryHandler, args[1]);
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
                new ConsoleMode(s_logger, s_fileEditor, s_fileHandler);
            }
        }

        private static void Initialize()
        {
            s_logger.UseId = false;

            //Create objects of default program components
            s_fileEditor = new FileEditor(s_logger);
            s_directoryHandler = new DirectoryHandler(s_dirConfig, s_logger);
            s_fileHandler = new FileHandler(s_dirConfig, s_logger);

            //Create every directory needed by the program
            s_directoryHandler.CreateProgramMainDir();
            s_directoryHandler.CreateInputDir();
            s_directoryHandler.CreateOutputDir();
        }
    }
}
