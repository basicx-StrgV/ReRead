using System;
using System.IO;
using EasyLogger;

namespace ReRead.Components
{
    class DirectoryHandler
    {
        Config config;
        Logger logger;

        public DirectoryHandler(Config config, Logger logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public void createProgramMainDir()
        {
            try
            {
                //Create the program folder (ReRead): "path_of_the_program/ReRead/"
                Directory.CreateDirectory(config.runningDirectory + config.programFolder);
            }
            catch (Exception e)
            {
                logger.write(e.Message);
            }
        }

        public void createInputDir()
        {
            try 
            {
                //Create the 'Input' folder: "path_of_the_program/ReRead/Input/"
                Directory.CreateDirectory(config.runningDirectory + config.programFolder + config.inputFolder);
            }
            catch(Exception e)
            {
                logger.write(e.Message);
            }
        }

        public void createOutputDir()
        {
            try
            {
                //Create the 'Output' folder: "path_of_the_program/ReRead/Output/"
                Directory.CreateDirectory(config.runningDirectory + config.programFolder + config.outputFolder);
            }
            catch (Exception e)
            {
                logger.write(e.Message);
            }
        }
    }
}
