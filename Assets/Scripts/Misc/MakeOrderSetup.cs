using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeOrderSetup {
    [MenuItem("Assets/Create/Order Setup")]
    public static void CreateMyAsset()
    {
        OrderSetup asset = ScriptableObject.CreateInstance<OrderSetup>();

        AssetDatabase.CreateAsset(asset, "Assets/Misc/OrderSetup.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}