using System.ComponentModel;
using UnityEngine;

public class PlayerThrowSystem : MonoBehaviour
{
    public static PlayerThrowSystem Instance;
    [SerializeField] public GameObject GO_ThrowObject;
    [SerializeField] KeyCode collectKeyInteract = KeyCode.E;
    [SerializeField] Transform targetThrow;

    void Start()
    {
        Instance = this;
    }
    void Update()
    {
        if (GO_ThrowObject == null) return;
        if (Input.GetKeyDown(collectKeyInteract))
        {
            QTESystem.Instance.StartQTE();
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("rockThrow"))
        {
            GO_ThrowObject = collision.gameObject;
            HUDManager.Instance.CG_infoContainer.alpha = 1f;
            HUDManager.Instance.textInfo.text = $"Press {collectKeyInteract} to throw.";
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("rockThrow"))
        {
            GO_ThrowObject = null;
            HUDManager.Instance.CG_infoContainer.alpha = 0f;
            HUDManager.Instance.textInfo.text = "";
        }
    }

    public void StartThrowing()
    {

        StartCoroutine(GO_ThrowObject.GetComponent<RockScript>().StartThrow(targetThrow));

    }
}
