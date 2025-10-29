using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Enter Object : {collision.gameObject.name}");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"Exit Object : {collision.gameObject.name}");
    }

}
