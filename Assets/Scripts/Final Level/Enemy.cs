using System;
using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public static event Action<float> OnPlayerHit;
    public static event Action OnEnemyDeath;

    public enum EnemyState
    {
        Idle,
        Walk,
        Attack,
        Death
    }

    [SerializeField] EnemyState enemyState = EnemyState.Idle;
    [SerializeField] Transform[] zombieSpawnLocations;

    [Header("AI NAVIGATION")]
    [SerializeField] Transform _targetPlayer;
    [SerializeField] NavMeshAgent _zombie;
    [SerializeField] float _stoppingDistance = 0.5f;
    [Header("STATS")]
    [SerializeField] float _health = 100f;
    [SerializeField] float _attackRange = 20f;
    [SerializeField] float _damagingPower = 5f;
    [SerializeField] float _turnSpeed = 5f;
    [Header( "VFX" )]
    [SerializeField] GameObject _bloodSplash;
    [SerializeField] AudioSource _zombieAS;
    [SerializeField] AudioClip _playerFoundClip;
    [SerializeField] AudioClip _attackPlayerClip;
    [Header("ANIMATIONS")]
    [SerializeField] Animator _anim;
    [SerializeField] string _WalkAnim;
    [SerializeField] string _IdleAnim;
    [SerializeField] string _AttackAnim;
    [SerializeField] string _ShotAnim;
    [SerializeField] string _DeathAnim;

    bool isShot;
    bool isAudioPlayed;

    void Start()
    {
        _targetPlayer = FindObjectOfType<XROrigin>().transform;
    }

    void Update()
    {

        if(IsPlayerInRange() && !isAudioPlayed)
        {
            _zombieAS.PlayOneShot( _playerFoundClip );
            isAudioPlayed = true;
        }

        if(!IsPlayerInRange())
        {
            isAudioPlayed=false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            EnemyDeath();
        }

        switch( enemyState )
        {
            case EnemyState.Idle:
                DoIdle();
                break;
            case EnemyState.Walk:
                DoWalk();
                break;
            case EnemyState.Attack:
                DoAttack();
                break;
            case EnemyState.Death:
            default:
                break;
        }
        
    }

    void SpawnZombie()
    {
        int randomPos = Random.Range( 0, zombieSpawnLocations.Length );
        transform.position = zombieSpawnLocations[randomPos].position;
        enemyState = EnemyState.Idle;
        _health = 100;
    }

    void FaceTarget()
    {
        Vector3 direction = ( _targetPlayer.position - transform.position ).normalized;
        Quaternion lookRotation = Quaternion.LookRotation( new Vector3( direction.x, 0, direction.z ) );
        transform.rotation = Quaternion.Slerp( transform.rotation, lookRotation, Time.deltaTime * _turnSpeed );
    }
    

    bool HasFoundPlayer()
    {
        float distance = Vector3.Distance(_targetPlayer.position,transform.position);
        if(distance<0)
        {
            distance *= -1;
        }
        return ( distance <= _stoppingDistance );
    }

    void DoIdle()
    {
        _anim.Play( _IdleAnim );
        _zombie.isStopped = true;

        // FROM IDLE TO WALK
        if(IsPlayerInRange() && !HasFoundPlayer())
        {
            enemyState = EnemyState.Walk;
        }
        // TODO : IF SHOT TRANSIT INTO WALK
        if(isShot)
        {
            enemyState = EnemyState.Walk;
        }

        // FROM IDLE TO ATTACK
        if( HasFoundPlayer() )
        {
            enemyState = EnemyState.Attack;
        }
    }

    void DoWalk()
    {
        FaceTarget();
        _zombie.isStopped = false;
        _anim.Play( _WalkAnim );
        _zombie.SetDestination( _targetPlayer.position );

        // TRANSIT FROM WALK TO IDLE
        if( !IsPlayerInRange() && !isShot)
        {
            enemyState = EnemyState.Idle;
        }

        // TRANSIT FROM WALK TO IDLE
        if( HasFoundPlayer() )
        {
            enemyState = EnemyState.Attack;
        }
    }

    public void AttackPlayer()
    {
        Debug.Log( "ATTACKING PLAYER" );
        _zombieAS.PlayOneShot( _attackPlayerClip );
        //  TODO : INVOKE DAMAGE SEQUENCE FOR PLAYER - HAVE BLOOD OVERLAP ON THE SCREEN - HEALTH-- - IF DIED RESTART LEVEL
        OnPlayerHit?.Invoke(_damagingPower);

    }

    void DoAttack()
    {
        _anim.Play(_AttackAnim );
        _zombie.isStopped = true;
        // TRANSIT FROM ATTACK TO IDLE
        if( !IsPlayerInRange() )
        {
            enemyState = EnemyState.Idle;
        }
        // TRANSIT FROM ATTACK TO WALK
        if( !HasFoundPlayer() )
        {
            enemyState = EnemyState.Idle;
        }
    }


    bool IsPlayerInRange()
    {
        // RETURN IF PLAYER IS IN ZOMBIE'S RANGE
        Vector3 distance = transform.position - _targetPlayer.position;
        return distance.z < _attackRange;
    }

    public void TakeDamage(float damage, Vector3 hitPoint, Vector3 normal)
    {

        // TRIGGER ZOMBIE TO FOLLOW PLAYER CUZ HE SHOT HIM
        isShot = true;

        // PERFORM VFX WHEN TAKING HITS
        DoVFX( hitPoint, normal );

        // DEPLETE HEALTH
        _health -= damage;

        // DIE WHEN HEALTH IS ZERO
        if(_health <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        enemyState = EnemyState.Death;

        // PLAY SHOT ANIMATION
        _anim.Play( _DeathAnim );

        // CHANGE ENEMY POSITION
        Invoke( "SpawnZombie", 1 );


        // TODO : INCREASE TOTAL ENEMIES KILLED UI IN ANOTHER SCRIPT USING ACTIONS
        OnEnemyDeath?.Invoke();

        // TODO : PLAY SOUND WHEN DIED
        _zombieAS.PlayOneShot(_playerFoundClip );
    }

    void DoVFX(Vector3 hitPoint, Vector3 normal)
    {
        // CREATE AND DESTROY BLOOD SPATTER EFFECT
        GameObject go = (GameObject) Instantiate( _bloodSplash, hitPoint, Quaternion.LookRotation( normal ) );
        Destroy( go, 1 );
    }
    
}
