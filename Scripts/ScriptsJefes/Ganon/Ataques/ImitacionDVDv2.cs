using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitacionDVDv2 : AtqBasicoJefe
{
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 3f;//EnemigoData.CargarEnemigo("DVDV2").Valores[2];
        lifeSpan = 3f;  //7 unidades
        dano = (int)EnemigoData.CargarEnemigo("DVDV2").Valores[0];

        Invoke("DestruirProyectil", lifeSpan);
        EncontrarJugador();
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }
}
