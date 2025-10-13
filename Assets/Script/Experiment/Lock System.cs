using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;


public class LockSystem : MonoBehaviour
{
 
    // item, messagelock, message open, audio, isOpen, collider2d void Open
    // action : open, back
    // trigger ? on collide or on key input?
    [SerializeField] ItemSO itemForUnlock;
    [SerializeField] string messageLock, messageOpen;
    [SerializeField] AudioClip audioMessage;
    [SerializeField] AudioSource audioSource; 
    [SerializeField] bool isOpen = false, isOnArea = false;
    
    public enum InteractType
    {
        Collide, InputKey
    };


    Collider2D collider2d;
    SpriteRenderer spriteRenderer;
    
    Interact interactSc;

    void Start()
    {
        collider2d = gameObject.GetComponent<Collider2D>();
        interactSc = gameObject.GetComponent<Interact>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && interactSc.isInteract)
        {
            HUDManager.Instance.ChooseActionUI();
            interactSc.isInteract = false;
        } 

        if (interactSc.interactType != Interact.InteractType.InputKey) return;
        if (Input.GetKeyDown(interactSc.keyCode) && !interactSc.isInteract && isOnArea)
        {
            interactSc.isInteract = true;
            StartCoroutine(HUDManager.Instance.PrepareUILockSystem(interactSc));
        }
    }

    public void TryOpen()
    {
        ItemSO itemInInventory = InventorySystem.Instance.itemListOnInventory.Find(itemIn => itemIn.id == itemForUnlock.id);
        
        if (itemInInventory != null)
        {
            SlotScript slotUIItem = InventorySystem.Instance.listSlotScript.Find(itemSlot => itemSlot.itemSO.id == itemInInventory.id);
            // kurangi count
            itemInInventory.count -= 1;
            slotUIItem.UpdateUiCountItem();
            // turn off collider
            // change color
            collider2d.enabled = false;
            spriteRenderer.color = new Color32(124, 252, 0, 255);
            StartCoroutine(HUDManager.Instance.ShowActionFeedback(messageOpen, audioSource, audioMessage));
        }

        else
        {
            StartCoroutine(HUDManager.Instance.ShowActionFeedback(messageLock, audioSource, audioMessage));
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        isOnArea = true;

        if (interactSc.interactType == Interact.InteractType.Collide && !interactSc.isInteract)
        {
            interactSc.isInteract = true;
            Debug.Log("Udah prepare UI");
            StartCoroutine(HUDManager.Instance.PrepareUILockSystem(interactSc));
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        interactSc.isInteract = false;
        isOnArea = false;
    }
}
