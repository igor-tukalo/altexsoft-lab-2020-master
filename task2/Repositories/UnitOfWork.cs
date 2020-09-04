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
            Categories = new CategoryRepository(jsonManager.Read<Category>());
            Ingredients = new IngredientRepository(jsonManager.Read<Ingredient>());
            Recipes = new RecipeRepository(jsonManager.Read<Recipe>());
            AmountIngredients = new AmountIngredientRepository(jsonManager.Read<AmountIngredient>());
            CookingSteps = new CookingStepRepository(jsonManager.Read<CookingStep>());
        }

        public void SaveAllData()
        {
            jsonManager.Save(Recipes.Items);
            jsonManager.Save(AmountIngredients.Items);
            jsonManager.Save(CookingSteps.Items);
            jsonManager.Save(Categories.Items);
            jsonManager.Save(Ingredients.Items);
            jsonManager.Save(AmountIngredients.Items);
        }
    }
}