using UnityEngine;

public class TrianguloMovement : MonoBehaviour
{
    [SerializeField] float aceleracion = 6f;
    [SerializeField] float velocidadMax = 10f;
    [SerializeField] Transform targetLocation;

    Vector2 direction;

    ParticleSystem particles;
    
    private Rigidbody2D rb;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();

        ResetVelocity();

    }

    private void Update()
    {
        transform.up = targetLocation.position - transform.position;
    }

    void FixedUpdate()
    {
        direction = (targetLocation.position - transform.position).normalized;
        rb.velocity += direction * aceleracion * Time.fixedDeltaTime;
        if (rb.velocity.magnitude > velocidadMax)
            rb.velocity = rb.velocity.normalized * velocidadMax;

    }
    void ResetVelocity()
    {
        rb.velocity = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
            ).normalized * rb.velocity.magnitude;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        particles.Stop();

        transform.position = Vector2.zero;
        ResetVelocity();

        particles.Play();
    }

}
