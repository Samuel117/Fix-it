using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codo : Piezas_Elevador
{
    private bool arribaCodo, abajoCodo, izquierdaCodo, derechaCodo;
    // Start is called before the first frame update
    //void Start()
    //{
       
    //}

    // Update is called once per frame
    void Update()
    {
        Rotacion();
        Direccionamiento();
    }

    //ROTAR
    //DIRECCIONAR BOLA 

    private void Direccionamiento()
    {
        if(index == 0)
        {
            if (FindObjectOfType<Jugador_Elevador>().abajo)
            {
                derechaCodo = true;
                abajoCodo = arribaCodo = izquierdaCodo = false;
            }

            if (FindObjectOfType<Jugador_Elevador>().izquierda)
            {
                arribaCodo = true;
                abajoCodo = derechaCodo = izquierdaCodo = false;
            }
        }

        if(index == 1)
        {
            if (FindObjectOfType<Jugador_Elevador>().abajo)
            {
                izquierdaCodo = true;
                abajoCodo = arribaCodo = derechaCodo = false;
            }

            if (FindObjectOfType<Jugador_Elevador>().derecha)
            {
                arribaCodo = true;
                abajoCodo = derechaCodo = izquierdaCodo = false;
            }
        }

        if(index == 2)
        {
            if (FindObjectOfType<Jugador_Elevador>().derecha)
            {
                abajoCodo = true;
                arribaCodo = derechaCodo = izquierdaCodo = false;
            }

            if (FindObjectOfType<Jugador_Elevador>().arriba)
            {
                izquierdaCodo = true;
                arribaCodo = derechaCodo = abajoCodo = false;
            }
        }
        
        if (index == 3)
        {
            if (FindObjectOfType<Jugador_Elevador>().izquierda)
            {
                abajoCodo = true;
                arribaCodo = derechaCodo = izquierdaCodo = false;
            }

            if (FindObjectOfType<Jugador_Elevador>().arriba)
            {
                derechaCodo = true;
                arribaCodo = izquierdaCodo = abajoCodo = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetType() == typeof(CircleCollider2D))
        {
            if (derechaCodo)
            {
                FindObjectOfType<Jugador_Elevador>().derecha = true;
                FindObjectOfType<Jugador_Elevador>().abajo = FindObjectOfType<Jugador_Elevador>().izquierda = FindObjectOfType<Jugador_Elevador>().arriba = false;
            }

            if (arribaCodo)
            {
                FindObjectOfType<Jugador_Elevador>().arriba = true;
                FindObjectOfType<Jugador_Elevador>().abajo = FindObjectOfType<Jugador_Elevador>().izquierda = FindObjectOfType<Jugador_Elevador>().derecha = false;
            }

            if (izquierdaCodo)
            {
                FindObjectOfType<Jugador_Elevador>().izquierda = true;
                FindObjectOfType<Jugador_Elevador>().abajo = FindObjectOfType<Jugador_Elevador>().arriba = FindObjectOfType<Jugador_Elevador>().derecha = false;
            }

            if (abajoCodo)
            {
                FindObjectOfType<Jugador_Elevador>().abajo = true;
                FindObjectOfType<Jugador_Elevador>().izquierda = FindObjectOfType<Jugador_Elevador>().arriba = FindObjectOfType<Jugador_Elevador>().derecha = false;
            }
        }


    }
}
