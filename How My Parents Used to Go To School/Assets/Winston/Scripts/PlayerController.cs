using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private UserInputs inputActions;
    private InputAction movement;
   // private InputAction onPanel;
    Rigidbody rb;
    public GameObject panel;
    private bool isOpened = false;
    //public event Action<InputAction.CallbackContext> openPanel;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new UserInputs();
        inputActions.Player.Enable();
        movement = inputActions.Player.Movement;
        //movement.Enable();
        inputActions.Player.CallOutPanel.performed += openConsole;
        panel.GetComponent<CanvasGroup>().alpha = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 v2 = movement.ReadValue<Vector2>();

        Vector3 v3 = new Vector3(0.2f * (v2.x), 0, 0.2f * (v2.y));

        transform.Translate(v3);


    }

    public void openConsole(InputAction.CallbackContext context)
    {
       // openPanel.Invoke();
        //Debug.Log("close");
        //panel.GetComponent<Renderer>().enabled = true;
        if (isOpened)   // close the panel
        {
            isOpened = !isOpened;
            panel.GetComponent<CanvasGroup>().alpha = 0;
            panel.GetComponent<CanvasGroup>().interactable = false;
        }
        else     // open the panel
        {
            isOpened = !isOpened;
            panel.GetComponent<CanvasGroup>().alpha = 1;
            panel.GetComponent<CanvasGroup>().interactable = true;
        }
    }
}
