using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    public enum AnimState { idle, attack };

    public static EnemyAnimator Instance;

    void Start()
    {
        Instance = this;
    }
    public void PlayAnimation(AnimState animState)
    {
        animator.Play(animState.ToString(), 0, 0);
    }
}
