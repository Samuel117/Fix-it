using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDanoDVDv2 : MonoBehaviour
{
    private int dano = 5;
    private float esperarHacerDano = 0f;
    private float cadenciaDano = 1f;
    private float lifeSpan = 7f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestruirZona", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestruirZona()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (DebeHacerDano(esperarHacerDano))
            {
                FindObjectOfType<Jugador>().RecibirDano(dano);
                esperarHacerDano = Time.time + cadenciaDano;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (DebeHacerDano(esperarHacerDano))
            {
                FindObjectOfType<Jugador>().RecibirDano(dano);
                esperarHacerDano = Time.time + cadenciaDano;
            }
        }
    }

    private bool DebeHacerDano(float esperarHacerDano)
    {
        return Time.time > esperarHacerDano;
    }
}
