using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Dialog
{
    public int id;
    public string actor;
    public string words;
    public bool isNext = true;
}
public class Scene04Manager : MonoBehaviour
{
    [SerializeField] bool isHeart1IsCollect = false, isHeart2IsCollect = false, isHeart3IsCollect = false;
    int countHeartCollect = 0;

    // Gameobject
    [Header("=== Game Object ===")]
    [SerializeField] List<GameObject> shardOfHeart;

    // HUD
    [Header("=== HUD ===")]
    [SerializeField] TMP_Text textAlam, textName;

    [SerializeField] Animator animatorTextAlam;

    // Dialog

    [Header("===Dialog===")]
    [SerializeField] List<Dialog> heartsWordsCollect1, heartsWordsCollect2, heartsWordsCollect3, wordsAtStart, wordsAtLabyrinth;
    [SerializeField] Texture2D cutsceneTex, cutsceneTex2, cutsceneTex3;
    [SerializeField] RawImage cutsceneImage;
    [SerializeField] CanvasGroup canvasGroupCutscene, canvasGroupBlackStart;
    [SerializeField] List<AudioClip> audioClipBGSound;


    // Audio
    [Header("===Audio===")]
    [SerializeField] AudioSource audioSourceAlam, audioBGSound, collectSound;

    public static Scene04Manager Instance;

    void Start()
    {
        Instance = this;

        canvasGroupBlackStart.alpha = 1;
        StartCoroutine(WaitAndDo(2f, (() =>
        {
            canvasGroupBlackStart.DOFade(0f, 1f);
        })));

        canvasGroupCutscene.alpha = 0;


        StartCoroutine(WaitAndDo(3f, () =>
                {
                    UpdateTextDialog(wordsAtStart[0], 3f, wordsAtStart);
                }));
    }

    IEnumerator WaitAndDo(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action?.Invoke();
    }

    public void UpdateTextDialog(Dialog dialog, float duration = 5f, List<Dialog> refNext = null, Action OnComplete = null)
    {
        textAlam.text = "";
        animatorTextAlam.Play("TextEvent_play_without_end", 0, 0);
        audioSourceAlam.Stop();
        audioSourceAlam.Play();

        StartCoroutine(DoType(duration / dialog.words.Length, dialog.words, dialog.actor, () =>
        {

            audioSourceAlam.Stop();

            if (dialog.isNext)
            {
                // safety: check that the next index exists and refNext is not null
                if (refNext == null)
                {
                    Debug.LogWarning("UpdateTextDialog: refNext is null while dialog.isNext is true — stopping dialog sequence.");
                }
                else
                {
                    int nextIndex = dialog.id + 1;
                    if (nextIndex >= 0 && nextIndex < refNext.Count)
                    {
                        UpdateTextDialog(refNext[nextIndex], duration, refNext, OnComplete);
                    }
                    else
                    {
                        Debug.LogWarning($"UpdateTextDialog: nextIndex {nextIndex} out of bounds for refNext (count={refNext.Count}). Stopping sequence.");
                    }
                }
            }
            else
            {
                float t = 0;
                while (t < 10.5f)
                {
                    t += Time.deltaTime;
                }

                OnComplete?.Invoke();
                animatorTextAlam.Play("TextEvent_end", 0, 0);
            }
        }));
    }

    public bool CheckIsCollectAllHeart()
    {
        // bugfix: make sure we check each flag separately
        bool isCompleted = isHeart1IsCollect && isHeart2IsCollect && isHeart3IsCollect;
        return isCompleted;
    }

    IEnumerator DoType(float delay, string words, string actorName, Action OnClear)
    {
        // chnaging name 
        textName.text = actorName;
        foreach (char c in words)
        {

            if (c == '|')
            {
                audioSourceAlam.Stop();
                yield return new WaitForSecondsRealtime(3f);
                textAlam.text = "";
                audioSourceAlam.Play();
                continue;
            }

            textAlam.text += c;

            if (c == '.' && textAlam.text[textAlam.text.Length - 1] != '.')
            {
                yield return new WaitForSecondsRealtime(0.6f);
            }

            yield return new WaitForSecondsRealtime(delay);
        }

        yield return new WaitForSecondsRealtime(1f);
        OnClear?.Invoke();
    }

    public void StartLabyrinthDialog()
    {
        UpdateTextDialog(wordsAtLabyrinth[0], 4f, wordsAtLabyrinth);
    }


