using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVDv1 : Enemigo
{

    protected float esperarLiberacion = 5f;
    protected bool usandoHabilidadEspecial = false;
    [SerializeField] protected GameObject prefabHabilidadEspecial;
   

    // Start is called before the first frame update
    void Start()
    {
        EnemigoData.EnemyData enemigo = EnemigoData.CargarEnemigo("DVDV1");
        bateria = enemigo.Valores[1];
        cadenciaDeDisparo = 1f;
        velocidadMovimiento = enemigo.Valores[3];
        alcance = enemigo.Valores[4];
        engranajesDerrota = 4;

        Matdefault = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
       if(bateria >= 40)
        {
            Movimiento();
            Disparar();
        }
        else
        {
            if (!usandoHabilidadEspecial)
            {
                esperarLiberacion = Time.time + esperarLiberacion;
            }
           
            HabilidadEspecial();
        }

       
    }

    protected override void HabilidadEspecial()
    {
        this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetBool("Idle", false);

        usandoHabilidadEspecial = true;
        velocidadMovimiento = 10f;

        posicionJugadorX = FindObjectOfType<Jugador>().transform.position.x;
        posicionEnemigoX = this.transform.position.x;

        posicionJugadorY = FindObjectOfType<Jugador>().transform.position.y;
        posicionEnemigoY = this.transform.position.y;

        distanciaX = posicionEnemigoX - posicionJugadorX;
        distanciaY = Mathf.Abs(posicionEnemigoY - posicionJugadorY);

        Girar();

        if (mirandoIzquierda)
        {
            //Sigue corriendo
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocidadMovimiento, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            //Sigue corriendo
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadMovimiento, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        

        if (Time.time > esperarLiberacion)
        {
            Destruir();
        }
    }

    protected override void Destruir()
    {
     if(this.transform.Find("DVDv1Desactivado") == null && this.transform.Find("DVDv2Desactivado") == null)
        {
            Instantiate(Desactivado, new Vector2(this.transform.position.x, this.transform.position.y + 1f), this.transform.Find("Enemigo").gameObject.transform.rotation, this.gameObject.transform);
        }
      
        Instantiate(prefabHabilidadEspecial, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

    private void Girar()
    {
        if (mirandoIzquierda && posicionJugadorX > posicionEnemigoX && distanciaX <= -2)
        {
            mirandoIzquierda = false;
            this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(!mirandoIzquierda && posicionJugadorX < posicionEnemigoX && distanciaX >= 2)
        {
            mirandoIzquierda = true;
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.GetComponent<Jugador>() != null)
        {
            if (usandoHabilidadEspecial)
            {
                this.Destruir();
            }
            else
            {
                var magnitude = 3000f;
                var force = transform.position - collision.transform.position;
                force.Normalize();

                if (this.transform.position.x >= collision.gameObject.transform.position.x)
                {
                    //collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x - 3, collision.gameObject.transform.position.y);

                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * magnitude);
                }
                else
                {
                    //collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x + 3, collision.gameObject.transform.position.y);
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * magnitude);
                }

               // this.HacerDano();
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (!usandoHabilidadEspecial)
            {
                if (mirandoIzquierda)
                {
                    mirandoIzquierda = false;
                    this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else
                {
                    mirandoIzquierda = true;
                    this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            else
            {
                this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetBool("Aire", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && usandoHabilidadEspecial)
        {
            this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetBool("Aire", false);
        }
    }

}
