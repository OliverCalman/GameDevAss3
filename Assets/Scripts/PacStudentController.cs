
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] private GameObject pacStudent;
    private Tweener tweener;
    private Animator animator;
    private Tilemap tilemap;
    private Vector3 startPosition = new Vector3(-3.5f,6.5f,0.0f);
    private KeyCode currentInput;
    private KeyCode lastInput;
    private Vector3 movementTarget;
    private String animDirection;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        tilemap = GameObject.FindWithTag("Tilemap").GetComponent<Tilemap>();
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
                animDirection = "Up";
                break;
            case KeyCode.A:
                movementTarget = pacStudent.transform.position + Vector3.left;
                animDirection = "Left";
                break;
            case KeyCode.S:
                movementTarget = pacStudent.transform.position + Vector3.down;
                animDirection = "Down";
                break;
            case KeyCode.D:
                movementTarget = pacStudent.transform.position + Vector3.right;
                animDirection = "Right";
                break;
        }

        if (CollisionDetector(movementTarget) == false)
        {
            tweener.AddTween(pacStudent.transform, pacStudent.transform.position, movementTarget, 0.5f);  
            animator.Play(animDirection);
        }
                                
    }

    public bool CollisionDetector(Vector3 movementTarget)
    {
        
        Tile adjacentTile = tilemap.GetTile<Tile>(tilemap.WorldToCell(movementTarget));
        //if colliding with tile return true
        if (adjacentTile != null)
        {
            Debug.Log("collision");
            return true;
        }
        else
        {
            currentInput = lastInput;
            return false;
        }
    }
} 