using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    public GameObject Tool;
    Animator animator;

    public bool raised = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = Tool.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // FollowMouse();   
        // SwingTool();

    }

    void FollowMouse()
    {
        mousePosition = Input.mousePosition;
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }


    //! called via string reference
    public void SwingTool()
    {
            if (!raised) {
                animator.SetTrigger("Raise");
                raised = true;
            } else {
                animator.SetTrigger("Swing");
                raised = false;
            }
    }


}
