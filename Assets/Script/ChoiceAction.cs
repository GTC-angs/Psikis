using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
public class ChoiceAction : MonoBehaviour
{
    [SerializeField] TMP_Text actionText;
    public string message;
    public bool isChoose;
    public UnityEvent OnChoose;
    [SerializeField] CanvasGroup canvasGroup;


    public void Choose()
    {
        OnChoose?.Invoke();
        PlayerMovement.Instance.isCanMoveInput = true;
    }

    public void Initialize()
    {
        actionText.text = message;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (isChoose) canvasGroup.alpha = 1f;
        else canvasGroup.alpha = 0;
    }
}
