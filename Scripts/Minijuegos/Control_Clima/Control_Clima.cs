using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Clima : Minijuegos
{
    // Start is called before the first frame update
    void Start()
    {
        engranajes = 8; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Recompensa()
    {
        //Recuperar 15 puntos de bateria
        int bateria = FindObjectOfType<Jugador>().BateriaGetter();
        FindObjectOfType<Jugador>().BateriaSetter(bateria + 15);
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //Perder 5 puntos de bateria 
        FindObjectOfType<Jugador>().RecibirDano(10);
        FindObjectOfType<Focos_Clima>().Reiniciar();
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
