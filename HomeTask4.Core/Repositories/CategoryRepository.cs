using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace HomeTask4.Core.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        public string IsNameMustExist(string name)
        {
            do
            {
                if (!Items.Exists(x => x.Name.ToLower(CultureInfo.CurrentUICulture) == name.ToLower(CultureInfo.CurrentUICulture)))
                {
                    Console.Write("    No name found. Enter an existing name: ");
                    name = ValidManager.NullOrEmptyText(Console.ReadLine());
                }
            } while (!Items.Exists(x => x.Name == name));
            return name;
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
