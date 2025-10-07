using UnityEngine;

public class CollectSystem : MonoBehaviour
{
    public static CollectSystem Instance;
    [SerializeField] LayerMask collisonLayer; // menampung layer untuk collect
    public Item itemInDistance; // menampung item yang bersentuhan dengan player

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
        // cek raycast di arah player
        RaycastHit2D item = Physics2D.Raycast(transform.position, direction, rayDistance, collisonLayer);
    
        if (item) // jika ada yg kedetect
        {
            // Debug.LogWarning(item.collider.gameObject.name);
            GameObject objek = item.collider.gameObject; // dapatkan gameobject
            IsCollisionWithItem(objek.GetComponent<Item>()); // validasi item, apakah valid?
        }
    }

    /// <summary>
    /// fungsi untuk mendeteksi player bersentuhan dengan item / object lain
    /// </summary>
    /// <param name="Objek"></param>
    void IsCollisionWithItem(Item Objek)
    {
        if (Objek == null) return; // jika object tidak memiliki script Item
        Debug.Log($"Name item : {Objek.item.name} | count : {Objek.item.count}");
        itemInDistance = Objek; // set object yang bsa di interact
    }

    /// <summary>
    /// Fungsi yng dijalankan ketika player interact E unut collect
    /// </summary>
    void Collect()
    {
        if (itemInDistance == null) return;
        Debug.Log($"Collectted item {itemInDistance.item.name}");
        
        // call event Inventory system
        InventorySystem.Instance.GetItemOnGround(itemInDistance.item, () =>
        {
            // destroy item on world
            Destroy(itemInDistance.gameObject);
        });

    }
}
