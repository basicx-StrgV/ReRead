namespace ReRead
{
    class Config
    {
        //Path of the program
        public string runningDirectory { get; set; }
        
        //Names of different folders
        public string programFolder { get; } = "\\ReRead\\"; // "/runningDirectory/ReRead/"
        public string inputFolder { get; } = "\\Input\\"; // "/runningDirectory/ReRead/Input/"
        public string outputFolder { get; } = "\\Output\\"; // "/runningDirectory/ReRead/Output/"
    }
}
