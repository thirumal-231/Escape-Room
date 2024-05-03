using UnityEngine;

public class WalkieTalkie : MonoBehaviour
{
    [SerializeField] BoxCollider talkingZone;
    [SerializeField] Transform phone;
    [SerializeField] GameObject missionBriefCanvas;
    [SerializeField] GameObject walkieTalkie;
    [SerializeField] AudioSource talkieAudioSource;
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] AudioDataSO audios;
    [SerializeField] GameObject locomotionSystem;
    [SerializeField] GameObject rayInteractor;


    [SerializeField] Light[] lights;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] Light candle;
    [SerializeField] Material emissionMat;

    private float timer;
    private bool startLights;

    void Start()
    {
        locomotionSystem.SetActive(false);
        LitCandle( false );
        timer = Random.Range( minTime, maxTime );
        Invoke( "TalkieSequence", 0 );
        missionBriefCanvas.SetActive( false );
    }

    void Update()
    {
        

        if(startLights)
        {
            if( timer > 0 )
            {
                timer -= Time.deltaTime;
            }
            if( timer <= 0 )
            {
                ToggleLights();
            }
        }

    }

    void ToggleLights()
    {
        foreach( Light light in lights )
        {
            light.enabled = !light.enabled;
            timer = Random.Range( minTime, maxTime );
            if(light.enabled)
            {
                emissionMat.EnableKeyword( "_EMISSION" );
            } else if (!light.enabled)
            {
                emissionMat.DisableKeyword( "_EMISSION" );
            }
        }
        LitCandle( true );


    }

    void LitCandle( bool isLit )
    {
        candle.enabled = isLit;
    }   

    void TalkieSequence()
    {
        walkieTalkie.GetComponent<Animator>().Play( "WalkieEmission" );
        talkieAudioSource.PlayOneShot( audios.seargentClip );
        Invoke( "ShowMissionCanvas", 18 );

    }

    void ShowMissionCanvas()
    {
        missionBriefCanvas.SetActive( true );
    }

    public void AcceptMission()
    {
        startLights = true;
        locomotionSystem.SetActive( true );
        if(rayInteractor != null)
        {
            Destroy( rayInteractor, 3);
        }

        playerAudioSource.PlayOneShot( audios.acceptMissionClip );
        missionBriefCanvas.SetActive( false );
        Destroy(missionBriefCanvas,1 );
        Destroy(walkieTalkie,5 );
    }
}
