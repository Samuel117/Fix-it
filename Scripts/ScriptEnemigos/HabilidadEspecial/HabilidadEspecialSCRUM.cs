using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadEspecialSCRUM : MonoBehaviour
{
    private int dano = 5;
    private float velocidad = 10f;
    private bool jugadorTocado = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destruir", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (FindObjectOfType<Jugador>()!= null && FindObjectOfType<Jugador>().atrapado == false && !jugadorTocado)
        {
            GenerarRed();
            //Destroy(this.gameObject);
        }
        else if(FindObjectOfType<Jugador>() != null && jugadorTocado)
        {
            this.transform.position = FindObjectOfType<Jugador>().transform.position;
            
            if (!FindObjectOfType<Jugador>().atrapado)
            {
                Destroy(this.gameObject);
            }
        }
    }

    protected void GenerarRed()
    {
        this.transform.Translate(Vector2.left * this.velocidad * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Hacer daño constante
            FindObjectOfType<Jugador>().AtrapadoPorSCRUM(dano);
            // Detener movimiento y posicionarse
            velocidad = 0f;
            this.gameObject.transform.position = FindObjectOfType<Jugador>().transform.position;
            jugadorTocado = true;
            this.GetComponent<Animator>().SetBool("Atrapar", true);
        }

        if (collision.gameObject.layer == 16)
        {
            Destroy(this.gameObject);
        }
    }

    private void Destruir()
    {
        if (jugadorTocado == false)
        {
            Destroy(this.gameObject);
        }
    }
}
