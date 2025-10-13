using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement Instance;
    public bool isCanMove = true;
    [SerializeField] Transform CharacterTransform;
    [SerializeField] float moveSpeed = 1.5f, runningSpeed = 1f, delayFindLocation = 3f;
    public Vector2 lastMoveDir = Vector2.right; // default kanan
    public Vector3 targetPosition;

    void Start()
    {
        Instance = this;
        InvokeRepeating("FindTargetPosition", 0f, delayFindLocation);
    }

    void FindTargetPosition()
    {
        targetPosition = CharacterTransform.position;
    }

    public IEnumerator RunningForSecond(float duration, float runningS = 2f)
    {
        Debug.LogWarning("We running");
        runningSpeed = runningS;
        float time = 0;
        while (time <= duration)
        {
            FindTargetPosition();
            time += Time.deltaTime;
        }
        yield return new WaitForSeconds(duration);
        runningSpeed = 1f;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, targetPosition);

        if (EnemyAttack.Instance.currentState != EnemyAttack.EnemyState.Charge)
            lastMoveDir = (targetPosition - transform.position).normalized;

        if (isCanMove)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * (moveSpeed * runningSpeed));
    }
}
