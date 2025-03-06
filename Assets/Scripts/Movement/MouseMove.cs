using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{

    public Camera cam;
    public float moveSpeed = 1f;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        Vector2 targetPos = new Vector2(mousePos.x, transform.position.y);


       // Vector3.SmoothDamp() para el movimiento
        transform.position = Vector2.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
