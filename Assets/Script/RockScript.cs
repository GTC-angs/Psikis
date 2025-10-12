using System.Collections;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public Transform target;
    public bool isThrow = false;
    public float speedThrow = 30f, knockForce = 20f;

    public IEnumerator StartThrow(Transform targetTransform, float durationFall = 0.3f)
    {
        yield return new WaitUntil(() => !PlayerMovement.Instance.isMove);
        PlayerMovement.Instance.isCanMoveInput = false;
        target = targetTransform;
        isThrow = true;
        yield return new WaitForSeconds(durationFall);
        isThrow = false;
        PlayerMovement.Instance.isCanMoveInput = true;
        PlayerThrowSystem.Instance.GO_ThrowObject = null;

    }

    void Update()
    {
        if (isThrow)
        {
            transform.position = Vector3.MoveTowards(

            transform.position,                         // posisi sekarang
            target.position,                            // posisi tujuan
            speedThrow * Time.deltaTime * speedThrow   // kecepatan

            );

        }
    }
}
