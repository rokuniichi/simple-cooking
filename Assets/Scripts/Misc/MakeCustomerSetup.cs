using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeCustomerSetup {
    [MenuItem("Assets/Create/Customer Setup")]
    public static void CreateMyAsset()
    {
        CustomerSetup asset = ScriptableObject.CreateInstance<CustomerSetup>();

        AssetDatabase.CreateAsset(asset, "Assets/Misc/CustomerSetup.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}