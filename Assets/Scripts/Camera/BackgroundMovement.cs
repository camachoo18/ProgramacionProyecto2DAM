using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] float animatioDuration;
    [SerializeField] Vector2 velocity;
    float seconds;
    void Start()
    {
        StartCoroutine(movementBackground());
    }

    public IEnumerator movementBackground()
    {
        while (true)
        {
            while (seconds <= animatioDuration)
            {
                seconds += Time.deltaTime;

                transform.GetComponent<SpriteRenderer>().material.mainTextureOffset += velocity * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            seconds = 0;
            yield return null;
        }
    }
}
