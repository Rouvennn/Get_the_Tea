using UnityEngine;

public class Collectible : MonoBehaviour
{
    void Start()
    {
        RoomManager.Instance.RegisterCollectible();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomManager.Instance.CollectItem();
            Destroy(this.gameObject);
        }
    }
}
