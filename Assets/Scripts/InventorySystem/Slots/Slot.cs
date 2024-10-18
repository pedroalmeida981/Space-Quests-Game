using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum SlotType {  None, Quick}

public class Slot : MonoBehaviour
{
    [HideInInspector] public Item currentItem = null;
    public ItemType itemAccepted = ItemType.All;
    public SlotType slotType = SlotType.None;

    private Image im_Enter = null;
    private Image im_Select = null;
    private Image im_Icon = null;
    private TMP_Text txt_Quantity = null;

    // Slot fields
    private void Awake()
    {
        im_Enter = transform.Find("Enter").GetComponent<Image>();
        im_Select = transform.Find("Select").GetComponent<Image>();
        im_Icon = transform.Find("Icon").GetComponent<Image>();
        txt_Quantity = transform.Find("Quantity").GetComponent<TMP_Text>();

        im_Enter.enabled = false;
        im_Select.enabled = false;
    }

    // Quick slots
    private void Update()
    {
        if (slotType == SlotType.Quick)
        {
            Update_Quickslot_Actions();
        }
    }

    // Items
    #region Items.
    public void ChangeItem(Item item)
    {
        currentItem = item;
        Refresh();
    }

    // Delete item
    public void DeleteItem()
    {
        if (currentItem == null || currentItem.quantity <= 0)
            return;

        currentItem.quantity --;
        if (currentItem.quantity <= 0)
        {
            ChangeItem(null);
            return;
        }

        Refresh();
    }

    public void DeleteItem(int quantity)
    {
        if (currentItem == null || currentItem.quantity <= 0 || quantity <= 0)
            return;

        currentItem.quantity -= quantity;
        if (currentItem.quantity <= 0)
        {
            ChangeItem(null);
            return;
        }

        Refresh();
    }

    // Refresh
    public void Refresh()
    {
        if (currentItem != null && currentItem.quantity <= 0)
            currentItem = null;
        
        Refresh_Icon();
        Refresh_Quantity();
    }

    // Refresh Icon
    private void Refresh_Icon()
    {
        if (currentItem == null)
        {
            im_Icon.sprite = null;
            im_Icon.color = new Color(255, 255, 255, 0);
            return;
        }

        im_Icon.sprite = currentItem.itemIcon;
        im_Icon.type = Image.Type.Simple;
        im_Icon.preserveAspect = true;
        im_Icon.color = Color.white;
    }

    // Refresh Quantity
    private void Refresh_Quantity()
    {
        if (currentItem == null)
        {
            txt_Quantity.text = string.Empty;
            return;
        }

        txt_Quantity.text = (currentItem.quantity > 1) 
            ? currentItem.quantity.ToString() : string.Empty;
    }
    #endregion

    // UI
    #region UI.
    public void Set_SelectImage(bool active)
    {
        im_Select.enabled = active;
    }
    #endregion

    //Mouse Events
    #region Mouse Events.
    // Enter
    public void MouseEvent_Enter()
    {
        im_Enter.enabled = true;

        if (Inventory.instance.dragEnable)
            Inventory.instance.endSlot = this;
    }

    // Exit
    public void MouseEvent_Exit()
    {
        im_Enter.enabled = false;

        Inventory.instance.endSlot = null;
    }

    // Select
    public void MouseEvent_Select(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;
        if (pointer.button == PointerEventData.InputButton.Right)
        {
            Inventory.instance.panel_Options.Show_Options(this);
        }
    }

    // Down
    public void MouseEvent_Down()
    {
        if (Inventory.instance.dragEnable)
            return;

        if (Input.GetKey(KeyCode.Mouse0) && currentItem != null)
        {
            Inventory.instance.StartDrag(this);
        }
    }

    // Up
    public void MouseEvent_Up()
    {
        Inventory.instance.StopDrag();
    }
    #endregion

    // Actions
    #region Actions.
    private void Update_Quickslot_Actions()
    {
       if (currentItem != null)
        {
            if (Input.GetKeyDown(GameManager.instance.inputs.key_Drop))
            {
                Inventory.instance.panel_Options.Action_Drop(this);
                Quickslot.instance.Selection();
            }
        }
    }
    #endregion
}