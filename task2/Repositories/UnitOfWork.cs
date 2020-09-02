using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using task2.Models;

namespace task2.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private CategoryRepository categoryRepository;
        private IngredientRepository ingredientRepository;
        private RecipeRepository recipeRepository;
        private AmountIngredientRepository amountIngredientRepository;
        private CookingStepRepository cookingStepRepository;

        public UnitOfWork()
        {
            categoryRepository = new CategoryRepository(JsonConvert.DeserializeObject<List<Category>>(GetJsonData("Categories.json")));
            ingredientRepository = new IngredientRepository(JsonConvert.DeserializeObject<List<Ingredient>>(GetJsonData("Ingredients.json")));
            recipeRepository = new RecipeRepository(JsonConvert.DeserializeObject<List<Recipe>>(GetJsonData("Recipes.json")));
            amountIngredientRepository = new AmountIngredientRepository(JsonConvert.DeserializeObject<List<AmountIngredient>>(GetJsonData("AmountIngredients.json")));
            cookingStepRepository = new CookingStepRepository(JsonConvert.DeserializeObject<List<CookingStep>>(GetJsonData("CookingSteps.json")));
        }

        public CategoryRepository Categories => categoryRepository ?? categoryRepository;
        public IngredientRepository Ingredients => ingredientRepository ?? ingredientRepository;
        public RecipeRepository Recipes => recipeRepository ?? recipeRepository;
        public AmountIngredientRepository AmountIngredients => amountIngredientRepository ?? amountIngredientRepository;
        public CookingStepRepository CookingSteps => cookingStepRepository ?? cookingStepRepository;

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
            SaveChanges("Recipes.json", JsonConvert.SerializeObject(Recipes.Items));
            SaveChanges("AmountIngredients.json", JsonConvert.SerializeObject(AmountIngredients.Items));
            SaveChanges("CookingSteps.json", JsonConvert.SerializeObject(CookingSteps.Items));
            SaveChanges("Categories.json", JsonConvert.SerializeObject(Categories.Items));
            SaveChanges("Ingredients.json", JsonConvert.SerializeObject(Ingredients.Items));
            SaveChanges("AmountIngredients.json", JsonConvert.SerializeObject(AmountIngredients.Items));
        }
        private void SaveChanges(string jsonName, string content)
        {
            File.WriteAllText(GetJsonPathFile(jsonName), content);
        }
    }
}