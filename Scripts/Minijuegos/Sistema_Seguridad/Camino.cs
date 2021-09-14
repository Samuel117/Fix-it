using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camino : MonoBehaviour
{
    [SerializeField] public AudioSource player;
    
    [SerializeField] AudioClip alarma;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Bola_Jugador>() != null && !FindObjectOfType<Bola_Jugador>().enNucleo)
        {
            player.volume = 0.1f;
            player.PlayOneShot(alarma);
            Invoke("PararAlarma", 2f);
            FindObjectOfType<Bola_Jugador>().Reinicio();
            FindObjectOfType<Sistema_Seguridad>().Derrota(); 
        }
    }

    private void PararAlarma()
    {
        player.volume = 0.5f;
        player.Stop();
    }
}
