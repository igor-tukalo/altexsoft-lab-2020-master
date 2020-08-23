using System;
using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;

namespace task2.Controls
{
    public class RecipesCategoryControl : CategoriesControl
    {
        protected int IdPrevCategory { get; set; }
        public RecipesCategoryControl()
        {
        }

        public override void GetMenuItems(int IdMenu = 1)
        {
            Console.Clear();

            ItemsMenu = new List<EntityMenu>
                {
                    new Category(name: "    Add recipe"),
                    new Category(name: "    Return to main menu")
                };

            var parent = unitOfWork.Categories.GetAll().ToList().Find((x) => x.Id == IdMenu);
            IdPrevCategory = parent.Id;
            BuildHierarchicalMenu(new List<EntityMenu>(unitOfWork.Categories.GetAll().ToList()), unitOfWork.Recipes.GetAll().ToList(), parent, 1, 2);

            Console.WriteLine(" Recipe categories");
            Console.WriteLine(" To return to the previous category, click on the topmost category.");
            Console.WriteLine(" Recipes are inside categories and do not have a symbol in the name -");

            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        int idRecipe = unitOfWork.Recipes.GetAll().Count() > 0 ? unitOfWork.Recipes.GetAll().Max(x => x.Id) + 1 : 1;

                        unitOfWork.Recipes.Create(new Recipe() { Id = idRecipe, IdCategory = IdPrevCategory });

                        new RecipeСreateControl(idRecipe, IdPrevCategory, unitOfWork).GetMenuItems();

                    }
                    break;
                case 1:
                    {
                        new MainMenuControl().GetMenuItems();
                    }
                    break;
                default:
                    {
                        if (ItemsMenu[id].TypeEntity == "Recipe")
                        {
                            new RecipeViewControl(unitOfWork.Recipes.Get(ItemsMenu[id].Id).Id, IdPrevCategory, unitOfWork).GetMenuItems();
                        }
                        else
                        {
                            if (ItemsMenu[id].ParentId != 0)
                            {
                                new NavigateRecipeCategories().GetRecipesCategory(ItemsMenu[id], IdPrevCategory);
                            }

                        }
                    }
                    break;
            }
        }

        //private EntityMenu GetCategory()
        //{
        //    return (from t in ItemsMenu
        //            where t.Id == IdPrevCategory
        //            select t).First();
        //}
    }
}
