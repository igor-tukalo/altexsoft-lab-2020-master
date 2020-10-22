using System;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces.Navigation
{
    public interface IConsoleHelper
    {
        Task<int> BatchExistAsync(int batch, int countBatch);
        Task<string> CheckNullOrEmptyTextAsync(string text);
        Task<string> WrapTextAsync(int numChar, string text, string wrapChar = "\n");
        Task<ConsoleKey> ShowYesNoAsync();
    }
}
