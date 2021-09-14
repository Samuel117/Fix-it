using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megabit : Jugador
{
    //Valores unicos para personaje Megabit. (Deberan leerse desde los archivos del juego)
    //private float bateriaMegabit = 50f;
    //private float velocidadMegabit = 7f; //7
    //private float cadenciaDeDisparoMegabit = 0.5f;
    //private float nivelHabilidadMegabit = 1f;
    //private float danoMegabit = 10f;

    //Control de habilidad especial.
    private bool puedeUsarHabilidadEspecial = true;
    private bool usandoHabilidadEspecial = false;
    private float duracionHabilidadEspecial = 0f;
    private float cadenciaHabilidadEspecial = 0.5f;
    private float esperarHabilidadEspecial = 0f;
    private float contadorHabilidadEspecial = 0f;
    public bool formaBola;

    [SerializeField] private GameObject prefabHabilidadEspecial;
    [SerializeField] private AudioClip HEsound;


    void Start()
    {
        //Inicializamos los valores unicos de Megabit. 

        CargarJugador();

        //bateria = bateriaMegabit;
        //dano = danoMegabit;
        //nivelHE = nivelHabilidadMegabit;

        //velocidad = velocidadMegabit;
        //cadenciaDeDisparo = cadenciaDeDisparoMegabit;

        bateriaMax = bateria;
        velocidadRelentizado = velocidad / 2;
        velocidadEstandar = velocidad;
        Matdefault = sr.material;

        //GuardarJugador();
    }

    void FixedUpdate()
    {
        //Controles de movimiento 
        if (esperarConf > Time.time && confundido == true)
        {
            MovimientoInvertido();
        }
        else
        {
            confundido = false;
            ManejarMovimiento();
        }

        ManejarSalto();

        ComprobarRelentizado();
        ComprobarAtrapado();
    }

    private void Update()
    {
        //Controles de disparo

        if (usandoHabilidadEspecial && Time.time <= duracionHabilidadEspecial)
        {
            DisparoHabilidadEspecial();
        }
        else
        {
            if (nivelHE == 3 && contadorHabilidadEspecial < 2)
            {
                if (!FindObjectOfType<UI_Habilidad>().ready)
                {
                    FindObjectOfType<UI_Habilidad>().HElista();
                }
                puedeUsarHabilidadEspecial = true;
            }

            usandoHabilidadEspecial = false;
            ManejarDisparo();
            ManejarHabilidadEspecial();
        }
    }

    protected override void ManejarHabilidadEspecial()
    {
        //Control.
        float ejeFuego2 = Input.GetAxisRaw("Fire2");

        //Verifica que sea posible disparar en el momento.
        if (ejeFuego2 != 0f && PuedeUsarHabilidadEspecial() && !MinijuegoAbierto)
        {
            usandoHabilidadEspecial = true;
            puedeUsarHabilidadEspecial = false;

            FindObjectOfType<UI_Habilidad>().HEnoLista();

            HabilidadNivel2();
            HabilidadNivel3();
        }
    }

    private void DisparoHabilidadEspecial()
    {
        //Control.
        float ejeFuego1 = Input.GetAxisRaw("Fire1");

        //Verifica que sea posible disparar en el momento.
        if (ejeFuego1 != 0f && PuedeDispararHabilidadEspecial(esperarHabilidadEspecial))
        {
            Soundplayer.PlayOneShot(HEsound);
            Instantiate(this.prefabHabilidadEspecial, new Vector2(this.transform.position.x, this.transform.position.y + 0.3f), this.transform.rotation);
            //FindObjectOfType<DisparoSkippy>().danoRealizado(danoHabilidadEspecial);

            //Detener al jugador
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            this.disparando = true;
            this.GetComponent<Animator>().SetTrigger("Disparar");
            esperarHabilidadEspecial = Time.time + cadenciaHabilidadEspecial;
        }
    }

    private bool PuedeUsarHabilidadEspecial()
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 

        return  puedeUsarHabilidadEspecial && !saltando;
    }

    private bool PuedeDispararHabilidadEspecial(float esperaHabilidadEspecial)
    {
        return Time.time > esperaHabilidadEspecial && !saltando;
    }

    private void HabilidadNivel2()
    {
        if (nivelHE >= 2)
        {
            duracionHabilidadEspecial = Time.time + 15f;
        }
        else
        {
            duracionHabilidadEspecial = Time.time + 10f;
        }
    }

    private void HabilidadNivel3()
    {
        if (nivelHE == 3)
        {
            contadorHabilidadEspecial++;
        }
    }

}

