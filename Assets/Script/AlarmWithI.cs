using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class AlarmWithI : MonoBehaviour, IInteractable
{

    [SerializeField] string interactText;
    [SerializeField] UnityEvent eventInteract;

    bool isUsed = false;

    public string GetInteractText()
    {
        return "";

    }
    public void CancelInteract()
    {
        Debug.Log("You cancel to interact Alarm");
        PlayerInteractSystem.Instance.HideInteractUI();
        PlayerInteractSystem.Instance.interactable = null;
        PlayerMovement.Instance.isCanMoveInput = true;

    }

    public void EnteringArea()
    {
        if (isUsed) return;
        // PlayerInteractSystem.Instance.SetActionStringList();
        PlayerInteractSystem.Instance.ShowInteractUI(2, new List<string>() { "Turn Off (E)", "Cancel (Q)" }); // show 2 options

        PlayerMovement.Instance.isCanMoveInput = false;
        Debug.Log("You entering the alarm area");
    }

    public void Interact()
    {
        if (isUsed) return;
        isUsed = true;
        PlayerMovement.Instance.isCanMoveInput = true;
        CancelInteract();
        eventInteract?.Invoke();
        Scene01Manager.Instance.TutorialUIInteract.SetActive(false);
        Debug.Log("You turn off th alarm");

    }
}
