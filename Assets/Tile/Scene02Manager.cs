using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;




[System.Serializable]
public class Pasient
{
    public string name;
    public int umur;
    public enum Sex { LakiLaki, Perempuan }

    public Sex gender;
    public List<string> Synthomps;
}
public class Scene02Manager : MonoBehaviour
{

    public static Scene02Manager Instance;
    [SerializeField] List<AudioSource> envAudioSource;

    void Start()
    {
        Instance = this;
    }

    public void StartWork()
    {
        PlayerMovement.Instance.isCanMoveInput = false;
        Scene02HUDManager.Instance.Showpasient_UI();
    }


    public void EndWork()
    {
        PlayerMovement.Instance.isCanMoveInput = true;
    }





}
