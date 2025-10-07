using UnityEngine;
using UnityEngine.Events;

public class TriggerQuest : MonoBehaviour
{
    [SerializeField] QuestSO questSO; // quest yang akan dijadikan active
    bool isUsed = false; // is still affect to add Qouest active

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;
        
        // cek yang bersentuhan apakah player
        PlayerMovement isPlayer = collision.gameObject.GetComponent<PlayerMovement>();
        if (isPlayer)
        {
            QuestManager.Instance.AddQuestToActive(questSO);
            isUsed = true;
        }
    }
}
