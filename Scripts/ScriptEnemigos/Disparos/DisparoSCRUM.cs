using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoSCRUM : DisparoEnemigo
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = EnemigoData.CargarEnemigo("SCRUM").Valores[2];
        lifeSpan = 0.7f;  //7 unidades
        dano = (int)EnemigoData.CargarEnemigo("SCRUM").Valores[0] - 10;

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
