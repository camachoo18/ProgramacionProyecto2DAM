using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RestartWithDelay(float duration)
    {
        StartCoroutine(AnimateDeathAndRestart(duration));
    }

    private IEnumerator AnimateDeathAndRestart(float duration)
    {
        Vector3 startPos = transform.position; 
        Vector3 endPos = new Vector3(startPos.x, startPos.y - 5f, startPos.z); 

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
            float curveY = Mathf.Sin(t * Mathf.PI) * -2f; 
            currentPos.y += curveY;

            transform.position = currentPos;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RestartWithDelay(2.8f); 
        }
    }
}
