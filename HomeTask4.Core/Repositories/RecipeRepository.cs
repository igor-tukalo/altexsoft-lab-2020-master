using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace HomeTask4.Core.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(DbContext context) : base(context)
        {
        }

        public string IsNameMustNotExist(string name)
        {
            while (GetItems().ToList().Exists(x => x.Name.ToLower(CultureInfo.CurrentUICulture) == name.ToLower(CultureInfo.CurrentUICulture)))
            {
                Console.Write("    This name is already in use. enter another name: ");
                name = ValidManager.NullOrEmptyText(Console.ReadLine());
            }
            return name;
        }
    }
}
