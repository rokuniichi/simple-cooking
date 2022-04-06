using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopUI : MonoBehaviour
{
    public TMP_Text Timer;
    public TMP_Text CustomersNumber;

    GameController _gc;
    CustomerController _cc;
    
    // Start is called before the first frame update
    void Start()
    {
        _gc = GameController.Instance;
        _cc = CustomerController.Instance;
        _cc.CustomersRemainingChanged += OnCustomersRemainingChanged;
        OnCustomersRemainingChanged();
    }

    private void OnDestroy()
    {
        _cc.CustomersRemainingChanged -= OnCustomersRemainingChanged;
    }

    // Update is called once per frame
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
