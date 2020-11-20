using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Circle Clockwise", menuName = "tpopl001/Input/Detect Motion/Circle Clockwise")]
public class DetectMotion_Circle : DetectMotion
{
    Vector2[] pos = new Vector2[4];

    public override void Init()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = Vector2.zero;
        }
    }

    //bottom left is 0,0
    protected override int MotionDetection(Vector2 began, Vector2 end, Vector2 current)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if(pos[i] == Vector2.zero)
            {
                if (i == 0) //top
                {
                    pos[i] = began;
                    break;
                }
                else if(i==1 && pos[0].x + 0.1f < current.x && pos[0].y - 0.1f > current.y) //right
                {
                    pos[i] = current;
                    break;
                }
                else if (i == 2 && pos[1].x - 0.1f > current.x && pos[1].y - 0.1f > current.y) //down
                {
                    pos[i] = current;
                    break;
                }
                else if (i == 3 && pos[0].x - 0.1f > current.x && pos[0].y - 0.1f > current.y && pos[2].x - 0.1f > current.x && pos[2].y + 0.1f < current.y) //left
                {
                    pos[i] = current;
                    return 4;
                }

            }
        }

        return -1;
    }
}
