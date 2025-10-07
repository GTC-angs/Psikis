using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;
using System;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public QuestSO activeQuest;
    public string keyOnCompleted;


    [Header("===UI===")]
    [Space(10)]
    [SerializeField] TMP_Text textQuestTitle;
    [SerializeField] TMP_Text textQuestHint;
    [SerializeField] RectTransform RT_questContainer;

    [Header("===Component===")]
    [SerializeField] CanvasGroup CG_questContainer;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(gameObject);
    }

    // Quest itu kita cuma add active to list ativeMission
    // abistu uat method untuk ubah progress quest
    // didalamnya di cek juga apakah suda selesai

    public void AddQuestToActive(QuestSO quest)
    {
        if (activeQuest != null)
        {
            Debug.LogWarning("Cant add another Quest while, a quest active");
            return;
        }

        // add to active
        activeQuest = quest;

        // update UI
        UpdateUIQuest();
    }

    void UpdateUIQuest()
    {
        if (activeQuest) CG_questContainer.alpha = 1f;
        else
        {
            CG_questContainer.alpha = 0f;
            return;
        }

        string progressText = "";
        if (activeQuest.isShowProgress)
            progressText = $" , {activeQuest.currentCount} / {activeQuest.requiredCount}";

        textQuestTitle.text = activeQuest.questTitle + progressText;
        textQuestHint.text = activeQuest.questHint;
    }

    public void UpdateQuest(string IDKey, int countProgress = 1)
    {
        if (activeQuest == null) return;
        // add progress
        activeQuest.currentCount += countProgress;
        // update UI
        UpdateUIQuest();
        // cek currentCount >= target
        if (activeQuest.currentCount >= activeQuest.requiredCount)
        {
            StartCoroutine(CompletedQuest(activeQuest));
        }
        // jika true, silang teks dan fadeout dan call invoke
    }

    IEnumerator CompletedQuest(QuestSO quest)
    {
        quest.OnQuestCompleted?.Invoke();
        // coret text
        textQuestTitle.fontStyle = FontStyles.Strikethrough;
        // delay
        yield return new WaitForSeconds(1.2f);

        // fade up
        RT_questContainer.DOMoveY(-10f, 0.6f);
        CG_questContainer.DOFade(0, 0.6f);

        // udate property
        yield return new WaitForSeconds(1f);
        activeQuest = null;

        UpdateUIQuest();
        RT_questContainer.DOMoveY(10f, 0.6f);
    }
}
