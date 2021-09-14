using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoDVDv1 : DisparoEnemigo
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = EnemigoData.CargarEnemigo("DVDV1").Valores[2];
        lifeSpan = 0.85f;  //7 unidades
        dano = (int)EnemigoData.CargarEnemigo("DVDV1").Valores[0];

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
