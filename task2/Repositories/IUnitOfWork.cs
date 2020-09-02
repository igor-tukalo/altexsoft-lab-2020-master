namespace task2.Interfaces
{
    public interface IUnitOfWork
    {
        AmountIngredientRepository AmountIngredients { get; }
        CategoryRepository Categories { get; }
        CookingStepRepository CookingSteps { get; }
        IngredientRepository Ingredients { get; }
        RecipeRepository Recipes { get; }

        string GetJsonData(string jsonFileName);
        string GetJsonPathFile(string jsonFileName);
        void SaveAllData();
    }
}