using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private UserInputs inputActions;
    private InputAction movement;
   // private InputAction onPanel;
    Rigidbody rb;
    public GameObject panel;
    private bool isOpened = false;
    private float speedFactor = 8f;
    public Transform AttackCube;
    public Transform LongRangeAttackCube;
    private StatContainer StatContainer;
    private bool firstSkillLearned;
    public GameObject player;
    public MajorSkill skill1;
    public MajorSkill skill2;
    public MajorSkill skill3;
    public FamilyTree tree;
    //public event Action<InputAction.CallbackContext> openPanel;
    // Start is called before the first frame update
    void Start()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.name);
        if (currentScene.name == "Stage") {
           // skill1.isLearned = false;
            skill2.isLearned = false;
            skill3.isLearned = false;
            openPanel();
        }
        
        rb = GetComponent<Rigidbody>();
        inputActions = new UserInputs();
        inputActions.Player.Enable();
        movement = inputActions.Player.Movement;
        movement.Enable();
        //inputActions.Player.CallOutPanel.performed += openConsole;
        
        //panel.GetComponent<CanvasGroup>().alpha = 1;
        StatContainer = GetComponentInChildren<StatContainer>();
   
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 v2 = movement.ReadValue<Vector2>();

        //Vector2 v2;
        //v2.x = Input.GetAxis("Horizontal");
        //v2.y = Input.GetAxis("Vertical");

        //Debug.Log(v2.x + " " + v2.y);
        Vector3 v3 = new Vector3(1f * (v2.x), 0, 1f * (v2.y));

        //v3.x = Mathf.Clamp(v3.x, 0, 0.08f);
        //v3.z = Mathf.Clamp(v3.z, 0, 0.08f);
        //v3.y = Mathf.Clamp(v3.y, 0, 0.08f);
        Vector3 finsihedVol = Quaternion.Euler(0, -45, 0) * v3.normalized * StatContainer.GetSpeed() * Time.deltaTime * speedFactor;
        transform.Translate(finsihedVol);
    }

    //public void openConsole(InputAction.CallbackContext context)
    //{
        
    //    //Debug.Log("close");
    //    //panel.GetComponent<Renderer>().enabled = true;
    //    if (isOpened)   // close the panel
    //    {
    //        isOpened = !isOpened;
    //        panel.GetComponent<CanvasGroup>().alpha = 1;
    //        panel.GetComponent<CanvasGroup>().interactable = true;
    //    }
    //    else     // open the panel
    //    {
    //        isOpened = !isOpened;
            
    //        panel.GetComponent<CanvasGroup>().alpha = 0;
    //        panel.GetComponent<CanvasGroup>().interactable = false;
    //    }
    //}
    public void ClosePanel() {
        
        panel.GetComponent<CanvasGroup>().alpha = 0;
        panel.GetComponent<CanvasGroup>().interactable = false;
        firstSkillLearned = true;
    }
    private void Reset()
    {
        //SkillHolder dummy = GetComponent<SkillHolder>();
        //dummy.skill.resetLearned();
        //Debug.Log("Resetting" + dummy.skill.name);
    }
    public void openPanel()
    {
        
        Reset();
        //MajorSkill skill = player.GetComponent<MajorSkill>();
        //skill.resetLearned();

        panel.GetComponent<CanvasGroup>().alpha = 1;
        panel.GetComponent<CanvasGroup>().interactable = true;

    }

    public Vector3 getCube() {
        return AttackCube.position;
    }

    public Vector3 getLongRangeAttackCube()
    {
        return LongRangeAttackCube.position;
    }

    public FamilyTree GetTheTree() {
        return tree;
    }

}
