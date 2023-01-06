using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsComp
{
    internal class Menu
    {
        int currentIndex;
        int maxIndex;
        string prompt;
        string[] options;

        public Menu(int maxIndex, string prompt, string[] options)
        {
            this.prompt = prompt;
            this.options = options;
            this.maxIndex = maxIndex;
        }

        private void DisplayOptions()
        {
            Console.WriteLine(prompt);

            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];
                if (i == currentIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"<< {currentOption} >>");
            }
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                switch(keyPressed)
                {
                    case ConsoleKey.UpArrow:
                        if(currentIndex == 0)
                        {
                            currentIndex = maxIndex;
                        } else
                        {
                            currentIndex--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentIndex == maxIndex)
                        {
                            currentIndex = 0;
                        }
                        else
                        {
                            currentIndex++;
                        }
                        break;
                }

            } while (keyPressed != ConsoleKey.Enter);
            return currentIndex;
        }
    }
}
