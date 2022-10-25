using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private float mouseSensitivity;

    // REFERENCE
    private Transform parent;
    public Transform target;

    private void Start()
    {
        parent = transform.parent;

        // hide the mouse and lock it in the midle of the screen
        // Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Rotate();
        // transform.position = new Vector3(0, 0, 0);


    }

    // mouse controlls the camera directin
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        parent.Rotate(Vector3.up, mouseX);
    }
}
