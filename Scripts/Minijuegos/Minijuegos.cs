using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Minijuegos : MonoBehaviour
{
    public int IntentosDentro;
    protected int IntentosFuera;
    public bool MinijuegoAbierto;
    private bool abierto = false;
    protected int engranajes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void AbrirMinijuego()
    {
        if (!abierto)
        {
            Debug.Log("Minijuego abierto");
            MinijuegoAbierto = true;
            FindObjectOfType<Jugador>().MinijuegoAbierto = true;
            this.transform.Find("Canvas").gameObject.SetActive(true);
            abierto = true;
        }
    }

    protected void CerrarMinijuegoVictoria()
    {
        Debug.Log("Minijuego completado.");
       
        if(FindObjectOfType<ControladorNvl>()!= null)
        {
            FindObjectOfType<ControladorNvl>().SumarEngranajes(engranajes);
        }
        MinijuegoAbierto = false;
        FindObjectOfType<Jugador>().MinijuegoAbierto = false;
        abierto = false;
        this.transform.Find("Canvas").gameObject.SetActive(false);
        FindObjectOfType<Bateria>().ActualizarBateria((int)FindObjectOfType<Jugador>().BateriaGetter());
        this.gameObject.SetActive(false);
        Cursor.visible = true;
    }

    protected void CerrarMinijueogDerrota()
    {
        Debug.Log("Minijuego fallido.");
        MinijuegoAbierto = false;
        FindObjectOfType<Jugador>().MinijuegoAbierto = false;
        abierto = false;
        this.transform.Find("Canvas").gameObject.SetActive(false);
        FindObjectOfType<Bateria>().ActualizarBateria((int)FindObjectOfType<Jugador>().BateriaGetter());
    }
    protected virtual void Recompensa()
    {

    }

    protected virtual void Sansion()
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Jugador>() != null)
        {
         if(Input.GetAxisRaw("Vertical") == -1)
            {
                AbrirMinijuego();
                //CANCELAR MOVIMIENTO JUGADOR.
            }
            if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                CerrarMinijuegoVictoria();
            }
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Jugador>() != null)
        {
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                AbrirMinijuego();
                //CANCELAR MOVIMIENTO JUGADOR.
            }

            if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                CerrarMinijuegoVictoria();
            }
        }


    }
}