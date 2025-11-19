using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DoorWithInt : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent eventInteract, eventInteractR;
    [SerializeField] List<string> actions;
    [SerializeField] List<DialogSO> DialogWhileCantInteract;
    [SerializeField] bool isChangeUsedWhenInteract = true;
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
        // PlayerInteractSystem.Instance.SetActionStringList();
        PlayerInteractSystem.Instance.ShowInteractUI(actions.Count, actions); // show 2 options

        PlayerMovement.Instance.isCanMoveInput = false;
        Debug.Log("You entering the door area");
    }

    public void Interact() // fungsi dari interact.cs
    {
        if (isUsed) return;
        if (!isCanInteract)
        {
            // show dialog and return
            if (DialogWhileCantInteract != null && DialogWhileCantInteract.Count > 0)
            {
                DialogManager.Instance.indexDialog = 0;
                DialogManager.Instance.listDialogScene = DialogWhileCantInteract;
                DialogManager.Instance.StartDialog();
                DialogManager.Instance.AfterFinishDialog.AddListener(ClearUiDialog);
            }

            CancelInteract();
            PlayerMovement.Instance.isCanMoveInput = true;
            return;
        }

        if (isChangeUsedWhenInteract)
            isUsed = true;
            
        CancelInteract();
        PlayerMovement.Instance.isCanMoveInput = true;
        eventInteract?.Invoke();

    }


    void ClearUiDialog()
    {
        DialogManager.Instance.HideDialog();
    }

    public void Interact2()
    {
        eventInteractR?.Invoke();
        CancelInteract();
        PlayerMovement.Instance.isCanMoveInput = true;
    }

}
