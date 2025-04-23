using UnityEngine;

public class Collectible : MonoBehaviour
{
    void Start()
    {
        RoomManager.Instance.RegisterCollectible();
    }

    void OnTriggerEnter(Collider other)
    {
        var playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Collected Item");
            RoomManager.Instance.CollectItem();
            Destroy(this.gameObject);
        }
    }
}
