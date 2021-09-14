using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : Jugador
{
    //Valores unicos para personaje Atom. (Deberan leerse desde los archivos del juego).
    //private float bateriaAtom = 70f;
    //private float velocidadAtom = 5f; 
    //private float cadenciaDeDisparoAtom = 0.5f;
    //private float nivelHabilidadAtom = 3f;
    //private float danoAtom = 20f;

    //Control de habilidad especial.
    private float duracionHabilidadEspecial = 0f;
    private float cadenciaHabilidadEspecial = 35f;
    private float esperarHabilidadEspecial = 0f;

    void Start()
    {
        //Inicializamos los valores unicos de Atom. 

        CargarJugador();
        bateriaMax = bateria;

        //bateria = bateriaAtom;
        //nivelHE = nivelHabilidadAtom;
        //dano = danoAtom;

        //velocidad = velocidadAtom;
        //cadenciaDeDisparo = cadenciaDeDisparoAtom;

        velocidadRelentizado = velocidad / 2;
        velocidadEstandar = velocidad;
        Matdefault = sr.material;
        //GuardarJugador();

        Debug.Log(bateria);
        Debug.Log(nivelHE);
        Debug.Log(dano);
        Debug.Log(velocidad);
        Debug.Log(cadenciaDeDisparo);
    }

    //void FixedUpdate()
    //{
    //    //Controles de movimiento 
    //    ManejarMovimiento();
    //    ManejarSalto();
    //}

    //private void Update()
    //{
    //        ManejarHabilidadEspecial();
    //        ManejarDisparo();
    //}

    protected override void ManejarHabilidadEspecial()
    {
        //Control.
        float ejeFuego2 = Input.GetAxisRaw("Fire2");

        if (PuedeUsarHabilidadEspecial(esperarHabilidadEspecial))
        {
            if (!FindObjectOfType<UI_Habilidad>().ready)
            {
                FindObjectOfType<UI_Habilidad>().HElista();
            }
        }

        //Verifica que sea posible disparar en el momento.
        if (ejeFuego2 != 0f && PuedeUsarHabilidadEspecial(esperarHabilidadEspecial) && !MinijuegoAbierto)
        {
            FindObjectOfType<UI_Habilidad>().HEnoLista();
            NivelHabilidadEspecial();
            HabilidadEspecial();
        }
        else if (Time.time > duracionHabilidadEspecial)
        {
            TerminarHabilidadEspecial();
        }
    }

    private void HabilidadEspecial()
    {
        cadenciaDeDisparo = 0.25f;
    }

    private void TerminarHabilidadEspecial()
    {
        cadenciaDeDisparo = 0.5f;
    }

    private bool PuedeUsarHabilidadEspecial(float tiempoEsperarHabilidadEspecial)
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 
        return Time.time > tiempoEsperarHabilidadEspecial && !saltando;
    }

    private void NivelHabilidadEspecial()
    {
        if (nivelHE >= 2)
        {
            duracionHabilidadEspecial = Time.time + 15f;

            if (nivelHE == 3)
            {
                esperarHabilidadEspecial = Time.time + (15f + cadenciaHabilidadEspecial - 10f); //40 - 15 = 25
            }
            else
            {
                esperarHabilidadEspecial = Time.time + (cadenciaHabilidadEspecial + 15f); // 50 - 15 = 35
            }
        }
        else
        {
            duracionHabilidadEspecial = Time.time + 10f;
            esperarHabilidadEspecial = Time.time + cadenciaHabilidadEspecial + 10f;   //45 - 10  = 35 
        }
    }
}
