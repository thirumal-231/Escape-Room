using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    [SerializeField] float _health;
    [SerializeField] GameObject _bloodOverlayCanvas;

    private void OnEnable()
    {
        Enemy.OnPlayerHit += TakeDamage;
    }

    private void OnDisable()
    {
        Enemy.OnPlayerHit -= TakeDamage;
    }

    void Start()
    {
        _bloodOverlayCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(float damage)
    {
        // BLOOD EFFECT WHEN PLAYER GET HIT
        StartCoroutine( DoBloodOverlay() );

        _health -= damage;
        if( _health <= 0 )
        {
            // INVOKE DEATH SEQUENCE VIA UI AND LEVEL MANAGER
            OnPlayerDeath?.Invoke();
        }
    }

    IEnumerator DoBloodOverlay()
    {
        _bloodOverlayCanvas.SetActive( true );
        yield return new WaitForSeconds( 1 );
        _bloodOverlayCanvas.SetActive(false );
    }
}
