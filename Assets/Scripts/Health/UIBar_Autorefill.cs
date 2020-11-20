using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar_Autorefill : UIBar
{
    float refillSpeed = 1;

    private void Start()
    {
        Init(1);
        UpdateUI(0);
    }

    public bool IsFull()
    {
        return current < 0.05f;//>= max - 0.1f;
    }

    public void SetRefillSpeed(float speed)
    {
        refillSpeed = speed;
    }

    private void Update()
    {
        if (!IsFull())
            UpdateUI(current - Time.deltaTime * refillSpeed);
    }

}
