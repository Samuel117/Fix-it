using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoSkippy : Disparo
{
    public float danoSkippy; 

    // Start is called before the first frame update
    void Start()
    {
        dano = FindObjectOfType<Jugador>().DanoGetter();  
        velocidad = 10f;
        lifeSpan = 0.70f;
     
        //Debug.Log(dano);

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
