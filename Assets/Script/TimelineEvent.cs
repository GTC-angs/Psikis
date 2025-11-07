using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineEvent : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public bool isAudioLoop = false;
    public void PlayAudio(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void setIsAudioLoop(bool newStateBool)
    {
        isAudioLoop = newStateBool;
    }

    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }

    public void LoadSceneAddictive(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    
    public void StopAudio()
    {
        audioSource.Stop();
    }
}
