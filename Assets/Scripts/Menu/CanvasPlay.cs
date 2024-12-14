using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasPlay : MonoBehaviour
{
    [SerializeField] GameObject menuInitial;
    [SerializeField] GameObject menuCreators;
    [SerializeField] GameObject menuPowerUps;

    private void Start()
    {
        menuCreators.SetActive(false);
        menuInitial.SetActive(true);
        menuPowerUps.SetActive(false);
    }
    public void Play()
    {
        Debug.Log("Botón play");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Pruebas");
    }


    public void Credits()
    {
        menuCreators.SetActive(true);
        menuInitial.SetActive(false);
        menuPowerUps.SetActive(false);
    }

    public void PowerUps()
    {
        menuCreators.SetActive(false);
        menuInitial.SetActive(false);
        menuPowerUps.SetActive(true);
    }

    public void Back()
    {
        menuCreators.SetActive(false);
        menuInitial.SetActive(true);
        menuPowerUps.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}