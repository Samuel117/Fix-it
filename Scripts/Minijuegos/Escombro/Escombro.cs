using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escombro : Minijuegos
{
    int num;
    public bool victoria = false;

    // Start is called before the first frame update
    void Start()
    {
        engranajes = 5;
        num = Random.Range(1, 4);
        this.transform.Find("Canvas").Find("Obstaculos" + num.ToString()).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Recompensa()
    {
        //Recuperar 15 pts de bateria
        int bateriaJugador = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateriaJugador + 15);

        Debug.Log(FindObjectOfType<Jugador>().BateriaGetter());
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //El jugador pierde 5 puntos de bateria
        FindObjectOfType<Jugador>().RecibirDano(10);

        CerrarMinijueogDerrota();
    }

    public void Victoria()
    {
        Recompensa();
    }

    public void Derrota()
    {
        Reinicio();
        Sansion();
    }

    public void Reinicio()
    {
        for(int x = 1; x < 4; x++)
        {
            this.transform.Find("Canvas").Find("Obstaculos" + x.ToString()).gameObject.SetActive(false);
        }

        num = Random.Range(1, 4);
        this.transform.Find("Canvas").Find("Obstaculos" + num.ToString()).gameObject.SetActive(true);

        FindObjectOfType<Taladro>().vidas = 3;
        FindObjectOfType<Taladro>().gameObject.transform.position = FindObjectOfType<Taladro>().pos;
        FindObjectOfType<Taladro>().gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        FindObjectOfType<Taladro>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        PasoTals[] paso = FindObjectsOfType<PasoTals>();

        for(int x = 0; x < paso.Length; x++)
        {
            Destroy(paso[x].gameObject);
        }
    }
}
