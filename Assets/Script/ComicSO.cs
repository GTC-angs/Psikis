using UnityEngine;

[CreateAssetMenu(fileName = "ComicSO", menuName = "Scriptable Objects/ComicSO")]
public class ComicSO : ScriptableObject
{
    public Texture2D texture;
    public AudioClip audioClip;
    public bool isPlayAudio = true;
    public bool isAudioLoop = false;
}
