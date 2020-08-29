using System;
using System.Configuration;
using task2.Interfaces;

namespace task2.Controls
{
    class SettingsControl : ISettingsControl
    {
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
            config.AppSettings.Settings["Batch"].Value = Validation.ValidNumber(Console.ReadLine()).ToString();
            //save to apply changes
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
