using System;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces.Navigation
{
    public interface IValidationNavigation
    {
        Task<int> BatchExist(int batch, int countBatch);
        Task<string> NullOrEmptyText(string text);
        Task<string> WrapText(int numChar, string text, string wrapChar = "\n");
        Task<ConsoleKey> YesNoAsync();
    }
}
