using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public int health = 3, maxHealth = 3;
    public int Health{
        get
        {
            return health;
        }
        set
        {
            health = value;
            UpdateHealthUI();
            if (health <= 0) Time.timeScale = 0;
        }
    }
    public float stamina = 100f, maxStamina = 100f;
    public bool isCanGetHit = true;
    public static PlayerStat Instance;
    public SpriteRenderer spriteRenderer { get; private set; }

    void Start()
    {
        Instance = this;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    public void UpdateHealthUI()
    {
        Debug.Log(HUDManager.Instance.CG_heartsIcon.Count);
        Debug.Log(HUDManager.Instance.CG_heartsIcon[0].name);
        foreach (CanvasGroup image in HUDManager.Instance.CG_heartsIcon)
        {
            image.alpha = 0f;
        }

        for (int i = 0; i < Health; i++)
        {
            HUDManager.Instance.CG_heartsIcon[i].alpha = 1f;
        }
    }

    public IEnumerator BlinkEffect(float duration)
    {
        bool isBlink = false;
        float time = 0;

        isCanGetHit = false;

        yield return new WaitUntil(() => 
        {

            time += Time.deltaTime;
            if (time >= duration) return true;

            if (!isBlink)
            {
                spriteRenderer.color = new Color32(0, 0, 0, 255);
                isBlink = !isBlink;
            }
            else
            {
                spriteRenderer.color = new Color32(255, 255, 255, 255);
                isBlink = !isBlink;
            }
            
            return false;
        });

        spriteRenderer.color = new Color32(255, 255, 255, 255);
        PlayerStat.Instance.isCanGetHit = true;

    }

    public void UpdateStaminaUI()
    {
        float maxWidth = 197f;
        float normalizedStamina = stamina / maxStamina;
        float newWidth = maxWidth * normalizedStamina;

        HUDManager.Instance.RT_staminaBar.sizeDelta = new Vector2(newWidth, HUDManager.Instance.RT_staminaBar.sizeDelta.y);
    }

}
