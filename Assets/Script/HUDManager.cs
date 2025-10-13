using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public int chooseIndex = 0;
    [Header("==Component==")]
    [Space(10)]
    public CanvasGroup CG_choicesContainer;
    public CanvasGroup CG_actionContainer, CG_infoContainer;
    public RectTransform RT_choices;

    [Header("==Lock & Action==")]
    [Space(10)]
    public GameObject GO_textDialogAction;
    public GameObject P_ChooseActionUI;

    [Header("==UI==")]
    [Space(10)]
    public TMP_Text textInfo;
    public TMP_Text textDialogAction;


    [Header("==UI Bottom==")]
    [Space(10)]
    public RectTransform RT_staminaBar;
    public List<CanvasGroup> CG_heartsIcon;


    [Header("==System(jangan isi di inspector)==")]
    public List<ChoiceAction> GO_actionList;

    void Start()
    {
        Instance = this;
        StartCoroutine(Starting());
    }

    IEnumerator Starting()
    {
        yield return new WaitUntil(() => PlayerStat.Instance != null);
        PlayerStat.Instance.UpdateHealthUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !PlayerMovement.Instance.isMove) MoveSelect('L');
        if (Input.GetKeyDown(KeyCode.D) && !PlayerMovement.Instance.isMove) MoveSelect('R');

    }

    public IEnumerator ShowActionFeedback(string message, AudioSource audioSource, AudioClip audioClip)
    {
        // hide ui choicess
        CG_choicesContainer.alpha = 0f;
        CG_choicesContainer.interactable = false;
        CG_choicesContainer.blocksRaycasts = false;

        // check isSuccesMessage
        string messageShow = message;

        // start sound
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();

        // start typing effect
        GO_textDialogAction.SetActive(true);
        textDialogAction.text = "";

        foreach (char character in messageShow)
        {
            textDialogAction.text += character;
            yield return new WaitForSeconds(0.05f);
        }

        audioSource.Stop();

        yield return new WaitForSeconds(3f);
        GO_textDialogAction.SetActive(false);
        CancelChoose();
    }

    public void CancelChoose()
    {
        // turn on canvas choicess
        CG_actionContainer.alpha = 0f;
        CG_actionContainer.interactable = false;
        CG_actionContainer.blocksRaycasts = false;

        CG_choicesContainer.alpha = 0f;
        CG_choicesContainer.interactable = false;
        CG_choicesContainer.blocksRaycasts = false;

        PlayerMovement.Instance.isCanMoveInput = true;


    }

    public void MoveSelect(char direction)
    {
        if (direction == 'L')
        {
            if (chooseIndex < 1) return;
            chooseIndex--;
            ChoiceAction choiceSc = GO_actionList[chooseIndex];

            foreach (ChoiceAction choice in GO_actionList)
            {
                choice.isChoose = false;
                choice.UpdateUI();
            }

            choiceSc.isChoose = true;
            choiceSc.UpdateUI();
        }
        else
        {
            if (chooseIndex > GO_actionList.Count - 2) return;
            chooseIndex++;
            ChoiceAction choiceSc = GO_actionList[chooseIndex];

            foreach (ChoiceAction choice in GO_actionList)
            {
                choice.isChoose = false;
                choice.UpdateUI();
            }

            choiceSc.isChoose = true;
            choiceSc.UpdateUI();
        }

        Debug.Log(chooseIndex);
        Debug.Log(GO_actionList.Count);
    }

    public void ChooseActionUI()
    {
        Debug.Log("Sudah milih");
        GO_actionList[chooseIndex].Choose();

    }


    /// <summary>
    /// prepare ui choose
    /// </summary>
    public IEnumerator PrepareUILockSystem(Interact interactSc)
    {
        yield return new WaitUntil(() => !PlayerMovement.Instance.isMove);
        PlayerMovement.Instance.isCanMoveInput = false;

        chooseIndex = 0;
        // turn on canvas choicess
        CG_actionContainer.alpha = 1f;
        CG_actionContainer.interactable = true;
        CG_actionContainer.blocksRaycasts = true;

        CG_choicesContainer.alpha = 1f;
        CG_choicesContainer.interactable = true;
        CG_choicesContainer.blocksRaycasts = true;

        // remove last choices
        GO_actionList = new List<ChoiceAction>();
        foreach (Transform child in RT_choices)
        {
            Destroy(child.gameObject);
        }

        // add all choicees
        for (int i = 0; i < interactSc.actionChooses.Count; i++)
        {

            // inside prepare all
            GameObject choice = Instantiate(P_ChooseActionUI, RT_choices);

            ChoiceAction choiceScript = choice.GetComponent<ChoiceAction>();
            GO_actionList.Add(choiceScript); // to access

            choiceScript.message = interactSc.actionChooses[i].name;
            choiceScript.OnChoose = interactSc.actionChooses[i].action;

            if (i == 0)
            {
                choiceScript.isChoose = true;
            }

            choiceScript.Initialize();

        }
    }

}
