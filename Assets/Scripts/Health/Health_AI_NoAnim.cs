using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_AI_NoAnim : Health_AI
{
    protected override void Kill()
    {
        base.Kill();
        EventHandler.Death();
    }
}
