using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador_Exterior : Minijuegos
{
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip victoria;
    [SerializeField] AudioClip derrota;
    [SerializeField] GameObject PuntoAparicion;
    [SerializeField] GameObject Elevador;

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
        //Recuperar 20 puntos de bateria.
        int bateria = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateria + 20);
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //Perder 35 puntos de salud, dejar 1 punto en caso bateria < 35.
        float bateria = FindObjectOfType<Jugador>().BateriaGetter();

        if(bateria  > 35)
        {
            FindObjectOfType<Jugador>().RecibirDano(10);
        }
        else
        {
            FindObjectOfType<Jugador>().BateriaSetter(1);
        }

        //Cerrar minijuego.
        CerrarMinijueogDerrota();
    }

    public void Victoria()
    {
        player.PlayOneShot(victoria);
        Instantiate(Elevador, PuntoAparicion.transform.position, PuntoAparicion.transform.rotation);
        Recompensa();
    }

    public void Derrota()
    {
        player.PlayOneShot(derrota);
        Reiniciar();
        Sansion();
    }

    public void Reiniciar()
    {
        FindObjectOfType<Jugador_Elevador>().transform.position = FindObjectOfType<Jugador_Elevador>().pos;
        FindObjectOfType<Jugador_Elevador>().abajo = true;
    }
}
