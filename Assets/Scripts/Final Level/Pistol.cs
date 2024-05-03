using System;
using UnityEngine;


public class Pistol:MonoBehaviour
{
    public static event Action OnSphereHit;

    [Header( "GUN DETAILS" )]
    public float _damage;
    [Header("VFX")]
    [SerializeField] GameObject _hitImpact;
    [SerializeField] GameObject _muzzleFlash;
    [Header("AFX")]
    [SerializeField] AudioClip _shotAudio;
    [SerializeField] AudioClip _reloadAudio;
    [SerializeField] AudioSource _pistolAS;


    [Header( "MISC" )]
    [SerializeField] Transform _barrelLocation;
    [SerializeField] string _zombieTag;
    [SerializeField] string _UndamagedSphere;

    private void Update()
    {
        
    }

    public void Shoot()
    {
        // PLAY AUDIO WHEN SHOT
        _pistolAS.PlayOneShot( _shotAudio );

        // FLASH WHEN SHOT
        GameObject muzzleFX = (GameObject)Instantiate(_muzzleFlash,_barrelLocation.position, _muzzleFlash.transform.rotation );
        Destroy( muzzleFX, 0.15f );

        Ray ray = new Ray( _barrelLocation.position, _barrelLocation.forward );
        RaycastHit hit;

        if( Physics.Raycast( ray, out hit ) )
        {
            // TODO : GENERAL HIT IMPACT

            if( hit.collider != null )
            {
                if( hit.collider.gameObject.CompareTag( _zombieTag ) )
                {
                    // TODO : PLAY GET HIT ANIMATION IN SEPERATE ZOMBIE SCRIPT
                    // TODO : PLAY DIE ANIMATION IF HEALTH REDUCED TO ZERO IN SEPERATE ZOMBIE SCRIPT
                    // TODO : CHARGE TOWARDS PLAYER WHEN GET SHOT BY PLAYER IN SEPERATE ZOMBIE SCRIPT
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if(enemy != null)
                    {
                        enemy.TakeDamage( _damage, hit.point, hit.normal );
                    }
                }

                if(hit.collider.gameObject.CompareTag(_UndamagedSphere))
                {
                    OnSphereHit?.Invoke();
                }
            }
        }
    }
}
