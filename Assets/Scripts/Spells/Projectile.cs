using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 LookPos { get; set; } = Vector3.zero;
    [SerializeField][Range(0.1f, 10)] float speed = 5;

    private void Update()
    {
        transform.LookAt(LookPos);
        transform.position += transform.forward * Time.deltaTime * speed;
        FindAITarget();
    }

    private void FindAITarget()
    {
        Health t = UnitManager.instance.GetClosestHealthAI(transform.position);
        if (t)
        {
            Vector3 target = t.transform.position;
            target.y = transform.position.y;
            if (Vector3.Distance(transform.position, target) < 1)
            {
                t.DamageHealth(50);
                gameObject.SetActive(false);
            }
        }
    }
}
