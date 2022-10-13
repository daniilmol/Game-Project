using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private float mouseSensitivity;

    // REFERENCE
    private Transform parent;

    private void Start()
    {
        parent = transform.parent;

        // hide the mouse and lock it in the midle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();


        // Press the Escape to apple no locking to the Cursor
        /*if (Input.GetKey(KeyCode.Escape))
        {
            UnlockCursor();
        }*/
    }

    // mouse controlls the camera directin
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        parent.Rotate(Vector3.up, mouseX);
    }

    //private void UnlockCursor()
    //{
    //    Cursor.lockState = CursorLockMode.None;
    //}
}
