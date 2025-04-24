using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class LowerWall : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    [SerializeField]
    private Camera mainCamera;

    public List<GameObject> wallObject = new List<GameObject>();

    public float lerpedValue;
    public float duration = 3;

    public float end;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector2 cutoutPosition = mainCamera.WorldToScreenPoint(targetObject.position);
        cutoutPosition.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length;)
        {
            
            if(wallObject.Count != 0)
            {
                if (wallObject.Contains(hitObjects[i].transform.gameObject))
                {
                    return;
                }
                else
                {
                    wallObject.Add(hitObjects[i].transform.gameObject);

                    StartCoroutine(WallScale(hitObjects[i].transform.localScale.y, end, hitObjects[i].transform));
                }
            }
            else
            {
                wallObject.Add(hitObjects[i].transform.gameObject);

                StartCoroutine(WallScale(hitObjects[i].transform.localScale.y, end, hitObjects[i].transform));
                return;
            }
        }
    }

    public IEnumerator WallScale(float start, float end, Transform wallToScale)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float time = timeElapsed / duration;
            lerpedValue = Mathf.Lerp(start, end, time);
            wallToScale.transform.localScale = new Vector3(wallToScale.gameObject.transform.localScale.x, lerpedValue, wallToScale.gameObject.transform.localScale.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lerpedValue = end;
    }
}
