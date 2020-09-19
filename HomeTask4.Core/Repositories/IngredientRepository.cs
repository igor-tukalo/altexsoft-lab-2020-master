using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace HomeTask4.Core.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>
    {
        public IngredientRepository(DbContext context) : base(context)
        {
        }

        public string IsNameMustNotExist(string name)
        {
            while (Items.Exists(x => x.Name.ToLower(CultureInfo.CurrentUICulture) == name.ToLower(CultureInfo.CurrentUICulture)))
            {
                Console.Write("    This name is already in use. enter another name: ");
                name = ValidManager.NullOrEmptyText(Console.ReadLine());
            }
            return name;
        }
    }
}
