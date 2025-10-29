using UnityEngine;
using DG;
using DG.Tweening;

public class OptionUIManager : MonoBehaviour
{
     [SerializeField] CanvasGroup canvasGroupAudio, canvasGroupBg;
    public void OpenAudioMenu()
    {
        canvasGroupAudio.DOFade(1, 0.3f);
        canvasGroupAudio.interactable = true;
        canvasGroupAudio.blocksRaycasts = true;
    }
    public void CloseAudioMenu()
    {
        canvasGroupAudio.DOFade(0, 0.3f);
        canvasGroupAudio.interactable = false;
        canvasGroupAudio.blocksRaycasts = false;
    }

    public void OpenControlMenu()
    {
        canvasGroupBg.DOFade(0, 0.3f);
        canvasGroupBg.interactable = true;
        canvasGroupBg.blocksRaycasts = true;

        // then open

    }

    public void CloseControlMenu()
    {
        canvasGroupBg.DOFade(0, 0.3f);
        canvasGroupBg.interactable = true;
        canvasGroupBg.blocksRaycasts = true;

        // then close canvasgroup

    }
}
