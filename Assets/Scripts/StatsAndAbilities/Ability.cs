using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] public float CoolDown;
    public abstract void Activate(autoJump player);


}