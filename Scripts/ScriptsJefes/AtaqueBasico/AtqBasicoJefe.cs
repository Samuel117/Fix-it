using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtqBasicoJefe : MonoBehaviour
{
    protected float velocidad;
    protected float lifeSpan;
    protected int dano;
    private Vector2 direccionDisparo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    protected void GenerarProyectil()
    {
        this.GetComponent<Rigidbody2D>().AddForce(this.direccionDisparo * this.velocidad);
    }

    protected void DestruirProyectil()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<Jugador>() != null)
        {
            otro.GetComponent<Jugador>().RecibirDano(dano);
            Destroy(this.gameObject);
        }
    }

    protected void EncontrarJugador()
    {
        if(FindObjectOfType<Jugador>()!= null)
        {
            direccionDisparo = (GameObject.FindObjectOfType<Jugador>().transform.position - this.transform.position).normalized;
        }
    }
}
