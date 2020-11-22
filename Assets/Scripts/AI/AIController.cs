using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class AIController : MonoBehaviour
{
    protected Health target;
    [SerializeField] protected AISettings aISettings;
    protected Timer attackTimer = new Timer(0);
    protected Health h;
    ObjectPool_AI pool;
    protected Timer respawnTimer = new Timer(2);
    bool respawning = false;

    public void SetPool(ObjectPool_AI p)
    {
        pool = p;
    }

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        h = GetComponentInChildren<Health>();
        h.Init();
        h.InitHealth(aISettings.maxHealth);
        attackTimer.Duration = aISettings.timeBetweenAttack;
        target = UnitManager.instance.GetHealthPlayer();
    }

    private void Update()
    {
        if (h.IsDead)
        {
            if (respawning)
            {
                if (respawnTimer.GetComplete())
                {
                    respawning = false;
                    pool.Return(this);
                }
            }
            else { respawning = true; respawnTimer.StartTimer(); }
            return;
        }
        if (!h.CanMove) return;
        if (target == null)
        {
            target = UnitManager.instance.GetHealthPlayer();
        }

        if(Vector3.Distance(transform.position, target.transform.position) < aISettings.stoppingDist)
        {
            LookAtTarget();
            Stop();
            //ATTACK
            if (attackTimer.GetComplete())
            {
                attackTimer.StartTimer();
                Attack();
            }
        }
        else
        {
            LookAtTarget();
            MoveForward();
        }
    }

    protected virtual void Attack()
    {
        Health_AI health_AI = (Health_AI)h;
        health_AI.audioHandler.PlayAttackAudio(SoundManager.instance.GetAudioSource());
    }

    protected virtual void Stop()
    {
    }

    protected virtual void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * aISettings.moveSpeed;
    }

    void LookAtTarget()
    {
        Quaternion look = StaticMaths.GetLookRotation(target.transform.position, transform.position, transform.forward, out bool rotate, 1);
        if(rotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * aISettings.rotSpeed);
        }
    }

    public void Respawn()
    {
        if (!h) Init();
        h.InitHealth(aISettings.maxHealth);
        target = UnitManager.instance.GetHealthPlayer();
    }
}
