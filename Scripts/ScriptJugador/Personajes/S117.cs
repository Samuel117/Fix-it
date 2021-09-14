using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S117 : Jugador
{
    //Valores unicos para personaje Skippy. (Deberan leerse desde los archivos del juego)
    //private float bateriaS117 = 30f; //30f
    //private float velocidadS117 = 10f; 
    //private float cadenciaDeDisparoS117 = 0.5f;
    //private float nivelHabilidadS117 = 2f;
    //private float danoS117 = 15f;

    //Control de habilidad especial
    public float bateria2S117;
    private float bateria2;
    private float esperarHabilidadEspecial = 1f;
    private float cadenciaHabilidadEspecial = 1f;
    private float retrasoPorDano = 0f;

    [SerializeField] AudioSource PLayer;
    [SerializeField] AudioClip RecargaEscudo;
    [SerializeField] AudioClip EscudoBajo;
    void Start()
    {
        //Inicializamos los valores unicos de S-117. 
        CargarJugador();

        //bateria = bateriaS117;
        //nivelHE = nivelHabilidadS117;
        //dano = danoS117;

        //Se inicializan los datos de la habilidad segun el nivel de habilidad (nivelHE)
        InicializacionHabilidad();

        //velocidad = velocidadS117;
        //cadenciaDeDisparo = cadenciaDeDisparoS117;

        bateriaMax = bateria;
        velocidadRelentizado = velocidad / 2;
        velocidadEstandar = velocidad;
        Matdefault = sr.material;

        //GuardarJugador();

        //Debug.Log(bateria);
        //Debug.Log(nivelHE);
        //Debug.Log(dano);
        //Debug.Log(velocidad);
        //Debug.Log(cadenciaDeDisparo);
        //Debug.Log(bateria2);
    }

    //void FixedUpdate()
    //{
    //    //Controles de movimiento 
    //    ManejarMovimiento();
    //    ManejarSalto();
    //    ComprobarRelentizado();
    //}

    //private void Update()
    //{
    //    //Controles de disparo
    //    ManejarDisparo();
    //    ManejarHabilidadEspecial();
    //}

    protected override void ManejarHabilidadEspecial()
    {
        if (PuedeUsarHabilidadEspecial(esperarHabilidadEspecial + retrasoPorDano))
        {
            if (!FindObjectOfType<UI_Habilidad>().ready)
            {
                FindObjectOfType<UI_Habilidad>().HElista();
            }

            RegenerarBateria2();
            retrasoPorDano = 0;
        }
    }

    public override void RecibirDano(int danoRecibido)
    {
        Soundplayer.PlayOneShot(danoRecibidoSound);
        retrasoPorDano = 4f;
        esperarHabilidadEspecial = Time.time + cadenciaHabilidadEspecial;
      
        EfectoRecibirDano();
        Invoke("TerminarEfectoRecibirDano", 0.1f);

        if (bateria2S117 <= 0)
        {
            bateria = bateria - danoRecibido;
            //Debug.Log("Bateria: " + bateria);
        }
        else
        {
            bateria2S117 = bateria2S117 - danoRecibido;
            //Debug.Log("Bateria 2:" + bateria2S117);
            if(bateria2S117 < 0)
            {
                FindObjectOfType<UI_Habilidad>().HEnoLista();

                if (PLayer.isPlaying)
                {
                    PLayer.Stop();
                    PLayer.PlayOneShot(EscudoBajo);
                }

                bateria2S117 = 0;
            }
            //Retraso en segundos para reactivación de la regeneración de bateria 2.
            
        }
        FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);
        FindObjectOfType<BateriaS117>().ActualizarBateriaS117((int)bateria2S117);
        FindObjectOfType<BateriaS1172>().ActualizarBateriaS1172((int)bateria2S117);

        if (bateria <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("GAME OVER");
        }
        else
        {
            //if (mirandoDerecha)
            //{
            //    this.GetComponent<Rigidbody2D>().position = new Vector2(this.GetComponent<Rigidbody2D>().position.x - 1, this.GetComponent<Rigidbody2D>().position.y);
            //}
            //else
            //{
            //    this.GetComponent<Rigidbody2D>().position = new Vector2(this.GetComponent<Rigidbody2D>().position.x + 1, this.GetComponent<Rigidbody2D>().position.y);
            //}
        }
    }

    private void RegenerarBateria2()
    {
        if(bateria2S117 < bateria2)
        {
            if(bateria2S117 <= 0)
            {
                if (PLayer.isPlaying)
                {
                    PLayer.Stop();
                }
                PLayer.PlayOneShot(RecargaEscudo);
            }
            bateria2S117 += 5;
            //Debug.Log("Bateria 2: " + bateria2S117 +" Bateria: " + bateria);
            if(bateria2S117 > bateria2)
            {
                bateria2S117 = bateria2;
            }
        }
        esperarHabilidadEspecial = Time.time + cadenciaHabilidadEspecial;
        FindObjectOfType<BateriaS117>().ActualizarBateriaS117((int)bateria2S117);
        FindObjectOfType<BateriaS1172>().ActualizarBateriaS1172((int)bateria2S117);
    }

    private bool PuedeUsarHabilidadEspecial(float tiempoEsperarHabilidadEspecial)
    {
        //Establece si el jugador puede usar la habilidad especial. 
        return Time.time > tiempoEsperarHabilidadEspecial;
    }

    private void InicializacionHabilidad()
    {
        if (nivelHE == 1)
        {
            bateria2S117 = 25;

        }else if(nivelHE == 2)
        {
            bateria2S117 = 50;
        }
        else
        {
            bateria2S117 = 75;
        }
        bateria2 = bateria2S117;
    }
}
