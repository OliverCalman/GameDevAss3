
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
    private Vector3 startPosition = new Vector3(-3.5f,6.5f,0.0f);
    private KeyCode currentInput;
    private KeyCode lastInput;
    private Vector3 movementTarget;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        pacStudent.transform.position = startPosition;
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

            //check for an in input to start game
            if (lastInput != KeyCode.None)
            {
                MovementHandler();
            }
    }

    public void MovementHandler()
    {
        Debug.Log(lastInput);
        switch(lastInput)
        {
            case KeyCode.W:
                movementTarget = pacStudent.transform.position + Vector3.up;
                animator.Play("Up");
                break;
            case KeyCode.A:
                movementTarget = pacStudent.transform.position + Vector3.left;
                animator.Play("Left");
                break;
            case KeyCode.S:
                movementTarget = pacStudent.transform.position + Vector3.down;
                animator.Play("Down");
                break;
            case KeyCode.D:
                movementTarget = pacStudent.transform.position + Vector3.right;
                animator.Play("Right");
                break;
        }

        tweener.AddTween(pacStudent.transform, pacStudent.transform.position, movementTarget, 0.5f);                                
    }
} 