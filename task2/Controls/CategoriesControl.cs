using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    public class CategoriesControl : MenuNavigation
    {
        public CategoriesControl()
        {
            unitOfWork = new UnitOfWork();
        }
        public override void GetMenuItems(int IdMenu = 1)
        {
            Console.Clear();

                ItemsMenu = new List<EntityMenu>
                {
                    new Category(name: "    Add category"),
                    new Category(name: "    Return to settings"),
                    new Category(name: "    Return to main menu")
                };

                var parent = unitOfWork.Categories.GetAll().ToList().Find((x) => x.Id == IdMenu);
                BuildHierarchicalMenu(new List<EntityMenu>(unitOfWork.Categories.GetAll().ToList()), parent, 1);

            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        AddMenuItem();
                    }
                    break;
                case 1:
                    {
                        SettingsControl settingsControl = new SettingsControl();
                        settingsControl.GetMenuItems();
                    }
                    break;
                case 2:
                    {
                        MainMenuControl mainMenuControl = new MainMenuControl();
                        mainMenuControl.GetMenuItems();
                    }
                    break;
                // when selecting categories, we call the context menu of actions
                default:
                    {
                        if(ItemsMenu[id].Id!=1)
                        _ = new ContextMenuCategories(unitOfWork, ItemsMenu[id].Id);
                    }
                    break;
            }
        }

        protected override void AddMenuItem()
        {
            try
            {
                Console.WriteLine();
                int id = unitOfWork.Categories.GetAll().Max(x => x.Id) + 1;

                Console.Write(" Enter name category: ");
                string name = Console.ReadLine();

                Console.Write(" Enter name main category: ");
                string nameMainCategory = Validation.IsNameMustExist(new List<EntityMenu>(unitOfWork.Categories.GetAll().ToList()), Console.ReadLine());

                int idMainCategory = (from t in unitOfWork.Categories.GetAll()
                                                       where t.Name == nameMainCategory
                                                       select t.Id).First();

                //Add any new category
                unitOfWork.Categories.Create(new Category(id, name, idMainCategory));
                unitOfWork.SaveDataTable("Categories.json", JsonConvert.SerializeObject(unitOfWork.Categories.GetAll()));
                new CategoriesControl().GetMenuItems();
            }
            catch (Exception ex)
            { Console.WriteLine($"{ex.Message}"); }
        }
    }
}
