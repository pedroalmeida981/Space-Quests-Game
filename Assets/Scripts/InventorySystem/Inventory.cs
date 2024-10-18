using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ItemType
{
    //Slot accept.
    None, All,
    //Items.
    Consumable, Weapon, Ammo
}

public class Inventory : MonoBehaviour
{
    #region Variable
    public static Inventory instance = null;

    [HideInInspector] public Player player = null;
    [HideInInspector] public bool inventoryOpen = false;

    //Prefabs
    [SerializeField] public GameObject prf_Slot = null;
    
    //Panels
    [HideInInspector] public Panel_Backpack panel_Backpack = null;
    [HideInInspector] public Panel_Options panel_Options = null;

    //Drag and Drop
    private DragImage dragImage = null;
    private Slot startSlot = null;
    [HideInInspector] public Slot endSlot = null;
    [HideInInspector] public bool dragEnable = false;

    #endregion

    #region Unity Methods.
    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (GetComponent<Canvas>().isActiveAndEnabled)
            GetComponent<Canvas>().enabled = false;
    }

    // Item sprites follows the mouse
    private void Update()
    {
        if (dragEnable)
        {
            dragImage.transform.localPosition = (Input.mousePosition)
                - GetComponent<Canvas>().transform.localPosition;
        }
    }
    #endregion

    #region
    // Referencing 
    public void Init(Player _player)
    {
        player = _player;

        panel_Backpack = GetComponentInChildren<Panel_Backpack>();
        panel_Backpack.Init();

        panel_Options = GetComponentInChildren<Panel_Options>();
        panel_Options.Init();

        dragImage = transform.Find("DragImage").GetComponent<DragImage>();
    }
    #endregion

    // Show and hide  Inventory
    #region Show Hide Inventory.
    public void ShowHide_Inventory()
    {
        inventoryOpen = !inventoryOpen;

        GetComponent<Canvas>().enabled = inventoryOpen;

        player.SetController(!inventoryOpen);
        GameManager.instance.SetCursor(inventoryOpen);

        panel_Options.Hide_Options();
    }
    #endregion

    // Add items
    #region Items.
    public bool AddItems(Item item)
    {
        if (item == null || item.quantity <= 0)
            return false;

        List<Slot> listSlots = GetSlots(panel_Backpack.grid);
        if (listSlots.Count <= 0)
            return false;

        Slot slotFound = listSlots.FirstOrDefault(
            p => p.currentItem != null
            && p.currentItem.stackable
            && p.currentItem.quantity + item.quantity <= item.quantityMax);

        // Inventory info
        if (slotFound != null)
        {
            slotFound.currentItem.quantity += item.quantity;
            slotFound.Refresh();
        }
        else
        {
            slotFound = listSlots.FirstOrDefault(p => p.currentItem == null);
            if (slotFound == null)
            {
                print("Inventory full !");
                player.EjectObject(item);
                return true;
            }
            slotFound.ChangeItem(item);
        }

        return true;
    }

    // Add items to an empty slot
    public void AddItemsEmptySlot(Item item)
    {
        if (item == null || item.quantity <= 0)
            return;

        List<Slot> listSlots = GetSlots(panel_Backpack.grid);
        if (listSlots.Count <= 0)
            return;

        Slot slotFound = listSlots.FirstOrDefault(p => p.currentItem == null);
        if (slotFound == null)
        {
            print("Inventory full !");
            player.EjectObject(item);
            return;
        }

        slotFound.ChangeItem(item);
    }
    #endregion

    #region Slots.
    private List<Slot> GetSlots(Transform grid)
    {
        if (grid == null || grid.childCount <= 0)
            return null;

        List<Slot> slots = new List<Slot>();

        for (int i = 0; i < grid.childCount; i++)
        {
            slots.Add(grid.GetChild(i).GetComponent<Slot>());
        }

        return slots;
    }
    #endregion

    #region Drag & Drop.
    // Drag and Drop items
    public void StartDrag(Slot slot)
    {
        if (slot == null || slot.currentItem == null)
            return;

        dragEnable = true;
        dragImage.Refresh(slot.currentItem);

        startSlot = slot;
        startSlot.Set_SelectImage(true);
    }

    // Stop deagging
    public void StopDrag()
    {
        if (startSlot != null)
            startSlot.Set_SelectImage(false);

        dragEnable = false;
        dragImage.Refresh(null);

        if (endSlot != null)
            ChangeItemSlot();
    }

    // Move items between slots
    private void ChangeItemSlot()
    {
        if (startSlot == endSlot || startSlot.currentItem == null)
            return;

        ItemType startItemType = startSlot.currentItem.itemType;
        ItemType endSlotType = endSlot.itemAccepted;

        if (endSlotType == ItemType.All ||
            (endSlotType != ItemType.All && startItemType == endSlotType))
        {
            Item itemEndSlot = endSlot.currentItem;

            if (CheckItemSlot(endSlot, startSlot.currentItem) &&
                CheckItemSlot(startSlot, endSlot.currentItem))
            {
                //Same items.
                if (itemEndSlot != null &&
                    itemEndSlot.itemName == startSlot.currentItem.itemName
                    && itemEndSlot.stackable)
                {
                    while (endSlot.currentItem.quantity < endSlot.currentItem.quantityMax)
                    {
                        if (startSlot.currentItem.quantity > 0)
                        {
                            endSlot.currentItem.quantity++;
                            startSlot.currentItem.quantity--;
                        }
                        else
                            break;
                    }

                    startSlot.Refresh();
                    endSlot.Refresh();

                    startSlot = null;
                    endSlot = null;
                    return;
                }

                endSlot.ChangeItem(startSlot.currentItem);
                startSlot.ChangeItem(itemEndSlot);
            }
        }

        startSlot = null;
        endSlot = null;
    }

    // Item slot verification
    private bool CheckItemSlot(Slot slot, Item item)
    {
        if (item == null) return true;
        if (slot.itemAccepted == ItemType.All) return true;

        return (slot.itemAccepted == item.itemType);
    }
    #endregion
}
