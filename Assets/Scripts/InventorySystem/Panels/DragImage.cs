﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DragImage : MonoBehaviour
{
    private Image im_Icon = null;
    private TMP_Text txt_Qt = null;

    // Color and numer in hud backpack
    private void Awake()
    {
        im_Icon = transform.Find("Icon").GetComponent<Image>();
        txt_Qt = transform.Find("Quantity").GetComponent<TMP_Text>();
        Refresh(null);
    }

    public void Refresh(Item item)
    {
        if (item == null)
        {
            im_Icon.sprite = null;
            im_Icon.color = new Color(255, 255, 255, 0);
            txt_Qt.text = string.Empty;
            return;
        }

        im_Icon.sprite = item.itemIcon;
        im_Icon.color = Color.white;
        im_Icon.type = Image.Type.Simple;
        im_Icon.preserveAspect = true;
        txt_Qt.text = (item.quantity > 1) ? item.quantity.ToString() : string.Empty;
    }
}