using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tpopl001/Input/Detect Motion/Swipe")]
public class DetectMotion_Swipe : DetectMotion
{
    [SerializeField] protected float swipeThreshold = 40f;


    public override void Init()
    {
        
    }

    protected override int MotionDetection(Vector2 began, Vector2 end, Vector2 current)
    {
        if (end == Vector2.zero)
            return -1;

        var dirVector = end - began;
        if (dirVector.magnitude < swipeThreshold) return -1;

        float direction = Vector2.SignedAngle(Vector2.up, dirVector) + 180;

        if (direction >= 45 && direction < 135)
        {
        //    Debug.Log("Swipe Left");
            
            return 0;
        }
        else if (direction >= 135 && direction < 225)
        {
      //      Debug.Log("Swipe Down");
            return 1;
        }
        else if (direction >= 225 && direction < 315)
        {
      //      Debug.Log("Swipe Right");
            return 2;
        }
        else if (direction >= 315 && direction < 360 || direction >= 0 && direction < 45)
        {
        //    Debug.Log("Swipe Up");
            return 3;
        }

        return -1;
    }

}
