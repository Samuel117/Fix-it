using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoS117 : Disparo
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 10f;
        lifeSpan = 0.70f;
        dano = FindObjectOfType<Jugador>().DanoGetter();

        //Debug.Log(dano);

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
