using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCompleteSound : MonoBehaviour
{
    AudioSource missionCompleteAS;
    public GameObject exitButton;

    private void Awake()
    {
        missionCompleteAS = GetComponent<AudioSource>();
        Invoke( "PlayEndDialogue", 2 );
        Invoke( "DisplayExitBtn", 16 );
    }

    void Start()
    {
        exitButton.SetActive( false );
    }

    
    void PlayEndDialogue()
    {
        if (missionCompleteAS != null)
        {
            missionCompleteAS.Play();
        }
    }

    void DisplayExitBtn()
    {
        if (exitButton != null)
        {
            exitButton.SetActive( true );
        }
    }
}
