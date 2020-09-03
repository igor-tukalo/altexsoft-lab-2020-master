namespace task2.Interfaces
{
    interface IUnitOfWork
    {
        AmountIngredientRepository AmountIngredients { get; }
        CategoryRepository Categories { get; }
        CookingStepRepository CookingSteps { get; }
        IngredientRepository Ingredients { get; }
        RecipeRepository Recipes { get; }
        void SaveAllData();
    }
}