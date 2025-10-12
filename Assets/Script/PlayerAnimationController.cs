using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    static public PlayerAnimationController Instance;
    public enum AnimationStat
    {
        idle_horizontal,
        idle_top,
        idle_bottom,
        walk_top,
        walk_horizontal,
        walk_bottom
    }
    public enum DirectionFace
    {
        top, bottom, horizontal
    };


    public AnimationStat currentAnimation { get; private set; }
    public Animator animator { get; private set; }
    public DirectionFace directionPlayer { get; private set; } // bottom, top, horizontal
    public string currentTrigger { get; private set; }
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        currentAnimation = AnimationStat.idle_bottom;
        directionPlayer = DirectionFace.bottom;

    }

    public void CallTriggerIdle()
    {
        switch (directionPlayer)
        {
            case DirectionFace.top:
                SetNewAnimation(AnimationStat.idle_top);
                break;
            case DirectionFace.bottom:
                SetNewAnimation(AnimationStat.idle_bottom);
                break;
            case DirectionFace.horizontal:
                SetNewAnimation(AnimationStat.idle_horizontal);
                break;
        }
    }

    public void SetNewAnimation(AnimationStat newAnimation)
    {
        if (currentAnimation == newAnimation) return;
        currentAnimation = newAnimation;
        animator.Play(currentAnimation.ToString());
    }

    public void UpdateDirectionPlayer(DirectionFace newDir)
    {
        if (directionPlayer == newDir) return;
        directionPlayer = newDir;
    }
}
