using System.Collections.Generic;
using System.Linq;
using task2.Models;
using task2.Repositories;

namespace task2.Interfaces
{
    class AmountIngredientRepository : BaseRepository<AmountIngredient>
    {
        public AmountIngredientRepository(List<AmountIngredient> context) : base(context)
        {
        }

        public override void Update(AmountIngredient item)
        {
            Items = Items
            .Select(a => a.Id == item.Id
            ? new AmountIngredient { Id = item.Id, Amount = item.Amount, Unit = item.Unit, IdRecipe = item.IdRecipe, IdIngredient = item.IdIngredient }
            : a).ToList();
        }
    }
}
