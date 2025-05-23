using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    private bool triggered = false;

    public float openHight = 5;

    public void OpenDoor()
    {
        if (isOpen) return;
        isOpen = true;

        //Move door out of way
        transform.position += Vector3.up * openHight;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen) 
        {
            Debug.Log("Door not Open yet");
            return;
        }

        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;

            ScreenFader.Instance.FadeIn(() => {
                RoomManager.Instance.GetNextRoom();
                ScreenFader.Instance.FadeOut();
            });


        }
    }
}

