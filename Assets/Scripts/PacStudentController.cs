
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
    private bool tilemapCollision;
    [SerializeField] private ParticleSystem footsteps;
    [SerializeField] private ParticleSystem tilemapCollisionParticle;
    [SerializeField] private ParticleSystem deathAcid;
    private Vector3 startPosition = new Vector3(-3.5f,6.5f,0.1f);
    private KeyCode currentInput;
    private KeyCode lastInput;
    private Vector3 movementTarget;
    private string animDirection;
    private AudioSource audioSource;
    [SerializeField] private AudioClip eatAudio;
    [SerializeField] private AudioClip footstepsAudio;
    [SerializeField] private AudioClip deathAudio;
    [SerializeField] private AudioClip collisionAudio;
    [SerializeField] private AudioClip deadGhost; 
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        //gameController.PauseGameTimer();
        //assign tweener, animator, and world maps
        tweener = GetComponent<Tweener>();
        //gameController = GetComponent<GameController>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        tilemap = GameObject.FindWithTag("Tilemap").GetComponent<Tilemap>();
        footsteps.GetComponent<ParticleSystem>();
        tilemapCollisionParticle.GetComponent<ParticleSystem>();
        deathAcid.GetComponent<ParticleSystem>();
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
            gameController.StartGameTimer();

        }
        else if (MovementValidator(currentInput) == false) //if currentmovement is valid then move once able
        {
            MovementAnimator();
        }
        else if (tilemapCollision == false && lastInput != KeyCode.None)//stop animating, stop all other sounds except the bump and stop walk particle system
        {
            tilemapCollision = true;
            footsteps.Stop();
            audioSource.PlayOneShot(collisionAudio,1f);
            animator.enabled = false; 
            tilemapCollisionParticle.transform.position = 0.5f * (transform.position + movementTarget);
            tilemapCollisionParticle.Play();
        }
                                
    }
    public bool MovementValidator(KeyCode input)
    {
        if (dead == false)
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
                tilemapCollision = false;
                return false;
            }
        } else
        {
            return false;
        }
    }
    public void MovementAnimator()
    {
           // audioSource.Stop();
        if (dead == false)
        {
            tweener.AddTween(pacStudent.transform, pacStudent.transform.position, movementTarget, 0.4f);  
            animator.enabled = true;   
            animator.Play(animDirection);
            audioSource.PlayOneShot(footstepsAudio,1f);
            footsteps.Play();
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with " + collider.tag);
        switch (collider.tag)
        {
            case "LeftTeleporter":
                //teleport to right teleporter position and continue moving
                    movementTarget = new Vector3(22.5f,-6.5f,0);
                    pacStudent.transform.position = movementTarget;
                    MovementAnimator();
                break;
            case "RightTeleporter":
                //teleport to left teleporter position and continue moving
                    movementTarget = new Vector3(-4.5f,-6.5f,0);
                    pacStudent.transform.position = movementTarget;
                    MovementAnimator();
                break;
            case "Pellet":
                Destroy(collider.gameObject);
                gameController.RemovePellet();
                //play eating sound
                audioSource.Stop();
                audioSource.PlayOneShot(eatAudio,1f);
                //add 10 to score
                gameController.KeepScore(10);
                break;
            case "PowerPellet":
                Destroy(collider.gameObject);
                gameController.RemovePellet();
                //play eating sound
                audioSource.Stop();
                audioSource.PlayOneShot(eatAudio,1f);
                //trigger scared state coroutine
                gameController.ScareGhosts();
                break;
            case "BonusScore":
                Destroy(collider.gameObject);
                //if cherry is moving then stop invoking tweener
                //CherryController.tweener = null;
                audioSource.Stop();
                audioSource.PlayOneShot(eatAudio,1f);
                //add 100 to score
                gameController.KeepScore(100);
                break;
            case "Ghost":
                //check not in scared state. If scared then kill ghos
                if (gameController.scaredState == true)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(deadGhost,1f);
                    //change ghost to death and lerp back to mapCentre (9f,-6.5f,0f) 
                    //if ghost is alive (don't score off an already dead ghost!)
                    gameController.KeepScore(300);
                    collider.transform.position = new Vector3(9f,-6.5f,0f);
                }
                else if (gameController.scaredState == false)
                {
                    YouDied();
                }
                break;
        }
    }
    private void YouDied()
    {
        Debug.Log("You Died");
        dead = true;
        //kill the player
        audioSource.Stop();
        audioSource.PlayOneShot(deathAudio,1f);
        //play death animation
        animator.Play("Dead");
        deathAcid.Play();
        animator.speed = 1f;
        currentInput = KeyCode.None;
        lastInput = KeyCode.None;  
        movementTarget = startPosition; 
        //pause movement of ghosts or destroy them
        Respawn();
        //pause cherry
        //remove a life        
    }
    private void Respawn()
    {
        gameController.RemoveLife();
        //respawn if player still has health at start position
        //animator.enabled = true;   
        StartCoroutine(respawnWait());
        //reset ghosts back to starting position
    }
    private IEnumerator respawnWait()
    {
        yield return new WaitForSeconds(2f);
        pacStudent.transform.position = startPosition;
        dead = false;
    }
} 