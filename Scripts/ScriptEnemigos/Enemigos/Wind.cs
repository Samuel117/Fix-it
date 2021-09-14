using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Enemigo
{
    float esperarGiro = 0f;
    [SerializeField] private GameObject Viento;

    // Start is called before the first frame update
    void Start()
    {
        EnemigoData.EnemyData enemigo = EnemigoData.CargarEnemigo("Wind");
        bateria = enemigo.Valores[1];
        //cadenciaDeDisparo = 1f;
        //velocidadMovimiento = 6f;
        alcance = enemigo.Valores[4];
        engranajesDerrota = 2;

        Matdefault = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Disparar();
        DragViento();
    }


    protected override void Movimiento()
    {
        if(FindObjectOfType<Jugador>() != null)
        {
            //Posiciones de enemigo y jugador en eje X y Y.
            posicionJugadorX = FindObjectOfType<Jugador>().transform.position.x;
            posicionEnemigoX = this.transform.position.x;

            posicionJugadorY = FindObjectOfType<Jugador>().transform.position.y;
            posicionEnemigoY = this.transform.position.y;

            //Distancias entre enemigo y jugador en eje X y Y.
            distanciaX = posicionEnemigoX - posicionJugadorX;
            distanciaY = Mathf.Abs(posicionEnemigoY - posicionJugadorY);

        }

        //Evitamos que este enemigo pueda moverse.
        //this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //Girar cada cierto tiempo.
        if (PuedeGirar(esperarGiro))
        {
            if (mirandoIzquierda)
            {
                if (!DebePararMovimiento(alcance))
                {
                    mirandoIzquierda = false;
                    this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
            }
            else
            {
                if (!DebePararMovimiento(-alcance))
                {
                    mirandoIzquierda = true;
                    this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }

            esperarGiro += 5;
        }

        if (mirandoIzquierda)
        {//Izquierda
          
            //Alcance de disparo
            mira = Physics2D.Raycast(origen.transform.position, Vector2.left, alcance);
            Debug.DrawRay(origen.transform.position, Vector2.left * alcance, Color.blue);

            //Dirección a la que mira el enemigo.
            vision = Physics2D.Raycast(origen.transform.position, Vector2.left, 1000f);
            Debug.DrawRay(origen.transform.position, Vector2.left * 1000f, Color.green);
        }
        else
        {//Derecha

            //Alcance de disparo
            mira = Physics2D.Raycast(origen.transform.position, Vector2.right, alcance);
            Debug.DrawRay(origen.transform.position, Vector2.right * alcance, Color.blue);

            //Dirección a la que mira el enemigo.
            vision = Physics2D.Raycast(origen.transform.position, Vector2.right, 1000f);
            Debug.DrawRay(origen.transform.position, Vector2.right * 1000f, Color.green);
        }
    }

    private bool PuedeGirar(float tiempoEsperarGiro)
    {
        //Establece si Wind puede o no girar. 
        return Time.time > tiempoEsperarGiro;
    }

    protected override void Disparar()
    {
        if (mirandoIzquierda)
        {
            Viento.GetComponent<AreaEffector2D>().forceAngle = 180f;
        }
        else
        {
            Viento.GetComponent<AreaEffector2D>().forceAngle = 0f;
        }
       
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void DragViento()
    {
        if (FindObjectOfType<Jugador>()!= null && FindObjectOfType<Jugador>().disparando)
        {
            this.transform.Find("VientoPrueba").gameObject.GetComponent<AreaEffector2D>().drag = 250;
        }
        else
        {
            this.transform.Find("VientoPrueba").gameObject.GetComponent<AreaEffector2D>().drag = 0;
        }
    }
}
