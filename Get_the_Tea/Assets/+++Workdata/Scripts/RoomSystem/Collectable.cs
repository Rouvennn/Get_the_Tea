using UnityEngine;

public class Collectible : MonoBehaviour
{
    void Start()
    {
        RoomManager.Instance.RegisterCollectible();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == RoomManager.Instance.player.gameObject)
        {
            Debug.Log("Collected Item");
            RoomManager.Instance.CollectItem();
            Destroy(this.gameObject);
        }
    }
}
