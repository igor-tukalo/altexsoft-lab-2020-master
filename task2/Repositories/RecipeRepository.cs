using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Repositories
{
    public class RecipeRepository : IRepository<Recipe>
    {
        public List<Recipe> Items { get; set; }

        public RecipeRepository(List<Recipe> context)
        {
            Items = context;
        }
        public void Create(Recipe item)
        {
            Items.Add(item);
        }

        public void Delete(int id)
        {
            var recipe = Get(id);
            if (recipe != null)
                Items.Remove(recipe);
        }

        public Recipe Get(int id)
        {
            return (from r in Items
                    where r.Id == id
                    select r).FirstOrDefault();
        }

        public void Update(Recipe item)
        {
            Items = Items
            .Select(r => r.Id == item.Id
            ? new Recipe { Id = item.Id, Name = item.Name, Description = item.Description, IdCategory = item.IdCategory }
            : r).ToList();
        }

        public string IsNameMustNotExist(string name)
        {
            do
            {
                if (Items.Exists(x => x.Name.ToLower() == name.ToLower()))
                {
                    Console.Write("    This name is already in use. enter another name: ");
                    name = Validation.NullOrEmptyText(Console.ReadLine());
                }
            } while (Items.Exists(x => x.Name.ToLower() == name.ToLower()));
            return name;
        }
    }
}
