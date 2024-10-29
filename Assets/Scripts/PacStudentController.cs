
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
        //assign tweener, animator, and world maps
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        tilemap = GameObject.FindWithTag("Tilemap").GetComponent<Tilemap>();
        //reset pacstudents position
        pacStudent.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
            //get current keydown 
            GetInput();

            //check pacstudent is not lerping
            if (tweener.activeTween == null)
            {
                //pass input and attempt movement
               MovementHandler(lastInput);
            }
            /*
            Check lastInput and try to move in that direction to the
            adjacent grid position.
            If the adjacent grid position from lastInput is walkable, then
            store lastInput in a member variable called “currentInput” 
            */
    }

    public void GetInput()
    {
            if(Input.GetKeyDown(KeyCode.W))
            {
                lastInput = KeyCode.W;
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                lastInput = KeyCode.A;
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                lastInput = KeyCode.S;
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                lastInput = KeyCode.D;
            }
    }

    public void MovementHandler(KeyCode input)
    {
        Debug.Log("last Input = " + lastInput);
        Debug.Log("Current input = " + currentInput);

        InputHandler(lastInput);

        if (CollisionDetector(movementTarget) == false)
        {
            currentInput = lastInput;
            tweener.AddTween(pacStudent.transform, pacStudent.transform.position, movementTarget, 0.5f);  
            animator.Play(animDirection);
        }
        else if (CollisionDetector(movementTarget) == true)
        {
            
            //if currentinput equals a tile in tilemap/collision from collisionDetector
            //then stop movement
            //else move to the next tile
            //this should have the effect of allowing direction changes only when the direction can be changed
            //i.e. hold down key and once wall disappears then pacstudent will move 
            //ONLY if the previous lerp has finished, so PacStudent is in middle off map
            //Check distance to movementtarget is close to 0.0 to determine lerp is done

            //move last input. Store next input as currentInput until no collision is recorded and lerp is done, then clear current input.
        }
                                
    }

    public bool CollisionDetector(Vector3 movementTarget)
    {
        //if colliding with tile return true
        if (tilemap.GetTile<Tile>(tilemap.WorldToCell(movementTarget)) != null)
        {
            Debug.Log("collision");
            return true;
        }
        else
        {
            return false;
        }
    }
    private void InputHandler(KeyCode input)
    {
       // if (input != KeyCode.None)
       // {
            switch(input)
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
       // }
    }
} 