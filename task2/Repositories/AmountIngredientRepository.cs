using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Repositories
{
    public class AmountIngredientRepository : IRepository<AmountIngredient>
    {
        public List<AmountIngredient> Items { get; set; }
        public AmountIngredientRepository(List<AmountIngredient> context)
        {
            Items = context;
        }

        public void Create(AmountIngredient item)
        {
            Items.Add(item);
        }

        public void Delete(int id)
        {
            var amountIngredient = Get(id);
            if (amountIngredient != null)
                Items.Remove(amountIngredient);
        }

        public AmountIngredient Get(int id)
        {
            return (from a in Items
                    where a.Id == id
                    select a).FirstOrDefault();
        }

        public void Update(AmountIngredient item)
        {
            Items = Items
            .Select(a => a.Id == item.Id
            ? new AmountIngredient { Id = item.Id, Amount = item.Amount, Unit = item.Unit, IdRecipe = item.IdRecipe, IdIngredient = item.IdIngredient }
            : a).ToList();
        }
    }
}
