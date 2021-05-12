using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EasyLogger;

namespace ReRead.Components
{
    class FileHandler
    {
        Config config;
        Logger logger;

        public FileHandler(Config config, Logger logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public List<string> getFileList()
        {
            try
            {
                //Returns a list of every file in the 'Input' directory
                return Directory.GetFiles(config.runningDirectory + config.programFolder + config.inputFolder).ToList();
            }
            catch (Exception e)
            {
                logger.write(e.Message);
                List<string> emptyList = new List<string>();
                return emptyList;
            }
        }

        public string readFile(string file)
        {
            try
            {
                //Return the content of the file as a string
                return File.ReadAllText(file);
            }
            catch (Exception e)
            {
                logger.write(e.Message);
                return "";
            }
        }

        public void saveFile(string fileName, List<string> fileContent)
        {
            try
            {
                //Write the new file in the 'Output' directory
                File.WriteAllLines(config.runningDirectory + config.programFolder + config.outputFolder + "ReRead_" + fileName, fileContent);
            }
            catch (Exception e)
            {
                logger.write(e.Message);
            }
        }
    }
}
