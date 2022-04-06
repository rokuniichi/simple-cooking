using UnityEngine;
using UnityEditor;

public class ConfigEditor : EditorWindow
{
    string _customersTotal;
    string _ordersTotal;
    string _ordersPerCustomer;
    string _levelTime;
    string _boostersNumber;

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
        
        _customersTotal    = _config.CustomersTotal.ToString();
        _ordersTotal       = _config.OrdersTotal.ToString();
        _ordersPerCustomer = _config.OrdersPerCustomer.ToString();
        _levelTime         = _config.LevelTime.ToString();
        _boostersNumber    = _config.BoostersNumber.ToString();
    }
    
    void OnGUI() {
        GUILayout.Label("Customers total");
        _customersTotal = GUILayout.TextArea(_customersTotal);
        GUILayout.Label("Orders total");
        _ordersTotal = GUILayout.TextArea(_ordersTotal);
        GUILayout.Label("Orders per customer");
        _ordersPerCustomer = GUILayout.TextArea(_ordersPerCustomer);
        GUILayout.Label("Level time");
        _levelTime = GUILayout.TextArea(_levelTime);
        GUILayout.Label("Boosters number");
        _boostersNumber = GUILayout.TextArea(_boostersNumber);
        if ( GUILayout.Button("Save") )
        {
            _config.CustomersTotal = int.Parse(_customersTotal);
            _config.OrdersTotal = int.Parse(_ordersTotal);
            _config.OrdersPerCustomer = int.Parse(_ordersPerCustomer);
            _config.LevelTime = float.Parse(_levelTime);
            _config.BoostersNumber = int.Parse(_boostersNumber);
            _configLoader.Save(ConfigPaths.MAIN_CONFIG_PATH, _config);
        }
    }
}