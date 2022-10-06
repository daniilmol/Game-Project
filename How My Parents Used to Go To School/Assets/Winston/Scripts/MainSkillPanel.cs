//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class MainSkillPanel : MonoBehaviour
//{
//    public GameObject panel;
//    private UserInputs inputActions;
//    private InputAction onPanel;
//    private bool isOpened = false;
//    public void Awake()
//    {
//        inputActions = new UserInputs();
//        onPanel = inputActions.Player.CallOutPanel;
//    }
//    public void Update()
//    {
//        //if (onPanel)
//        //{
//        //    //Instantiate(console);
//        //    openConsole();
           

//        //}

//    }

//    public void openConsole()
//    {
//        panel.GetComponent<Renderer>().enabled = true;
//        if (isOpened)
//        {
//            isOpened = !isOpened;
//            for (int a = 0; a < transform.childCount; a++)
//            {
//                transform.GetChild(a).gameObject.SetActive(false);
//                Debug.Log("close");
//            }
//        }
//        else {
//            isOpened = !isOpened;
//            for (int a = 0; a < transform.childCount; a++)
//            {
//                transform.GetChild(a).gameObject.SetActive(true);
//                Debug.Log("Open");
//            }
//        }
//    }



//}