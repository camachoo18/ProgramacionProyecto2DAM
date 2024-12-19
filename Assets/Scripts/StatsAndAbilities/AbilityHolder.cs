using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    
    [SerializeField] List<Ability> abilities;
    int selectedAbilityIndex = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedAbilityIndex = 0; // Habilidad de click izquierdo
            ActivateSelectedAbility();
        }

        if (Input.GetMouseButtonDown(1))
        {
            selectedAbilityIndex = 1; // Habilidad de click derecho
            ActivateSelectedAbility();
        }
    }

    void ActivateSelectedAbility()
    {
        if (abilities == null || abilities.Count == 0) return;

        Ability selectedAbility = abilities[selectedAbilityIndex];
        autoJump player = GetComponent<autoJump>();
        if (player != null && selectedAbility != null)
        {
            selectedAbility.Activate(player);
        }
    }
}
