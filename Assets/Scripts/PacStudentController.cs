
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] private GameObject pacStudent;
    private Tweener tweener;
    private GameController gameController;
    private Animator animator;
    private Tilemap tilemap;
    private Vector3 startPosition = new Vector3(-3.5f,6.5f,0.0f);
    private KeyCode currentInput;
    private KeyCode lastInput;
    private Vector3 movementTarget;
    private string animDirection;
    private ParticleSystem footsteps;
    private AudioSource walkAudio;
  //  private AudioClip eatAudio;
   // private AudioClip footstepsAudio;
   // private AudioClip deathAudio;
    // Start is called before the first frame update
    void Start()
    {
        //assign tweener, animator, and world maps
        tweener = GetComponent<Tweener>();
        //gameController = GetComponent<GameController>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
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
        if (MovementValidator(lastInput) == false)
        {
            currentInput = lastInput;
            MovementAnimator();

        }
        else if (MovementValidator(currentInput) == false) //if currentmovement is valid then move once able
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
    public bool MovementValidator(KeyCode input)
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
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with " + collider.tag);
        switch (collider.tag)
        {
            case "Pellet":
                Destroy(collider.gameObject);
                //play eating sound
               // audioSource.PlayOneShot(eatAudio,1f);
                //add 10 to score
                gameController.KeepScore(10);
                break;
            case "PowerPellet":
                Destroy(collider.gameObject);
                //play eating sound
                //trigger scared state coroutine
                break;
            case "BonusScore":
                Destroy(collider.gameObject);
                //if cherry is moving then stop invoking tweener
                //CherryController.tweener = null;
                //add 100 to score
                gameController.KeepScore(100);
                break;
            case "Ghost":
                //check not in scared state. If scared then kill ghos
               // if (isScared == true)
               // {
                    //change ghost to death and lerp back to mapCentre (9f,-6.5f,0f) OR start point for that particular ghost
                //}
                //kill the player
                //play death animation
                //pause movement of ghosts or destroy them
                //pause cherry
                //remove a life
                break;
        }
    }
    private void Respawn()
    {
        //respawn if player still has health at start position
        //reset ghosts back to starting position
    }
} 