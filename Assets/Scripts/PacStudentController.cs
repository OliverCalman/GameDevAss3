
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
    private KeyCode currentInput;
    private KeyCode lastInput;
    private Vector3 movementTarget;
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
            
            MovementHandler(); 

    }

    public void MovementHandler()
    {
        Debug.Log(lastInput);

                if (lastInput == KeyCode.D)
                {
                    movementTarget = pacStudent.transform.position + Vector3.right;
                    animator.Play("Right");
                }
                if (lastInput == KeyCode.S)
                {
                    movementTarget = pacStudent.transform.position + Vector3.down;
                    animator.Play("Down");
                }
                if (lastInput == KeyCode.A)
                {
                     movementTarget = pacStudent.transform.position + Vector3.left;
                    animator.Play("Left");
                }
                if (lastInput == KeyCode.W)
                {
                        movementTarget = pacStudent.transform.position + Vector3.up;
                    animator.Play("Up");
                }

        tweener.AddTween(pacStudent.transform, pacStudent.transform.position, movementTarget, 0.5f);                                
    }
} 