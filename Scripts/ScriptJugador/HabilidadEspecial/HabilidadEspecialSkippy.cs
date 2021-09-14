using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadEspecialSkippy : DisparoSkippy
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 10f;
        lifeSpan = 0.70f;
        
        //Se debe leer de los archivos para multiplicar x2 el daño normal de Skippy.

        dano = FindObjectOfType<Jugador>().DanoGetter() * 2;

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
