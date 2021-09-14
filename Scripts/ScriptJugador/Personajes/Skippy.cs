using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skippy : Jugador
{
    //Valores unicos para personaje Skippy. (Deberan leerse desde los archivos del juego)
    //private float bateriaSkippy = 40f;
    //private float velocidadSkippy = 7f; //7
    //private float cadenciaDeDisparoSkippy = 0.5f;
    //private float nivelHabilidadSkippy = 1f;
    //private float danoSkippy = 15f;
  
    //Variables de control de habilidad especial.
    private float esperarHabilidadEspecial = 0f;
    private float cadenciaHabilidadEspecialSkippy = 30f;

    [SerializeField] private GameObject prefabHabilidadEspecial;
    [SerializeField] AudioClip HEsound;

    void Start()
    {
        //Inicializamos los valores unicos de Skippy. 

        CargarJugador();

        //bateria = bateriaSkippy;
        //nivelHE = nivelHabilidadSkippy;
        //dano = danoSkippy;
        //velocidad = velocidadSkippy;
        //cadenciaDeDisparo = cadenciaDeDisparoSkippy;

        velocidadRelentizado = velocidad / 2;
        velocidadEstandar = velocidad;

        //Establecer limite maximo de bateria de Skippy.
        bateriaMax = bateria;
        Matdefault = sr.material;
        //GuardarJugador();//Guarda jugador
        //GeneralPlayerData.InicializarInfo();//Inicializa data

    }

    //void FixedUpdate()
    //{
    //    //Controles de movimiento 
    //    ManejarMovimiento();
    //    ManejarSalto();
    //}

    //private void Update()
    //{
    //    //Controles de disparo
    //    ManejarDisparo();
    //    ManejarHabilidadEspecial();
    //}

    protected override void ManejarHabilidadEspecial()
    {
        //Control.
        float ejeFuego2 = Input.GetAxisRaw("Fire2");

        if(PuedeUsarHabilidadEspecial(esperarHabilidadEspecial) && !FindObjectOfType<UI_Habilidad>().ready)
        {
            FindObjectOfType<UI_Habilidad>().HElista();
        }
       
        //Verifica que sea posible disparar en el momento.
        if (ejeFuego2 != 0f && PuedeUsarHabilidadEspecial(esperarHabilidadEspecial) && !MinijuegoAbierto)
        {
            Soundplayer.PlayOneShot(HEsound);
            Instantiate(this.prefabHabilidadEspecial, new Vector2(this.transform.position.x, this.transform.position.y + 0.3f), this.transform.rotation);
            //FindObjectOfType<DisparoSkippy>().danoRealizado(danoHabilidadEspecial);

            FindObjectOfType<UI_Habilidad>().HEnoLista();

            //Detener al jugador
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            this.disparando = true;
            this.GetComponent<Animator>().SetTrigger("Disparar");

            HabilidadNivel2();
            HabilidadNivel3();

        }
    }

    private bool PuedeUsarHabilidadEspecial(float tiempoEsperarHabilidadEspecial)
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 
        return Time.time > tiempoEsperarHabilidadEspecial && !saltando;
    }

    private void HabilidadNivel2()
    {
        if (nivelHE >= 2)
        {
            this.esperarHabilidadEspecial = Time.time + (this.cadenciaHabilidadEspecialSkippy - 10f);
        }
        else
        {
            this.esperarHabilidadEspecial = Time.time + this.cadenciaHabilidadEspecialSkippy;
        }
    }

    private void HabilidadNivel3()
    {
        if (nivelHE == 3)
        {
            bateria = bateria + 15;

            if (bateria > bateriaMax)
            {
                bateria = bateriaMax;
            }
        }

        FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);
    }

}
