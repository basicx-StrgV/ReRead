using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ReRead.Configs;
using BasicxLogger;

namespace ReRead
{
    class DirectoryHandler
    {
        private readonly DirectoryConfig _dirConfig;
        private readonly FileLogger _logger;

        public DirectoryHandler(DirectoryConfig directoryConfig, FileLogger logger)
        {
            _dirConfig = directoryConfig;
            _logger = logger;
        }

        public void CreateProgramMainDir()
        {
            try
            {
                //Create the program folder (ReRead): "path_of_the_program/ReRead/"
                Directory.CreateDirectory(_dirConfig.ProgramFolderPath);
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }

        public void CreateInputDir()
        {
            try 
            {
                //Create the 'Input' folder: "path_of_the_program/ReRead/Input/"
                Directory.CreateDirectory(_dirConfig.InputFolderPath);
            }
            catch(Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }

        public void CreateOutputDir()
        {
            try
            {
                //Create the 'Output' folder: "path_of_the_program/ReRead/Output/"
                Directory.CreateDirectory(_dirConfig.OutputFolderPath);
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
            }
        }
    
        public bool CheckDirectory(string directory)
        {
            return Directory.Exists(directory);
        }
    
        public List<string> GetFiles(string directory)
        {
            return Directory.GetFiles(directory).ToList();
        }
    }
}
