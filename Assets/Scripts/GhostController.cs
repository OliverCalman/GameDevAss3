using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Animator animator;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
      if (gameController.scaredState == true)
      {
        StartCoroutine(IsTerrified());
      }
    }
    IEnumerator IsTerrified()
    {
        //set animation as scared
        animator.Play("Scared");
        yield return new WaitForSeconds(7f);
        //set animation as recovering
        animator.Play("Recovering");
        yield return new WaitForSeconds(3f);
        //return animation to normal state
        animator.Play("Left");
    }
}
