using System;
using System.Collections.Generic;
using System.Linq;
using task2.Models;
using task2.Repositories;

namespace task2.Interfaces
{
    class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(List<Category> context) : base(context)
        {
        }

        public override void Update(Category item)
        {
            Items = Items
            .Select(c => c.Id == item.Id
            ? new Category { Id = item.Id, Name = item.Name, ParentId = item.ParentId }
            : c).ToList();
        }

        public string IsNameMustExist(string name)
        {
            do
            {
                if (!Items.Exists(x => x.Name.ToLower() == name.ToLower()))
                {
                    Console.Write("    No name found. Enter an existing name: ");
                    name = Validation.NullOrEmptyText(Console.ReadLine());
                }
            } while (!Items.Exists(x => x.Name == name));
            return name;
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
