using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVitals : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth;
    public int healthFallRate;

    public Slider thirstSlider;
    public int maxThirst;
    public int thirstFallRate;

    public Slider hungerSlider;
    public int maxHunger;
    public int hungerFallRate;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        thirstSlider.maxValue = maxThirst;
        thirstSlider.value = maxThirst;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;
    }

    // Vitals of th Player
    void Update()
    {
        //Health
        if (hungerSlider.value <= 0 && (thirstSlider.value <= 0))
        {
            healthSlider.value -= Time.deltaTime / healthFallRate * 2;
        }
        else if (hungerSlider.value <= 0 || thirstSlider.value <= 0)
        {
            healthSlider.value -= Time.deltaTime / healthFallRate;
        }
        if (healthSlider.value <= 0)
        {
            CharacterDeath();
        }

        //Hunger
        if (hungerSlider.value >= 0)
        {
            hungerSlider.value -= Time.deltaTime / hungerFallRate;
        }
        else if (hungerSlider.value <= 0)
        {
            hungerSlider.value = 0;
        }
        else if (hungerSlider.value >= maxHunger)
        {
            hungerSlider.value = maxHunger;
        }

        //Thirst
        if (thirstSlider.value >= 0)
        {
            thirstSlider.value -= Time.deltaTime / thirstFallRate;
        }
        else if (thirstSlider.value <= 0)
        {
            thirstSlider.value = 0;
        }
        else if (thirstSlider.value >= maxThirst)
        {
            thirstSlider.value = maxThirst;
        }
    }

    // When the bar gets to the end the game restarts
    public void CharacterDeath()
    {
        if (healthSlider.value == 0)
        {
            Destroy(gameObject);
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
