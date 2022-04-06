using System;
using TMPro;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public TMP_Text BoosterText;

    CustomerController _cc;
    
    public void Init()
    {
        _cc = CustomerController.Instance;
        UpdateText();
    }
    
    public void UseBooster()
    {
        CustomerController.Instance.UseBooster();
        UpdateText();
    }

    void UpdateText()
    {
        BoosterText.text = String.Format("Booster ({0})", _cc.BoostersRemaining);
    }
}
