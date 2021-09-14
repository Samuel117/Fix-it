using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeonV2 : Enemigo
{
    // Start is called before the first frame update
    void Start()
    {
        EnemigoData.EnemyData enemigo = EnemigoData.CargarEnemigo("PeonV2");
        bateria = enemigo.Valores[1];
        cadenciaDeDisparo = 1f;
        velocidadMovimiento = enemigo.Valores[3];
        alcance = enemigo.Valores[4];
        engranajesDerrota = 2;

        Matdefault = sr.material;

    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Disparar();
    }
}
