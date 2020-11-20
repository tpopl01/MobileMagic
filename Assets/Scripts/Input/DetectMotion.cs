using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectMotion : ScriptableObject
{
    [SerializeField] protected float timeThreshold = 0.3f;

    public abstract void Init();
    public virtual int MotionDetected(Vector2 began, Vector2 end, Vector2 current, float duration)
    {
        if (duration > timeThreshold) return -1;

        return MotionDetection(began, end, current);
    }

    protected abstract int MotionDetection(Vector2 began, Vector2 end, Vector2 current);

}
