using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordHandler : MonoBehaviour
{
    [SerializeField] string crtPin = "1234";
    [SerializeField] TextMeshProUGUI inputField;
    [SerializeField] Animator cupboardAnim;

    private string enteredPin = "";

    void Start()
    {
        inputField.text = $"Enter PIN";
    }

    void Update()
    {
        
    }

    public void TakePin(int digit)
    {
        if( enteredPin.Length >= 4 )
        {
            return;
        }

        enteredPin += $"{digit}";
        Debug.Log( $"{digit} entered" );
        DisplayPinMessage();
    }

    public void SubmitPin()
    {
        if(crtPin == enteredPin)
        {
            Debug.Log( "Correct pin" );
            inputField.text = $"Success";
            // play animation of vault door
            cupboardAnim.SetBool( "isUnlock", true );

        }
        else
        {
            Debug.Log( "Try again" );
            inputField.text = $"Try again";
            Cancel();
        }
    }

    public void Cancel()
    {
        enteredPin = "";
        DisplayPinMessage();
    }

    public void DisplayPinMessage()
    {
        inputField.text = enteredPin;
    }

}
