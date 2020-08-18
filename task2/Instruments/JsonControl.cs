using System;
using System.IO;
using System.Text.RegularExpressions;

namespace task2.Instruments
{
    public class JsonControl
    {
        public string JsonFileName { get; set; }
        public JsonControl(string jsonFileName)
        {
            JsonFileName = jsonFileName;
        }

        /// <summary>
        /// Get json file data at specified path
        /// </summary>
        /// <returns>json data</returns>
        public string GetJsonData()
        {
            try
            {
                // Read existing json data
                string jsonData = File.ReadAllText(GetJsonPathFile());
                if (!string.IsNullOrWhiteSpace(Regex.Replace(jsonData, "[{}]", "")))
                    return jsonData;
                else return string.Empty;
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"{ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Get path to json file by specified filename
        /// </summary>
        /// <returns>path to json file</returns>
        public string GetJsonPathFile()
        {
            try
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
                return Path.Combine(exePath, $"json\\{JsonFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return string.Empty;
            }
        }
    }
}
