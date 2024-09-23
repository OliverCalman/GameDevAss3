using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f) {
                float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
                timeFraction = Mathf.Pow(timeFraction, 3);
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos,
                                                          activeTween.EndPos,
                                                           timeFraction);                
            } else {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
            }

    }
}
