using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using task2.Controls;
using task2.Models;

namespace task2.Instruments
{
    public static class Validation
    {
        /// <summary>
        /// Check string value for number
        /// </summary>
        /// <param name="nubmer"></param>
        /// <returns></returns>
        public static int ValidNumber(string nubmer)
        {
            int NumId;

            bool validStart = false;
            while (validStart == false)
            {
                //check for number
                bool isNum = int.TryParse(nubmer, out NumId);
                if (!string.IsNullOrWhiteSpace(nubmer) && isNum)
                {
                    return NumId;
                }
                else
                {
                    Console.WriteLine(" Warning! Value must be numeric!");
                    Console.Write("\r\n Enter value: ");
                    nubmer = Console.ReadLine();
                }
            }
            return 0;
        }

        /// <summary>
        /// Check string value for number
        /// </summary>
        /// <param name="nubmer"></param>
        /// <returns></returns>
        public static double ValidDouble(string nubmer)
        {
            double NumId;

            bool validStart = false;
            while (validStart == false)
            {
                //check for number
                bool isNum = double.TryParse(nubmer, out NumId);
                if (!string.IsNullOrWhiteSpace(nubmer) && isNum)
                {
                    return NumId;
                }
                else
                {
                    Console.WriteLine(" Warning! Value must be numeric!");
                    Console.Write("\r\n Enter value: ");
                    nubmer = Console.ReadLine();
                }
            }
            return 0;
        }

        /// <summary>
        /// Сhecking menu navigation selection from dictionary
        /// </summary>
        /// <param name="nameNavigationMenu"></param>
        /// <param name="navigations"></param>
        /// <returns></returns>
        public static int ValidNavigationMenu(string nameNavigationMenu, Dictionary<int, string> navigations)
        {
            Console.WriteLine(nameNavigationMenu);
            Console.WriteLine(" _______________________");

            foreach (KeyValuePair<int, string> keyValue in navigations)
                Console.WriteLine($" {keyValue.Key}. { keyValue.Value}");

            Console.Write("\r\n Enter value: ");
            int keyNavigation = Validation.ValidNumber(Console.ReadLine());

            while (!navigations.ContainsKey(keyNavigation))
            {
                Console.Write(" Warning! The value must be a menu number! Enter value: ");
                keyNavigation = Validation.ValidNumber(Console.ReadLine());
            }
            return keyNavigation;
        }

        /// <summary>
        /// Obtain a Y or N response
        /// </summary>
        /// <returns>response</returns>
        public static ConsoleKey YesNo()
        {
            ConsoleKey response;

            do
            {
                while (Console.KeyAvailable) // Flushes the input queue.
                    Console.ReadKey();

                Console.Write("y or n? ");
                response = Console.ReadKey().Key;
                Console.WriteLine();
            } while (response != ConsoleKey.Y && response != ConsoleKey.N); // If the user did not respond with a 'Y' or an 'N', repeat the loop.

            return response;
        }

        /// <summary>
        /// Obtain a 1 or 2 or 3 response to edit entity
        /// </summary>
        /// <returns>response</returns>
        public static ConsoleKey EdirOrDeleteorCancel()
        {
            ConsoleKey response;
            do
            {
                while (Console.KeyAvailable) // Flushes the input queue.
                    Console.ReadKey();

                Console.WriteLine("Press:\n1 to edit\n2 to remove\n3 to cancel");
                response = Console.ReadKey().Key;
                Console.WriteLine();
            } while (response != ConsoleKey.D1 && response != ConsoleKey.D2 && response != ConsoleKey.D3); // If the user did not respond, repeat the loop.

            return response;
        }

        /// <summary>
        /// Wrapping text on a new line after n characters
        /// </summary>
        /// <param name="numChar">number of characters in one line</param>
        /// <param name="text">number of characters</param>
        /// <param name="wrapChar">indent mark</param>
        public static string WrapText(int numChar, string text, string wrapChar = "\n")
        {
            string[] wrapText = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (wrapText.Length > numChar)
                for (int i = numChar - 1; i < wrapText.Length; i += numChar)
                {
                    wrapText[i] += wrapChar;
                }
            return string.Join(" ", wrapText).Replace(wrapChar + " ", wrapChar);
        }

        /// <summary>
        /// Сheck for text emptiness
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NullOrEmptyText(string text)
        {
            do
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.Write(" The value cannot be empty! Enter the value: ");
                    text = Console.ReadLine();
                }
            }
            while (string.IsNullOrWhiteSpace(text));
            return text;
        }

        /// <summary>
        /// Сheck item for unique name
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string IsExsistsNameList(List<EntityMenu> list, string name)
        {
            do
            {
                if (list.Exists(x => x.Name.ToLower() == name.ToLower()))
                {
                    Console.Write(" This name is already in use. enter another name: ");
                    name = Console.ReadLine();
                }
                else if (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write(" Name cannot be empty! Enter the name: ");
                    name = Console.ReadLine();
                }

            } while (list.Exists(x => x.Name.ToLower() == name.ToLower()) || string.IsNullOrWhiteSpace(name));
            return name;
        }

        /// <summary>
        /// Save json data
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="amountRecipeIngredients"></param>
        /// <param name="stepCookings"></param>
        /// <param name="ingredients"></param>
        public static void SaveSelectedDataJson(List<Recipe> recipes = null, List<AmountRecipeIngredient> amountRecipeIngredients = null,
            List<StepCooking> stepCookings = null, List<Ingredient> ingredients = null, List<Category> categories = null)
        {
            // Update json data string
            if (recipes != null)
                File.WriteAllText(new JsonControl("Recipes.json").GetJsonPathFile(), JsonConvert.SerializeObject(recipes));
            if (amountRecipeIngredients != null)
                File.WriteAllText(new JsonControl("AmountsRecipeIngredients.json").GetJsonPathFile(), JsonConvert.SerializeObject(amountRecipeIngredients));
            if (stepCookings != null)
                File.WriteAllText(new JsonControl("StepsCooking.json").GetJsonPathFile(), JsonConvert.SerializeObject(stepCookings));
            if (ingredients != null)
                File.WriteAllText(new JsonControl("Ingredients.json").GetJsonPathFile(), JsonConvert.SerializeObject(ingredients));
            if (categories != null)
                File.WriteAllText(new JsonControl("Categories.json").GetJsonPathFile(), JsonConvert.SerializeObject(categories));
        }
    }
}
