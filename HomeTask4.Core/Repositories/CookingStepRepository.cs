using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Core.Repositories
{
    public class CookingStepRepository : BaseRepository<CookingStep>
    {
        public CookingStepRepository(DbContext context) : base(context)
        {
        }
    }
}
