using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPeonV3 : DisparoEnemigo
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = EnemigoData.CargarEnemigo("PeonV3").Valores[2];
        lifeSpan = 0.7f;  //7 unidades
        dano = (int)EnemigoData.CargarEnemigo("PeonV3").Valores[0];

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
