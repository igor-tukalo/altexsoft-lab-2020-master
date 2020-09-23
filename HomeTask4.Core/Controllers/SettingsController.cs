using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Configuration;

namespace HomeTask4.Core.Controllers
{
    public class SettingsController : BaseController, ISettingsControl
    {
        public SettingsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Сhange the maximum number of lines for a page
        /// </summary>
        public void EditBatch()
        {
            Console.WriteLine($"  Current number of navigation menu lines: {ConfigurationManager.AppSettings.Get("Batch")}");
            Console.Write("  Enter the number of navigation menu lines: ");
            //Create the object
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //make changes
            config.AppSettings.Settings["Batch"].Value = ValidManager.ValidNumber(Console.ReadLine()).ToString();
            //save to apply changes
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
