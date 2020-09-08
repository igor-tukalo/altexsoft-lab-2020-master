using System;
using System.Collections.Generic;
using System.Linq;
using task2.Models;
using task2.Repositories;

namespace task2.Interfaces
{
    class RecipeRepository : BaseRepository<Recipe>
    {
        public RecipeRepository(List<Recipe> context) : base(context)
        {
        }

        public override void Update(Recipe item)
        {
            Items = Items
            .Select(r => r.Id == item.Id
            ? new Recipe { Id = item.Id, Name = item.Name, Description = item.Description, IdCategory = item.IdCategory }
            : r).ToList();
        }

        public string IsNameMustNotExist(string name)
        {
            while (Items.Exists(x => x.Name.ToLower() == name.ToLower()))
            {
                Console.Write("    This name is already in use. enter another name: ");
                name = Validation.NullOrEmptyText(Console.ReadLine());
            }
            return name;
        }
    }
}
