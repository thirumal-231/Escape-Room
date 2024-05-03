using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button playBtn;
    [SerializeField] Button quitBtn;

    private int room1BuildIndex = 1;

    void Start()
    {
        if(playBtn != null)
        {
            playBtn.onClick.AddListener( StartGame );
        }
        quitBtn.onClick.AddListener( QuitGame );
        
    }

    void Update()
    {
        
    }

    void StartGame()
    {
        SceneManager.LoadScene( room1BuildIndex );
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

}
