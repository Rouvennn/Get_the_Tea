using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public float lerpedValue;
    public float duration = 3;

    public float start;
    public float end;

    public GameObject cube;

    [ContextMenu("Start Routine")]
    public void StartRoutine()
    {
        StartCoroutine(LerpScale(cube.gameObject.transform.localScale.y, end));
    }

    public IEnumerator LerpScale(float start, float end)
    {
        float timeElapsed = 0;

        while(timeElapsed < duration)
        {
            float time = timeElapsed / duration;
            lerpedValue = Mathf.Lerp(start, end, time);
            cube.gameObject.transform.localScale = new Vector3(cube.gameObject.transform.localScale.x, lerpedValue, cube.gameObject.transform.localScale.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lerpedValue = end;
    }
}
