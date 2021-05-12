using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReRead
{
    class FileEditor
    {
        public List<string> splitToLines(string file)
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
