using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellSettingBase : ScriptableObject
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] AnimationTimer animationTimer;
    [Range(0.01f, 1)] public float timeBetweenUse = 0.1f;

    public void TryUseSpell(Transform spawnPos, Animator playerAnim, Vector3 targetPos, AudioSource aS)
    {
        if(audioClip)
        {
            aS.clip = audioClip;
            aS.Play();
        }
        if(animationTimer)
        {
            playerAnim.Play(animationTimer.anim_name);
        }

        UseSpell(spawnPos, animationTimer.timer, targetPos);
    }

    public void TryUseSpell(Transform spawnPos, Health target, AudioSource aS)
    {
        if (audioClip)
        {
            aS.clip = audioClip;
            aS.Play();
        }

        UseSpell(spawnPos, animationTimer.timer, target);
    }

    public void TryUseSpell(Transform spawnPos, Animator playerAnim, Health target, AudioSource aS)
    {
        if (audioClip)
        {
            aS.clip = audioClip;
            aS.Play();
        }
        if (animationTimer)
        {
            playerAnim.Play(animationTimer.anim_name);
        }

        UseSpell(spawnPos, animationTimer.timer, target);
    }

    public abstract void Initialise(GameObject objectPooler);
    protected abstract void UseSpell(Transform spawnPos, float timer, Vector3 targetPos);
    protected abstract void UseSpell(Transform spawnPos, float timer, Health target);
}
