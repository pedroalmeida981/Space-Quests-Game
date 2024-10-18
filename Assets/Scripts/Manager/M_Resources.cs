using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Resources : MonoBehaviour
{
    private Item[] itemsDataBase = null;

    // Initialize items from the folder "Scriptables/Items"
    public void Init()
    {
        itemsDataBase = Resources.LoadAll<Item>("Scriptables/Items");
    }

    //Find items by its name
    public Item GetItemByName(string _name)
    {
        if (itemsDataBase.Length <= 0)
            return null;

        for (int i = 0; i < itemsDataBase.Length; i++)
        {
            if (itemsDataBase[i].itemName == _name)
            {
                return Instantiate(itemsDataBase[i]);
            }
        }

        return null;
    }
}
