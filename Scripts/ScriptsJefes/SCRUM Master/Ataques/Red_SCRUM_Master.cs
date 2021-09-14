using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_SCRUM_Master : MonoBehaviour
{
    private int dano = 10;
    private float velocidad = 20f;
    private bool jugadorTocado = false;
    private Vector2 direccionDisparo;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destruir", 2f);
        EncontrarJugador();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GenerarRed();

        if (FindObjectOfType<Jugador>()!= null && FindObjectOfType<Jugador>().atrapado == false && jugadorTocado == true)
        {
            Destroy(this.gameObject);
        }
    }

    protected void GenerarRed()
    {
        this.GetComponent<Rigidbody2D>().AddForce(this.direccionDisparo * this.velocidad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Hacer daño constante
            FindObjectOfType<Jugador>().AtrapadoPorSCRUM(dano);
            // Detener movimiento y posicionarse
            velocidad = 0f;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            this.gameObject.transform.position = FindObjectOfType<Jugador>().transform.position;
            jugadorTocado = true;
            this.GetComponent<Animator>().SetBool("Atrapar", true);
        }
    }

    private void Destruir()
    {
        if (jugadorTocado == false)
        {
            Destroy(this.gameObject);
        }
    }

    protected void EncontrarJugador()
    {
        direccionDisparo = (GameObject.FindObjectOfType<Jugador>().transform.position - this.transform.position).normalized;
    }
}
