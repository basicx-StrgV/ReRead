namespace ReRead.Configs
{
    class DirectoryConfig
    {
        //Path of the program
        private string workingDirectory;

        //Configur names of different folders
        private string programFolder = "ReRead"; // "/workingDirectory/ReRead/"
        private string inputFolder = "Input"; // "/workingDirectory/ReRead/Input/"
        private string outputFolder = "Output"; // "/workingDirectory/ReRead/Output/"

        //Complet paths for easy access
        public string programFolderPath { get; }
        public string inputFolderPath { get; }
        public string outputFolderPath { get; }

        public DirectoryConfig(string workingDirectory)
        {
            this.workingDirectory = workingDirectory + "\\";

            this.programFolderPath = this.workingDirectory + this.programFolder + "\\";
            this.inputFolderPath = this.programFolderPath + this.inputFolder + "\\";
            this.outputFolderPath = this.programFolderPath + this.outputFolder + "\\";
        }
    }
}
