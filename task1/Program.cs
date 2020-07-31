using System;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuSelectMethods();
        }

        /// <summary>
        /// Launch the method selection menu
        /// </summary>
        static void MenuSelectMethods()
        {
            string selectedMethod = string.Empty;

            Console.WriteLine("1) Read the text file specified in the path and delete (after saving the original file) the character / word specified in the console in it, if the specified word is not present in the text, display a corresponding message.\r\n");
            Console.WriteLine("2) Reads a text file and print the number of words in the text, and print every 10th word separated by commas.\r\n");
            Console.WriteLine("3) Type the 3rd sentence in the text. Words must be in reverse order.\r\n");
            Console.WriteLine("4) Display folder names at the specified path in the console. Each folder must have an identifier by which the user can find the desired folder and view all the files that it contains. Folder and file names must be sorted in alphabetical order.\r\n");
            Console.Write("\r\nSelect method number: ");
            selectedMethod = Console.ReadLine();

            bool validStart = false;
            while (validStart == false)
            {
                //check for number
                int Num;
                bool isNum = int.TryParse(selectedMethod, out Num);

                if (!string.IsNullOrWhiteSpace(selectedMethod) && (isNum && Num > 0 && Num < 5))
                {
                    validStart = true;
                    SelectMethod(Convert.ToInt32(selectedMethod));
                }
                else
                {
                    Console.WriteLine("Warning! Method number must be numeric and in the range from 1 to 4!");
                    Console.WriteLine("\r\nSelect method number: ");
                    selectedMethod = Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Select method by number
        /// </summary>
        /// <param name="numberMethod"></param>
        static void SelectMethod(int numberMethod)
        {
            FileManegment fileManegment = new FileManegment();
            switch (numberMethod)
            {
                case 0:
                    {
                        bool validMenuSelectMethods = false;
                        while (validMenuSelectMethods == false)
                        {
                            Console.WriteLine("\r\nReturn to the method selection menu? y/n");
                            string answer = Console.ReadLine();
                            if (answer == "y" || answer == "Y")
                            {
                                Console.Clear();
                                MenuSelectMethods();
                            }

                            else if (answer == "n" || answer == "N")
                            { validMenuSelectMethods = true; }
                            else Console.WriteLine("\r\nPlease press y or n to choose!");
                        }
                    }
                    break;
                case 1:
                    {
                        fileManegment.Method1();
                    }
                    goto case 0;
                case 2:
                    {
                        fileManegment.Method2(); 
                    };
                    goto case 0;
                case 3:
                    {
                        fileManegment.Method3();
                    };
                    goto case 0;
                case 4:
                    {
                        fileManegment.Method4();
                    };
                    goto case 0;
            }
        }
    }
}
