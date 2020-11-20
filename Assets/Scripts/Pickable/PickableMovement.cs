using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class PickableMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.2f;
    [SerializeField] float rotSpeed = 50;
    Transform t;
    [SerializeField] Vector3 upperLimit = Vector3.up;
    [SerializeField] Vector3 lowerLimit = Vector3.zero;
   // [SerializeField] Vector3 upperRotLimit = Vector3.right;
    //[SerializeField] Vector3 lowerRotLimit = Vector3.left;
    [SerializeField] float changeDirTimer = 3;
    Timer timer = new Timer(0);
    bool up = true;

    private void Start()
    {
        t = transform.GetChild(0);
        timer.Duration = changeDirTimer;
        timer.StartTimer();
    }

    void Update()
    {
        if (up)
        {
            t.localPosition = Vector3.Lerp(t.localPosition, upperLimit, Time.deltaTime * moveSpeed);
          //  t.localRotation = Quaternion.Slerp(t.localRotation, Quaternion.Euler(upperRotLimit), Time.deltaTime* rotSpeed);
        }
        else
        {
            t.localPosition = Vector3.Lerp(t.localPosition, lowerLimit, Time.deltaTime * moveSpeed);
            //  t.localRotation = Quaternion.Slerp(t.localRotation, Quaternion.Euler(lowerRotLimit), Time.deltaTime * rotSpeed);
        }
        t.Rotate(Vector3.up * Time.deltaTime * rotSpeed);

        if (timer.GetComplete())
        {
            up = !up;
            timer.StartTimer();
        }
    }
}
