namespace task2.Repositories
{
    public class UnitOfWork
    {
        private readonly CookBookContext db = new CookBookContext();

        private CategoryRepository categoryRepository;
        private IngredientRepository ingredientRepository;
        private RecipeRepository recipeRepository;
        private AmountIngredientRepository amountIngredientRepository;
        private StepCookingRepository stepCookingRepository;

        public CategoryRepository Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
            }
        }

        public IngredientRepository Ingredients
        {
            get
            {
                if (ingredientRepository == null)
                    ingredientRepository = new IngredientRepository(db);
                return ingredientRepository;
            }
        }

        public RecipeRepository Recipes
        {
            get
            {
                if (recipeRepository == null)
                    recipeRepository = new RecipeRepository(db);
                return recipeRepository;
            }
        }

        public AmountIngredientRepository AmountIngredients
        {   get
            {
                if (amountIngredientRepository == null)
                    amountIngredientRepository = new AmountIngredientRepository(db);
                return amountIngredientRepository;
            }
        }

        public StepCookingRepository StepsCooking
        {
            get
            {
                if (stepCookingRepository == null)
                    stepCookingRepository = new StepCookingRepository(db);
                return stepCookingRepository;
            }
        }

        public void SaveAllData()
        {
            db.SaveChanges();
        }

        public void SaveDataTable(string jsonName, string content)
        {
            db.SaveChanges(jsonName, content);
        }
    }
}
