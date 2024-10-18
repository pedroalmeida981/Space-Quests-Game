using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class InteractObject : MonoBehaviour
{
    private Outline[] outlines = null;
    private bool isMoving = false;

    [SerializeField] private UnityEvent actionPrimary = new UnityEvent();
    [SerializeField] private UnityEvent actionSecondary = new UnityEvent();

    private void Start()
    {
        Init();
    }

    // Outline items when mouse is over
    private void OnMouseOver()
    {
        if (FindObjectOfType<Player>().GetDistance(transform, 3))
        {
            if (!isMoving) SetOutlines(true);
            else SetOutlines(false);

            if (GetComponent<MoveObject>())
                isMoving = GetComponent<MoveObject>().UpdateMove();

            if (!isMoving)
            {
                if (Input.GetKeyDown(GameManager.instance.inputs.key_Interact1))
                    actionPrimary.Invoke();

                if (Input.GetKeyDown(GameManager.instance.inputs.key_Interact2))
                    actionSecondary.Invoke();
            }
        }
        else
        {
            SetOutlines(false);
        }
    }

    private void OnMouseExit()
    {
        SetOutlines(false);
    }

    private void SetOutlines(bool active)
    {
        if (outlines.Length > 0)
        {
            for (int i = 0; i < outlines.Length; i++)
            {
                outlines[i].enabled = active;
            }
        }
    }

    // Outline mode, color and width
    private void Init()
    {
        outlines = GetComponentsInChildren<Outline>();

        if (outlines.Length > 0)
        {
            for (int i = 0; i < outlines.Length; i++)
            {
                outlines[i].OutlineMode = GameManager.instance.options.outline_Mode;
                outlines[i].OutlineColor = GameManager.instance.options.outline_Color;
                outlines[i].OutlineWidth = GameManager.instance.options.outline_Width;
            }
        }

        SetOutlines(false);

        if (GetComponent<MoveObject>())
            GetComponent<MoveObject>().Init();
    }
}
