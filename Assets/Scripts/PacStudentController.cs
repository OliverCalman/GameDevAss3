
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
    private string animDirection;
    private ParticleSystem footsteps;
    private AudioSource walkAudio;
    // Start is called before the first frame update
    void Start()
    {
        //assign tweener, animator, and world maps
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        walkAudio = GetComponent<AudioSource>();
        tilemap = GameObject.FindWithTag("Tilemap").GetComponent<Tilemap>();
        footsteps = GameObject.Find("Footsteps").GetComponent<ParticleSystem>();
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
    }

    public void GetInput()
    {
        //get current keycode and assign it to lastinput
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
        //if lastinput is a valid movement, move and assign to current input. 
        if (CollisionDetector(lastInput) == false)
        {
            currentInput = lastInput;
            MovementAnimator();

        }
        else if (CollisionDetector(currentInput) == false) //if currentmovement is valid then move once able
        {
            MovementAnimator();
        }
        else //stop animating, stop footstep sound and stop particle system
        {
            footsteps.Stop();
            walkAudio.Stop();
            animator.enabled = false; 
        }
                                
    }
    public bool CollisionDetector(KeyCode input)
    {
        //get movement target based on input and assign correct animation to be used in movementhandler
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
        //if colliding with tile return true
        if (tilemap.GetTile<Tile>(tilemap.WorldToCell(movementTarget)) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void MovementAnimator()
    {
            tweener.AddTween(pacStudent.transform, pacStudent.transform.position, movementTarget, 0.5f);  
            animator.enabled = true;   
            animator.Play(animDirection);
            footsteps.Play();
            walkAudio.Play();
    }
} 