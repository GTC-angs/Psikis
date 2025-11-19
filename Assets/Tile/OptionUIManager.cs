using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionUIManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] CanvasGroup canvasGroupAudio, canvasGroupBg;

    [Header("Text Tab")]
    [SerializeField] TMP_Text basicText, audioText;

    [Header("Colors")]
    [SerializeField] Color activeTabColor = Color.yellow;
    [SerializeField] Color inactiveTabColor = Color.white;

    string activeMenu = "basic";

    void Update()
    {
        UpdateTabColor();
    }

    // --------------------------
    // UPDATE TAB COLOR
    // --------------------------
    void UpdateTabColor()
    {
        if (activeMenu == "basic")
        {
            basicText.color = activeTabColor;   // Kuning
            audioText.color = inactiveTabColor; // Putih
        }
        else if (activeMenu == "audio")
        {
            audioText.color = activeTabColor;    // Kuning
            basicText.color = inactiveTabColor;  // Putih
        }
       
    }

    // --------------------------
    // BUTTON FUNCTIONS
    // --------------------------
    public void OpenAudioMenu()
    {
        CloseControlMenu();
        canvasGroupAudio.DOFade(1, 0.3f);
        canvasGroupAudio.interactable = true;
        canvasGroupAudio.blocksRaycasts = true;

        activeMenu = "audio";   // tab audio aktif
    }

    public void CloseAudioMenu()
    {
        canvasGroupAudio.DOFade(0, 0.3f);
        canvasGroupAudio.interactable = false;
        canvasGroupAudio.blocksRaycasts = false;
    }

    public void OpenControlMenu()
    {
        CloseAudioMenu();
        canvasGroupBg.DOFade(1, 0.3f);
        canvasGroupBg.interactable = true;
        canvasGroupBg.blocksRaycasts = true;

        activeMenu = "basic";
    }

    public void CloseControlMenu()
    {
        canvasGroupBg.DOFade(0, 0.3f);
        canvasGroupBg.interactable = false;
        canvasGroupBg.blocksRaycasts = false;
    }

    public void CloseSettingScene()
    {
        SceneManager.UnloadSceneAsync("OptionsUI");
    }
}
