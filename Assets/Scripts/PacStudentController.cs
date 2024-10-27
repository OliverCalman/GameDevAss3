using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] private GameObject pacStudent;
    private Tweener tweener;
    private Animator animator;
    KeyCode currentInput;
    KeyCode lastInput;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetKeyDown(KeyCode.W))
                lastInput = KeyCode.W;
            if(Input.GetKeyDown(KeyCode.A))
                lastInput = KeyCode.A;
            if(Input.GetKeyDown(KeyCode.S))
                lastInput = KeyCode.S;
            if(Input.GetKeyDown(KeyCode.D))
                lastInput = KeyCode.D;

        Debug.Log(lastInput);   
        MovementHandler();

    }

    public void MovementHandler()
    {
        Vector3 topLeft = new Vector3(-3.2f, 5.98f, 0.0f);
        Vector3 topRight = new Vector3(1.83f, 5.98f, 0.0f);
        Vector3 bottomRight = new Vector3(1.83f, 2.0f, 0.0f);
        Vector3 bottomLeft = new Vector3(-3.2f, 2.0f, 0.0f);

                if (lastInput == KeyCode.D)
                {
                    tweener.AddTween(pacStudent.transform, pacStudent.transform.position, topRight, 1.5f);
                    animator.Play("Right");
                }
                if (lastInput == KeyCode.S)
                {
                    tweener.AddTween(pacStudent.transform, pacStudent.transform.position, bottomRight, 1.5f);
                    animator.Play("Down");
                }
                if (lastInput == KeyCode.A)
                {
                    tweener.AddTween(pacStudent.transform, pacStudent.transform.position, bottomLeft, 1.5f);
                    animator.Play("Left");
                }
                if (lastInput == KeyCode.W)
                {
                    tweener.AddTween(pacStudent.transform, pacStudent.transform.position, topLeft, 1.5f);
                    animator.Play("Up");
                }
    }

}
