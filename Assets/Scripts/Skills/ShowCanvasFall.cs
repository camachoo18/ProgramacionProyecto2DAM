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
            canvasToShow.SetActive(false);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canvasToShow.SetActive(true);
            textEnemy.gameObject.SetActive(true);
            textFall.gameObject.SetActive(false);
            //StartCoroutine(ShowCanvas());
        }
        if (collision.gameObject.CompareTag("FallBackground"))
        {
            canvasToShow.SetActive(true);
            textFall.gameObject.SetActive(true);
            textEnemy.gameObject.SetActive(false);
            //StartCoroutine(ShowCanvas());
        }
    }

   /* IEnumerator ShowCanvas()
    {
        isShowing = true;
        
        if (canvasToShow1 != null)
        {
            canvasToShow1.SetActive(true);
        }

        
        yield return new WaitForSeconds(1);

        
        if (canvasToShow1 != null)
        {
            canvasToShow1.SetActive(false);
        }

        isShowing = false;
    }*/
}
