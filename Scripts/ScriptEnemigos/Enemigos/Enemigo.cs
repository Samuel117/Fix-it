using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    protected float bateria;
    protected float velocidadMovimiento;
    protected bool mirandoIzquierda = false;
    protected float cadenciaDeDisparo;
    protected float esperarDisparo = 0f;
    public bool disparando = false;
    static protected float danoDisparo;
    protected float alcance;
    
    protected float distanciaX = 0f;
    protected float distanciaY = 0f;

    protected float posicionJugadorX;
    protected float posicionJugadorY;

    protected float posicionEnemigoX;
    protected float posicionEnemigoY;

   protected RaycastHit2D mira;
   protected RaycastHit2D vision;
    private int ignorarLayer = 9 | 13; //layer 9, otros enemigos.
    
    //ENGRANAJES.
    protected int engranajesDerrota;

    //Prefab del proyectil
    [SerializeField] protected GameObject prefabDisparo;
    [SerializeField] protected GameObject origen;

    [SerializeField] protected SpriteRenderer sr;
    protected Material Matdefault;
    [SerializeField] protected Material MatWhite;

    [SerializeField] protected GameObject Desactivado;
    [SerializeField] private AudioSource SoundPlayer;
    [SerializeField] private AudioClip disparoSound;
    [SerializeField] private AudioClip danoRecibido;
    [SerializeField] protected GameObject paquete;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    protected virtual void Movimiento()
    {
        //Distancia entre enemigo y jugador en eje X.
        //distancia = this.transform.position.x - FindObjectOfType<Jugador>().transform.position.x;
        if(FindObjectOfType<Jugador>() != null)
        {
            posicionJugadorX = FindObjectOfType<Jugador>().transform.position.x;
            posicionEnemigoX = this.transform.position.x;

            posicionJugadorY = FindObjectOfType<Jugador>().transform.position.y;
            posicionEnemigoY = this.transform.position.y;

            distanciaX = posicionEnemigoX - posicionJugadorX;
            distanciaY = Mathf.Abs(posicionEnemigoY - posicionJugadorY);
        }
      
        if (!disparando)
        {
            if (mirandoIzquierda)
            {//Izquierda
                if (DebePararMovimiento(alcance - 1))
                {
                    this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetBool("Idle", true);
                    this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
                else
                {
                    this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetBool("Idle", false);
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocidadMovimiento, this.GetComponent<Rigidbody2D>().velocity.y);
                }
                //Alcance de disparo
                mira = Physics2D.Raycast(origen.transform.position, Vector2.left, alcance, ignorarLayer);
                Debug.DrawRay(origen.transform.position, Vector2.left * alcance, Color.blue);

                vision = Physics2D.Raycast(origen.transform.position, Vector2.left, 1000f);
                Debug.DrawRay(origen.transform.position, Vector2.left * 1000f, Color.green);


            }
            else
            {//Derecha

                if (DebePararMovimiento(-(alcance - 1)))
                {
                    this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetBool("Idle", true);
                    this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
                else
                {
                    this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetBool("Idle", false);
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadMovimiento, this.GetComponent<Rigidbody2D>().velocity.y);
                }
                mira = Physics2D.Raycast(origen.transform.position, Vector2.right, alcance, ignorarLayer);
                Debug.DrawRay(origen.transform.position, Vector2.right * alcance, Color.blue);

                vision = Physics2D.Raycast(origen.transform.position, Vector2.right, 1000f);
                Debug.DrawRay(origen.transform.position, Vector2.right * 1000f, Color.green);
            }
        }
      
    }

    protected virtual void Disparar()
    {
        //Verifica que sea posible disparar en el momento.
        if (PuedeDisparar(esperarDisparo) && mira != false && !disparando)
        {
            if (mira.collider.tag == "Player")
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.disparando = true;
                this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetTrigger("Disparar");
                this.transform.Find("Enemigo").gameObject.GetComponent<Animator>().SetBool("Idle", false);
                SoundPlayer.PlayOneShot(disparoSound);
                Instantiate(this.prefabDisparo, this.transform.position, this.transform.rotation);
                 
                this.esperarDisparo = Time.time + this.cadenciaDeDisparo;
            }
        }
    }

    private bool PuedeDisparar(float tiempoEsperaDisparo)
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 
        return Time.time > tiempoEsperaDisparo;
    }

    protected virtual void HabilidadEspecial()
    {

    }

    public virtual void RecibirDano(float danoRecibido)
    {
        SoundPlayer.PlayOneShot(this.danoRecibido);
        this.bateria = this.bateria - danoRecibido;
        
        EfectoRecibirDano();
        Invoke("TerminarEfectoRecibirDano", 0.1f);
      
        if (this.bateria <= 0)
        {
            FindObjectOfType<ControladorNvl>().SumarEngranajes(engranajesDerrota);
            Instantiate(Desactivado, new Vector2(this.transform.position.x, this.transform.position.y + 1f) , this.transform.Find("Enemigo").gameObject.transform.rotation);
            AparecerPaquete();
            this.Destruir();
        }

        GirarAlRecibirDaño();
    }

    protected void EfectoRecibirDano()
    {
        sr.material = MatWhite;
    }

    private void TerminarEfectoRecibirDano()
    {
        sr.material = Matdefault;
    }

    protected virtual void Destruir()
    {
        Destroy(this.gameObject);
    }

    public void HacerDano()
    {
        FindObjectOfType<Jugador>().RecibirDano(1);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
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
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Jugador>() != null)
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

            this.HacerDano();
        }
    }

    protected bool DebePararMovimiento(float limiteX)
    {
     
        if(limiteX < 0)
        {
            return posicionJugadorX > posicionEnemigoX && distanciaX > limiteX && distanciaY <= 2.5f;
        }
        else
        {
            return posicionJugadorX < posicionEnemigoX && distanciaX < limiteX && distanciaY <= 2.5f;
        }
    }

    protected virtual void GirarAlRecibirDaño()
    {
        if (mirandoIzquierda)
        {
            if ((vision != false && vision.collider.tag != "Player" && posicionEnemigoX < posicionJugadorX) || vision == false && posicionEnemigoX < posicionJugadorX)
            {
                mirandoIzquierda = false;
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
        else
        {
            if ((vision != false && vision.collider.tag != "Player" && posicionEnemigoX > posicionJugadorX) || vision == false && posicionEnemigoX > posicionJugadorX)
            {
                mirandoIzquierda = true;
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

  protected void AparecerPaquete()
    {
        int ran = Random.Range(0, 2);

        if(ran == 0)
        {
            Instantiate(paquete, new Vector2(this.transform.position.x, this.transform.position.y + 0.5f), paquete.transform.rotation);
        }
    }

}
