using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public ItemSO currentItemDetail;
    public List<ItemSO> itemListOnInventory;
    [SerializeField] List<SlotScript> listSlotScript;
    bool isOpen = false;

    [Header("===UI===")]
    [Space(10)]
    [SerializeField] CanvasGroup CG_Inventory;
    [SerializeField] CanvasGroup CG_detailItem, CG_textureDetailItem;
    [SerializeField] TMP_Text textItemName, textItemCount, textItemDeskripsi;
    [SerializeField] RawImage textureItem;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isOpen) OpenInventory();
            else CloseInventory();
        }
    }

    public void OpenInventory()
    {
        if (PlayerMovement.Instance.isMove) return;
        
        CG_Inventory.alpha = 1f;
        CG_Inventory.blocksRaycasts = true;
        CG_Inventory.interactable = true;

        isOpen = true;

        // turn off player movement
        PlayerMovement.Instance.isCanMoveInput = false;
    }

    public void CloseInventory()
    {
        CG_Inventory.alpha = 0f;
        CG_detailItem.blocksRaycasts = false;
        CG_detailItem.interactable = false;


        CG_detailItem.alpha = 0f;
        CG_detailItem.blocksRaycasts = false;
        CG_detailItem.interactable = false;

        isOpen = false;

        // turn on player movement
        PlayerMovement.Instance.isCanMoveInput = true;
    }

    public void GetItemOnGround(ItemSO item, Action callback)
    {
        bool isComplite = false;
        foreach (SlotScript slot in listSlotScript)
        {
            if (!isComplite)
            {
                if (slot.isEmpty)
                    isComplite = slot.AddItemToSlot(item);
                else
                {
                    if (item.id == slot.itemSO.id && item.isStackable)
                        isComplite = slot.AddItemToSlot(item);
                }
            }
            else
            {
                break;
            }
        }

        callback.Invoke();
    }

    public void UpdateContentDetail()
    {
        textItemName.text = currentItemDetail.name;
        textItemCount.text = $"Count : {currentItemDetail.count}";
        textItemDeskripsi.text = currentItemDetail.deskripsi;
        if (currentItemDetail.texture)
        {
            textureItem.texture = currentItemDetail.texture;
            CG_textureDetailItem.alpha = 1f;
        }

        CG_detailItem.alpha = 1f;
        CG_detailItem.blocksRaycasts = true;
        CG_detailItem.interactable = true;
    }
}
