using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private string itemName = string.Empty;

    private Item refItem = null;

    #region Actions.
    // Items pick up
    public void PickItem()
    {
        bool result = Inventory.instance.AddItems(GetItem());
        if (result)
            Destroy(gameObject);
    }

    // Collectitem destroying him from the environment and adding it to the inventory 
    public void Action_EquipItem()
    {
        bool result = Quickslot.instance.SetItemToSlot(GetItem());
        if (result)
            Destroy(gameObject);
    }
    #endregion

    // Find the item
    private Item GetItem()
    {
        if (refItem == null)
        {
            refItem = GameManager.instance.resources.GetItemByName(itemName);
        }

        return refItem;
    }

    // Set the item
    public void SetItem(Item item)
    {
        if (item == null)
            return;

        refItem = item;
    }
}
