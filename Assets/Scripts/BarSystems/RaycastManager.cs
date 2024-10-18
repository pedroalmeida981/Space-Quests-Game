using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaycastManager : MonoBehaviour
{
    private GameObject raycastedObj;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLenght = 10;
    [SerializeField] private LayerMask newLayerMask;

    [Header("References")]
    [SerializeField] private Image crossHair;
    [SerializeField] private TMP_Text itemNameText;

    // Ray cast manager of the player
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLenght, newLayerMask.value))
        {
            if (hit.collider.CompareTag("Consumable"))
            {
                CrosshairActive();
                raycastedObj = hit.collider.gameObject;
                itemNameText.text = raycastedObj.GetComponent<ItemProperties>().itemName;

                if (Input.GetMouseButtonDown(0))
                {
                    raycastedObj.GetComponent<ItemProperties>().Interaction();
                    raycastedObj.SetActive(false);
                }
            }
        }
        else
        {
            CrosshairNormal();
            itemNameText.text = null;
        }
    }

    //  Crosshair
    void CrosshairActive()
    {
        crossHair.color = Color.yellow;
    }

    void CrosshairNormal()
    {
        crossHair.color = Color.red;
    }
}
