using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpCinematic : MonoBehaviour
{
    [SerializeField] float seconds;
    void Start()
    {
        EndingCinematic(seconds);
    }

    public void EndingCinematic(float seconds)
    {
        StartCoroutine(TurnOffCinematic (seconds));
    }


    private IEnumerator TurnOffCinematic(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
