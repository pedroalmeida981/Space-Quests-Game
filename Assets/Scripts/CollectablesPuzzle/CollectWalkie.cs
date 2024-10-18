using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWalkie : MonoBehaviour
{
    public static int objects = 0;
    public AudioSource collectSound;

    // Use this for initialization
    void Awake()
    {
        objects++;
    }

    // Collect of the walkies to trigger a door
    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
            objects--;
        gameObject.SetActive(false);
        collectSound.Play();
    }
}
