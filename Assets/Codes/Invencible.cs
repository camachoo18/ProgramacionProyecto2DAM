using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invencible : MonoBehaviour
{
    [SerializeField] Image iconInvencible;
    private BoxCollider2D boxCollider;
    [SerializeField] GameObject[] invencibleHB;
    [SerializeField] float duration = 7f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        iconInvencible.gameObject.SetActive(false);

    }

    private IEnumerator MultiColorEffect(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            
            spriteRenderer.color = new Color(
                Random.Range(0f, 1f), // Rojo
                Random.Range(0f, 1f), // Verde
                Random.Range(0f, 1f)  // Azul
            );

            
            yield return new WaitForSeconds(0.1f);

            elapsedTime += 0.1f;
        }

       
        spriteRenderer.color = Color.white;
    }

    public void TurnOffColliderTemporally(float time)
    {
        StartCoroutine(TurnOnTurnOffCollider(time));
    }


    IEnumerator TurnOnTurnOffCollider(float time)
    {

        boxCollider.enabled = false;

        yield return new WaitForSeconds(time);

        boxCollider.enabled = true;
    }

    public void ActivateImmortality()
    {

        iconInvencible.gameObject.SetActive(true);
        StartCoroutine(ImmortalityTimer());
    }

    private IEnumerator ImmortalityTimer()
    {


        yield return new WaitForSeconds(duration);

        iconInvencible.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("invencible"))
        {
            StartCoroutine(TurnOnTurnOffCollider(7.0f));
            StartCoroutine(MultiColorEffect(7.0f)); // Inicia el efecto multicolor
            //invencibleHB[0].SetActive(false);
        }

        if (collision.gameObject.CompareTag("invencible"))
        {
            StartCoroutine(TurnOnTurnOffCollider(7.0f));
            StartCoroutine(MultiColorEffect(7.0f)); // Inicia el efecto multicolor
            //invencibleHB[0].SetActive(false);
        }
    }

}
