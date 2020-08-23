using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;

namespace task2.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly CookBookContext db;

        public CategoryRepository(CookBookContext context)
        {
            this.db = context;
        }

        public void Create(Category item)
        {
            db.Categories.Add(item);
        }

        public void Delete(int id)
        {
            Category category = (from c in db.Categories
                                 where c.Id == id
                                 select c).FirstOrDefault();
            if (category != null)
                db.Categories.Remove(category);
        }

        public Category Get(int id)
        {
            return (from c in db.Categories
                   where c.Id==id
                   select c).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public void Update(Category item)
        {
            db.Categories = db.Categories
            .Select(c => c.Id == item.Id
            ? new Category { Id = item.Id, Name = item.Name, ParentId = item.ParentId }
            : c).ToList();
        }
    }

}
