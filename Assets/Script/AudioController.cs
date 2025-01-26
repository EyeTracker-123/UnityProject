using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    

    public TETConnect eyeTracker;

    private GameObject tet;

    public float minVolume;

    public float maxVolume;

    private bool isIncrease;

    //private bool isCoruting = false;

    private bool fixFlag = false;
    // Start is called before the first frame update

    private void Awake()
    {
        tet = GameObject.Find("EyeTrackerController");
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        eyeTracker = tet.GetComponent<TETConnect>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool isFixed = 
        /*if(flag == false){
            audioSource.volume = maxVolume;
        }
        else{
            audioSource.volume = minVolume;
            
        }*/
        
        fixFlag = eyeTracker.TET.values.frame.fix;
        
        

        //Debug.Log(isIncrease);
        if(fixFlag == true){
            Debug.Log("true");
            audioSource.volume = minVolume;
            Debug.Log(audioSource.volume);
            StartCoroutine(increase());
        }
        else if(isIncrease == false){
            Debug.Log("false");
            audioSource.volume = maxVolume;
            Debug.Log(audioSource.volume);
            
        }
    }
    
    IEnumerator increase(){
        isIncrease = true;
        yield return new WaitForSeconds(2f);

        isIncrease = false;
    } 

   
}
