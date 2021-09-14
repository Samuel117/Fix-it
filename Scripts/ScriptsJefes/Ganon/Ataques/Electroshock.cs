using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electroshock : MonoBehaviour
{
   // private float lifeSpam = 15f;
    private int dano = 10;
    private float esperarDano = 0f;
    private float cadenciaDano = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject, lifeSpam);
    }

    // Update is called once per frame
    void Update()
    {
        
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
