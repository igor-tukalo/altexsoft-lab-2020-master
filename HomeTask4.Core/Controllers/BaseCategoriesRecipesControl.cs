using HomeTask4.Core.Repositories;

namespace HomeTask4.Core.CRUD
{
    public abstract class BaseCategoriesRecipesControl : BaseControl
    {
        internal readonly CategoryRepository CategoryRepository;
        internal readonly RecipeRepository RecipeRepository;
        internal readonly AmountIngredientRepository AmountIngredientRepository;
        internal readonly CookingStepRepository CookingStepRepository;

        public BaseCategoriesRecipesControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            CategoryRepository = UnitOfWork.Categories;
            RecipeRepository = UnitOfWork.Recipes;
            AmountIngredientRepository = UnitOfWork.AmountIngredients;
            CookingStepRepository = UnitOfWork.CookingSteps;
        }
    }
}
