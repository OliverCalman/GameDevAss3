using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudent_Movement : MonoBehaviour
{
    [SerializeField] private GameObject pacStudent;
    private Tweener tweener;

    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
