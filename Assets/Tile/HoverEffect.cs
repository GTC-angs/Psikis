using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TMP_Text tMP_Text;
    [SerializeField] Color32 colorHover = new Color32(255, 255, 255, 255);
    [SerializeField] float hoverDuration = 0.3f;
    [SerializeField] float xTransformHover = 30f;
    Color colorNormal;
    public Vector3 locationNormal;
    [SerializeField] bool changeColorWhileHover = true;

    void Start()
    {
        tMP_Text = gameObject.GetComponent<TMP_Text>();
        colorNormal = tMP_Text.color;
    }
    public void HoverStart()
    {
        if (changeColorWhileHover)
        {
            DOTween.To(
             () => tMP_Text.color,
             x => tMP_Text.color = x,
             colorHover,
             hoverDuration).SetUpdate(true);
        }

        MoveXText(tMP_Text, new Vector4(xTransformHover, 0, 0, 0));
    }

    public void HoverEnd()
    {
        // transform.DOMoveX(locationNormal.x, hoverDuration);
        if (changeColorWhileHover)
        {
            DOTween.To(
            () => tMP_Text.color,
            x => tMP_Text.color = x,
            colorNormal,
            hoverDuration).SetUpdate(true);
        }

        MoveXText(tMP_Text, new Vector4(0, 0, 0, 0));
    }

    public void MoveXText(TMP_Text tMP_Text, Vector4 target)
    {
        tMP_Text.DOKill();
        DOTween.To(
            () => tMP_Text.margin,
            (x) => tMP_Text.margin = x,
            target,
            hoverDuration
        ).SetUpdate(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HomeSceneManager.Instance != null) HomeSceneManager.Instance.PlayHoverSfx();
        if (MenuManagerHUD.Instance != null) MenuManagerHUD.Instance.PlayHoverAudio();
        HoverStart();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HomeSceneManager.Instance != null) HomeSceneManager.Instance.PlayHoverSfx();
        HoverEnd();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MenuManagerHUD.Instance != null) MenuManagerHUD.Instance.PlaySelectAudio();
    }

}
