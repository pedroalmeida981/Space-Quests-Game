using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // InputValue
    [System.Serializable]
    internal class InputValue
    {
        public float axe_H;
        public float axe_V;
        public float axe_Hr;
        public float axe_Vr;

        public bool run;
        public bool jump;
    }

    /// <summary>
    /// Player movement
    /// </summary>
    // Stats
    [System.Serializable]
    public class Stats
    {
        [Header("Speed Walk :")]
        public float walkSpeed = 3;
        public float runSpeed = 6;
        public float jumpPower = 6;
        public float ejectForce = 800;

        [Header("Smooth :")]
        public float smoothWalkSpeed = 3;
        public float smoothAirSpeed = 1;
        public float smoothStopSpeed = 1;
    }

    // Status
    [System.Serializable]
    internal class Status
    {
        public bool canMove = true;
        
        public bool isWalking = false;
        public bool isRunning = false;
        public bool isInAir = false;
    }

    [SerializeField] private InputValue inp = new InputValue();
    [SerializeField] public Stats stats = new Stats();
    [SerializeField] private Status status = new Status();

    private CharacterController cc = null;
    [HideInInspector] public FpsCamera fpsCamera = null;

    private Vector3 move = new Vector3();
    private Vector3 moveAir = new Vector3();

    [Header("Debug :")]
    [SerializeField] private float currentWalkSpeed = 0;
    [SerializeField] private float currentGravity = 0;
    [SerializeField] private bool initialized = false;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (initialized)
        {
            Update_Inputs_HUD();

            if (status.canMove)
            {
                Update_Inputs_Movements();

                Motor();
                fpsCamera.CameraRotation(inp.axe_Vr);
                Running();
            }
            else
            {
                StopPlayer();
            }

            Jumping();
        }
    }

    private void FixedUpdate()
    {
        if (initialized)
        {
            cc.Move(move * Time.fixedDeltaTime);
        }
    }

    // Inventory and quickslot interacting with player and camera
    private void Init()
    {
        cc = GetComponent<CharacterController>();
        fpsCamera = GetComponentInChildren<FpsCamera>();
        fpsCamera.Init();

        if (Inventory.instance != null)
            Inventory.instance.Init(this);
        if (Quickslot.instance != null)
            Quickslot.instance.Init(this);

        initialized = true;
    }

    // Movement inputs
    private void Update_Inputs_Movements()
    {
        inp.axe_H = Input.GetAxis("Horizontal");
        inp.axe_V = Input.GetAxis("Vertical");
        inp.axe_Hr = Input.GetAxis("Mouse X") 
            * Time.deltaTime * GameManager.instance.options.MouseSensitivity;
        inp.axe_Vr = Input.GetAxis("Mouse Y")
            * Time.deltaTime * GameManager.instance.options.MouseSensitivity;

        inp.run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        inp.jump = Input.GetKeyDown(KeyCode.Space);
    }

    // Inputed key to open inventory
    private void Update_Inputs_HUD()
    {
        if (Input.GetKeyDown(GameManager.instance.inputs.key_Inventory))
            Inventory.instance.ShowHide_Inventory();
    }
    
    // Character controller motor
    private void Motor()
    {
        if (status.isInAir)
        {
            move = moveAir;
        }
        else
        {
            move = new Vector3(inp.axe_H, 0, inp.axe_V);
        }

        if (move.magnitude > 1)
            move.Normalize();

        transform.Rotate(0, inp.axe_Hr, 0);

        if (!status.isInAir)
        move = transform.rotation * move;
    }

    // Character controller running
    private void Running()
    {
        bool inpDir = (inp.axe_H != 0 || inp.axe_V != 0);

        status.isWalking = !status.isRunning && inpDir;

        if (inp.run && inpDir && !status.isInAir)
        {
            currentWalkSpeed = Mathf.Lerp(currentWalkSpeed, stats.runSpeed, 
                Time.deltaTime * stats.smoothWalkSpeed);
            status.isRunning = true;
        }
        else
        {
            if (status.isWalking && !status.isInAir)
            {
                currentWalkSpeed = Mathf.Lerp(currentWalkSpeed, stats.walkSpeed,
                    Time.deltaTime * stats.smoothWalkSpeed);
                status.isRunning = false;
            }
            else
            {
                if (status.isInAir)
                {
                    currentWalkSpeed = Mathf.Lerp(currentWalkSpeed, 0,
                    Time.deltaTime * stats.smoothAirSpeed);
                    status.isRunning = false;
                }
                else
                {
                    currentWalkSpeed = Mathf.Lerp(currentWalkSpeed, 0,
                    Time.deltaTime * stats.smoothWalkSpeed);
                    status.isRunning = false;
                }
            }
        }

        move = currentWalkSpeed * move;
    }

    // Character controller jump
    private void Jumping()
    {
        if (inp.jump && !status.isInAir && status.canMove)
        {
            currentGravity = stats.jumpPower;
            moveAir = move;
        }
        else 
        { 
            if (cc.isGrounded)
            {
                currentGravity = GameManager.instance.options.gravityEarth;
                status.isInAir = false;
            }
            else
            {
                status.isInAir = true;
                currentGravity += GameManager.instance.options.gravityEarth 
                    * Time.deltaTime;
            }
        }

        move.y = currentGravity;
    }

    // Stop player
    private void StopPlayer()
    {
        move.x = Mathf.Lerp(move.x, 0, Time.deltaTime * stats.smoothStopSpeed);
        move.z = Mathf.Lerp(move.z, 0, Time.deltaTime * stats.smoothStopSpeed);
    }

    // Get distance
    public bool GetDistance(Transform obj, float distMax)
    {
        float dist = Vector3.Distance(obj.position, fpsCamera.transform.position);
        return (dist <= distMax);
    }

    public void SetController(bool active)
    {
    status.canMove = active;
    }

    // Throw the object
    public void EjectObject(Item item)
    {
        if (item == null || item.quantity <= 0 || item.prf_Ground == null)
            return;

        GameObject g = Instantiate(item.prf_Ground,
            fpsCamera.targetEject.position, fpsCamera.targetEject.rotation);

        if (g.GetComponent<Rigidbody>())
        {
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.AddForce(fpsCamera.transform.forward
            * (stats.ejectForce / rb.mass));
        }

        if (g.GetComponent<PickupObject>())
            g.GetComponent<PickupObject>().SetItem(item);
    }
}
