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
        }
        }
    }

}
