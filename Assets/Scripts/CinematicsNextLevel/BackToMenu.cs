using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] Image iconoInvencible;
    [SerializeField] float seconds;
    void Start()
    {
        FinalCinematic(seconds);
    }

    public void FinalCinematic(float seconds)
    {
        StartCoroutine(EndCinematicAnimation(seconds));
    }

  
    private IEnumerator EndCinematicAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Menu");
    }
}
