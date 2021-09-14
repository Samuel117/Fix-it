using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadEspecialDVD1 : MonoBehaviour
{
    [SerializeField] private AudioSource AudioPlayer;
    [SerializeField] private AudioClip Explosion;

    // Start is called before the first frame update
    private int dano = 60;
    void Start()
    {
        AudioPlayer.PlayOneShot(Explosion);
        Invoke("DestruirLiberacion", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestruirLiberacion()
    {
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<Jugador>().RecibirDano(dano);
        }
    }

}
