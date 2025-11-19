using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
public class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] List<string> actions;
    [SerializeField] List<DialogSO> DialogWhileInteract;

    public bool isCanInteract = true;
    public string GetInteractText()
    {
        return "";
    }

    public void CancelInteract()
    {
        PlayerInteractSystem.Instance.HideInteractUI();
        PlayerMovement.Instance.isCanMoveInput = true;
        PlayerInteractSystem.Instance.interactable = null;
    }

    public void EnteringArea()
    {
        PlayerInteractSystem.Instance.ShowInteractUI(actions.Count, actions); // show 2 options
        PlayerMovement.Instance.isCanMoveInput = false;
    }

    public void Interact()
    {
        if (!isCanInteract)
        {
            CancelInteract();
            return;
        }

        // show dialog and return
        if (DialogWhileInteract != null && DialogWhileInteract.Count > 0)
        {
            DialogManager.Instance.indexDialog = 0;
            DialogManager.Instance.listDialogScene = DialogWhileInteract;
            DialogManager.Instance.StartDialog();
            DialogManager.Instance.AfterFinishDialog.AddListener(ClearUiDialog);
        }

        CancelInteract();
        PlayerMovement.Instance.isCanMoveInput = true;
    }

    public void Interact2() // press R
    {
        // nothing 
    }


    void ClearUiDialog()
    {
        DialogManager.Instance.HideDialog();
    }
}
