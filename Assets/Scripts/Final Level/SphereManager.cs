using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SphereManager : MonoBehaviour
{

    [SerializeField] int zombieLeft = 10;

    [SerializeField] GameObject zombieLeftCanvas;
    [SerializeField] TextMeshProUGUI zombieLeftText;
    public int hitPoints = 0;

    [SerializeField] GameObject _notDamaged;
    [SerializeField] GameObject _damaged;
    [SerializeField] Animator _anim;
    [SerializeField] string _sphereArrive;
    [SerializeField] string _damageBall;

    [SerializeField] GameObject _gem;
    [SerializeField] Transform _gemPos;
    [SerializeField] Transform _targetCam;
    [SerializeField] GameObject _bonfire;

    [SerializeField] AudioSource _magicSphereAS;
    [SerializeField] AudioClip _revealClip;
    [SerializeField] AudioClip _metalHitClip;

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += UpdateZombiesLeft;
        Pistol.OnSphereHit += AppearMagicSphere;
        Pistol.OnSphereHit += DamageSphere;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= UpdateZombiesLeft;
        Pistol.OnSphereHit -= AppearMagicSphere;
        Pistol.OnSphereHit -= DamageSphere;
    }

    void Start()
    {
        zombieLeftCanvas.SetActive( false );
        _damaged.SetActive(false );
    }

    void Update()
    {
        AppearMagicSphere();
        if(Input.GetKeyDown(KeyCode.B))
        {
            DamageSphere();
        }
    }

    void UpdateZombiesLeft()
    {
        if(zombieLeft>0)
        {
            zombieLeft--;
        }
        if(zombieLeft==0)
        {
            zombieLeftText.text = $"Every zombie is killed";
        } else if(zombieLeft==1)
        {
            zombieLeftText.text = $"Only one remaining";
        } else
        {
            zombieLeftText.text = $"{zombieLeft} zombies left";
        }
        StartCoroutine( ToggleZombieLeft() );
    }

    IEnumerator ToggleZombieLeft()
    {
        zombieLeftCanvas.SetActive( true );
        yield return new WaitForSeconds( 3 );
        zombieLeftCanvas.SetActive( false );
    }

    // IF ALL ZOMBIES ARE DEAD ANIMATE THIS MAGICAL SPHERE EFFECT
    void AppearMagicSphere()
    {
        if( zombieLeft < 1 )
        {
            Destroy( _bonfire );
            _magicSphereAS.PlayOneShot( _revealClip );
            _anim.Play( _sphereArrive );
            Enemy enemy = FindObjectOfType<Enemy>();
            if( enemy != null )
            {
                GameObject go = enemy.gameObject;
                go.SetActive( false );
                enemy.enabled = false;
            }
        }
    }

    void DamageSphere()
    {
        Debug.Log( "Sphere hit" );
        _magicSphereAS.PlayOneShot(_metalHitClip);
        hitPoints++;

        if(hitPoints==5)
        {
            Destroy( _notDamaged );
            _damaged.SetActive(true );
        }
        if(hitPoints==10)
        {
            Destroy( _damaged );
            Invoke( "AppearRuby", 2 );
        }
    }

    void WinSequence()
    {
        // TODO : PLAY WIN SOUND
    }

    void AppearRuby()
    {
        _gem.SetActive( true );
    }

    public void QuitGame()
    {
        WinSequence();
        Invoke( "DelayLoad", 2 );
    }

    void DelayLoad()
    {
        SceneManager.LoadScene( 3 );
    }
}
