using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowCanvasFall : MonoBehaviour
{
    public GameObject canvasToShow;
    private bool isShowing = false;
    public TextMeshProUGUI textFall;
    public TextMeshProUGUI textEnemy;

    void Start()
    {
        if (canvasToShow != null)
        {
            // Evitar que el canvas sea destruido al cambiar de escena
            DontDestroyOnLoad(canvasToShow);
            canvasToShow.SetActive(false);  // Asegura que el Canvas esté desactivado al inicio
            Canvas.ForceUpdateCanvases();  // Forzar actualización del Canvas
        }
    }


    // Método para mostrar el Canvas y forzar su actualización
    private void ShowCanvas()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(true);  // Activar el Canvas
            Canvas.ForceUpdateCanvases();  // Forzar a Unity a actualizar el Canvas
            Debug.Log("Canvas Activado: " + canvasToShow.activeSelf);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ShowCanvas();  // Llamar al método para activar el Canvas
            textEnemy.gameObject.SetActive(true);
            textFall.gameObject.SetActive(false);
            Debug.Log("Mostrando Canvas para: " + textEnemy.name);
        }
        if (collision.gameObject.CompareTag("FallBackground"))
        {
            ShowCanvas();  // Llamar al método para activar el Canvas
            textFall.gameObject.SetActive(true);
            textEnemy.gameObject.SetActive(false);
            Debug.Log("Mostrando Canvas para: " + textFall.name);
            Debug.Log("Mecai");
        }
    }

    // Puedes mantener esta línea para depurar el estado del Canvas en cada frame
    void Update()
    {
        if (canvasToShow != null)
        {
            Debug.Log("Estado del Canvas: " + canvasToShow.activeSelf);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        // Si el Canvas está activo, asegurarse que se mantiene activo al cambiar de escena
        if (canvasToShow != null && !canvasToShow.activeSelf)
        {
            canvasToShow.SetActive(true);
        }
    }

}
