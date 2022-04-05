using UnityEngine;
using UnityEditor;

public class ConfigUtility : EditorWindow
{
    MainConfigManager _mainConfigManager;

    string _customersNumber;
    string _ordersNumber;
    string _levelTime;

    [MenuItem("Tools/Config Utility")]
    static void ShowWindow()
    {
       
        var window = (ConfigUtility)GetWindow(typeof(ConfigUtility));
        window.Init();
        window.Show();
    }

    void Init()
    {
        _mainConfigManager = new MainConfigManager();
        var config = _mainConfigManager.MainConfig;
        _customersNumber = config.CustomersNumber.ToString();
        _ordersNumber = config.OrdersNumber.ToString();
        _levelTime = config.LevelTime.ToString();
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
            var config = _mainConfigManager.MainConfig;
            config.CustomersNumber = int.Parse(_customersNumber);
            config.OrdersNumber = int.Parse(_ordersNumber);
            config.LevelTime = float.Parse(_levelTime);
            _mainConfigManager.Save();
        }
    }
}