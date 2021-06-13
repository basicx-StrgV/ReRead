using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using BasicxLogger;

namespace ReRead
{
    class FileEditor
    {
        private readonly FileLogger _logger;

        public FileEditor(FileLogger logger)
        {
            _logger = logger;
        }

        public List<string> Edit(List<string> fileContent)
        {
            string preEditedFileContent = PreEdit(fileContent);
            string newString = DefaultEdit(preEditedFileContent);
            List<string> newFileContent = PostEdit(newString);
            return newFileContent;
        }

        private string DefaultEdit(string fileContent)
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
                _logger.Log(LogTag.EXCEPTION, e.Message);
                return "";
            }
        }
    
        private string PreEdit(List<string> fileContent)
        {
            try
            {
                //List that will contain every line in the file but with white-spaces, \n usw removed
                List<string> cleanList = new List<string>();

                //Edits every line and adds it to the new list
                foreach (string line in fileContent)
                {
                    //Remove white-spaces at the start and end of the string
                    string trimedFileContent = line.Trim();

                    //Create a string builder
                    StringBuilder lineBuilder = new StringBuilder();

                    //Create a string reader for the trimed file content
                    StringReader fileContentReader = new StringReader(trimedFileContent);

                    //bool will bes set to true if the end of the string is reached
                    bool stringEnd = false;

                    while (!stringEnd)
                    {
                        //Get the dezimal value of the next char in the string
                        int dezChar = fileContentReader.Read();

                        char test = (char)dezChar;

                        if (dezChar == -1)
                        {
                            //End of string is reached, set the bool to true
                            stringEnd = true;
                        }
                        else if (dezChar != 9 && dezChar != 10 && dezChar != 13)
                        {
                            lineBuilder.Append((char)dezChar);
                        }
                    }

                    //Add new line to the list
                    cleanList.Add(lineBuilder.ToString());

                    fileContentReader.Close();
                    fileContentReader.Dispose();
                    lineBuilder.Clear();
                }

                //Create a string builder
                StringBuilder stringBuilder = new StringBuilder();

                //Adds every line into one string
                foreach(string line in cleanList)
                {
                    if (!line.StartsWith("//") && !line.EndsWith("*/"))
                    {
                        stringBuilder.Append(line);
                    }
                    else
                    {
                        stringBuilder.Append(line + ";");
                    }
                }

                //Return the new string
                return stringBuilder.ToString();
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
                return "";
            }
        }
    
        private List<string> PostEdit(string fileContentString)
        {
            try
            {
                //Create a array from the string, where every entry is a line in the new file
                string[] fileContentArray = fileContentString.Split((char)10);

                //Create a list to save the new file content
                List<string> fileContentList = new List<string>();

                for (int i = 0; i < fileContentArray.Length; i++)
                {
                    if (fileContentArray[i].Contains("for(") || fileContentArray[i].Contains("for ("))
                    {
                        //Save the current an next two lines as one, so the for loop is not split
                        fileContentList.Add(fileContentArray[i].TrimEnd() + 
                                                fileContentArray[i + 1].Trim() + 
                                                fileContentArray[i + 2].Trim());

                        //Skip the next two lines
                        i += 2;
                    }
                    else if (fileContentArray[i].Trim().Equals(""))
                    {
                        //Only add empty line if the line before contains '{'
                        if(fileContentArray[i - 1].Contains("{"))
                        {
                            fileContentList.Add(fileContentArray[i]);
                        }
                    }
                    else
                    {
                        //Only add line to list
                        fileContentList.Add(fileContentArray[i]);
                    }
                }

                //Return list of new file
                return fileContentList;
            }
            catch (Exception e)
            {
                _logger.Log(LogTag.EXCEPTION, e.Message);
                return new List<string>();
            }
        }
    }
}
