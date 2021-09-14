using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoCayde6 : Disparo
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 6f;
        lifeSpan = 1.66f; //10 unidades
        dano = FindObjectOfType<Jugador>().DanoGetter();

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
