using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    private bool irDerecha = true;
    public int velocidadMovimiento = 350;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoFlecha();
    }


    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.gameObject.name == "Barra_Incendio")
        {
            if (irDerecha)
            {
                irDerecha = false;
            }
            else
            {
                irDerecha = true;
            }
        }
    }

    private void MovimientoFlecha()
    {
        if (irDerecha)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadMovimiento, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadMovimiento * -1, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}