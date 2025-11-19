using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public enum path { Top, Down, Left, Right };

    public bool isCanMoveInput = true, isMove = false;
    bool isRunning = false, isKnocked = false;

    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] LayerMask collisionMaskObstacle;
    [SerializeField] float rayDistance = 0.5f;

    PlayerAnimationController animator;
    Rigidbody2D rb;

    Vector2 moveInput;

    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimationController>();
    }

    void Update()
    {
        if (isKnocked) return;

        // Toggle running
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning = false;

        // Arah input
        float moveX = 0;
        float moveY = 0;

        if (isCanMoveInput)
        {
            if (Input.GetKey(KeyCode.W)) moveY = 1;
            else if (Input.GetKey(KeyCode.S)) moveY = -1;
            if (Input.GetKey(KeyCode.D)) moveX = 1;
            else if (Input.GetKey(KeyCode.A)) moveX = -1;
        }

        moveInput = new Vector2(moveX, moveY).normalized;

        // Update animasi arah
        if (moveInput != Vector2.zero)
        {
            UpdateDirection(moveInput);
            isMove = true;
        }
        else
        {
            if (isMove) animator.CallTriggerIdle();
            isMove = false;
        }

        // Stamina logic
        if (PlayerStat.Instance)
        {
            if (isRunning)
            {
                if (PlayerStat.Instance.stamina <= 0) isRunning = false;
                else
                {
                    PlayerStat.Instance.stamina -= Time.deltaTime * 30f;
                    PlayerStat.Instance.UpdateStaminaUI();
                }
            }
            else
            {
                if (PlayerStat.Instance.stamina <= PlayerStat.Instance.maxStamina)
                {
                    PlayerStat.Instance.stamina += Time.deltaTime * 10f;
                    PlayerStat.Instance.UpdateStaminaUI();
                }

            }
        }

        // Cek item di depan
        if (moveInput != Vector2.zero)
            CheckItemOnGround(moveInput);
    }

    void FixedUpdate()
    {
        if (isKnocked) return;

        // Cegah nabrak dengan raycast
        if (moveInput != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveInput, rayDistance, collisionMaskObstacle);
            if (hit.collider != null)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }
        }

        // Tentukan kecepatan
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.linearVelocity = moveInput * currentSpeed;
    }

    public void UpdateDirection(Vector2 dir)
    {
        if (dir.y > 0) // top
        {
            animator.UpdateDirectionPlayer(PlayerAnimationController.DirectionFace.top);
            animator.SetNewAnimation(PlayerAnimationController.AnimationStat.walk_top);
        }
        else if (dir.y < 0) // down
        {
            animator.UpdateDirectionPlayer(PlayerAnimationController.DirectionFace.bottom);
            animator.SetNewAnimation(PlayerAnimationController.AnimationStat.walk_bottom);
        }
        else if (dir.x != 0)
        {
            animator.UpdateDirectionPlayer(PlayerAnimationController.DirectionFace.horizontal);
            animator.SetNewAnimation(PlayerAnimationController.AnimationStat.walk_horizontal);

            // flipX
            PlayerStat.Instance.spriteRenderer.flipX = dir.x < 0;
        }
    }

    void CheckItemOnGround(Vector2 dir)
    {
        CollectSystem.Instance.itemInDistance = null;
        CollectSystem.Instance.ItemScanFrontOfPlayer(dir, rayDistance);
    }

    public void Knockback(Vector2 fromPosition, float knockbackForce, float knockbackDuration)
    {
        if (isKnocked) return;
        isKnocked = true;

        Vector2 direction = (rb.position - fromPosition).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(StopKnockback(knockbackDuration));
    }

    IEnumerator StopKnockback(float knockbackDuration)
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
        isKnocked = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * rayDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * rayDistance);
    }
}
