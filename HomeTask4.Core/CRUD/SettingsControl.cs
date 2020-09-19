using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Repositories;
using System;
using System.Configuration;

namespace HomeTask4.Core.CRUD
{
    public class SettingsControl : BaseControl, ISettingsControl
    {
        public SettingsControl(IUnitOfWork unitOfWork) : base(unitOfWork)
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
