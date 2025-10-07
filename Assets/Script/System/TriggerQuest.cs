using UnityEngine;
using UnityEngine.Events;

public class TriggerQuest : MonoBehaviour
{
    [SerializeField] QuestSO questSO;
    bool isUsed = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;
        // if(collision.)
        PlayerMovement isPlayer = collision.gameObject.GetComponent<PlayerMovement>();
        if (isPlayer)
        {
            QuestManager.Instance.AddQuestToActive(questSO);
            isUsed = true;
        }
    }
}
