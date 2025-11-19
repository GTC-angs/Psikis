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
    [SerializeField] bool changeColorWhileHover = true;

    void Start()
    {
        tMP_Text = gameObject.GetComponent<TMP_Text>();
        colorNormal = tMP_Text.color;
    }
    public void HoverStart()
    {
        if (changeColorWhileHover)
            tMP_Text.CrossFadeColor(colorHover, hoverDuration, true, false);
        MoveXText(tMP_Text, new Vector4(30, 0, 0, 0));
    }

    public void HoverEnd()
    {
        // transform.DOMoveX(locationNormal.x, hoverDuration);
        if (changeColorWhileHover)
            tMP_Text.CrossFadeColor(colorNormal, hoverDuration, true, false);
        MoveXText(tMP_Text, new Vector4(0, 0, 0, 0));
    }

    public void MoveXText(TMP_Text tMP_Text, Vector4 target)
    {

        float t = 0f;
        float duration = hoverDuration;
        while (t < duration)
        {
            t += Time.deltaTime;

            // Lerp margin
            float lerp = t / duration;
            tMP_Text.margin = Vector4.Lerp(tMP_Text.margin, target, lerp);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HomeSceneManager.Instance != null) HomeSceneManager.Instance.PlayHoverSfx();
        HoverStart();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HomeSceneManager.Instance != null) HomeSceneManager.Instance.PlayHoverSfx();
        HoverEnd();
    }

}
