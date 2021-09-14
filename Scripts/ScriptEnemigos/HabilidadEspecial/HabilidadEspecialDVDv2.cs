using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadEspecialDVDv2 : MonoBehaviour
{
    private int dano = 60; //60f
    [SerializeField] GameObject prefabZonaDano;

    [SerializeField] private AudioSource AudioPlayer;
    [SerializeField] private AudioClip Explosion;

    // Start is called before the first frame update
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
        Vector2 posicion = new Vector2(this.transform.position.x, this.transform.position.y - 0.6f);
        Instantiate(prefabZonaDano, posicion, this.transform.rotation);
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
