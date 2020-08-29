using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
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

        public List<Category> GetAll()
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

        public string IsNameMustExist(string name)
        {
            do
            {
                if (!db.Categories.Exists(x => x.Name.ToLower() == name.ToLower()))
                {
                    Console.Write("    No name found. Enter an existing name: ");
                    name = Validation.NullOrEmptyText(Console.ReadLine());
                }
            } while (!db.Categories.Exists(x => x.Name == name));
            return name;
        }

        public string IsNameMustNotExist(string name)
        {
            do
            {
                if (db.Categories.Exists(x => x.Name.ToLower() == name.ToLower()))
                {
                    Console.Write("    This name is already in use. enter another name: ");
                    name = Validation.NullOrEmptyText(Console.ReadLine());
                }
            } while (db.Categories.Exists(x => x.Name.ToLower() == name.ToLower()));
            return name;
        }
    }

}
