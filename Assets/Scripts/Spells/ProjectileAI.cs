using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAI : MonoBehaviour
{
    Vector3 lookPos = Vector3.zero;
    [SerializeField] [Range(0.1f, 10)] float speed = 5;
    [SerializeField] [Range(1, 50)] int damage = 50;
    Health target;

    private void Update()
    {
        transform.LookAt(lookPos);
        transform.position += transform.forward * Time.deltaTime * speed;
        FindTarget();
    }

    public void Fire(Health t, Vector3 targetPos)
    {
        lookPos = targetPos;
        target = t;
    }

    private void FindTarget()
    {
       // Health t = UnitManager.instance.GetHealthPlayer();
        if (target)
        {
            Vector3 t = target.transform.position;
            t.y = transform.position.y;
            if (Vector3.Distance(transform.position, t) < 1)
            {
                target.DamageHealth(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
