using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal_Datos : Minijuegos
{
    [SerializeField] GameObject Coleccionable;
    [SerializeField] GameObject Enemigo;
    [SerializeField] GameObject Aparicion;

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
        Instanciar(1);
        float bateria = FindObjectOfType<Jugador>().BateriaGetter();
        if (bateria < FindObjectOfType<Jugador>().bateriaMax)
        {
            FindObjectOfType<Jugador>().BateriaSetter(FindObjectOfType<Jugador>().bateriaMax);
        }
        else
        {
            FindObjectOfType<Jugador>().escudo = 25;
        }
        CerrarMinijuegoVictoria();

        Destroy(this.gameObject);
    }

    protected override void Sansion()
    {
        //El jugador pierde el nivel
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().RecibirDano(bateriaJugador);
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

    private void Instanciar(int x)
    {
        switch (x)
        {
            case 0:
                Instantiate(Enemigo, Aparicion.transform.position, Enemigo.transform.rotation);
              
                break;
            case 1:
                Instantiate(Coleccionable, new Vector2(this.transform.position.x + 2, FindObjectOfType<Jugador>().transform.position.y), this.transform.rotation);
                
                break;
        }
    }
}
