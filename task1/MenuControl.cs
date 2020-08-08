using System;
using System.Collections.Generic;
using System.Text;

namespace task1
{
    public class MenuControl
    {
        public void CallMenuSelectMethods()
        {
            Console.WriteLine("1) Read the text file specified in the path and delete (after saving the original file) the character / word specified in the console in it, if the specified word is not present in the text, display a corresponding message.\r\n");
            Console.WriteLine("2) Reads a text file and print the number of words in the text, and print every 10th word separated by commas.\r\n");
            Console.WriteLine("3) Type the 3rd sentence in the text. Words must be in reverse order.\r\n");
            Console.WriteLine("4) Display folder names at the specified path in the console. Each folder must have an identifier by which the user can find the desired folder and view all the files that it contains. Folder and file names must be sorted in alphabetical order.\r\n");
            
            int selectedMethod;
            do
            {
                Console.Write("\rSelect method number: ");
                selectedMethod = Validation.ValidNumber(Console.ReadLine());
            }
            while (selectedMethod < 1 || selectedMethod > 4);

            MethodsManegment methodsManegment = new MethodsManegment();
            methodsManegment.CallMethod(selectedMethod);
        }
    }
}
