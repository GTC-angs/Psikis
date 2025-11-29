using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource, audioSourceScream, audioSourceSlash;
    [SerializeField] List<AudioClip> audioClips;

    public static EnemyAudio Instance;

    void Start()
    {
        Instance = this;
    }

    public void PlaySound(int clipI, bool isChancePitch, bool loop = false)
    {
        if (isChancePitch) audioSource.pitch = Random.Range(0.9f, 1.1f);

        audioSource.Stop();
        audioSource.loop = loop;
        if (clipI >= 0 && clipI < audioClips.Count)
        {
            audioSource.clip = audioClips[clipI];
            audioSource.Play();
        }

        else return;

    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PlayScreamSound()
    {
        audioSourceScream.Stop();
        audioSourceScream.Play();
    }

    public void PlaySlashSound()
    {
        audioSourceSlash.Stop();
        audioSourceSlash.Play();
    }
}
