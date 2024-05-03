using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] BoxCollider doorCollider;
    [SerializeField] Transform mainCam;
    [SerializeField] Animator doorAnimator;

    public string openDoorAnimName;
    public string closeDoorAnimName;

    private Vector3 camPos;
    private bool isInRange;
    private bool isDoorOpen;

    void Start()
    {

    }

    void Update()
    {
        camPos = mainCam.position;
        if(doorCollider.bounds.Contains(camPos))
        {
            isInRange = true;
            OpenDoor();
        } else
        {
            CloseDoor();
        }

    }
    void OpenDoor()
    {
        if(isDoorOpen) return;
        else
        {
            doorAnimator.Play( openDoorAnimName );
        }
        isDoorOpen = true;
    }
    
    void CloseDoor()
    {
        if(!isDoorOpen) return;
        else
        {
            doorAnimator.Play( closeDoorAnimName );
        }
        isDoorOpen = false;
    }


}
