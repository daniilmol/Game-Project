using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private Camera mainCamera;
    public Camera care;
    // Update is called once per frame
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;



    //Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    private Vector3 mousePos;
    //Vector3 object_pos;
    //float angle;
    private void Awake()
    {
        target = GetComponent<Transform>();
    }
    void Update()
    {
        Ray ray = care.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetVec = hit.point;
            targetVec.y = target.position.y;
            transform.LookAt(targetVec);
            //transform.LookAt(hit.point); // Look at the point
            //transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)); // Clamp the x and z rotation
        }
        //mouse_pos = Input.mousePosition;
        //mouse_pos.z = 5.23f; //The distance between the camera and object
        //object_pos = Camera.main.WorldToScreenPoint(target.position);
        //mouse_pos.x = mouse_pos.x - object_pos.x;
        //mouse_pos.y = mouse_pos.y - object_pos.y;
        //angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        //target.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
        //    GetMousePosition();
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hitInfo;

        //if (Physics.Raycast(ray, out hitInfo, 200))
        //{
        //    Vector3 targetVec = hitInfo.point;
        //    targetVec.y = target.position.y;
        //    target.LookAt(targetVec);
        //}
        //}

        //private void GetMousePosition()
        //{
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit raycastHit))
        //    {
        //        mousePos = raycastHit.point;
        //    }
        //}
    }
}
