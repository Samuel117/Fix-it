using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro_Fracturado : Minijuegos
{
    // Start is called before the first frame update
    void Start()
    {
        engranajes = 12;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Recompensa()
    {
        //Recuperar 40 puntos de bateria.
        int bateria = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateria + 40);
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //Perder nivel
        Destroy(FindObjectOfType<Jugador>().gameObject);
        //Cerrar minijuego.
        CerrarMinijueogDerrota();
    }

    public void Victoria()
    {
        Cursor.visible = true;
        Recompensa();
    }

    public void Derrota()
    {
        Cursor.visible = true;
        Sansion();
    }
}