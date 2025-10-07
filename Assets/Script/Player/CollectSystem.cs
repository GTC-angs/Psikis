using UnityEngine;

public class CollectSystem : MonoBehaviour
{
    public static CollectSystem Instance;
    [SerializeField] LayerMask collisonLayer;
    public Item itemInDistance;

    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Collect();
    }

    public void ItemScanFrontOfPlayer(Vector3 direction, float rayDistance)
    {
        RaycastHit2D item = Physics2D.Raycast(transform.position, direction, rayDistance, collisonLayer);
        Debug.LogWarning('A');
        if (item)
        {
            Debug.LogWarning(item.collider.gameObject.name);
            GameObject objek = item.collider.gameObject;
            IsCollisionWithItem(objek.GetComponent<Item>());
        }
    }

    void IsCollisionWithItem(Item Objek)
    {
        if (Objek == null) return;
        Debug.Log($"Name item : {Objek.item.name} | count : {Objek.item.count}");
        itemInDistance = Objek;
    }

    void Collect()
    {
        if (itemInDistance == null) return;
        Debug.Log($"Collectted item {itemInDistance.item.name}");
        
        InventorySystem.Instance.GetItemOnGround(itemInDistance.item, () =>
        {
            Destroy(itemInDistance.gameObject);
        });

    }
}
