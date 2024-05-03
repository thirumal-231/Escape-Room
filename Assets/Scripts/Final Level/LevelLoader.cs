using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject levelFadeCanvas;
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += LoadCurrentScene;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= LoadCurrentScene;
    }

    void Start()
    {
        levelFadeCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadCurrentScene();
        }
    }

    void LoadCurrentScene()
    {
        StartCoroutine( LoadActiveScene() );
    }

    IEnumerator LoadActiveScene()
    {
        levelFadeCanvas.SetActive( true );
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }
}
