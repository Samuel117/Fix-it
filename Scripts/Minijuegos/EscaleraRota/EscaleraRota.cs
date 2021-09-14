using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraRota : Minijuegos
{
    [SerializeField] GameObject puntoAparicion;
    [SerializeField] GameObject EscaleraReparada;

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
        if (IntentosDentro == 0)
        {
            Perder();
            IntentosDentro = 3;
        }
    }

    public void ActivarMensaje()
    {
        this.transform.Find("Canvas").Find("Mensaje").gameObject.SetActive(true);
        this.transform.Find("Canvas").Find("Cadenas").gameObject.SetActive(true);
    }

    public void DesactivarMensaje()
    {
        this.transform.Find("Canvas").Find("Mensaje").gameObject.SetActive(false);
        this.transform.Find("Canvas").Find("Cadenas").gameObject.SetActive(false);
    }

    public void ActivarLetras()
    {
        this.transform.Find("Canvas").Find("LetraRandom").gameObject.SetActive(true);
    }

    protected override void Recompensa()
    {
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateriaJugador + 20);

        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
    }
    public void ganar()
    {
        Instantiate(EscaleraReparada, puntoAparicion.transform.position, puntoAparicion.transform.rotation);
        Recompensa();
        CerrarMinijuegoVictoria();
    }

    public void Perder()
    {
        CerrarMinijueogDerrota();
    }

}
