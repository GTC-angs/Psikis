using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public int id, count = 1, maxStackable = 64;
    public string name, deskripsi;
    public bool isStackable;
    public Texture2D texture;
}
