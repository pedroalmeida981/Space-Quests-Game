﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Uses the collider on the door to play the animation of closing or open
    Animator _doorAnim;

    private void OnTriggerEnter(Collider other)
    {
        _doorAnim.SetBool("isOpening", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _doorAnim.SetBool("isOpening", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
