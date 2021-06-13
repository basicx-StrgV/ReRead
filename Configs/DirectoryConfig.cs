namespace ReRead.Configs
{
    class DirectoryConfig
    {
        //Path of the program
        private readonly string _workingDirectory;

        //Configur names of different folders
        private readonly string _programFolder = "ReRead"; // "/workingDirectory/ReRead/"
        private readonly string _inputFolder = "Input"; // "/workingDirectory/ReRead/Input/"
        private readonly string _outputFolder = "Output"; // "/workingDirectory/ReRead/Output/"

        //Complet paths for easy access
        public string ProgramFolderPath { get; }
        public string InputFolderPath { get; }
        public string OutputFolderPath { get; }

        public DirectoryConfig(string workingDirectory)
        {
            _workingDirectory = workingDirectory + "\\";

            ProgramFolderPath = _workingDirectory + _programFolder + "\\";
            InputFolderPath = ProgramFolderPath + _inputFolder + "\\";
            OutputFolderPath = ProgramFolderPath + _outputFolder + "\\";
        }
    }
}
