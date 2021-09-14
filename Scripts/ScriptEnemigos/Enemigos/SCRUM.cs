using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRUM : Enemigo
{
    private float cadenciaHabilidadEspecial = 8f;
    private float esperarHabilidadEspecial = 4f;

    [SerializeField] private GameObject prefabHabilidadEspecial;

    // Start is called before the first frame update
    void Start()
    {
        EnemigoData.EnemyData enemigo = EnemigoData.CargarEnemigo("SCRUM");
        bateria = enemigo.Valores[1];
        cadenciaDeDisparo = 1f;
        velocidadMovimiento = enemigo.Valores[3];
        alcance = enemigo.Valores[4];
        engranajesDerrota = 7;

        Matdefault = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Disparar();
        HabilidadEspecial();
    }

    protected override void HabilidadEspecial()
    {
        if (PuedeUsarHabilidadEspecial(esperarHabilidadEspecial))
        {
            if (mira != false && mira.collider.tag == "Player")
            {
                //instanciar el objeto
                Instantiate(this.prefabHabilidadEspecial, this.transform.position, this.transform.rotation);
                esperarHabilidadEspecial = Time.time + cadenciaHabilidadEspecial;
            }
        }
    }

    private bool PuedeUsarHabilidadEspecial(float tiempoEspera)
    {
        return Time.time > tiempoEspera;
    }
}
