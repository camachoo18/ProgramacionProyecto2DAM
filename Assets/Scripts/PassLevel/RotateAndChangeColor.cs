using System.Collections;
using UnityEngine;

public class RotateAndChangeColor : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; // Velocidad de rotación
    [SerializeField] private float colorChangeInterval = 0.5f; // Tiempo entre cambios de color
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Iniciar la corrutina para cambiar de color constantemente
        StartCoroutine(ChangeColor());
    }

    private void Update()
    {
        // Rotar continuamente
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            // Generar un color aleatorio
            Color newColor = new Color(Random.value, Random.value, Random.value);
            spriteRenderer.color = newColor;

            // Esperar el intervalo antes de cambiar de color nuevamente
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }
}
