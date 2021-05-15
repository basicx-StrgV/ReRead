using System;
using System.IO;
using ReRead.Configs;
using BasicxLogger;

namespace ReRead.Components
{
    class DirectoryHandler
    {
        private DirectoryConfig dirConfig;
        private Logger logger;

        public DirectoryHandler(DirectoryConfig directoryConfig, Logger logger)
        {
            this.dirConfig = directoryConfig;
            this.logger = logger;
        }

        public void createProgramMainDir()
        {
            try
            {
                //Create the program folder (ReRead): "path_of_the_program/ReRead/"
                Directory.CreateDirectory(dirConfig.programFolderPath);
            }
            catch (Exception e)
            {
                logger.log(e.Message);
            }
        }

        public void createInputDir()
        {
            try 
            {
                //Create the 'Input' folder: "path_of_the_program/ReRead/Input/"
                Directory.CreateDirectory(dirConfig.inputFolderPath);
            }
            catch(Exception e)
            {
                logger.log(e.Message);
            }
        }

        public void createOutputDir()
        {
            try
            {
                //Create the 'Output' folder: "path_of_the_program/ReRead/Output/"
                Directory.CreateDirectory(dirConfig.outputFolderPath);
            }
            catch (Exception e)
            {
                logger.log(e.Message);
            }
        }
    }
}
