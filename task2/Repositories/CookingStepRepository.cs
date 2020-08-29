using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Repositories
{
    public class CookingStepRepository : IRepository<CookingStep>
    {
        private readonly CookBookContext db;
        public CookingStepRepository(CookBookContext context)
        {
            this.db = context;
        }
        public void Create(CookingStep item)
        {
            db.CookingSteps.Add(item);
        }

        public void Delete(int id)
        {
            CookingStep stepCooking = (from s in db.CookingSteps
                             where s.Id == id
                             select s).FirstOrDefault();
            if (stepCooking != null)
                db.CookingSteps.Remove(stepCooking);
        }

        public CookingStep Get(int id)
        {
            return (from s in db.CookingSteps
                    where s.Id == id
                    select s).FirstOrDefault();
        }

        public List<CookingStep> GetAll()
        {
            return db.CookingSteps;
        }

        public void Update(CookingStep item)
        {
            db.CookingSteps = db.CookingSteps
            .Select(s => s.Id == item.Id
            ? new CookingStep { Id = item.Id, Step = item.Step, Name = item.Name, IdRecipe= item.IdRecipe }
            : s).ToList();
        }
    }
}
