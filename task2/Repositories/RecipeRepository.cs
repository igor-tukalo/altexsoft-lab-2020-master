using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Repositories
{
    public class RecipeRepository : IRepository<Recipe>
    {
        private readonly CookBookContext db;
        public RecipeRepository(CookBookContext context)
        {
            this.db = context;
        }
        public void Create(Recipe item)
        {
            db.Recipes.Add(item);
        }

        public void Delete(int id)
        {
            Recipe recipe = (from r in db.Recipes
                             where r.Id == id
                             select r).FirstOrDefault();
            if (recipe != null)
                db.Recipes.Remove(recipe);
        }

        public Recipe Get(int id)
        {
            return (from r in db.Recipes
                    where r.Id == id
                    select r).FirstOrDefault();
        }

        public List<Recipe> GetAll()
        {
            return db.Recipes;
        }

        public void Update(Recipe item)
        {
            db.Recipes = db.Recipes
            .Select(r => r.Id == item.Id
            ? new Recipe { Id = item.Id, Name = item.Name, Description = item.Description, IdCategory = item.IdCategory }
            : r).ToList();
        }

        public string IsNameMustNotExist(string name)
        {
            do
            {
                if (db.Recipes.Exists(x => x.Name.ToLower() == name.ToLower()))
                {
                    Console.Write("    This name is already in use. enter another name: ");
                    name = Validation.NullOrEmptyText(Console.ReadLine());
                }
            } while (db.Recipes.Exists(x => x.Name.ToLower() == name.ToLower()));
            return name;
        }
    }
}
