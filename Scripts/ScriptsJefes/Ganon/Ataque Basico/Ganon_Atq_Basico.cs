using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganon_Atq_Basico : AtqBasicoJefe
{
    // Start is called before the first frame update
    void Start()
    {
        //Debido al uso de AddForce no es posible usar los valores especificos.
        velocidad = 4.5f;
        lifeSpan = 4f; //Alcance 10 unidades.
        dano = 15;

        Invoke("DestruirProyectil", lifeSpan);
        EncontrarJugador();
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
