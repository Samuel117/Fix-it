using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuenteDesactivado : Minijuegos
{
    [SerializeField] GameObject PuenteReparado;
    [SerializeField] GameObject PuntoAparicion;

    // Start is called before the first frame update
    void Start()
    {
        engranajes = 5; 
        MinijuegoAbierto = false;
        IntentosDentro = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Recompensa()
    {
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateriaJugador + 15);

        Instantiate(PuenteReparado, PuntoAparicion.transform.position, PuntoAparicion.transform.rotation);

        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
    }
    public void Ganar()
    {
        Recompensa();
        CerrarMinijuegoVictoria();
    }

    public void Perder()
    {
        CerrarMinijueogDerrota();
    }

}