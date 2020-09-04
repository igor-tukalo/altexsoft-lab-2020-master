using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface ICookingStepsControl : IBaseControl
    {
        int IdRecipe { get; set; }
        List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe);
    }
}