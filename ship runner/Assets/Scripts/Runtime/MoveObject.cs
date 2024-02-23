using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public Vector3 direction;
    
    private float speed = 5f;
    private float duration = 0.5f;

    private void Start()
    {
        StartCoroutine(MoveObjectCoroutine());
    }

    private IEnumerator MoveObjectCoroutine()
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            yield return null;
        }

        if (direction == Vector3.up) yield break;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}