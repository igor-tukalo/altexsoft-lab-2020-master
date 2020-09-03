using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace task2.Repositories
{
    class JsonManager
    {
        public void JsonSerializer<T>(List<T> items) where T : class
        {
            File.WriteAllText(GetJsonPathFile(typeof(T).Name + ".json"), JsonConvert.SerializeObject(items));
        }

        public List<T> JsonDeSerializer<T>() where T : class
        {
            return JsonConvert.DeserializeObject<List<T>>(GetJsonData(typeof(T).Name + ".json"));
        }

        /// <summary>
        /// Get json file data at specified path
        /// </summary>
        /// <returns>json data</returns>
        string GetJsonData(string jsonFileName)
        {
            try
            {
                // Read existing json data
                string jsonData = File.ReadAllText(GetJsonPathFile(jsonFileName));
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
        string GetJsonPathFile(string jsonFileName)
        {
            try
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
                return Path.Combine(exePath, $"json\\{jsonFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return string.Empty;
            }
        }
    }
}
