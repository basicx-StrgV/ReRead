using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReRead
{
    class DirectoryHandler
    {
        Config config;
        public DirectoryHandler(Config config)
        {
            this.config = config;
        }

        public void createInputDir()
        {
            try {
                Directory.CreateDirectory(config.runningDirectory + config.inputFolder);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void createOutputDir()
        {
            try
            {
                Directory.CreateDirectory(config.runningDirectory + config.outputFolder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
