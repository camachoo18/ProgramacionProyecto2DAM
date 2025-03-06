using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffects : MonoBehaviour
{
    [SerializeField] private AudioClip collect1;
    [SerializeField] private Invencible invencibleItem;
    [SerializeField] private float jumpForce = 10f; // Ajusta la fuerza del salto

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ControllerSound.Instance.EjecutarSonido(collect1);

            
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0); 
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            //Destroy(gameObject);

            if (invencibleItem != null)
            {
                invencibleItem.ActivateImmortality();
            }
        }
    }
}
