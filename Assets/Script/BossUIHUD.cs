using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class BossUIHUD : MonoBehaviour
{
    [SerializeField] TMP_Text textDialogBoss;
    [SerializeField] Animator AnimatorTextDialog;
    [SerializeField] Image imageFillHealth;

    public static BossUIHUD Instance;

    void Start()
    {
        Instance = this;
        AnimatorTextDialog.Play("TextEvent_play_without_end", 0, 0);
    }


    public void UpdateFillHealth()
    {
        imageFillHealth.fillAmount = EnemyStat.Instance.GetForFillableHealth();
    }

    public void UpdateTextDialog(string words, float duration = 5f)
    {
        textDialogBoss.text = "";
        AnimatorTextDialog.Play("TextEvent_play_without_end", 0, 0);
        EnemyAudio.Instance.PlaySound(0, false, true);

        StartCoroutine(DoType(duration / words.Length, words, () =>
        {
            EnemyAudio.Instance.StopSound();
        }));

    }

    IEnumerator DoType(float delay, string words, Action OnClear)
    {

        foreach (char c in words)
        {
            textDialogBoss.text += c;
            yield return new WaitForSecondsRealtime(delay);
        }

        OnClear?.Invoke();
    }
}
