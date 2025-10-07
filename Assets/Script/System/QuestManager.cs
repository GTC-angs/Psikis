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
    public QuestSO activeQuest; // quest yang sedang berjalan
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

    /// <summary>
    /// fungsi yang berfungsi untuk menambahkan quest menjadi quest active.
    /// di set public agar bisa berkomunikasi di file lain dengan lebh fleksibel
    /// </summary>
    /// <param name="quest"></param>
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

    /// <summary>
    /// Fungsi yang dipanggil ketika terjadi perubahan dengan progress Quest
    /// </summary>
    void UpdateUIQuest()
    {
        if (activeQuest) CG_questContainer.alpha = 1f; // jika ada activeQuest yang aktif
        else // jika tidak matikan alpha
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

    /// <summary>
    /// sebuah fungsi untuk menambahkn progress ke dalam quest kita.
    /// mengupdate UI dan menegecek kondisi selesai/blm.
    /// </summary>
    /// <param name="IDKey"></param>
    /// <param name="countProgress"></param>
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
    }

    /// <summary>
    /// Fungsi animasi ketika quest telah selesai terpenuhi
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
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
