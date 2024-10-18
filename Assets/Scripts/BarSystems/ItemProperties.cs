using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    [Header("Your Consumable")]
    public string itemName;

    [SerializeField] private bool food;
    [SerializeField] private bool water;
    [SerializeField] private bool health;
    [SerializeField] private float value;

    [SerializeField] private PlayerVitals playerVitals;

    // Consumables slide bars
    public void Interaction()
    {
        if (food)
        {
            playerVitals.hungerSlider.value += value;
        }
        else if (water)
        {
            playerVitals.thirstSlider.value += value;
        }
        else if (health)
        {
            playerVitals.healthSlider.value += value;
        }
    }
}
