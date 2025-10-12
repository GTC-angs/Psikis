using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyAttackVisual attackVisual;
    public enum EnemyState { Idle, Walk, Run, Charge, Attack, Wait }
    public EnemyState currentState = EnemyState.Walk;

    [Header("References")]
    // public Image chargeIndicatorUI; // UI Image sebagai visual indikator serangan
    // public Transform indicatorAnchor; // tempat UI ditempel (biasanya di atas kepala enemy)


    [Header("Attack Settings")]
    public float detectionRange = 5f;
    public float chargeDuration = 1.5f;
    public float attackDelay = 0.5f;
    public float waitAfterAttack = 2f;
    public float attackAngle = 90f;
    public float attackRange = 2f;

    bool isAttacking = false;
    public static EnemyAttack Instance;

    Coroutine chargeRoutine;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log($"EnemyAttack Start: {gameObject.name}", gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, EnemyMovement.Instance.targetPosition);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distance < detectionRange) ChangeState(EnemyState.Charge);
                break;

            case EnemyState.Walk:
            case EnemyState.Run:
                if (!EnemyMovement.Instance.isCanMove) return;

                if (distance < detectionRange)
                    ChangeState(EnemyState.Charge);
                break;

            case EnemyState.Charge:
                if (chargeRoutine == null)
                {
                    chargeRoutine = StartCoroutine(HandleChargeAttack());
                }
                break;

        }

        UpdateUI();
    }

    void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }
    IEnumerator HandleChargeAttack()
    {
        EnemyMovement.Instance.isCanMove = false;

        // update arah cone agar sesuai arah musuh
        if (attackVisual != null)
        {
            // rotate local object visual berdasarkan arah gerak terakhir
            Vector2 dir = EnemyMovement.Instance.lastMoveDir;
            float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            attackVisual.transform.rotation = Quaternion.Euler(0, 0, angleZ);
            attackVisual.DrawCone();
        }

        float t = 0;
        Debug.Log("Charging..");
        while (t < chargeDuration)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // ChangeState(EnemyState.Attack);


        yield return new WaitForSeconds(attackDelay);

        Debug.Log("Attack..");
        PerformAttack();

        if (attackVisual != null)
            attackVisual.HideCone();

        yield return new WaitForSeconds(waitAfterAttack);
        EnemyMovement.Instance.isCanMove = true;
        isAttacking = false;

        ChangeState(EnemyState.Walk);

        chargeRoutine = null; // say its over

    }

    void PerformAttack()
    {
        // arah dari musuh ke player
        Vector2 dirToPlayer = (PlayerMovement.Instance.transform.position - transform.position).normalized;

        // arah depan musuh (berdasarkan arah gerak terakhir)
        Vector2 attackDir = EnemyMovement.Instance.lastMoveDir;

        // hitung jarak dan sudut
        float dist = Vector2.Distance(transform.position, PlayerMovement.Instance.transform.position);
        float angle = Vector2.Angle(attackDir, dirToPlayer);

        ChangeState(EnemyState.Attack);
        isAttacking = true;

        if (angle <= attackAngle / 2 && dist <= attackRange)
        {
            Debug.Log("🎯 HIT Player!");
            // TODO: tambahkan damage ke player
            if (PlayerStat.Instance.isCanGetHit)
            {
                PlayerStat.Instance.Health -= 1;
                CameraFollow.Instance.transform.DOShakePosition(0.2f, 0.4f, 10, 90, false);
                PlayerMovement.Instance.Knockback(transform.position, 15, 0.4f);
                StartCoroutine(PlayerStat.Instance.BlinkEffect(5f));
            }
        }
        else
        {
            Debug.Log($"❌ Missed! dist={dist:F2}, angle={angle:F2}, attackDir={attackDir}");
        }
    }

    void UpdateUI()
    {
        // if (chargeIndicatorUI && indicatorAnchor)
        // {
        //     Vector3 screenPos = Camera.main.WorldToScreenPoint(indicatorAnchor.position);
        //     chargeIndicatorUI.transform.position = screenPos;
        // }
    }

    void OnDrawGizmosSelected()
    {
        if (EnemyMovement.Instance == null) return;

        Vector2 attackDir = Application.isPlaying
            ? EnemyMovement.Instance.lastMoveDir
            : Vector2.right;

        float halfAngle = attackAngle / 2f;
        Gizmos.color = Color.red;

        Vector3 left = Quaternion.Euler(0, 0, -halfAngle) * attackDir * attackRange;
        Vector3 right = Quaternion.Euler(0, 0, halfAngle) * attackDir * attackRange;

        Gizmos.DrawRay(transform.position, left);
        Gizmos.DrawRay(transform.position, right);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)EnemyMovement.Instance.lastMoveDir * attackRange);
    }
}
