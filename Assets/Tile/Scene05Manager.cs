using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class Scene05Manager : MonoBehaviour
{

    public static Scene05Manager Instance;
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
