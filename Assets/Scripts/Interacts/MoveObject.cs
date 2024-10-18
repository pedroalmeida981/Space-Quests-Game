using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Player player = null;
    private Rigidbody rb = null;
    private bool moving = false;
    
    public void Init()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
    }

    // Inputs
    public bool UpdateMove()
    {
        if (Input.GetKeyDown(GameManager.instance.inputs.key_MoveObject) && !moving)
            Move_Object();
        if (Input.GetKeyUp(GameManager.instance.inputs.key_MoveObject) && moving)
            Drop_Object();
        if (Input.GetKeyDown(GameManager.instance.inputs.key_Drop) && moving)
            Eject_Object();

        return moving;
    }

    // Move object
    private void Move_Object()
    {
        rb.isKinematic = true;
        transform.parent = player.fpsCamera.transform;
        moving = true;
    }

    //Drop object
    private void Drop_Object()
    {
        rb.isKinematic = false;
        transform.parent = null;
        moving = false;
    }

    // Eject object
    private void Eject_Object()
    {
        rb.isKinematic = false;
        transform.parent = null;
        moving = false;

        rb.AddForce(player.fpsCamera.transform.forward 
            * (player.stats.ejectForce / rb.mass));
    }
}
