using System.IO;
using Newtonsoft.Json;

public class MainConfigManager
{
    public const string MainConfigPath = "Assets/Configs/mainconfig.json";

    public MainConfig MainConfig = new MainConfig();
    
    public MainConfigManager()
    {
        Load();
    }

    void Load()
    {
        if (!File.Exists(MainConfigPath))
        {
            Save();
            return;
        }
        using (StreamReader streamReader = new StreamReader(MainConfigPath))
        {
            var json = streamReader.ReadToEnd();
            MainConfig = JsonConvert.DeserializeObject<MainConfig>(json);
        }
    }

    /// <summary>
    /// For editor utility purposes only
    /// </summary>
    public void Save()
    {
        using (StreamWriter streamWriter = new StreamWriter(MainConfigPath, false))
        {
            streamWriter.Write(JsonConvert.SerializeObject(MainConfig));
        }
    }
}
