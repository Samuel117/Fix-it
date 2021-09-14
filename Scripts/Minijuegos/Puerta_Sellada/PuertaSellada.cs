using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaSellada : Minijuegos
{
    // Start is called before the first frame update
    void Start()
    {
        engranajes = 5;
        MinijuegoAbierto = false;
        IntentosDentro = 2;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IntentosDentro == 0)
        {
            FindObjectOfType<Columnas>().ReiniciarJuego();
            IntentosDentro = 2;
            MinijuegoAbierto = false;
            CerrarMinijueogDerrota();
        }
    }

    protected override void Recompensa()
    {
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateriaJugador + 10);

        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
    }

    public void Ganar()
    {
        Recompensa();
        CerrarMinijuegoVictoria();
    }
}