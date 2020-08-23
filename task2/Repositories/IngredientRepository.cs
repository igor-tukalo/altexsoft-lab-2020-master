using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;

namespace task2.Repositories
{
    public class IngredientRepository : IRepository<Ingredient>
    {
        private readonly CookBookContext db;
        public IngredientRepository(CookBookContext context)
        {
            this.db = context;
        }
        public void Create(Ingredient item)
        {
            db.Ingredients.Add(item);
        }

        public void Delete(int id)
        {
            Ingredient ingredient = (from i in db.Ingredients
                                   where i.Id == id
                                 select i).FirstOrDefault();
            if (ingredient != null)
                db.Ingredients.Remove(ingredient);
        }

        public Ingredient Get(int id)
        {
            return (from i in db.Ingredients
                    where i.Id == id
                    select i).FirstOrDefault();
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return db.Ingredients;
        }

        public void Update(Ingredient item)
        {
            db.Ingredients = db.Ingredients
            .Select(i => i.Id == item.Id
            ? new Ingredient { Id = item.Id, Name = item.Name}
            : i).ToList();
        }
    }
}
