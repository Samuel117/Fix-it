using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incendio : Minijuegos
{
    public int puntos = 0;

    // Start is called before the first frame update
    void Start()
    {
        engranajes = 5;
        MinijuegoAbierto = false;
        IntentosDentro = 3;
    }

    // Update is called once per frame
    void Update()
    {
     if(IntentosDentro <= 0)
        {
            Sansion();
        }

     if(puntos >= 3)
        {
            Recompensa();
        }
    }

    protected override void Recompensa()
    {
        //Recuperar 15 pts de bateria
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateriaJugador + 15);

        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //El jugador pierde 10 de bateria
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        if (bateriaJugador > 1)
        {
            FindObjectOfType<Jugador>().RecibirDano(10);
        }

        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
        ReiniciarMinijuego();
        CerrarMinijueogDerrota();
    }

    private void ReiniciarMinijuego()
    {
        FindObjectOfType<Flecha>().velocidadMovimiento = 350;
        FindObjectOfType<SeccionBlanca>().ReiniciarTamano();
        IntentosDentro = 4;
        puntos = 0;
    }
}