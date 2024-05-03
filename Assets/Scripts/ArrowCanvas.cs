using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCanvas : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ChangeActiveState(bool state)
    {
        Debug.Log( $"{state}" );
        this.gameObject.SetActive( state );
    }
}
