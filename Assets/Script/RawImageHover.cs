using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RawImageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    RawImage rawImage;
    RectTransform rectTransform;
    Color defaultColor;
    [SerializeField] Color hoverColor;
    [SerializeField] UnityEvent OnClicked;

    void Start()
    {
        rawImage = gameObject.GetComponent<RawImage>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        defaultColor = rawImage.color;
    }

    void StartHover()
    {
        rawImage.color = defaultColor;
        rawImage.DOColor(hoverColor, 0.3f);
        rectTransform.DORotate(new Vector3(0, 0, 5), 0.2f);
        Debug.Log("Hover");
    }

    void EndHover()
    {
        rawImage.color = hoverColor;
        rawImage.DOColor(defaultColor, 0.3f);
        rectTransform.DORotate(new Vector3(0, 0, 0), 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EndHover();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 
        Debug.Log("YOU CLICKED");
        OnClicked?.Invoke();
    }
}
