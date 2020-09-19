using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Core.Repositories
{
    public class AmountIngredientRepository : BaseRepository<AmountIngredient>
    {
        public AmountIngredientRepository(DbContext context) : base(context)
        {
        }
    }
}
