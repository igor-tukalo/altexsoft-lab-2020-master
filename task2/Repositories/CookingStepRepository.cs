using System.Collections.Generic;
using System.Linq;
using task2.Models;
using task2.Repositories;

namespace task2.Interfaces
{
    public class CookingStepRepository : IRepository<CookingStep>
    {
        public List<CookingStep> Items { get; set; }
        public CookingStepRepository(List<CookingStep> context)
        {
            Items = context;
        }

        public void Create(CookingStep item)
        {
            Items.Add(item);
        }

        public void Delete(int id)
        {
            var stepCooking = Get(id);
            if (stepCooking != null)
                Items.Remove(stepCooking);
        }

        public CookingStep Get(int id)
        {
            return (from s in Items
                    where s.Id == id
                    select s).FirstOrDefault();
        }

        public void Update(CookingStep item)
        {
            Items = Items
            .Select(s => s.Id == item.Id
            ? new CookingStep { Id = item.Id, Step = item.Step, Name = item.Name, IdRecipe= item.IdRecipe }
            : s).ToList();
        }
    }
}
