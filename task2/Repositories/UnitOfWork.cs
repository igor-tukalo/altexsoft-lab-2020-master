using task2.Models;
using task2.Repositories;

namespace task2.Interfaces
{
    class UnitOfWork : IUnitOfWork
    {
        readonly JsonManager jsonManager = new JsonManager();

        public CategoryRepository Categories { get; }
        public IngredientRepository Ingredients { get; }
        public RecipeRepository Recipes { get; }
        public AmountIngredientRepository AmountIngredients { get; }
        public CookingStepRepository CookingSteps { get; }

        public UnitOfWork()
        {
            Categories = new CategoryRepository(jsonManager.JsonDeSerializer<Category>());
            Ingredients = new IngredientRepository(jsonManager.JsonDeSerializer<Ingredient>());
            Recipes = new RecipeRepository(jsonManager.JsonDeSerializer<Recipe>());
            AmountIngredients = new AmountIngredientRepository(jsonManager.JsonDeSerializer<AmountIngredient>());
            CookingSteps = new CookingStepRepository(jsonManager.JsonDeSerializer<CookingStep>());
        }

        public void SaveAllData()
        {
            jsonManager.JsonSerializer(Recipes.Items);
            jsonManager.JsonSerializer(AmountIngredients.Items);
            jsonManager.JsonSerializer(CookingSteps.Items);
            jsonManager.JsonSerializer(Categories.Items);
            jsonManager.JsonSerializer(Ingredients.Items);
            jsonManager.JsonSerializer(AmountIngredients.Items);
        }
    }
}