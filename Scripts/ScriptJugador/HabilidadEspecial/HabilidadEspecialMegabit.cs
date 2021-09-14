using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadEspecialMegabit : Disparo
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 14f;
        lifeSpan = 0.28f;
        dano = 50f;

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
