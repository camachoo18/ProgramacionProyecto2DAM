using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorMap : MonoBehaviour
{
  public GameObject[] objects;
    void Start()
    {
        int randomObject = Random.Range(0, objects.Length);
        Instantiate(objects[randomObject], transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
