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

        public override void Delete(int id)
        {
            var stepCooking = Get(id);
            if (stepCooking != null)
                Items.Remove(stepCooking);
        }

        public override CookingStep Get(int id)
        {
            return (from s in Items
                    where s.Id == id
                    select s).FirstOrDefault();
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
