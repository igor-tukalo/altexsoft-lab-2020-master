using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HomeTask4.Core.Controllers
{
    public class CategoriesController : BaseController, ICategoriesControl
    {
        private List<Category> Categories => UnitOfWork.Repository.GetListAsync<Category>().Result;
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Category GetParentCategory(int id)
        {
            return UnitOfWork.Repository.GetByIdAsync<Category>(1).Result;
        }

        public void Add()
        {
            Console.Write("\n    Enter name category: ");
            string name = Console.ReadLine();
            Console.Write("    Enter name main category: ");
            string nameMainCategory = IsNameMustExist(ValidManager.NullOrEmptyText(Console.ReadLine()));
            int idMainCategory = (from t in Categories
                                  where t.Name == nameMainCategory
                                  select t.Id).First();
            UnitOfWork.Repository.AddAsync(new Category() { Name = name, ParentId = idMainCategory });
        }

        public void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level)
        {
            if (items != null && thisEntity != null)
            {
                items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
            }
            foreach (Category child in Categories.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                BuildHierarchicalCategories(items, child, level + 1);
            }
        }

        public void RemoveHierarchicalCategory(Category thisEntity, int level)
        {
            if (thisEntity != null)
            {
                UnitOfWork.Repository.DeleteAsync(UnitOfWork.Repository.GetByIdAsync<Category>(thisEntity.Id).Result);
            }
            foreach (Category child in Categories.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                RemoveHierarchicalCategory(child, level + 1);
            }
        }

        public void Edit(int id)
        {
            Console.Write("    Enter new name: ");
            string newName = IsNameMustNotExist(Console.ReadLine());
            Category category = UnitOfWork.Repository.GetByIdAsync<Category>(id).Result;
            category.Name = newName;
            UnitOfWork.Repository.UpdateAsync(category);
        }

        public void Delete(int id)
        {
            if (UnitOfWork.Repository.GetByIdAsync<Category>(id).Result.ParentId == 0)
            {
                return;
            }
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            Category parent = UnitOfWork.Repository.GetByIdAsync<Category>(id).Result;
            RemoveHierarchicalCategory(parent, 1);
        }

        private string IsNameMustExist(string name)
        {
            do
            {
                if (!Categories.Exists(x => x.Name.ToLower(CultureInfo.CurrentUICulture) == name.ToLower(CultureInfo.CurrentUICulture)))
                {
                    Console.Write("    No name found. Enter an existing name: ");
                    name = ValidManager.NullOrEmptyText(Console.ReadLine());
                }
            } while (!Categories.Exists(x => x.Name == name));
            return name;
        }

        private string IsNameMustNotExist(string name)
        {
            while (Categories.Exists(x => x.Name.ToLower(CultureInfo.CurrentUICulture) == name.ToLower(CultureInfo.CurrentUICulture)))
            {
                Console.Write("    This name is already in use. enter another name: ");
                name = ValidManager.NullOrEmptyText(Console.ReadLine());
            }
            return name;
        }
    }
}
