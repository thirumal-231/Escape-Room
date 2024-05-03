using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShotsLeftCanvas : MonoBehaviour
{
    SphereManager sphereManager;
    [SerializeField] TextMeshProUGUI shotsLeftText;
    public Transform canvasParent;
    Camera mainCam;

    private void Awake()
    {
        sphereManager = FindObjectOfType<SphereManager>();
    }

    void Start()
    {
        mainCam = Camera.main;
    }


    void Update()
    {
        shotsLeftText.text = $"{10- sphereManager.hitPoints} shots left";
        if( 10 - sphereManager.hitPoints <= 0)
        {
            this.gameObject.SetActive(false);
        }
        LookAtCam();
        
    }

    void LookAtCam()
    {
        this.transform.LookAt(mainCam.transform.position);
    }
}
