using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecircle : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
