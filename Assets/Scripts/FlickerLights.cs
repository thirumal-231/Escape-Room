using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    [SerializeField] BoxCollider lightZoneCollider;
    [SerializeField] Transform mainCam;
    [SerializeField] Transform trigger;

    [SerializeField] AudioSource radio;
    [SerializeField] AudioDataSO audios;


    private Vector3 camPos;
    private bool isRadioOn;

    void Start()
    {
    }

    void Update()
    {
        //Debug.Log( timer );

        camPos = trigger.position;
        if( lightZoneCollider.bounds.Contains( camPos ) )
        {
            
            PlayRadio();
        }

    }

    

    

    void PlayRadio()
    {
        if(isRadioOn)
        { return; }
        radio.PlayOneShot( audios.radioClip );
        isRadioOn = true;
    }
}
