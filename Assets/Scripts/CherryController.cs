using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] private GameObject cherry;
    private GameObject thisCherry;
    private Tweener tweener;
    private Vector3 startPosition;
    private Vector3 movementTarget;
    private float inverseX;
    private float inverseY;
    private Vector3 mapCentre = new(9f,-6.5f,0f);
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
            yield return new WaitForSeconds(10f);
            CreateCherry();

            tweener.AddTween(thisCherry.transform, thisCherry.transform.position, movementTarget, 5f); 


        }

    }
    private void CreateCherry()
    {
        if (thisCherry != null)
        {
            //destroy cherry
            Destroy(thisCherry);
        } 
        //generate start and end points
        GeneratePath();
        //create cherry at start position
        thisCherry = Instantiate(cherry, startPosition, Quaternion.identity);

    }
    private void GeneratePath()
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
                //movementTarget = new Vector3(inverseX, 10f, 0);
            }
            else if (b == 1)
            {
                startPosition = new Vector3(x, 10f, 0);
                //movementTarget = new Vector3(inverseX, -26f, 0);
            }
        }
        else if (a == 1)
        {
            int b = Random.Range(0,2);
            if (b == 0)
            {
                startPosition = new Vector3(-22f, y, 0);
                //movementTarget = new Vector3(40f, inverseY, 0);
            }
            else if (b == 1)
            {
                startPosition = new Vector3(40f, y, 0);
                //movementTarget = new Vector3(-22f, inverseY, 0);
            }
        }
        //set endpoint of movement based on centre position (must pass through) and start position
        //simple calculation to work out opposite point if passing through centre
        //i.e. on a square ranging from (0,0) to (10,10) where the midpoint is (5,5) and startpos is (0,10)
        //(10,0) = (5,5) - ((0,10)-(5,5))
        //(10,0) = (5,5) -      (5,5)
        movementTarget = mapCentre - (startPosition - mapCentre);
    }
}
