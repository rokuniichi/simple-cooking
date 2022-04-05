using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopUI : MonoBehaviour
{
    public TMP_Text Timer;
    public TMP_Text CustomersNumber;

    GameController gc;
    
    // Start is called before the first frame update
    void Start()
    {
        gc = GameController.Instance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var timerText = TimeSpan.FromSeconds(gc.LevelTime).ToString("mm':'ss");
        if ( timerText != Timer.text ) {
            Timer.text = timerText;
        }
    }
}
