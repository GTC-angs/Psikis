using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotScript : MonoBehaviour
{
    public ItemSO itemSO;
    [SerializeField] RawImage textureIconItem;
    [SerializeField] TMP_Text countItemText;
    [SerializeField] CanvasGroup canvasGroup;

    public bool isEmpty = true;

    void Start()
    {
        canvasGroup.alpha = 0f;
        countItemText.text = "";
    }

    public void UpdateUiCountItem()
    {
        if (itemSO.count >= 1)
        {
            countItemText.text = itemSO.count.ToString();
        }
        else
        {
            canvasGroup.alpha = 0f;
            textureIconItem.texture = null;
            countItemText.text = "";
            isEmpty = true;
        }
         
    }

    public bool AddItemToSlot(ItemSO item)
    {
        canvasGroup.alpha = 1f;
        textureIconItem.texture = item.texture;
        // countItemText.text = item.count.ToString();

        // check item di inventory
        bool IsEmptyOnInventory = true;

        foreach (ItemSO itemInInventory in InventorySystem.Instance.itemListOnInventory)
        {
            Debug.Log('A');
            if (itemInInventory.id == item.id)
            {
                // sudah ada di inventory
                // check isStack
                if (item.isStackable)
                {
                    // check isStack count
                    if (itemInInventory.count < itemInInventory.maxStackable)
                    {
                        Debug.Log('B');
                        // masi ada slot stack
                        itemInInventory.count += 1;

                        countItemText.text = itemInInventory.count.ToString();
                        return true;
                    }
                    else
                    {
                        Debug.Log('C');
                        // sudah penuh
                        continue;
                    }
                }
            }
        }

        if (!isEmpty) return false; // mencegah slot lama diganti dengan yg baru 

        if (IsEmptyOnInventory)
        {
            Debug.Log('D');
            ItemSO newItem = CreateNewItemSO(item);
            // countItemText.text = newItem.count.ToString();

            if (!newItem.isStackable) countItemText.text = ""; // jika item tidak bisa distack jangan tampilkan UI count

            InventorySystem.Instance.itemListOnInventory.Add(newItem);
            isEmpty = false;
            return true;
        }


        Debug.Log('E');
        return false;

    }

    ItemSO CreateNewItemSO(ItemSO item)
    {
        ItemSO newItem = ScriptableObject.CreateInstance<ItemSO>();
        newItem.id = item.id;
        newItem.name = item.name;
        newItem.isStackable = item.isStackable;
        newItem.count = 1;
        newItem.maxStackable = item.maxStackable;
        newItem.texture = item.texture;
        newItem.deskripsi = item.deskripsi;

        countItemText.text = newItem.count.ToString();
        itemSO = newItem;

        return newItem;
    }

    public void EventClickSlot()
    {
        if (itemSO == null) return;

        InventorySystem.Instance.currentItemDetail = itemSO;
        InventorySystem.Instance.UpdateContentDetail();
    }
}
