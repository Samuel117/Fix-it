using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPeonV2 : DisparoEnemigo
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = EnemigoData.CargarEnemigo("PeonV2").Valores[2];
        lifeSpan = 1.17f;  //7 unidades
        dano = (int)EnemigoData.CargarEnemigo("PeonV2").Valores[0];

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
