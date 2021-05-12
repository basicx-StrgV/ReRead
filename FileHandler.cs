using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReRead
{
    class FileHandler
    {
        Config config;
        public FileHandler(Config config)
        {
            this.config = config;
        }

        public List<string> getFileList()
        {
            try
            {
                List<string> files = Directory.GetFiles(config.runningDirectory + config.inputFolder).ToList();
                return files;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                List<string> emptyList = new List<string>();
                return emptyList;
            }
        }

        public string readFile(string input)
        {
            try
            {
                return File.ReadAllText(config.runningDirectory + config.inputFolder + input);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        public void saveFile(List<string> file, string fileName)
        {
            try
            {
                File.WriteAllLines(config.runningDirectory + config.outputFolder + fileName, file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
