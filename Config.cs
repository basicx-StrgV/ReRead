using System;
using System.Collections.Generic;
using System.Text;

namespace ReRead
{
    class Config
    {
        public string runningDirectory { get; set; }
        public string inputFolder { get; } = "\\ReRead_Input\\";
        public string outputFolder { get; } = "\\ReRead_Output\\";
    }
}
