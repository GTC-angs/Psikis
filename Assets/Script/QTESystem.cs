using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
public class QTESystem : MonoBehaviour
{
    [Header("==UI==")]
    [SerializeField] CanvasGroup CG_containerQTE;
    [SerializeField] TMP_Text textQTEkey;
    [SerializeField] Image imageCircleFill;
    [SerializeField] KeyCode keyCodeQTE;
    [SerializeField] float strictStrength = 0.3f, changeKeyEverySecond = 1f, strengthFill = 0.05f;
    [SerializeField] bool isQTE = false, isMixKeyEverySecond = false, isQTEOnStart = false;
    [SerializeField] OnSuccessCallback typeCallback;
    UnityEvent onSuccess = new UnityEvent();

    enum OnSuccessCallback
    {
        PlayerThrowRock
    };
    public static QTESystem Instance;
    float val = 0.1f;
    List<KeyCode> keyCodes = new List<KeyCode>
    {
        KeyCode.B,KeyCode.C,KeyCode.F,KeyCode.G,KeyCode.H,KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.T,KeyCode.U,KeyCode.V,KeyCode.X,KeyCode.Y,KeyCode.Z
    };


    void Update()
    {
        if (isQTE)
        {
            if (val > 0) val -= Time.deltaTime * strictStrength;

            if (Input.GetKeyDown(keyCodeQTE))
            {
                val += strengthFill;
            }

            imageCircleFill.fillAmount = val;

            if (val >= 1)
            {
                SuccessQTE();
            }

            // cek if player want to cancel
            if (Input.GetKeyDown(KeyCode.Space)) CancelQTE();
        }
    }

    void Start()
    {
        Instance = this;
        // update UI
        textQTEkey.text = keyCodeQTE.ToString();

        if (isMixKeyEverySecond) InvokeRepeating("ChangeKey", 0f, changeKeyEverySecond);
        if (isQTEOnStart) StartQTE();

        if (typeCallback == OnSuccessCallback.PlayerThrowRock) onSuccess.AddListener(ThrowRock_OnSuccess);
    }
    void ChangeKey()
    {
        if (!isQTE) return;

        int rand = Random.Range(0, keyCodes.Count);
        KeyCode newKey = keyCodes[rand];
        keyCodeQTE = newKey;

        // update UI
        textQTEkey.text = newKey.ToString();
    }

    public void StartQTE()
    {
        val = 0.1f;
        isQTE = true;
        // show QTE
        CG_containerQTE.alpha = 1f;
        PlayerMovement.Instance.isCanMoveInput = false;
    }

    void CancelQTE()
    {
        isQTE = false;
        HideUIQTE();
    }

    void SuccessQTE()
    {
        isQTE = false;
        HideUIQTE();
        onSuccess?.Invoke();
    }

    void HideUIQTE()
    {
        CG_containerQTE.alpha = 0f;
        PlayerMovement.Instance.isCanMoveInput = true;
    }

    // 
    public void ThrowRock_OnSuccess()
    {
        PlayerThrowSystem.Instance.StartThrowing();
    }

}
