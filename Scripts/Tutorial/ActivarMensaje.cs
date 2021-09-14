using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarMensaje : MonoBehaviour
{
    [SerializeField] GameObject interfaz;
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip efecto;

     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.PlayOneShot(efecto);
        Time.timeScale = 0;
        interfaz.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
    }

}
