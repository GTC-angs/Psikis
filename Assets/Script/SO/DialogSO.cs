using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogSO", menuName = "Scriptable Objects/DialogSO")]
public class DialogSO : ScriptableObject
{
    public enum ExpresionS
    {
        Happy, Sad, Confuse, Angry, Depresed
    };

    public enum TypeDialogs
    {
        Character, World
    };

    public enum PositionCharacterTexture
    {
        LEFT, RIGHT
    };


    [Header("==Basic==")]
    [Space(10)]
    public int id;
    public List<string> word_ID, word_ENG;
    public TypeDialogs typeDialog; // character / world

    [Header("==Sound==")]
    [Space(10)]
    public AudioClip audioVoiceOffer_ID;
    public AudioClip audioVoiceOffer_ENG;
    public bool isLoop = true;
    public float volume = 1f;
    public float pitch = 1f;


    [Header("==Character==")]
    [Space(10)]
    // character
    public Texture2D imageActor;
    public string nameActor;
    public ExpresionS expresionActor;
    public float widthTextureCharacter, heightTextureCharacter;
    public PositionCharacterTexture positionTextureCharacter;


    [Header("==World==")]
    [Space(10)]
    public Vector3 positionTexture;
    public bool isIncludeTexture = false, isIcludeAmbientSound = false;
    public Texture2D imageWorld;
    public float widthTexture, heightTexture;
    
    public AudioClip ambientAudio;


    [Header("==Actions==")]
    [Space(10)]
    public UnityEvent customEventDialog;

}
