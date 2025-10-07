using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogSO", menuName = "Scriptable Objects/DialogSO")]
public class DialogSO : ScriptableObject
{
    public enum ExpresionS // expresi dalam dialog
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
    public List<string> word_ID, word_ENG; // word in indonesia or engligh
    public TypeDialogs typeDialog; // character / world
    public bool isShowExpresionText = false; // show text fade up about expresion

    [Header("==Sound==")]
    [Space(10)]
    public AudioClip audioVoiceOffer_ID;
    public AudioClip audioVoiceOffer_ENG;
    public bool isLoop = true;
    public float volume = 1f;
    public float pitch = 1f; // pithc suara


    [Header("==Character==")]
    [Space(10)]
    // character
    public Texture2D imageActor; // texture Ui character
    public string nameActor; // nama aktor
    public ExpresionS expresionActor; // ekspresi
    public float widthTextureCharacter, heightTextureCharacter; // set width and heigh texture UI
    public PositionCharacterTexture positionTextureCharacter; // Left or Right


    [Header("==World==")]
    [Space(10)]
    public Vector3 positionTexture; // position texture
    public bool isIncludeTexture = false, isIcludeAmbientSound = false;
    public Texture2D imageWorld; // texture
    public float widthTexture, heightTexture; // set width dan height texture
    
    public AudioClip ambientAudio; 


    [Header("==Actions==")]
    [Space(10)]
    public UnityEvent customEventDialog; // jika Quest selesai

}
