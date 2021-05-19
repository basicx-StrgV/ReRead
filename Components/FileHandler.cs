using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReRead.Configs;
using BasicxLogger;
using BasicxLogger.Message;

namespace ReRead.Components
{
    class FileHandler
    {
        private DirectoryConfig dirConfig;
        private Logger logger;

        public FileHandler(DirectoryConfig directoryConfig, Logger logger)
        {
            this.dirConfig = directoryConfig;
            this.logger = logger;
        }

        public List<string> getFileList()
        {
            try
            {
                //Returns a list of every file in the 'Input' directory
                return Directory.GetFiles(dirConfig.inputFolderPath).ToList();
            }
            catch (Exception e)
            {
                logger.log(Tag.EXCEPTION, e.Message);
                List<string> emptyList = new List<string>();
                return emptyList;
            }
        }

        public List<string> readFile(string file)
        {
            try
            {
                //Return the content of the file as a string
                return File.ReadAllLines(file).ToList();
            }
            catch (Exception e)
            {
                logger.log(Tag.EXCEPTION, e.Message);
                return new List<string>();
            }
        }

        public bool saveFile(string fileName, string fileContent)
        {
            try
            {
                //Write the new file in the 'Output' directory
                File.WriteAllText(dirConfig.outputFolderPath + "ReRead_" + fileName, fileContent);

                //Return true to signal that the file got saved
                return true;
            }
            catch (Exception e)
            {
                logger.log(Tag.EXCEPTION, e.Message);
                return false;
            }
        }

        public bool saveFile(string fileName, List<string> fileContent)
        {
            try
            {
                //Write the new file in the 'Output' directory
                File.WriteAllLines(dirConfig.outputFolderPath + "ReRead_" + fileName, fileContent);

                //Return true to signal that the file got saved
                return true;
            }
            catch (Exception e)
            {
                logger.log(Tag.EXCEPTION, e.Message);
                return false;
            }
        }
    }
}
