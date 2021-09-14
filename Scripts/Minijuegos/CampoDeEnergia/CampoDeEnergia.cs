using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoDeEnergia : Minijuegos
{
    // Start is called before the first frame update
    void Start()
    {
        engranajes = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Recompensa()
    {
        //Recuperar 15 puntos de bateria.
        int bateria = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateria + 15);
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //Perde 15 puntos de bateria mientras tenga mas de 1.
        float bateria = FindObjectOfType<Jugador>().BateriaGetter();

        if(bateria <= 15)
        {
            FindObjectOfType<Jugador>().BateriaSetter(1);
        }
        else
        {
            FindObjectOfType<Jugador>().RecibirDano(10);
        }

        //Cerrar minijuego.
        CerrarMinijueogDerrota();
    }

    public void Victoria()
    {
        Recompensa();
    }

    public void Derrota()
    {
        Sansion();
    }
}
