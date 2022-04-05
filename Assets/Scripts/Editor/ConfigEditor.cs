using UnityEngine;
using UnityEditor;

public class ConfigEditor : EditorWindow
{
    string _customersNumber;
    string _ordersNumber;
    string _levelTime;

    ConfigHandler _configLoader;
    MainConfig    _config;

    [MenuItem("Tools/Config Editor")]
    static void ShowWindow()
    {
       
        var window = (ConfigEditor)GetWindow(typeof(ConfigEditor));
        window.Init();
        window.Show();
    }

    void Init()
    {
        _configLoader = new ConfigHandler(); 
        _config = _configLoader.Load<MainConfig>(ConfigPaths.MAIN_CONFIG_PATH);
        _customersNumber = _config.CustomersNumber.ToString();
        _ordersNumber = _config.OrdersNumber.ToString();
        _levelTime = _config.LevelTime.ToString();
    }
    
    void OnGUI() {
        GUILayout.Label("Customers number");
        _customersNumber = GUILayout.TextArea(_customersNumber);
        GUILayout.Label("Orders number");
        _ordersNumber = GUILayout.TextArea(_ordersNumber);
        GUILayout.Label("Level time");
        _levelTime = GUILayout.TextArea(_levelTime);
        if ( GUILayout.Button("Save") )
        {
            _config.CustomersNumber = int.Parse(_customersNumber);
            _config.OrdersNumber = int.Parse(_ordersNumber);
            _config.LevelTime = float.Parse(_levelTime);
            _configLoader.Save(ConfigPaths.MAIN_CONFIG_PATH, _config);
        }
    }
}