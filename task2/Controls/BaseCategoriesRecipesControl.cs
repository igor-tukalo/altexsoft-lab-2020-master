using task2.Interfaces;

namespace task2.Controls
{
    class BaseCategoriesRecipesControl : BaseControl
    {
        readonly protected CategoryRepository categoryRepository;
        readonly protected RecipeRepository recipeRepository;
        readonly protected AmountIngredientRepository amountIngredientRepository;
        readonly protected CookingStepRepository cookingStepRepository;

        public BaseCategoriesRecipesControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            categoryRepository = UnitOfWork.Categories;
            recipeRepository = UnitOfWork.Recipes;
            amountIngredientRepository = UnitOfWork.AmountIngredients;
            cookingStepRepository = UnitOfWork.CookingSteps;
        }
    }
}