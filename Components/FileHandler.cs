using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ReRead.Configs;
using BasicxLogger;

namespace ReRead
{
    class FileHandler
    {
        private readonly DirectoryConfig _dirConfig;
        private readonly FileLogger _logger;

        public FileHandler(DirectoryConfig directoryConfig, FileLogger logger)
        {
            _dirConfig = directoryConfig;
            _logger = logger;
        }

        public List<string> GetFileList()
        {
            try
            {
                //Returns a list of every file in the 'Input' directory
                return Directory.GetFiles(_dirConfig.InputFolderPath).ToList();
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
                List<string> emptyList = new List<string>();
                return emptyList;
            }
        }

        public List<string> ReadFile(string file)
        {
            try
            {
                //Return the content of the file as a string
                return File.ReadAllLines(file).ToList();
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
                return new List<string>();
            }
        }

        public bool SaveFile(string fileName, string fileContent)
        {
            try
            {
                //Write the new file in the 'Output' directory
                File.WriteAllText(_dirConfig.OutputFolderPath + "ReRead_" + fileName, fileContent);

                //Return true to signal that the file got saved
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
                return false;
            }
        }

        public bool SaveFile(string fileName, List<string> fileContent)
        {
            try
            {
                //Write the new file in the 'Output' directory
                File.WriteAllLines(_dirConfig.OutputFolderPath + "ReRead_" + fileName, fileContent);

                //Return true to signal that the file got saved
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
                return false;
            }
        }
    
        public bool CheckFile(string file)
        {
            return File.Exists(file);
        }
    }
}
