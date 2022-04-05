using System.IO;
using Newtonsoft.Json;

public class ConfigHandler
{
    public T Load<T>(string path) where T : IConfig, new()
    {
        if (!File.Exists(path))
        {
            var config = new T();
            Save(path, config);
            return config;
        }

        using (StreamReader streamReader = new StreamReader(path))
        {
            var json = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
    
    public void Save<T>(string path, T config) where T : IConfig
    {
        using (StreamWriter streamWriter = new StreamWriter(path, false))
        {
            streamWriter.Write(JsonConvert.SerializeObject(config));
        }
    }
   
}
