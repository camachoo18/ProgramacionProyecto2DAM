using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    [SerializeField] private AudioClip collect1;
    [SerializeField] private Vector3 positionInitial; 

    // Animacion de muerte del gatete
    private IEnumerator AnimateDeath(float duration)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = positionInitial;     

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
            float curveY = Mathf.Sin(t * Mathf.PI) * 2f; // Subida hacia arriba en el movimiento
            currentPos.y += curveY;

            transform.position = currentPos; 

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos; 
    }

    private IEnumerator DeathEffectCoroutine()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float duration = 2.25f; // Duración total de la animación
        float elapsedTime = 0f;
        Color startColor = sprite.color;
        Color transparentColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        // Parpadeo mientras el personaje hace la animación
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Transparencia y parpadeo
            sprite.color = Color.Lerp(startColor, transparentColor, t);
            sprite.enabled = Mathf.FloorToInt(elapsedTime * 10) % 2 == 0; 

            yield return null;
        }

        sprite.enabled = true;
        sprite.color = startColor; // Restaurar color original
    }

    public void Die(Vector3 collisionPosition)
    {
        StartCoroutine(AnimateDeath(2.2f)); // Duración del movimiento
        StartCoroutine(DeathEffectCoroutine()); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die(collision.transform.position);
            ControllerSound.Instance.EjecutarSonido(collect1);
        }
        if (collision.gameObject.CompareTag("FallBackground"))
        {
            Die(collision.transform.position);
            ControllerSound.Instance.EjecutarSonido(collect1);
        }
    }
}
