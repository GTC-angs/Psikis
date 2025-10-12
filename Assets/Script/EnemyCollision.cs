using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;

public class EnemyCollision : MonoBehaviour
{
    Rigidbody2D rb;
    bool isKnocked = false, isBlink;
    private Material mat;
    SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        mat = spriteRenderer.material;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    IEnumerator EffectSpriteHit()
    {
        mat.SetVector("_ColorTint", new Vector4(-1, 0, 0, 0));
        for (int i = 0; i < 30; i++)
        {
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

            yield return new WaitForSeconds(0.7f / 30);

        }

        mat.SetVector("_ColorTint", new Vector4(-1, -2, -1, 0));
        EnemyMovement.Instance.isCanMove = false;
        yield return new WaitForSeconds(1.1f);
        EnemyMovement.Instance.isCanMove = true;

        StartCoroutine(EnemyMovement.Instance.RunningForSecond(12f, 7f));
    }

    public void Knockback(Vector2 fromPosition, float knockbackForce, float knockbackDuration)
    {
        if (isKnocked) return;
        isKnocked = true;

        Vector2 direction = (rb.position - fromPosition).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(EffectSpriteHit());
        StartCoroutine(StopKnockback(knockbackDuration));
    }

    IEnumerator StopKnockback(float knockbackDuration)
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
        PlayerMovement.Instance.isCanMoveInput = true;
        isKnocked = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("rockThrow") && collision.gameObject.GetComponent<RockScript>().isThrow)
        {
            Debug.LogWarning("You hit the boss");
            Knockback(collision.gameObject.transform.position, collision.gameObject.GetComponent<RockScript>().knockForce, 0.4f);
            CameraFollow.Instance.transform.DOShakePosition(0.2f, 0.3f, 10, 90, false);
        }
    }
}
