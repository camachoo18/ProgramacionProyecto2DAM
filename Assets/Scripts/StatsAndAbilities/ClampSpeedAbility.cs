using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ClampSpeedAbility")]
public class ClampSpeedAbility : Ability
{
    public override void Activate(autoJump player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Limitar la velocidad máxima tanto de subida como de bajada
            float clampedYVelocity = Mathf.Clamp(rb.velocity.y, -player.Stats.maxSpeed, player.Stats.maxSpeed);
            rb.velocity = new Vector2(rb.velocity.x, clampedYVelocity);
        }

    }
}
