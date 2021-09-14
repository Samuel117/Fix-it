using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRUM_Master_Atq_Basico : AtqBasicoJefe
{
    // Start is called before the first frame update
    void Start()
    {
        //Debido al uso de AddForce no es posible usar los valores especificos.
         velocidad = 20f;
         lifeSpan = 2f; //Alcance 10 unidades.
         dano = 20;

        Invoke("DestruirProyectil", lifeSpan);
        EncontrarJugador();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GenerarProyectil();
    }
}
