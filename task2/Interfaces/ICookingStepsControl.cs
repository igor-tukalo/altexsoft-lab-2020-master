using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface ICookingStepsControl
    {
        int IdRecipe { get; set; }
        void Add(int recipeId);
        void Delete(int idCookingStep, int idRecipe);
        void Edit(int idCookingStep);
        List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe);
    }
}