using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "I_",menuName = "Scriptable/Item")]
public class Item : ScriptableObject
{
    public string itemName = string.Empty;
    public string itemDesc = string.Empty;
    public Sprite itemIcon = null;

    public int quantity = 1;
    public int quantityMax = 1;
    public bool stackable = false;
    public bool stackOnGround = true;

    public ItemType itemType = ItemType.None;

    public GameObject prf_Ground = null;
    public GameObject prf_Arm = null;
}
