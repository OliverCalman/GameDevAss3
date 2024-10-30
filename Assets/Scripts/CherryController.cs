using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherry;
    private Tweener tweener;
    private Vector3 startPosition;
    private Vector3 movementTarget;
    // Start is called before the first frame update
    void Start()
    {
        //just in case, destroy the existing cherry
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
            yield return new WaitForSeconds(2f);
            DoCherry();
        }

    }
    private void DoCherry()
    {
        //generate a random value on a range based on max x and y values. Maybe use the camera size
        //randomly pick whether x or y range will be used
        //depending on range used, select a side using a fixed value
        //i.e. if y is used, set x as either -14 or 32

        float x = Random.Range(-22f, 40f);
        float y = Random.Range(-26f, 10f);

        int a = Random.Range(0,2); 
        if (a == 0)
        {
            int b = Random.Range(0,2);
            if (b == 0)
            {
                startPosition = new Vector3(x, -26f, 0);
            }
            else if (b == 1)
            {
                startPosition = new Vector3(x, 10f, 0);
            }
        }
        else if (a == 1)
        {
            int b = Random.Range(0,2);
            if (b == 0)
            {
                startPosition = new Vector3(-22f, y, 0);
            }
            else if (b == 1)
            {
                startPosition = new Vector3(40f, y, 0);
            }
        }

        //get distance d of range from lowest value
        //set point on opposing range by subtracting distance d from max in range
        //use function to select side above to determine which side of screen inverse range is on

        //create cherry and set random start position
        Instantiate(cherry, startPosition, Quaternion.identity);

        //set movementtarget on opposite side of screen
        float xInv = 32 - x;
        float yInv = 26 - y;
        movementTarget = new Vector3(0, -5 ,0);

        //animate cherry
        tweener.AddTween(cherry.transform, startPosition, movementTarget, 5f);  
        Debug.Log(tweener.activeTween);
        //destroy cherry
        if (tweener.activeTween == null)
        {
            //destroy cherry once it finishes moving
            Destroy(cherry);
        }

    }
}
