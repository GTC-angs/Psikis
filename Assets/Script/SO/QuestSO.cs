using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/QuestSO")]
public class QuestSO : ScriptableObject
{
    public string IDKey, questTitle, questHint; // IDKey is unique
    [TextArea] public string questDesc;

    public bool isCompleted = false, isActive = true, isShowProgress = true;

    public int requiredCount, currentCount = 0; // tracking current progress & kemauan quest

    public UnityEvent OnQuestCompleted; // event ketika quest selesai

    /// <summary>
    /// Sebuah fungsi untuk berkomunikasi di script lain. dan mengatur OnQuestCompleted event
    /// </summary>
    /// <param name="action"></param>
    public void AddOnCompletedListener(UnityAction action)
    {
        OnQuestCompleted.AddListener(action);
    }
}
