using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public enum path { Top, Down, Left, Right };
    public bool isCanMoveInput = true, isMove = false;
    bool isRunning = false, isKnocked = false;
    [SerializeField] float speedCharacterMoveTransition = 0.2f;
    [SerializeField] LayerMask collisionMaskObstacle; // layer dinding/halangan
    [SerializeField] float rayDistance = 1f, runningSpeedTransition;    // jarak ray sesuai step per move
    PlayerAnimationController animator;
    Rigidbody2D rb;
    void Start()
    {
        Instance = this;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<PlayerAnimationController>();
    }

    void Update()
    {
        // running state
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning = false;

        // if (!isCanMoveInput) return;

        if (Input.GetKey(KeyCode.W) && isCanMoveInput) HandleDirectionMove(path.Top);
        else if (Input.GetKey(KeyCode.A) && isCanMoveInput) HandleDirectionMove(path.Left);
        else if (Input.GetKey(KeyCode.S) && isCanMoveInput) HandleDirectionMove(path.Down);
        else if (Input.GetKey(KeyCode.D) && isCanMoveInput) HandleDirectionMove(path.Right);

        bool isNotMove = !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D);
        if (isNotMove && !isMove) animator.CallTriggerIdle();

        if (isRunning)
        {
            if (PlayerStat.Instance && PlayerStat.Instance.stamina <= 0) return;
            PlayerStat.Instance.stamina -= Time.deltaTime * 14f;
            PlayerStat.Instance.UpdateStaminaUI();
        }
        else
        {
            if (PlayerStat.Instance && PlayerStat.Instance.stamina >= 100) return;
            // regen
            PlayerStat.Instance.stamina += Time.deltaTime * 10f;
            PlayerStat.Instance.UpdateStaminaUI();
        }

        // Debug.Log($"{PlayerStat.Instance.stamina} | {isRunning}");

    }

    void HandleDirectionMove(path pathDirection)
    {
        if (!isCanMoveInput) return;
        Vector3 direction = Vector3.zero;

        switch (pathDirection)
        {
            case path.Top:
                direction = Vector3.up;
                animator.UpdateDirectionPlayer(PlayerAnimationController.DirectionFace.top);
                animator.SetNewAnimation(PlayerAnimationController.AnimationStat.walk_top);
                break;
            case path.Down:
                direction = Vector3.down;
                animator.UpdateDirectionPlayer(PlayerAnimationController.DirectionFace.bottom);
                animator.SetNewAnimation(PlayerAnimationController.AnimationStat.walk_bottom);
                break;
            case path.Left:
                direction = Vector3.left;
                if (!PlayerStat.Instance.spriteRenderer.flipX) PlayerStat.Instance.spriteRenderer.flipX = true;

                animator.UpdateDirectionPlayer(PlayerAnimationController.DirectionFace.horizontal);
                animator.SetNewAnimation(PlayerAnimationController.AnimationStat.walk_horizontal);
                break;
            case path.Right:
                direction = Vector3.right;
                if (PlayerStat.Instance.spriteRenderer.flipX) PlayerStat.Instance.spriteRenderer.flipX = false;

                animator.UpdateDirectionPlayer(PlayerAnimationController.DirectionFace.horizontal);
                animator.SetNewAnimation(PlayerAnimationController.AnimationStat.walk_horizontal);
                break;
        }

        // 🔍 cek dengan raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, collisionMaskObstacle);

        if (hit.collider == null) // tidak ada halangan
        {
            float speed = 1f;
            isMove = true;

            isCanMoveInput = false;
            Vector3 targetPos = transform.position + direction * speed;

            transform.DOMove(targetPos, isRunning ? speedCharacterMoveTransition - runningSpeedTransition : speedCharacterMoveTransition)
                     .OnComplete(() =>
                     {
                         isCanMoveInput = true;
                         isMove = false;
                     });
        }
        else
        {

            Debug.Log("Nabrak: " + hit.collider.name);
        }

        CheckItemOnGround(pathDirection);
    }

    void CheckItemOnGround(path pathDirection)
    {
        CollectSystem.Instance.itemInDistance = null;
        Vector3 direction = Vector3.zero;

        switch (pathDirection)
        {
            case path.Top: direction = Vector3.up; break;
            case path.Down: direction = Vector3.down; break;
            case path.Left: direction = Vector3.left; break;
            case path.Right: direction = Vector3.right; break;
        }

        CollectSystem.Instance.ItemScanFrontOfPlayer(direction, rayDistance);
    }

    public void Knockback(Vector2 fromPosition, float knockbackForce, float knockbackDuration)
    {
        if (isKnocked) return;
        isKnocked = true;

        Vector2 direction = (rb.position - fromPosition).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        // StartCoroutine(EffectSpriteHit());
        StartCoroutine(StopKnockback(knockbackDuration));
    }

    IEnumerator StopKnockback(float knockbackDuration)
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
        // PlayerMovement.Instance.isCanMoveInput = true;
        isKnocked = false;
    }


    // Biar kelihatan garis ray di Scene View
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * rayDistance);
    }
}
