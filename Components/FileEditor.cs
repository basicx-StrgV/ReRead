using System.Collections.Generic;
using System.Linq;
using EasyLogger;

namespace ReRead.Components
{
    class FileEditor
    {
        Logger logger;

        public FileEditor(Logger logger)
        {
            this.logger = logger;
        }

        public List<string> edit(string file)
        {
            List<string> splitToLinesFile = splitToLines(file);
            return splitToLinesFile;
        }

        private List<string> splitToLines(string file)
        {
            List<string> splitFile = file.Split(';').ToList();
            for(int i = 0; i < splitFile.Count; i++)
            {
                splitFile[i] = splitFile[i] + ";";
            }
            return splitFile;
        }

    }
}
