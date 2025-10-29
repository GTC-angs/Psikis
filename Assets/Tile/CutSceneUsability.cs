using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class CutSceneUsability : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] List<TMP_Text> textDisplays;
    [SerializeField] List<string> textStory = new List<string>();
    private AudioSource audioSource;

    public void PlayAudioClip(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }

    public void SetLoop(bool isLoop)
    {
        audioSource.loop = isLoop;
    }   

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }


    public void SetText(int index, string text)
    {
        textDisplays[index].text = text;
    }
    
    public IEnumerator TypingEffect(int textIndex, float delay)
    {
        string fullText = textStory[textIndex];
        textDisplays[textIndex].text = "";
        foreach (char c in fullText)
        {
            textDisplays[textIndex].text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}
