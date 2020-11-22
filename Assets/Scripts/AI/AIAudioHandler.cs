using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="tpopl001/AI/AI Audio Handler")]
public class AIAudioHandler : ScriptableObject
{
    [SerializeField] AudioClip onDeath;
    [SerializeField] AudioClip onAttack;
    [SerializeField] AudioClip onDamaged;

    public void PlayDeathAudio(AudioSource audioSource)
    {
        PlayAudio(onDeath, audioSource);
    }

    public void PlayAttackAudio(AudioSource audioSource)
    {
        PlayAudio(onAttack, audioSource);
    }

    public void PlayDamagedAudio(AudioSource audioSource)
    {
        PlayAudio(onDamaged, audioSource);
    }

    private void PlayAudio(AudioClip clip, AudioSource audioSource)
    {
        if(audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
