using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopUI : MonoBehaviour
{
    public TMP_Text Timer;
    public TMP_Text CustomersNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var timerText = TimeSpan.FromSeconds(GameController.Instance.LevelTime).ToString("mm':'ss");
        if ( timerText != Timer.text ) {
            Timer.text = timerText;
        }
    }
}