    public void CollectHeart(int number)
    {
        Destroy(shardOfHeart[number - 1]);
        canvasGroupCutscene.DOFade(1f, 1f);
        PlayerMovement.Instance.isCanMoveInput = false;

        textAlam.color = Color.black;
        textName.color = Color.black;

        collectSound.Stop();
        collectSound.Play();

        audioBGSound.volume = 1f;

        audioBGSound.Stop();
        audioBGSound.clip = GetAudioClipBGSoundByCurrentCollectHeart();
        audioBGSound.Play();

        if (number == 1)
        {
            isHeart1IsCollect = true;
            cutsceneImage.texture = GetTextureCutsceneByCurrentCollectHeart();

            StartCoroutine(WaitAndDo(1f, () =>
            {
                canvasGroupCutscene.DOFade(1f, 1f);
            }));

            List<Dialog> DialogWord = GetListDialogByCurrentCollectHeart();
            UpdateTextDialog(DialogWord[0], 6f, DialogWord, () =>
            {
                canvasGroupCutscene.DOFade(0f, 1f).OnComplete(() =>
                {
                    if (isHeart1IsCollect && isHeart2IsCollect && isHeart3IsCollect)
                    {
                        audioBGSound.Stop();
                        SceneManager.LoadScene("Cutscene_heartcombine");
                    }

                    audioBGSound.volume = 1f;
                    PlayerMovement.Instance.isCanMoveInput = true;
                });
            });

        }
        if (number == 2)
        {
            isHeart2IsCollect = true;
            cutsceneImage.texture = GetTextureCutsceneByCurrentCollectHeart();

            StartCoroutine(WaitAndDo(1f, () =>
           {
               canvasGroupCutscene.DOFade(1f, 1f);
           }));

            List<Dialog> DialogWord = GetListDialogByCurrentCollectHeart();
            UpdateTextDialog(DialogWord[0], 6f, DialogWord, () =>
            {
                canvasGroupCutscene.DOFade(0f, 1f).OnComplete(() =>
                {
                    if (isHeart1IsCollect && isHeart2IsCollect && isHeart3IsCollect)
                    {
                        audioBGSound.Stop();
                        SceneManager.LoadScene("Cutscene_heartcombine");
                    }

                    audioBGSound.volume = 1f;
                    PlayerMovement.Instance.isCanMoveInput = true;
                });
            });

        }
        if (number == 3)
        {
            isHeart3IsCollect = true;
            cutsceneImage.texture = GetTextureCutsceneByCurrentCollectHeart();

            StartCoroutine(WaitAndDo(1f, () =>
           {
               audioBGSound.volume = 1f;
               canvasGroupCutscene.DOFade(1f, 1f);
           }));

            List<Dialog> DialogWord = GetListDialogByCurrentCollectHeart();
            UpdateTextDialog(DialogWord[0], 6f, DialogWord, () =>
            {
                canvasGroupCutscene.DOFade(0f, 1f).OnComplete(() =>
                {

                    if (isHeart1IsCollect && isHeart2IsCollect && isHeart3IsCollect)
                    {
                        audioBGSound.Stop();
                        SceneManager.LoadScene("Cutscene_heartcombine");
                    }

                    audioBGSound.volume = 1f;
                    PlayerMovement.Instance.isCanMoveInput = true;
                });


            });
        }

        countHeartCollect++;
    }

    List<Dialog> GetListDialogByCurrentCollectHeart()
    {
        if (countHeartCollect == 0) return heartsWordsCollect1;
        if (countHeartCollect == 1) return heartsWordsCollect2;
        if (countHeartCollect == 2) return heartsWordsCollect3;

        return null;
    }

    Texture2D GetTextureCutsceneByCurrentCollectHeart()
    {
        if (countHeartCollect == 0) return cutsceneTex;
        if (countHeartCollect == 1) return cutsceneTex2;
        if (countHeartCollect == 2) return cutsceneTex3;

        return null;
    }

    AudioClip GetAudioClipBGSoundByCurrentCollectHeart()
    {
        if (countHeartCollect == 0) return audioClipBGSound[0];
        if (countHeartCollect == 1) return audioClipBGSound[1];
        if (countHeartCollect == 2) return audioClipBGSound[2];

        return null;
    }
}
