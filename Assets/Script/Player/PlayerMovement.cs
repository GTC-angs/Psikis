using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public enum path { Top, Down, Left, Right };
    public bool isCanMoveInput = true, isMove = false;
    bool isRunning = false;
    [SerializeField] float speedCharacterMoveTransition = 0.2f;
    [SerializeField] LayerMask collisionMaskObstacle; // layer dinding/halangan
    [SerializeField] float rayDistance = 1f, runningSpeedTransition;    // jarak ray sesuai step per move

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        // running state
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) isRunning = false;

        if (!isCanMoveInput) return;

        if (Input.GetKey(KeyCode.W)) HandleDirectionMove(path.Top);
        if (Input.GetKey(KeyCode.A)) HandleDirectionMove(path.Left);
        if (Input.GetKey(KeyCode.S)) HandleDirectionMove(path.Down);
        if (Input.GetKey(KeyCode.D)) HandleDirectionMove(path.Right);
    }

    void HandleDirectionMove(path pathDirection)
    {
        if (!isCanMoveInput) return;
        Vector3 direction = Vector3.zero;

        switch (pathDirection)
        {
            case path.Top: direction = Vector3.up; break;
            case path.Down: direction = Vector3.down; break;
            case path.Left: direction = Vector3.left; break;
            case path.Right: direction = Vector3.right; break;
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
