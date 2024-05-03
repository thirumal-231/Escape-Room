using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door_close : MonoBehaviour
{
    /*    public GameObject door;*/
    public string doorAnim;
    public Transform camLoc;
    public AudioSource doorAS;
    /*public AudioClip doorClip;*/

    public Animator dooranimtor;
    public bool isPlayed = false;
    public GameObject _textMeshPro;
    private bool highlightFrame = false;
     [SerializeField] Material emissionMat;
    private void Start()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "door_trigger")
        {
            Debug.Log("OnTriggerExit called");
            if (!isPlayed)
            {
                doorAS.Play();
                isPlayed = true;
                StartCoroutine(CallMethodAfterDelay());
                highlightFrame = true;
            }
            dooranimtor.Play(doorAnim);
            if (highlightFrame)
            {
                emissionMat.EnableKeyword("_EMISSION");
                emissionMat.DisableKeyword("_EMISSION");
                emissionMat.EnableKeyword("_EMISSION");
            }

        }
        


    }

    IEnumerator CallMethodAfterDelay()
    {
        yield return new WaitForSeconds(10f); // Wait for 10 seconds
        revealText();
    }

    void revealText()
    {
        _textMeshPro.SetActive(true);
    }

    private void Update()
    {
 /*       playDoorAnim();*/
    }


}
