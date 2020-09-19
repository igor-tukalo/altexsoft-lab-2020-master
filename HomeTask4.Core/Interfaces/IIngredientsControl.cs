using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces
{
    public interface IIngredientsControl : IBaseControl
    {
        List<EntityMenu> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1);
    }
}
