using UnityEngine;

public class ResetWall : MonoBehaviour
{
    [SerializeField]
    private LowerWall lowerWall;

    [SerializeField]
    private float end;

    private void OnTriggerExit(Collider other)
    {
        if(lowerWall != null)
        {
            if (lowerWall.wallObject.Contains(other.gameObject))
            {
                Debug.Log(other.transform.localScale.y);

                lowerWall.StartCoroutine(lowerWall.WallScale(other.transform.localScale.y, end, other.transform));

                lowerWall.wallObject.Remove(other.gameObject);
            }
        }
    }
}
