using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    [Header("Salto Habilidades")]
    public float jumpStenghtHB = 100f;
    public float jumpStenghtBS = 15f;
    public float maxSpeed = 12f;
}