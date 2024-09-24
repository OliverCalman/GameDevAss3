using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour {
    private Tween activeTween;
    void Start() 
    {

    }

    void Update() 
    {
        Debug.Log(activeTween);
        if (activeTween != null)
        {
            float distance = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);

            if (distance > 0.1f)
            {
                activeTween.Target.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, (Time.time - activeTween.StartTime) / activeTween.Duration); 
            }
            else 
            {
                activeTween.Target.transform.position = activeTween.EndPos;
                activeTween = null;
            }
        }
    }

    public void AddTween(Transform target, Vector3 startPos, Vector3 endPos, float duration)
    {
        activeTween = new Tween(target, startPos, endPos, Time.time, duration);
    }

}
