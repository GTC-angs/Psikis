using UnityEngine;

public class TriggerChangeCam1 : MonoBehaviour
{
    [SerializeField] GameObject camA, camB;
 BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.transform.position.y > transform.position.y)
        {
            Scene04Manager.Instance.StartLabyrinthDialog();
            boxCollider.isTrigger = false;
            camA.SetActive(false);
            camB.SetActive(true);
        }
    }
}
