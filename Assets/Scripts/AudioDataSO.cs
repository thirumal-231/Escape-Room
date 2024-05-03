using UnityEngine;

[CreateAssetMenu(fileName = "AudioData")]
public class AudioDataSO : ScriptableObject
{

    [Header( "Room 1" )]
    public AudioClip seargentClip;
    public AudioClip acceptMissionClip;
    public AudioClip ambientClip;
    public AudioClip radioClip;
}
