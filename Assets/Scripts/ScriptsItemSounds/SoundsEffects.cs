using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffects : MonoBehaviour
{
    [SerializeField] private AudioClip collect1;
    [SerializeField] private Invencible invencibleItem;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ControllerSound.Instance.EjecutarSonido(collect1);
            //Destroy.(gameObject);

            if (invencibleItem != null)
            {
                invencibleItem.ActivateImmortality();
            }
        }
    }
}
