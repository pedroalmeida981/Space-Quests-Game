using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Options : MonoBehaviour
{
    private Transform gridButtons = null;
    private Slot slotSelected = null;

    [Header("Buttons :")]
    [SerializeField] private GameObject btn_DropAll = null;
    [SerializeField] private GameObject btn_Split = null;

    public void Init()
    {
        gridButtons = transform.Find("GridButtons");

        Hide_Options();
    }

    #region Show Hide.
    // Options on the inventory
    public void Show_Options(Slot slot)
    {
        if (slot == null || slot.currentItem == null)
            return;

        slotSelected = slot;
        slotSelected.Set_SelectImage(true);
        
        gameObject.SetActive(true);

        //Buttons.
        btn_DropAll.SetActive(slotSelected.currentItem.quantity > 1);
        btn_Split.SetActive(slotSelected.currentItem.quantity > 1);

        gridButtons.localPosition = slot.transform.localPosition;
    }

    public void Hide_Options()
    {
        if (slotSelected != null)
            slotSelected.Set_SelectImage(false);

        slotSelected = null;
        
        gameObject.SetActive(false);
    }
    #endregion

    // Actions
    #region Actions.
    public void Action_Drop(Slot slot)
    {
        if (slot == null || slot.currentItem == null || slot.currentItem.quantity <= 0)
            return;

        slotSelected = slot;
        EventBtn_Drop();
    }
    #endregion

    // Events Buttons
    #region Events Buttons.
    // Drop items
    public void EventBtn_Drop()
    {
        if (slotSelected == null || slotSelected.currentItem == null)
        {
            Hide_Options();
            return;
        }

        Item item = Instantiate(slotSelected.currentItem);
        item.quantity = 1;
        Inventory.instance.player.EjectObject(item);

        slotSelected.DeleteItem();

        Hide_Options();
    }

    //Drop all items
    public void EventBtn_DropAll()
    {
        if (slotSelected == null || slotSelected.currentItem == null)
        {
            Hide_Options();
            return;
        }

        int qtItems = slotSelected.currentItem.quantity;

        if (slotSelected.currentItem.stackOnGround)
        {
            Item item = Instantiate(slotSelected.currentItem);
            Inventory.instance.player.EjectObject(item);
            slotSelected.ChangeItem(null);
        }
        else
        {
            for (int i = 0; i < qtItems; i++)
            {
                Item item = Instantiate(slotSelected.currentItem);
                item.quantity = 1;
                Inventory.instance.player.EjectObject(item);

                slotSelected.DeleteItem();
            }

            Hide_Options();
        }  
    }

    // Split items
    public void EventBtn_Split()
    {
        if (slotSelected == null || slotSelected.currentItem == null)
        {
            Hide_Options();
            return;
        }

        //Rest.
        int rest = Mathf.RoundToInt(slotSelected.currentItem.quantity / 2);
        //print("Rest : " + rest);

        //Items to empty slot.
        Item item = Instantiate(slotSelected.currentItem);
        item.quantity = rest;
        Inventory.instance.AddItemsEmptySlot(item);

        slotSelected.DeleteItem(rest);

        Hide_Options();
    }
    #endregion
}
