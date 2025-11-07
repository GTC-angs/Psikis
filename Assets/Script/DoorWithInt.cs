using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DoorWithInt : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent eventInteract;
    [SerializeField] List<string> actions;

    bool isUsed = false;
    public bool isCanInteract = true;
    public string GetInteractText()
    {
        return "";
    }

    public void CancelInteract()
    {
   
        Debug.Log("You cancel to open the door");
        PlayerInteractSystem.Instance.HideInteractUI();
        PlayerMovement.Instance.isCanMoveInput = true;
        PlayerInteractSystem.Instance.interactable = null;
    }

    public void EnteringArea()
    {
        if (isUsed) return;
        if (!isCanInteract) return;
        // PlayerInteractSystem.Instance.SetActionStringList();
        PlayerInteractSystem.Instance.ShowInteractUI(actions.Count, actions); // show 2 options

        PlayerMovement.Instance.isCanMoveInput = false;
        Debug.Log("You entering the door area");
    }

    public void Interact()
    {
        if (isUsed) return;
        if (!isCanInteract) return;
        
        isUsed = true;
        CancelInteract();
        PlayerMovement.Instance.isCanMoveInput = true;
        eventInteract?.Invoke();
        Debug.Log("You open the door");
    }

}
