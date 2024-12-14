using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class autoJump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] GameObject[] JumpHB;
    [SerializeField] bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float distanceToGround;
    [SerializeField] Transform[] groundCheckPoints;
    float currentJumpPressTime;
    [SerializeField] int performedJumpCount;
    [SerializeField] int maxOnAirJumps = 2;
    [SerializeField] float jumpStrength = 10f;
    [SerializeField] float jumpInterval = 1f;
    private float jumpTimer;
    LayerMask Player;
    LayerMask Ground;
    public Stats Stats;
    [SerializeField] float upGravity = 1f;
    [SerializeField] float downGravity = 2f;
    [SerializeField] float peakGravity = 0.5f;
    [SerializeField] float yVelocityLowGravityThreshold = 0.1f;

    [SerializeField] private ParticleSystem jumpParticles;

    float timeOnAir;
    public void RestartWithDelay(float seconds)
    {
        StartCoroutine(AnimateDeath(seconds));
    }

    void Start()
    {
        performedJumpCount = 0;
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpInterval;
        Player = LayerMask.NameToLayer("Player");
        Ground = LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0 && (isGrounded || performedJumpCount < maxOnAirJumps))
        {
            currentJumpPressTime = 0;
            performedJumpCount += 1;
            rb.velocity = new Vector2(rb.velocity.x, Stats.jumpStenghtBS);
            jumpTimer = jumpInterval;

        
            if (jumpParticles != null)
            {
                jumpParticles.Play();
            }
            StartCoroutine(SquashAnimation(0.2f, new Vector3(1.2f, 0.8f, 1f)));
        }

        if (rb.velocity.y < yVelocityLowGravityThreshold && rb.velocity.y > -yVelocityLowGravityThreshold)
        {
            rb.gravityScale = peakGravity;
        }
        else if (rb.velocity.y > 0)
        {
            rb.gravityScale = upGravity;
        }
        else
        {
            rb.gravityScale = downGravity;
        }

        isGrounded = false;
        for (int i = 0; i < groundCheckPoints.Length; i++)
        {
            bool hit = Physics2D.Raycast(
                groundCheckPoints[i].position,
                Vector2.down,
                distanceToGround,
                groundLayer);

            if (hit)
            {
                timeOnAir = 0;
                isGrounded = true;
                performedJumpCount = 0;
                rb.gravityScale = upGravity;
                break;
            }
        }

        if (!isGrounded)
        {
            timeOnAir += Time.deltaTime;
        }

        Physics2D.IgnoreLayerCollision(Player, Ground, rb.velocity.y > 0);
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpHB"))
        {
            Stats.jumpStenghtBS = Stats.jumpStenghtHB;
            JumpHB[0].SetActive(false);

            // Iniciar la animación de achatamiento
            StartCoroutine(SquashAnimation(0.5f, new Vector3(1.2f, 0.8f, 1f)));

            StartCoroutine(backToNormal(1f));
        }

        if (collision.gameObject.CompareTag("JumpHB1"))
        {
            Stats.jumpStenghtBS = Stats.jumpStenghtHB;

            JumpHB[1].SetActive(false);
            StartCoroutine(SquashAnimation(0.5f, new Vector3(1.2f, 0.8f, 1f)));
            StartCoroutine(backToNormal(1f));
        }

        if (collision.gameObject.CompareTag("JumpHB2"))
        {
            Stats.jumpStenghtBS = Stats.jumpStenghtHB;

            JumpHB[2].SetActive(false);
            StartCoroutine(SquashAnimation(0.5f, new Vector3(1.2f, 0.8f, 1f)));
            StartCoroutine(backToNormal(1f));
        }

        if (collision.gameObject.CompareTag("JumpHB3"))
        {
            Stats.jumpStenghtBS = Stats.jumpStenghtHB;

            JumpHB[3].SetActive(false);
            StartCoroutine(SquashAnimation(0.5f, new Vector3(1.2f, 0.8f, 1f)));
            StartCoroutine(backToNormal(1f));
        }
        
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