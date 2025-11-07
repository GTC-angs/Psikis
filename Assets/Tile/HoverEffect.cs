using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TMP_Text tMP_Text;
    [SerializeField] Color32 colorHover = new Color32(255, 255, 255, 255);
    [SerializeField] float hoverDuration = 0.3f;
    [SerializeField] float xTransformHover = 10f;
    Color colorNormal;
    public Vector3 locationNormal;

    void Start()
    {
        locationNormal = gameObject.GetComponent<RectTransform>().position;
        tMP_Text = gameObject.GetComponent<TMP_Text>();
        colorNormal = tMP_Text.color;
    }
    public void HoverStart()
    {
        transform.DOMoveX(locationNormal.x + xTransformHover, hoverDuration);
        tMP_Text.CrossFadeColor(colorHover, hoverDuration, true, false);
    }

    public void HoverEnd()
    {
        transform.DOMoveX(locationNormal.x, hoverDuration);
        tMP_Text.CrossFadeColor(colorNormal, hoverDuration, true, false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverStart();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverEnd();
    }

}
