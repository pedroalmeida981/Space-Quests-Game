using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Backpack : MonoBehaviour
{
    [HideInInspector] public Transform grid = null;
    
    public void Init()
    {
        grid = transform.Find("GridSlots");
    }
}
