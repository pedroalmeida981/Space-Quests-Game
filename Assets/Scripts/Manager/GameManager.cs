using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(M_Resources))]
[RequireComponent(typeof(M_Options))]
[RequireComponent(typeof(M_Inputs))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [HideInInspector] public M_Options options = null;
    [HideInInspector] public M_Inputs inputs = null;
    [HideInInspector] public M_Resources resources = null;

    public bool CheckHUDactive
    {
        get
        {
            if (Inventory.instance != null)
                return Inventory.instance.inventoryOpen;
            else return false;
        }
    }

    // Get manager components
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            options = GetComponent<M_Options>();
            inputs = GetComponent<M_Inputs>();
            resources = GetComponent<M_Resources>();
            resources.Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetCursor(false);
    }

    // Make cursor visible or not visible
    public void SetCursor(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = (visible) ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
