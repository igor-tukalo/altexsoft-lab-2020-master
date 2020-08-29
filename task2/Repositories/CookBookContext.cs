using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using task2.Models;

namespace task2.Repositories
{
    public class CookBookContext
    {
        public List<Category> Categories { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<AmountIngredient> AmountIngredients { get; set; }
        public List<CookingStep> CookingSteps { get; set; }
        public CookBookContext()
        {
            Categories = JsonConvert.DeserializeObject<List<Category>>(GetJsonData("Categories.json"));
            Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(GetJsonData("Ingredients.json"));
            Recipes = JsonConvert.DeserializeObject<List<Recipe>>(GetJsonData("Recipes.json"));
            AmountIngredients = JsonConvert.DeserializeObject<List<AmountIngredient>>(GetJsonData("AmountIngredients.json"));
            CookingSteps = JsonConvert.DeserializeObject<List<CookingStep>>(GetJsonData("CookingSteps.json"));
        }
        /// <summary>
        /// Get json file data at specified path
        /// </summary>
        /// <returns>json data</returns>
        public string GetJsonData(string jsonFileName)
        {
            try
            {
                // Read existing json data
                string jsonData = File.ReadAllText(GetJsonPathFile(jsonFileName));
                if (!string.IsNullOrWhiteSpace(Regex.Replace(jsonData, "[{}]", "")))
                    return jsonData;
                else return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return string.Empty;
            }
        }
        /// <summary>
        /// Get path to json file by specified filename
        /// </summary>
        /// <returns>path to json file</returns>
        public string GetJsonPathFile(string jsonFileName)
        {
            try
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
                return Path.Combine(exePath, $"json\\{jsonFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return string.Empty;
            }
        }
        public void SaveAllData()
        {
            File.WriteAllText(GetJsonPathFile("Recipes.json"), JsonConvert.SerializeObject(Recipes));
            File.WriteAllText(GetJsonPathFile("AmountIngredients.json"), JsonConvert.SerializeObject(AmountIngredients));
            File.WriteAllText(GetJsonPathFile("CookingSteps.json"), JsonConvert.SerializeObject(CookingSteps));
        }
        public void SaveChanges(string jsonName,string content)
        {
            File.WriteAllText(GetJsonPathFile(jsonName), content);
        }
    }
}