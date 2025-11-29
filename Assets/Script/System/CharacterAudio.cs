using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource, audioSourceInteract, audioSourceSelect;
    public enum State { idle, walk, run }
    State state = State.idle;



    void Start()
    {
        audioSource.loop = true;
        audioSource.mute = true;
        InvokeRepeating("ChangePitch", 0, 0.5f);
    }

    void ChangePitch()
    {
        if (state == State.idle) return;

        switch (state)
        {
            case State.idle:
                audioSource.pitch = Random.Range(0.8f, 0.9f);
                break;
            case State.run:
                audioSource.pitch = Random.Range(0.95f, 1.1f);
                break;
        }

    }

    public void Walk()
    {
        if (state == State.walk) return;
        audioSource.pitch = 0.9f;
        audioSource.volume = 0.85f;
        audioSource.mute = false;
        state = State.walk;
    }

    public void Run()
    {
        if (state == State.run) return;
        audioSource.pitch = 1f;
        audioSource.volume = 1f;
        audioSource.mute = false;
        state = State.run;
    }

    public void Idle()
    {
        if (state == State.idle) return;
        audioSource.mute = true;
        state = State.idle;
    }

    public void PlayInteractSound()
    {
         audioSourceInteract.Stop();
        audioSourceInteract.pitch = Random.Range(0.9f, 1.1f);
        audioSourceInteract.Play();
    }

    public void PlaySelectSound()
    {
        audioSourceSelect.Stop();
        audioSourceSelect.pitch = Random.Range(0.9f, 1.1f);
        audioSourceSelect.Play();
    }
}
