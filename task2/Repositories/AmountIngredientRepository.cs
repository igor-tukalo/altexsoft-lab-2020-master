using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;

namespace task2.Repositories
{
    public class AmountIngredientRepository : IRepository<AmountIngredient>
    {
        private readonly CookBookContext db;

        public AmountIngredientRepository(CookBookContext context)
        {
            this.db = context;
        }

        public void Create(AmountIngredient item)
        {
            db.AmountIngredients.Add(item);
        }

        public void Delete(int id)
        {
            AmountIngredient amountIngredient = (from a in db.AmountIngredients
                                                 where a.Id == id
                                                 select a).FirstOrDefault();
            if (amountIngredient != null)
                db.AmountIngredients.Remove(amountIngredient);
        }

        public AmountIngredient Get(int id)
        {
            return (from a in db.AmountIngredients
                    where a.Id == id
                    select a).FirstOrDefault();
        }

        public IEnumerable<AmountIngredient> GetAll()
        {
            return db.AmountIngredients;
        }

        public void GetMenuNavigation()
        {

        }

        public void Update(AmountIngredient item)
        {
            db.AmountIngredients = db.AmountIngredients
            .Select(a => a.Id == item.Id
            ? new AmountIngredient { Id = item.Id, Amount = item.Amount, Unit = item.Unit, IdRecipe = item.IdRecipe, IdIngredient = item.IdIngredient }
            : a).ToList();
        }
    }
}
