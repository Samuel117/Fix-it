using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fondo : MonoBehaviour
{
    public bool Reparado = false;
    private float esperar = 0f;
    private float velocidad = 15f;

    [SerializeField] GameObject efectoCaida;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!Reparado)
        {
            if (FindObjectOfType<Jugador>()!= null && FindObjectOfType<Jugador>().fuerzaSalto == 3f)
            {
                FindObjectOfType<Jugador>().fuerzaSalto = 2f;
                FindObjectOfType<Jugador>().GetComponent<Rigidbody2D>().gravityScale = 1f;
            }
            Movimiento();
        }
        else
        {
            if(FindObjectOfType<Jugador>() != null)
            {
                FindObjectOfType<Jugador>().fuerzaSalto = 3f;
                FindObjectOfType<Jugador>().GetComponent<Rigidbody2D>().gravityScale = 3f;
            }
            MovRed();
        }
    }

    private void Movimiento()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.up * 15;
    }

    private void MovRed()
    {
        if (Esperar(esperar) && velocidad > 0)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.up * velocidad;
            velocidad = velocidad - 2;

            esperar = Time.time + 0.5f;

            if(velocidad <= 7)
            {
                efectoCaida.gameObject.SetActive(false);
            }

            if(velocidad <= 0)
            {
                velocidad = 0;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    private bool Esperar(float esperar)
    {
       return Time.time > esperar;
    }
}
