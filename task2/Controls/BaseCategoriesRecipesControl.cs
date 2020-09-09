using task2.Interfaces;

namespace task2.Controls
{
    abstract class BaseCategoriesRecipesControl : BaseControl
    {
        protected readonly CategoryRepository CategoryRepository;
        protected readonly RecipeRepository RecipeRepository;
        protected readonly AmountIngredientRepository AamountIngredientRepository;
        protected readonly CookingStepRepository CookingStepRepository;

        protected BaseCategoriesRecipesControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CategoryRepository = UnitOfWork.Categories;
            RecipeRepository = UnitOfWork.Recipes;
            AamountIngredientRepository = UnitOfWork.AmountIngredients;
            CookingStepRepository = UnitOfWork.CookingSteps;
        }
    }
}