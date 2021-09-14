using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colega_Desactivado : Minijuegos
{
    [SerializeField] GameObject Reparado;

    // Start is called before the first frame update
    void Start()
    {
        engranajes = 10;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    protected override void Recompensa()
    {
        Instantiate(Reparado, new Vector2(this.transform.position.x, FindObjectOfType<Jugador>().transform.position.y), this.transform.rotation);

     float bateria = FindObjectOfType<Jugador>().BateriaGetter();
        if(bateria < FindObjectOfType<Jugador>().bateriaMax)
        {
            FindObjectOfType<Jugador>().BateriaSetter(FindObjectOfType<Jugador>().bateriaMax);
        }
        else
        {
            FindObjectOfType<Jugador>().escudo = 25;
        }
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //El jugador pierde el nivel
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().RecibirDano(bateriaJugador);
        if(FindObjectOfType<Jugador>() != null)
        {
            Destroy(FindObjectOfType<Jugador>().gameObject);
        }
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
