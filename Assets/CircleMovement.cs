using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCircle : MonoBehaviour
{
    [SerializeField] private Vector2 movement = Vector2.right;
    [SerializeField] private Vector2 bounds = Vector2.one;

    void Update()
    {
        transform.position += new Vector3(movement.x, movement.y, 0) * Time.deltaTime;


        if (transform.position.x > bounds.x)
            transform.position = new Vector3(-bounds.x, transform.position.y, 0);

        if (transform.position.x < -bounds.x)
            transform.position = new Vector3(bounds.x, transform.position.y, 0);

        if (transform.position.y > bounds.y)
            transform.position = new Vector3(transform.position.x, -bounds.y, 0);

        if (transform.position.y < -bounds.y)
            transform.position = new Vector3(transform.position.x, bounds.y, 0);
    }
}