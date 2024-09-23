using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public AudioClip Intro;
    public AudioClip GhostNormalState;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //intro music
        audioSource.clip = Intro;
        audioSource.PlayOneShot(Intro);
        
        //background - ghost normal state
        audioSource.clip = GhostNormalState;
        //delay by length of intro clip -2 to account for random quiet noise
        audioSource.PlayDelayed(Intro.length - 2.5f);
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
