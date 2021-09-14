using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso_EnergiaNegativa : MonoBehaviour
{
    private int dano = 10;
    private float esperarDano = 0f;
    private float cadenciaDano = 3f;

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
        if (collision.GetComponent<Jugador>() != null && PuedeHacerDano(esperarDano))
        {
            FindObjectOfType<Jugador>().RecibirDano(5);
            esperarDano = Time.time + cadenciaDano;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Jugador>() != null && PuedeHacerDano(esperarDano))
        {
            FindObjectOfType<Jugador>().RecibirDano(dano);
            esperarDano = Time.time + cadenciaDano;
        }
    }

    private bool PuedeHacerDano(float esperarDano)
    {
        return Time.time > esperarDano;
    }
}
