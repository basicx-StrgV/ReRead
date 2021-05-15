using System;
using System.Collections.Generic;
using BasicxLogger;

namespace ReRead.Components
{
    class InputHandler
    {
        private Logger logger;

        public InputHandler(Logger logger)
        {
            this.logger = logger;
        }

        public string fileSelectMenu(List<string> fileList)
        {
            try
            {
                //Create new list for the menu and add the option ALL to the list
                List<string> menuList = new List<string>();
                menuList.Add("ALL");

                //Add all files to the menu list
                foreach(string file in fileList)
                {
                    menuList.Add(file);
                }

                //Add EXIT and RELOAD to the List
                menuList.Add("RELOAD");
                menuList.Add("EXIT");

                //Create banner for list with info text
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nSelect your file: ");
                Console.ResetColor();

                //Save the index of the first list entry
                int menuTopLine = Console.CursorTop;

                //Lists every list entry but removes the file path
                foreach (string entry in menuList)
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
                    Console.Write(menuList[listIndex].Split('\\')[
                                    menuList[listIndex].Split('\\').Length - 1]);

                    //Set the cursor beck to the start of the selected line
                    Console.SetCursorPosition(0, currentLine);

                    //Reset the colors
                    Console.ResetColor();

                    //Wait for keyboard input
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.UpArrow && currentLine > menuTopLine)
                    {
                        //Write the line to remove the custom color
                        Console.Write(menuList[listIndex].Split('\\')[
                                        menuList[listIndex].Split('\\').Length - 1]);

                        //Go one line up
                        Console.SetCursorPosition(0, currentLine - 1);
                    }
                    else if (key.Key == ConsoleKey.DownArrow && currentLine + 1 < origanalLine)
                    {
                        //Write the line to remove the custom color
                        Console.Write(menuList[listIndex].Split('\\')[
                                        menuList[listIndex].Split('\\').Length - 1]);

                        //Go one line down
                        Console.SetCursorPosition(0, currentLine + 1);
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        //Reset the colors and set the cursor position back to the end of the origanel position
                        Console.ResetColor();
                        Console.SetCursorPosition(0, origanalLine);
                        //Return the value of the selected list entry
                        return menuList[listIndex];
                    }

                    //Safe current cursor position
                    currentLine = Console.CursorTop;
                }
            }
            catch(Exception e)
            {
                logger.log(e.Message);
                return "";
            }
        }
    
        public void pressEnterToContinue()
        {
            try
            {
                //Display message
                Console.WriteLine("Press ENTER to continue");

                //Only continue if enter gets pressed
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        return;
                    }
                }
            }
            catch(Exception e)
            {
                logger.log(e.Message);
            }
        }
    }
}
