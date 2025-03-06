using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class autoJump : MonoBehaviour
{
    public Rigidbody2D Rb;
    [SerializeField] GameObject[] JumpHB;
    [SerializeField] bool isGrounded = false;
    [SerializeField] float distanceToGround;
    [SerializeField] public Transform[] groundCheckPoints;
    float currentJumpPressTime;
    [SerializeField] int performedJumpCount;
    [SerializeField] int maxOnAirJumps = 2;
    [SerializeField] float jumpStrength = 10f;
    [SerializeField] float jumpInterval = 1f;
    private float jumpTimer;
    public Stats Stats;
    [SerializeField] float upGravity = 1f;
    [SerializeField] float downGravity = 2f;
    [SerializeField] float peakGravity = 0.5f;
    [SerializeField] float yVelocityLowGravityThreshold = 0.1f;
    [SerializeField] private bool isInvulnerable = false;

    [SerializeField] private ParticleSystem jumpParticles;

   

    float timeOnAir;
    float currentJumpStrength;

    void Start()
    {
        performedJumpCount = 0;
        Rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpInterval;
        currentJumpStrength = jumpStrength; // Iniciar el salto con la fuerza inicial
    }

    void Update()
    {
       
        

        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0 && (isGrounded || performedJumpCount < maxOnAirJumps))
        {
            currentJumpPressTime = 0;
            performedJumpCount += 1;
            Rb.velocity = new Vector2(Rb.velocity.x, currentJumpStrength);
            jumpTimer = jumpInterval;

            if (jumpParticles != null)
            {
                jumpParticles.Play();
            }
            StartCoroutine(SquashAnimation(0.2f, new Vector3(1.2f, 0.8f, 1f)));
        }

        if (Rb.velocity.y < yVelocityLowGravityThreshold && Rb.velocity.y > -yVelocityLowGravityThreshold)
        {
            Rb.gravityScale = peakGravity;
        }
        else if (Rb.velocity.y > 0)
        {
            Rb.gravityScale = upGravity;
        }
        else
        {
            Rb.gravityScale = downGravity;
        }

        isGrounded = false;
        for (int i = 0; i < groundCheckPoints.Length; i++)
        {
            bool hit = Physics2D.Raycast(
                groundCheckPoints[i].position,
                Vector2.down,
                distanceToGround);

            if (hit)
            {
                timeOnAir = 0;
                isGrounded = true;
                performedJumpCount = 0;
                Rb.gravityScale = upGravity;
                break;
            }
        }

        if (!isGrounded)
        {
            timeOnAir += Time.deltaTime;
        }

        // Ignorar colisión al subir con plataformas tipo A
        foreach (Transform groundCheck in groundCheckPoints)
        {
            Collider2D hitCollider = Physics2D.OverlapCircle(groundCheck.position, 0.2f);

            if (hitCollider != null)
            {
                if (hitCollider.CompareTag("PlatformA") && Rb.velocity.y > 0)
                {
                    Physics2D.IgnoreCollision(hitCollider, GetComponent<Collider2D>(), true);
                }
                else
                {
                    Physics2D.IgnoreCollision(hitCollider, GetComponent<Collider2D>(), false);
                }
            }
        }

        // Ignorar colisiones ascendentes para todas las plataformas del suelo
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), Rb.velocity.y > 0);

         }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("invencible"))
        {
            StartCoroutine(ActivateInvulnerability(5f)); // Ejemplo: 5 segundos de invulnerabilidad
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator ActivateInvulnerability(float duration)
    {
        isInvulnerable = true;

        // Opcional: Cambiar apariencia del jugador, por ejemplo, color o animación.
        yield return new WaitForSeconds(duration);

        isInvulnerable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       // Si toca PlatformA, aumentara su fuerza de salto.
        if (collision.gameObject.CompareTag("PlatformA"))
        {
            
            if (!isGrounded && performedJumpCount > 0)
            {
                currentJumpStrength *= 2.1f;  // esto aumentara su salto.
            }
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        } // Si toca PlatformB, rebota.
        else if (collision.gameObject.CompareTag("PlatformB"))
        {
            
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5f), ForceMode2D.Impulse);
        }

        // Restablece la fuerza del salto a su valor inicial cuando salta desde una plataforma tipo B
        if (collision.gameObject.CompareTag("PlatformB"))
        {
            //se reinicia al dar el otro tipo.
            currentJumpStrength = jumpStrength;
        }
    }


    private IEnumerator SquashAnimation(float duration, Vector3 squashScale)
    {
        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0f;

        // Achatar al personaje
        while (elapsedTime < duration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, squashScale, (elapsedTime / (duration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        // Restaurar al tamaño original
        while (elapsedTime < duration / 2)
        {
            transform.localScale = Vector3.Lerp(squashScale, originalScale, (elapsedTime / (duration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de restaurar exactamente el tamaño original
        transform.localScale = originalScale;
    }

    private IEnumerator backToNormal(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Stats.jumpStenghtBS = 10f;
    }

    private IEnumerator AnimateDeath(float duration)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y - 5f, startPos.z); // Nueva posición final de la caída

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Movimiento en curva (horizontal + vertical)
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
            float curveY = Mathf.Sin(t * Mathf.PI) * -2f; // Movimiento vertical (curvado hacia abajo)
            currentPos.y += curveY;

            transform.position = currentPos;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
