using System;
using System.Collections.Generic;
using System.Linq;
using task2.Models;
using task2.Repositories;

namespace task2.Interfaces
{
    class IngredientRepository : BaseRepository<Ingredient>, IRepository<Ingredient>
    {
        public IngredientRepository(List<Ingredient> context) : base(context)
        {
        }

        public void Delete(int id)
        {
            var ingredient = Get(id);
            if (ingredient != null)
                Items.Remove(ingredient);
        }

        public Ingredient Get(int id)
        {
            return (from i in Items
                    where i.Id == id
                    select i).FirstOrDefault();
        }

        public void Update(Ingredient item)
        {
            Items = Items
            .Select(i => i.Id == item.Id
            ? new Ingredient { Id = item.Id, Name = item.Name }
            : i).ToList();
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
