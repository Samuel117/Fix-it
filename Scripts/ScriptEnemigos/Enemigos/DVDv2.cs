using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVDv2 : DVDv1
{
   
    //[SerializeField] protected GameObject prefabHabilidadEspecial;

    // Start is called before the first frame update
    void Start()
    {
        EnemigoData.EnemyData enemigo = EnemigoData.CargarEnemigo("DVDV2");
        bateria = enemigo.Valores[1];
        cadenciaDeDisparo = 1f;
        velocidadMovimiento = enemigo.Valores[3];
        alcance = enemigo.Valores[4];
        engranajesDerrota = 5;

        Matdefault = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (bateria >= 20)
        {
            Movimiento();
            Disparar();
        }
        else
        {
            if (!usandoHabilidadEspecial)
            {
                esperarLiberacion = Time.time + esperarLiberacion;
            }

            HabilidadEspecial();
        }
    }
}
