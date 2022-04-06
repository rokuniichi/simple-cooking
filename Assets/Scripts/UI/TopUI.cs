using System;
using TMPro;
using UnityEngine;

public class TopUI : MonoBehaviour
{
    public TMP_Text Timer;
    public TMP_Text CustomersNumber;

    GameController _gc;
    CustomerController _cc;
    
    public void Init()
    {
        _gc = GameController.Instance;
        _cc = CustomerController.Instance;
        _cc.CustomersRemainingChanged += OnCustomersRemainingChanged;
        OnCustomersRemainingChanged();
    }

    public void DeInit()
    {
        _cc.CustomersRemainingChanged -= OnCustomersRemainingChanged;

    }

    private void OnDestroy()
    {
        DeInit();
    }

    void LateUpdate()
    {
        var timerText = TimeSpan.FromSeconds(_gc.LevelTime).ToString("mm':'ss");
        if ( timerText != Timer.text ) {
            Timer.text = timerText;
        }
    }

    void OnCustomersRemainingChanged()
    {
        CustomersNumber.text = $"{_cc.CustomersRemaining}/{_cc.CustomersTotal}";
    }
}
