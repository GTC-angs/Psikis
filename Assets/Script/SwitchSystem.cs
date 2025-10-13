using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchSystem : MonoBehaviour
{

    // item, messagelock, message open, audio, isOpen, collider2d void Open
    // action : open, back
    // trigger ? on collide or on key input?
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource audioSource;
    [SerializeField] bool isSwitched = false, isOnArea = false;
    [SerializeField] GameObject target;

    public enum InteractType
    {
        Collide, InputKey
    };


    Collider2D collider2d;
    SpriteRenderer spriteRendererTarget;
    Interact interactSc;

    void Start()
    {
        collider2d = target.GetComponent<Collider2D>();
        spriteRendererTarget = target.GetComponent<SpriteRenderer>();
        interactSc = gameObject.GetComponent<Interact>();
    }

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && interactSc.isInteract)
        {
            HUDManager.Instance.ChooseActionUI();
            interactSc.isInteract = false;
        }

        if (interactSc.interactType != Interact.InteractType.InputKey) return;
        if (Input.GetKeyDown(interactSc.keyCode) && !interactSc.isInteract && isOnArea)
        {
            interactSc.isInteract = true;
            StartCoroutine(HUDManager.Instance.PrepareUILockSystem(interactSc));
        }
    }

    public void SwitchEventUnityInspector(bool isOn)
    {
        StartCoroutine(Switch(isOn));
    }

    IEnumerator Switch(bool isOn, float durationLookat = 2f)
    {
        yield return new WaitUntil(() => !PlayerMovement.Instance.isMove);
        PlayerMovement.Instance.isCanMoveInput = false;

        if (isOn)
        {
            if (isSwitched)
            {
                HUDManager.Instance.CancelChoose();
                 yield break;
            }
           
            CameraFollow.Instance.target = target.transform;
            isSwitched = true;

            float timer = 0f;
            yield return new WaitUntil(() =>
            {
                timer += Time.deltaTime;
                float distance = Vector3.Distance(
                    CameraFollow.Instance.transform.position,
                    CameraFollow.Instance.target.position
                );
                return distance <= 5f || timer >= 1.5f; // maksimal tunggu 3 detik
            });

            OpenDoorUsingSwitch();
        }
        else
        {
            if (!isSwitched)
            {
                HUDManager.Instance.CancelChoose();
                 yield break;
            }

            CameraFollow.Instance.target = target.transform;
            isSwitched = false;

            float timer = 0f;
            yield return new WaitUntil(() =>
            {
                timer += Time.deltaTime;
                float distance = Vector3.Distance(
                    CameraFollow.Instance.transform.position,
                    CameraFollow.Instance.target.position
                );
                return distance <= 5f || timer >= 1.5f; // maksimal tunggu 2 detik
            });

            CloseDoorUsingSwitch();
        }

        yield return new WaitForSeconds(durationLookat);
        CameraFollow.Instance.target = PlayerMovement.Instance.gameObject.transform;
        PlayerMovement.Instance.isCanMoveInput = true;

        // Hide Ui
        HUDManager.Instance.CancelChoose();
    }

    void OpenDoorUsingSwitch()
    {
        float startWidth = spriteRendererTarget.size.x;
        float targetWidth = 0f; // lebar tujuan misalnya 100 pixel

        DOTween.To(
            () => spriteRendererTarget.size.x,                     // getter
            (v) => spriteRendererTarget.size =                     // setter
                new Vector2(v, spriteRendererTarget.size.y),        // ubah hanya X
            targetWidth,                                                // nilai akhir
            1f                                                          // durasi
        );

        collider2d.enabled = false;
    }

    void CloseDoorUsingSwitch()
    {
        float startWidth = spriteRendererTarget.size.x;
        float targetWidth = 2.1f; // lebar tujuan misalnya 100 pixel

        DOTween.To(
           () => spriteRendererTarget.size.x,                     // getter
           (v) => spriteRendererTarget.size =                     // setter
               new Vector2(v, spriteRendererTarget.size.y),        // ubah hanya X
           targetWidth,                                                // nilai akhir
           1f                                                          // durasi
       );

        collider2d.enabled = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        isOnArea = true;

        if (interactSc.interactType == Interact.InteractType.Collide && !interactSc.isInteract)
        {
            interactSc.isInteract = true;
            Debug.Log("Udah prepare UI");
            StartCoroutine(HUDManager.Instance.PrepareUILockSystem(interactSc));
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        interactSc.isInteract = false;
        isOnArea = false;
    }
}
