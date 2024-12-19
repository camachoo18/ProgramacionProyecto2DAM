using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/NoCollisionWithTypeAAbility")]
public class NoCollisionWithTypeAAbility : Ability
{
    public override void Activate(autoJump player)
    {
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (playerCollider == null) return;

        for (int i = 0; i < player.groundCheckPoints.Length; i++)
        {
            Transform groundCheck = player.groundCheckPoints[i];
            Collider2D hitCollider = Physics2D.OverlapCircle(groundCheck.position, 0.1f);
            if (hitCollider != null && hitCollider.CompareTag("PlatformA") && player.Rb.velocity.y < 0)
            {
                // Ignorar colisiones entre el jugador y la plataforma
                Physics2D.IgnoreCollision(hitCollider, player.GetComponent<Collider2D>(), true);

                // Opcional: Rehabilitar colisiones después de un tiempo
                player.StartCoroutine(ResetCollision(hitCollider, player.GetComponent<Collider2D>(), 0.5f));
            }
        }

    }

    private IEnumerator ResetCollision(Collider2D platformCollider, Collider2D playerCollider, float delay)
    {
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
    }
}
