using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Controls;
using task2.Models;
using task2.Repositories;

namespace task2.Instruments
{
    public class ContextMenuCategories : ContextMenuNavigation
    {
        public ContextMenuCategories(UnitOfWork _unitOfWork, int idMenuNavigation, int recipeId = 0) : base(_unitOfWork, idMenuNavigation, recipeId)
        {
        }

        protected override void Rename()
        {
            Console.Write("  Enter new name: ");
            string newName = Console.ReadLine();

            
            Category category = unitOfWork.Categories.Get(IdMenuNavigation);
            unitOfWork.Categories.Update(new Category { Id = category.Id, Name = newName, ParentId = category.ParentId });

            unitOfWork.SaveDataTable("Categories.json", JsonConvert.SerializeObject(unitOfWork.Categories.GetAll()));
            Cancel();
        }

        protected override void Delete()
        {
            Console.Write("  Attention! are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                var parent = unitOfWork.Categories.GetAll().ToList().Find((x) => x.Id == IdMenuNavigation);
                RemoveHierarchicalCategory(new List<EntityMenu>(unitOfWork.Categories.GetAll().ToList()), parent, 1);

                unitOfWork.SaveAllData();
                unitOfWork.SaveDataTable("Categories.json", JsonConvert.SerializeObject(unitOfWork.Categories.GetAll()));
                Cancel();
            }
            else Cancel();
        }

        protected void RemoveHierarchicalCategory(List<EntityMenu> items, EntityMenu thisEntity, int level)
        {
            unitOfWork.Categories.Delete(thisEntity.Id);
            foreach (var r in unitOfWork.Recipes.GetAll().ToList().Where(x => x.IdCategory == thisEntity.Id))
            {
                foreach (var a in unitOfWork.AmountIngredients.GetAll().ToList().Where(x => x.IdRecipe == r.Id))
                    unitOfWork.AmountIngredients.Delete(a.Id);

                foreach (var a in unitOfWork.StepsCooking.GetAll().ToList().Where(x => x.IdRecipe == r.Id))
                    unitOfWork.StepsCooking.Delete(a.Id);

                unitOfWork.Recipes.Delete(r.Id);
            }

            foreach (var child in items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                RemoveHierarchicalCategory(items, child, level + 1);
            }
        }
        protected override void Cancel()
        {
            new CategoriesControl().GetMenuItems();
        }

    }
}
