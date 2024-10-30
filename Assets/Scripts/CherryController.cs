using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] private GameObject cherry;
    private Tweener tweener;
    private Animator animator;
    private Vector3 startPosition;
    private Vector3 movementTarget;
    // Start is called before the first frame update
    void Start()
    {
        //just in case, destroy the existing cherry
      //  Destroy(cherry);
        tweener = GetComponent<Tweener>();
        StartCoroutine(CherryTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CherryTimer()
    {
        while (true)
        {
            CreateCherry();
            yield return new WaitForSeconds(10f);
        }
    }
    private void CreateCherry()
    {
        //if cherry doesn't exist then create it and set random start position
        Instantiate(cherry);

        float x = Random.Range(-14f, 32f);
        float y = Random.Range(10f, 26f);

        startPosition = new Vector3(x, y, 0);
        cherry.transform.position = startPosition;

        //set movementtarget on opposite side of screen
        float xInv = 32 - x;
        float yInv = 26 - y;
        movementTarget = new Vector3(0, -5 ,0);

        //upper left bound is (-14,10,0)
        //lower left bound is (-14,26,0)
        //upper right bound is (32,10.0)
        //lower right bound is (32,26,0)

        //animate cherry
        tweener.AddTween(cherry.transform, startPosition, movementTarget, 5f);  

        //destroy cherry
       // Destroy(cherry);

    }
}
