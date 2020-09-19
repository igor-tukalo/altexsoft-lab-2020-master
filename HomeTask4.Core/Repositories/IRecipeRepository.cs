namespace HomeTask4.Core.Repositories
{
    public interface IRecipeRepository
    {
        string IsNameMustNotExist(string name);
    }
}