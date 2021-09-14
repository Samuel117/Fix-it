using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TormentaArena : MonoBehaviour
{
    private float espera = 0f;
    private float esperaIntensidad = 0f;
    private float alpha = 0f;
    private bool conf = false;
    private bool desaparecer = false;
    private float esperarDesaparecer;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
        this.gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 0; 
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
            if(alpha == 0.5f && !conf)
            {
               this.gameObject.GetComponent<AreaEffector2D>().forceMagnitude = -250;
               conf = true;
               desaparecer = true;
               esperarDesaparecer = Time.time + 10f;
            }

            if (FindObjectOfType<Jugador>() != null && FindObjectOfType<Jugador>().disparando)
            {
                this.gameObject.GetComponent<AreaEffector2D>().drag = 250;
            }
            else
            {
                this.gameObject.GetComponent<AreaEffector2D>().drag = 0;
            }

        if (!desaparecer)
        {
            Intensidad();
        }
        else if(AumentarIntensidad(esperarDesaparecer))
        {
            this.gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 0;
            
            Desaparecer();
            if(alpha <= 0)
            {
                Destroy(this.gameObject);
            }
        }
       
        Movimiento();
    }
    private void Movimiento()
    {
        if (DebeEsperarMovimiento(espera))
        {
            this.transform.position = new Vector2(this.transform.position.x - 0.5f, this.transform.position.y);
            espera = Time.time + 0.1f;
        }
    }

    private void Intensidad()
    {
        if (AumentarIntensidad(esperaIntensidad) && alpha < 0.5f)
        {
            alpha+= 0.1f;
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
            esperaIntensidad = Time.time + 1f;
        }
    }

    private bool DebeEsperarMovimiento(float esperar)
    {
        return Time.time > esperar;
    }
    private bool AumentarIntensidad(float esperar)
    {
        return Time.time > esperar;
    }

    private void Desaparecer()
    {
        if (AumentarIntensidad(esperaIntensidad))
        {
            alpha -= 0.1f;
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
            esperaIntensidad = Time.time + 1f;
        }
    }
}
