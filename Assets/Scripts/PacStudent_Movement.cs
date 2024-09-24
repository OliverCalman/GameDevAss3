using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacStudent_Movement : MonoBehaviour
{
    [SerializeField] private GameObject pacStudent;
    private Tweener tweener;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
        StartCoroutine(DemoMovement());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator DemoMovement()
    {
        Vector3 topLeft = new Vector3(-3.2f, 5.98f, 0.0f);
        Vector3 topRight = new Vector3(1.83f, 5.98f, 0.0f);
        Vector3 bottomRight = new Vector3(1.83f, 2.0f, 0.0f);
        Vector3 bottomLeft = new Vector3(-3.2f, 2.0f, 0.0f);

        while (true)
        {

                if (pacStudent.transform.position == topLeft)
                {
                    tweener.AddTween(pacStudent.transform, pacStudent.transform.position, topRight, 1.5f);
                    animator.Play("Right");
                }
                if (pacStudent.transform.position == topRight)
                {
                    tweener.AddTween(pacStudent.transform, pacStudent.transform.position, bottomRight, 1.5f);
                    animator.Play("Down");
                }
                if (pacStudent.transform.position == bottomRight)
                {
                    tweener.AddTween(pacStudent.transform, pacStudent.transform.position, bottomLeft, 1.5f);
                    animator.Play("Left");
                }
                if (pacStudent.transform.position == bottomLeft)
                {
                    tweener.AddTween(pacStudent.transform, pacStudent.transform.position, topLeft, 1.5f);
                    animator.Play("Up");
                }

               yield return null;
        }
    }  
}
