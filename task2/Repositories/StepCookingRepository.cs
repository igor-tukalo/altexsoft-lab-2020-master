using System.Collections.Generic;
using System.Linq;
using task2.Models;

namespace task2.Repositories
{
    public class StepCookingRepository : IRepository<StepCooking>
    {
        private readonly CookBookContext db;
        public StepCookingRepository(CookBookContext context)
        {
            this.db = context;
        }
        public void Create(StepCooking item)
        {
            db.StepsCooking.Add(item);
        }

        public void Delete(int id)
        {
            StepCooking stepCooking = (from s in db.StepsCooking
                             where s.Id == id
                             select s).FirstOrDefault();
            if (stepCooking != null)
                db.StepsCooking.Remove(stepCooking);
        }

        public StepCooking Get(int id)
        {
            return (from s in db.StepsCooking
                    where s.Id == id
                    select s).FirstOrDefault();
        }

        public IEnumerable<StepCooking> GetAll()
        {
            return db.StepsCooking;
        }

        public void GetMenuNavigation()
        {
            
        }

        public void Update(StepCooking item)
        {
            db.StepsCooking = db.StepsCooking
            .Select(s => s.Id == item.Id
            ? new StepCooking { Id = item.Id, Step = item.Step, Name = item.Name, IdRecipe= item.IdRecipe }
            : s).ToList();
        }
    }
}
