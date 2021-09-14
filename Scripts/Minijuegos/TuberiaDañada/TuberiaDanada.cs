using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuberiaDanada : Minijuegos
{
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip Vic;
    [SerializeField] AudioClip Der;
    // Start is called before the first frame update
    void Start()
    {
        engranajes = 12;
        IntentosDentro = 1;
        IntentosFuera = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Recompensa()
    {
        //Recuperar 50% pts de bateria
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateriaJugador + 30);

        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
        CerrarMinijuegoVictoria();
    }
    protected override void Sansion()
    {
        //El jugador pierde el nivel
       
        CerrarMinijueogDerrota();
        Debug.Log("Nivel fallido");

        //FindObjectOfType<Jugador>().transform.position = new Vector2(-11, 1);
        Destroy(FindObjectOfType<Jugador>().gameObject);
    }

    public void Victoria()
    {
        player.PlayOneShot(Vic);
        Recompensa();
    }

    public void Derrota()
    {
        player.PlayOneShot(Der);
        Sansion();
    }
}