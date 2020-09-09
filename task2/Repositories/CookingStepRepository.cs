using System.Collections.Generic;
using System.Linq;
using task2.Models;
using task2.Repositories;

namespace task2.Interfaces
{
    class CookingStepRepository : BaseRepository<CookingStep>
    {
        public CookingStepRepository(List<CookingStep> context) : base(context)
        {
        }

        public override void Update(CookingStep item)
        {
            Items = Items
            .Select(s => s.Id == item.Id
            ? new CookingStep { Id = item.Id, Step = item.Step, Name = item.Name, IdRecipe = item.IdRecipe }
            : s).ToList();
        }
    }
}
