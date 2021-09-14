using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoAtom : Disparo
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 6f;
        lifeSpan = 1.16f; //7 unidades
        dano = FindObjectOfType<Jugador>().DanoGetter();

        Debug.Log(dano);

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
