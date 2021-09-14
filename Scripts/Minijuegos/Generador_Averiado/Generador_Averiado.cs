using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador_Averiado : Minijuegos
{
    private bool ActivarElect = false;
    private string EleccionJugador;
    private string RespuestaCorrecta = "d";
    public bool debeMoverElec = true;
    public int cont = 0;

    [SerializeField] public AudioSource player;
    [SerializeField] AudioClip seleccion;
    [SerializeField] AudioClip victoria;
    [SerializeField] AudioClip derrota;
    [SerializeField] public AudioClip llegar;
    [SerializeField] GameObject Generador;


    [SerializeField] private GameObject electricidad;
    // Start is called before the first frame update
    void Start()
    {
        engranajes = 10;
        IntentosDentro = 2;    
    }

    // Update is called once per frame
    void Update()
    {
        if (ActivarElect)
        {
            if (debeMoverElec)
            {
                this.transform.Find("Canvas").Find("Electricidad1").gameObject.GetComponent<MoviminetoElec>().MoverElect();
                this.transform.Find("Canvas").Find("Electricidad2").gameObject.GetComponent<MoviminetoElec>().MoverElect();
                this.transform.Find("Canvas").Find("Electricidad3").gameObject.GetComponent<MoviminetoElec>().MoverElect();
                this.transform.Find("Canvas").Find("Electricidad4").gameObject.GetComponent<MoviminetoElec>().MoverElect();
            }
        }
        else
        {
            ActivarMovimientoElec();
        }
    }

    protected override void Recompensa()
    {
        Instantiate(Generador, this.transform.position, this.transform.rotation);

        //Recuperar 15 pts de bateria
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateriaJugador + 35);

        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //El jugador pierde 50 de bateria, sera desactivado si tiene menos puntos de bateria.
        FindObjectOfType<Jugador>().RecibirDano(10);
        
        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
        //ReiniciarMinijuego();
        CerrarMinijueogDerrota();
    }

    private void ActivarMovimientoElec() {

        EleccionJugador = Input.inputString;
        
        if (EleccionJugador == "a" || EleccionJugador == "b" || EleccionJugador == "c" || EleccionJugador == "d")
        {
            player.PlayOneShot(seleccion);
            Debug.Log(EleccionJugador);
            ActivarElect = true;
        }
        else
        {
            EleccionJugador = "";
        }

    }

    public void CoomprovarResp()
    {
        if(EleccionJugador == RespuestaCorrecta)
        {
            player.PlayOneShot(victoria);
            Recompensa();
        }else if (IntentosDentro > 0)
        {
            player.PlayOneShot(derrota);
            IntentosDentro--;
            ReiniciarJuego();
        }else if(IntentosDentro <= 0)
        {
            player.PlayOneShot(derrota);
            Sansion();
        }

    }

    public void ReiniciarJuego()
    {
        //Vector2 posicionElec = new Vector2(550,800);

        // Instantiate(electricidad, posicionElec, this.transform.rotation, this.transform.Find("Canvas"));

        this.transform.Find("Canvas").Find("Electricidad1").gameObject.GetComponent<MoviminetoElec>().ReiniciarPosicion();
        this.transform.Find("Canvas").Find("Electricidad2").gameObject.GetComponent<MoviminetoElec>().ReiniciarPosicion();
        this.transform.Find("Canvas").Find("Electricidad3").gameObject.GetComponent<MoviminetoElec>().ReiniciarPosicion();
        this.transform.Find("Canvas").Find("Electricidad4").gameObject.GetComponent<MoviminetoElec>().ReiniciarPosicion();

        EleccionJugador = "";
        ActivarElect = false;
        debeMoverElec = true;
        cont = 0;

        Debug.Log("Reinicio");
    }
}
