using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using TMPro;
using System.Collections.Generic;
public class Scene04Manager : MonoBehaviour
{
    [SerializeField] bool isHeart1IsCollect = false, isHeart2IsCollect = false, isHeart3IsCollect = false;
    int countHeartCollect = 0;

    // Gameobject
    [Header("=== Game Object ===")]
    [SerializeField] List<GameObject> shardOfHeart;

    // HUD
    [Header("=== HUD ===")]
    [SerializeField] TMP_Text textAlam;
    [SerializeField] Animator animatorTextAlam;

    // Dialog

    [Header("===Dialog===")]
    [SerializeField] List<string> heartsWordsCollect;


    // Audio
    [Header("===Audio===")]
    [SerializeField] AudioSource audioSourceAlam;


    public static Scene04Manager Instance;

    void Start()
    {
        Instance = this;
    }

    public void UpdateTextDialog(string words, float duration = 5f)
    {
        textAlam.text = "";
        animatorTextAlam.Play("TextEvent_play_without_end", 0, 0);
        audioSourceAlam.Stop();
        audioSourceAlam.Play();

        StartCoroutine(DoType(duration / words.Length, words, () =>
        {

            audioSourceAlam.Stop();

            float t = 0;
            while (t < 10.5f)
            {
                t += Time.deltaTime;
            }

            animatorTextAlam.Play("TextEvent_end", 0, 0);
        }));
    }

    public bool CheckIsCollectAllHeart()
    {
        bool isCompleted = isHeart1IsCollect && isHeart1IsCollect && isHeart1IsCollect;
        return isCompleted;
    }

    IEnumerator DoType(float delay, string words, Action OnClear)
    {

        foreach (char c in words)
        {
            textAlam.text += c;
            yield return new WaitForSecondsRealtime(delay);
        }

        OnClear?.Invoke();
    }

    public void CollectHeart(int number)
    {
        UpdateTextDialog(heartsWordsCollect[countHeartCollect], 6f);
        Destroy(shardOfHeart[number - 1]);
        countHeartCollect++;

        if (number == 1)
        {
            isHeart1IsCollect = true;
        }
        if (number == 2)
        {
            isHeart2IsCollect = true;
        }
        if (number == 3)
        {
            isHeart3IsCollect = true;
        }
    }
}
