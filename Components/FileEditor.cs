using System;
using System.IO;
using System.Text;
using EasyLogger;

namespace ReRead.Components
{
    class FileEditor
    {
        private Logger logger;

        public FileEditor(Logger logger)
        {
            this.logger = logger;
        }

        public string edit(string fileContent)
        {
            string preEditedFileContent = preEdit(fileContent);
            string newString = defaultEdit(preEditedFileContent);
            return newString;
        }

        private string defaultEdit(string fileContent)
        {
            try
            {
                //Create a string builder
                StringBuilder stringBuilder = new StringBuilder();

                //Create a string reader for the given file content
                StringReader fileContentReader = new StringReader(fileContent);

                //Tab counter to formate the file with the right amount of tabs
                int tabCounter = 0;

                //bool will bes set to true if the end of the string is reached
                bool stringEnd = false;

                //Check every char in the file content string
                while (!stringEnd)
                {
                    //Get the dezimal value of the next char in the string
                    int dezChar = fileContentReader.Read();

                    if (dezChar == -1)
                    {
                        //End of string is reached, set the bool to true
                        stringEnd = true;
                    }
                    else
                    {
                        if (dezChar.Equals('{'))
                        {
                            //Go down a line
                            stringBuilder.Append((char)10);

                            //Add all tabs to the line
                            for (int tab = 0; tab < tabCounter; tab++)
                            {
                                stringBuilder.Append((char)9);
                            }

                            //Add the current char to the string
                            stringBuilder.Append((char)dezChar);

                            //Go down a line
                            stringBuilder.Append((char)10);

                            //Tab counter + 1
                            tabCounter++;

                            //Add all tabs to the line
                            for (int tab = 0; tab < tabCounter; tab++)
                            {
                                stringBuilder.Append((char)9);
                            }
                        }
                        else if (dezChar.Equals('}'))
                        {
                            //Go down a line
                            stringBuilder.Append((char)10);

                            //Tab counter - 1
                            tabCounter--;

                            //Add all tabs to the line
                            for (int tab = 0; tab < tabCounter; tab++)
                            {
                                stringBuilder.Append((char)9);
                            }

                            //Add the current char to the string
                            stringBuilder.Append((char)dezChar);

                            //Go down a line
                            stringBuilder.Append((char)10);

                            //Add all tabs to the line
                            for (int tab = 0; tab < tabCounter; tab++)
                            {
                                stringBuilder.Append((char)9);
                            }
                        }
                        else if (dezChar.Equals(';'))
                        {
                            //Add the current char to the string
                            stringBuilder.Append((char)dezChar);

                            //Go down a line
                            stringBuilder.Append((char)10);

                            //Add all tabs to the line
                            for (int tab = 0; tab < tabCounter; tab++)
                            {
                                stringBuilder.Append((char)9);
                            }
                        }
                        else
                        {
                            //Add the current char to the string
                            stringBuilder.Append((char)dezChar);
                        }
                    }
                }

                //Close and dispose of the string reader
                fileContentReader.Close();
                fileContentReader.Dispose();

                //Return the new file content string
                return stringBuilder.ToString();
            }
            catch(Exception e)
            {
                logger.log(e.Message);
                return "";
            }
        }
    
        private string preEdit(string fileContent)
        {
            //Remove white-spaces at the start and end of the string
            string trimedFileContent = fileContent.Trim();

            return trimedFileContent;
        }
    }
}
