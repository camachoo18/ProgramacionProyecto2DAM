using UnityEngine;

public class PlataformCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //al subir no choca con las PlatformA
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlatformA"))
        {

            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        // A la hora de colisionar con la platformB, rebotara hacia abajo.
        else if (collision.gameObject.layer == LayerMask.NameToLayer("PlatformB"))
        {

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5f), ForceMode2D.Impulse);
        }
    }
}
