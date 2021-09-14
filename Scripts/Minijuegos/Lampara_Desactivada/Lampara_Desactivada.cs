using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lampara_Desactivada : Minijuegos
{
    [SerializeField] private GameObject reparado;
    [SerializeField] private GameObject jugador;
    public int lamparaApoyada = 0;

    // Start is called before the first frame update
    void Start()
    {
        engranajes = 5;
    }

    // Update is called once per frame
    void Update()
    {
        ControlJugador();
    }

    protected override void Recompensa()
    {
        //Recuperar 10 puntos de bateria.
        int bateria = FindObjectOfType<Jugador>().BateriaGetter();
        
        FindObjectOfType<Jugador>().BateriaSetter(bateria + 20);
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //Cerrar minijuego.
        CerrarMinijueogDerrota();
    }

    public void Victoria()
    {
        Instantiate(reparado, this.transform.position, this.transform.rotation);
        Recompensa();
    }

    public void Derrota()
    {
        Sansion();
    }

    private void ControlJugador()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && FindObjectOfType<Jugador>() != null && FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            jugador.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            jugador.GetComponent<Animator>().SetBool("Up", false);
            lamparaApoyada = 2;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            jugador.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            jugador.GetComponent<Animator>().SetBool("Up", false);
            lamparaApoyada = 1;
        }

        if (FindObjectOfType<Jugador>()!= null && Input.GetKeyDown(KeyCode.UpArrow) && FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            //Sprite mirando Hacia Arriba
           // jugador.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            jugador.GetComponent<Animator>().SetBool("Up", true);
            lamparaApoyada = 3;
        }
    }
}