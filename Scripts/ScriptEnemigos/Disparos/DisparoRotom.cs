using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoRotom : DisparoEnemigo
{
    [SerializeField] GameObject EfectoRelentizar;

    // Start is called before the first frame update
    void Start()
    {
        velocidad = EnemigoData.CargarEnemigo("Rotom").Valores[2];
        lifeSpan = 0.3f;  //7 unidades
        dano = (int)EnemigoData.CargarEnemigo("Rotom").Valores[0];

        Invoke("DestruirProyectil", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        GenerarProyectil();
    }

    protected override void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<Jugador>() != null)
        {
            otro.GetComponent<Jugador>().RecibirDanoRotom(dano);

            if (FindObjectOfType<EfectoRelen>() == null)
            {
                Instantiate(EfectoRelentizar, FindObjectOfType<Jugador>().transform.position, EfectoRelentizar.transform.rotation);
            }

            Destroy(this.gameObject);
        }

        if (otro.gameObject.layer == 16)
        {
            Destroy(this.gameObject);
        }
    }
}
