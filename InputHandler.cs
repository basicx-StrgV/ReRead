using System;
using System.Collections.Generic;
using System.Text;

namespace ReRead
{
    class InputHandler
    {
        public string fileSelectMenu(List<string> fileList)
        {
            try
            {
                //Add EXIT and RELOAD to the List
                fileList.Add("RELOAD");
                fileList.Add("EXIT");

                //Create banner for list with info text
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nSelect your file: ");
                Console.ResetColor();

                //Save the index of the first list entry
                int menuTopLine = Console.CursorTop;

                //Lists every list entry but removes the file path
                foreach (string entry in fileList)
                {
                    Console.WriteLine(entry.Split('\\')[
                                        entry.Split('\\').Length - 1]);
                }

                //Save the index of the last console line
                int origanalLine = Console.CursorTop;

                //Set the cursor position to the position of the first list entry
                Console.SetCursorPosition(0, menuTopLine);

                //This int is used to save the index of the current cursor position in the loop 
                int currentLine = Console.CursorTop;

                while (true)
                {
                    //Position of cursor in the list
                    int listIndex = currentLine - menuTopLine;

                    //Colors for selected entry
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;

                    //Write the line to highlight it with the selected colors
                    Console.Write(fileList[listIndex].Split('\\')[fileList[listIndex].Split('\\').Length - 1]);

                    //Set the cursor beck to the start of the selected line
                    Console.SetCursorPosition(0, currentLine);

                    //Reset the colors
                    Console.ResetColor();

                    //Wait for keyboard input
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.UpArrow && currentLine > menuTopLine)
                    {
                        //Write the line to remove the custom color
                        Console.Write(fileList[listIndex].Split('\\')[
                                        fileList[listIndex].Split('\\').Length - 1]);

                        //Go one line up
                        Console.SetCursorPosition(0, currentLine - 1);
                    }
                    else if (key.Key == ConsoleKey.DownArrow && currentLine + 1 < origanalLine)
                    {
                        //Write the line to remove the custom color
                        Console.Write(fileList[listIndex].Split('\\')[
                                        fileList[listIndex].Split('\\').Length - 1]);

                        //Go one line down
                        Console.SetCursorPosition(0, currentLine + 1);
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        //Reset the colors and set the cursor position back to the end of the origanel position
                        Console.ResetColor();
                        Console.SetCursorPosition(0, origanalLine);
                        //Return the value of the selected list entry
                        return fileList[listIndex];
                    }

                    //Safe current cursor position
                    currentLine = Console.CursorTop;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }
}
