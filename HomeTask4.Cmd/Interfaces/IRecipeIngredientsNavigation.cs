using HomeTask4.Cmd.Interfaces.ContextMenuNavigation;
using HomeTask4.Cmd.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Interfaces
{
    public interface IAmountRecipeIngredientsNavigation : IBaseContextMenuNavigation
    {
        Task<List<EntityMenu>> GetAmountIngredientsRecipeAsync(int recipeId);
    }
}
