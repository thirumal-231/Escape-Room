using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
    }

    public void LoadFinalLevel()
    {
        Invoke( "FinalLevel", 2 );
    }

    void FinalLevel()
    {
        // load new level or
        SceneManager.LoadScene( 2 );
    }
}
