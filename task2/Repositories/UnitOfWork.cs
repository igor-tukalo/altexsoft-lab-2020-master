namespace task2.Repositories
{
    public class UnitOfWork
    {
        private readonly CookBookContext db = new CookBookContext();
        private CategoryRepository categoryRepository;
        private IngredientRepository ingredientRepository;
        private RecipeRepository recipeRepository;
        private AmountIngredientRepository amountIngredientRepository;
        private CookingStepRepository cookingStepRepository;

        public CategoryRepository Categories => categoryRepository ?? (categoryRepository = new CategoryRepository(db));
        public IngredientRepository Ingredients => ingredientRepository ?? (ingredientRepository = new IngredientRepository(db));
        public RecipeRepository Recipes => recipeRepository ?? (recipeRepository = new RecipeRepository(db));
        public AmountIngredientRepository AmountIngredients => amountIngredientRepository ?? (amountIngredientRepository = new AmountIngredientRepository(db));
        public CookingStepRepository CookingSteps => cookingStepRepository ?? (cookingStepRepository = new CookingStepRepository(db));

        public void SaveChangesRecipe()
        {
            db.SaveAllData();
        }

        public void SaveDataTable(string jsonName, string content)
        {
            db.SaveChanges(jsonName, content);
        }
    }
}
