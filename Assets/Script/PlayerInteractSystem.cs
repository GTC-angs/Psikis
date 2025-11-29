using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerInteractSystem : MonoBehaviour
{

    // UI
    [SerializeField] List<GameObject> interactUIList;
    [SerializeField] List<TMP_Text> interactTextList;


    // component
    [SerializeField] CanvasGroup CG_interactUI;
    [SerializeField] List<RectTransform> interactUIRectTransform;
    public List<string> actionStringList;

    public IInteractable interactable;
    string nameGO;

    // singletoon
    public static PlayerInteractSystem Instance;
    CharacterAudio characterAudio;

    void Start()
    {   
        characterAudio = gameObject.GetComponent<CharacterAudio>();
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            TryInteract2();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (interactable == null) return;
            interactable.CancelInteract();
        }
    }

    void TryInteract() // e
    {

        if (interactable == null) return;
        Debug.Log($"Try interact {interactable.GetInteractText()}");
        interactable.Interact();
        characterAudio.PlaySelectSound();
    }

    void TryInteract2() // r
    {

        if (interactable == null) return;
        Debug.Log($"Try interact {interactable.GetInteractText()}");
        interactable.Interact2();
        characterAudio.PlaySelectSound();
    }

    public void ShowInteractUI(int count, List<string> listActionText)
    {
        characterAudio.PlayInteractSound();
        actionStringList = listActionText;

        for (int i = 0; i < interactUIList.Count; i++)
        {
            interactTextList[i].text = "";
            // interactUIRectTransform[i].sizeDelta = new Vector2(interactUIRectTransform[i].sizeDelta.x, 0);
        }


        for (int i = 0; i < count; i++)
        {
            // interactUIRectTransform[i].sizeDelta = new Vector2(interactUIRectTransform[i].sizeDelta.x, 2);
            interactTextList[i].text = actionStringList[i];
        }

        CG_interactUI.DOFade(1f, 0.3f);

    }


    public void HideInteractUI()
    {
        CG_interactUI.DOFade(0f, 0.3f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        interactable = other.gameObject.GetComponent<IInteractable>();
        nameGO = other.gameObject.name;
        interactable?.EnteringArea();

        Debug.Log(nameGO);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (nameGO == other.gameObject.name) interactable = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        interactable = other.gameObject.GetComponent<IInteractable>();
        nameGO = other.gameObject.name;
        interactable?.EnteringArea();

        Debug.Log(nameGO);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (nameGO == other.gameObject.name) interactable = null;
        HideInteractUI();
        PlayerMovement.Instance.isCanMoveInput = true;
    }
}
