using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext db;
        public CategoryRepository Categories { get; }
        public IngredientRepository Ingredients { get; }
        public RecipeRepository Recipes { get; }
        public AmountIngredientRepository AmountIngredients { get; }
        public CookingStepRepository CookingSteps { get; }

        public UnitOfWork(DbContext context)
        {
            db = context;
            Categories = new CategoryRepository(db);
            Ingredients = new IngredientRepository(db);
            Recipes = new RecipeRepository(db);
            AmountIngredients = new AmountIngredientRepository(db);
            CookingSteps = new CookingStepRepository(db);
        }

        public void RefreshContext()
        {
            db.Update(AmountIngredients);
            db.Update(Ingredients);
        }

        public void SaveAllData()
        {
            db.SaveChanges();
        }
    }
}
