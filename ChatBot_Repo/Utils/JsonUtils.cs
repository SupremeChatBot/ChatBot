using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Utils
{
    public static class JsonUtils
    {
        public static void AppendToJson<T>(T obj, string filePath)
        {
            var file = File.ReadAllText(filePath);
            var list = JsonConvert.DeserializeObject<List<T>>(file);
            list.Add(obj);
            var jsonData = JsonConvert.SerializeObject(list);
            File.WriteAllText(filePath, jsonData);
        }
        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static List<T> DeserializeJsonList<T>(string filePath)
        {
            var file = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(file);
        }

    }
}
