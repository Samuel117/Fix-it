using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{    
    //Salud del jugador.
    protected int bateria;
    protected float nivelHE;
    protected float dano;
    public float escudo = 0;

    public int bateriaMax;

    //Variables de control para movimiento del personaje.
    public float velocidad;
    public float fuerzaSalto = 3f;
    private float gravedad = 29.43f;
    protected bool mirandoDerecha = true;
    public bool saltando = false;

    //Variables de control de disparo.
    protected float cadenciaDeDisparo;
    private float esperarDisparo = 0f;
    public bool disparando = false;
    static protected float danoDisparo;

    //EFECTO DE RELENTIZADO DE ROTOM
    public float esperarRelentizar = 0;
    public float velocidadRelentizado;
    public float velocidadEstandar;

    //EFECTO DE RELENTIZADO DE SCRUM
    private float esperarDanoAtrapado = 0f;
    private float contadorLiberacion = 5f;
    public bool atrapado = false;
    private bool controlLiberar = false;

    //EFECTO CONFUSIÓN DE SCRUM MASTER
    public bool confundido = false;
    private float duracionConf = 10f;
    protected float esperarConf = 0f;

    //Condiciones de los minijuegos
    public bool MinijuegoAbierto = false;
    //Prefab del proyectil
    [SerializeField] protected GameObject prefabDisparo;

    [SerializeField] protected SpriteRenderer sr;
    protected Material Matdefault;
    [SerializeField] protected Material MatWhite;

    [SerializeField] protected AudioSource Soundplayer;
    [SerializeField] protected AudioClip disparoSound;
    [SerializeField] protected AudioClip danoRecibidoSound;

    [SerializeField] private AudioClip saltoSound;
    [SerializeField] private AudioClip landSound;

    void FixedUpdate()
    {
        //Controles de movimiento 
        if (esperarConf > Time.time && confundido == true)
        {
            MovimientoInvertido();
        }
        else{
            confundido = false;
            ManejarMovimiento();
        }

        ManejarSalto();

        ComprobarRelentizado();
        ComprobarAtrapado();
    }

    private void Update()
    {
        //Controles de disparo
        ManejarDisparo();
        ManejarHabilidadEspecial();
    }

    public void ManejarMovimiento()
    {

        if (MinijuegoAbierto)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //Agrregar animacion de sin moverse : )
        }
        if (!disparando && !MinijuegoAbierto)
        {
            //Control y movimiento
            float ejeHorizontal = Input.GetAxisRaw("Horizontal");
            
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(ejeHorizontal * velocidad, this.GetComponent<Rigidbody2D>().velocity.y);
           
            //Animacion
            this.GetComponent<Animator>().SetBool("EnMovimiento", this.GetComponent<Rigidbody2D>().velocity.x != 0f);

            //Dirección a la que mira 
            if (this.GetComponent<Rigidbody2D>().velocity.x > 0f && !mirandoDerecha)
            {
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                this.mirandoDerecha = true;
            }
            else if (this.GetComponent<Rigidbody2D>().velocity.x < 0f && mirandoDerecha)
            {
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                this.mirandoDerecha = false;
            }
        }
    }

    public void ManejarSalto()
    {
       //Obtener input del usuario.
        float ejeVertical = Input.GetAxisRaw("Vertical");

       //Control en caso de ser atrapado por un SCRUM.
        if (atrapado == true && ejeVertical > 0)
        {
            if (controlLiberar == false)
            {
                contadorLiberacion++;
                controlLiberar = true;
            }
          
            if(ejeVertical == 0)
            {
                controlLiberar = false;
            }
        }
        else
        {
            controlLiberar = false;
            //SALTO COMÚN
            // Verificar que el jugador no este ya en el aire.
            if (ejeVertical > 0f && !saltando && !MinijuegoAbierto && !disparando)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, Mathf.Sqrt(2 * fuerzaSalto * gravedad));
                this.GetComponent<Animator>().SetBool("EnElAire", true);
                this.GetComponent<Animator>().SetBool("Saltando", true);
                this.GetComponent<Animator>().SetTrigger("Rotar");
                this.saltando = true;
               
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica que el jugador se encuentre en contacto con el piso.
        if (collision.gameObject.layer == 8 && this.transform.position.y > collision.transform.position.y)
        {
            TerminarSalto();
            Soundplayer.PlayOneShot(landSound);
            this.GetComponent<Animator>().SetBool("EnElAire", false);
            this.GetComponent<Animator>().SetBool("Saltando", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Verifica que el jugador se encuentre en contacto con el piso.
        if (collision.gameObject.layer == 8 && this.transform.position.y > collision.transform.position.y)
        {
            TerminarSalto();
            this.GetComponent<Animator>().SetBool("EnElAire", false);
            this.GetComponent<Animator>().SetBool("Saltando", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && saltando || collision.gameObject.GetComponent<Escalable>()!= null && saltando)
        {
            Soundplayer.PlayOneShot(saltoSound);
        }
        //Verifica cuando el jugador deja de tocar el piso y entra en estado de salto.
        this.GetComponent<Animator>().SetBool("EnElAire", true);
        saltando = true;
    }

    private void TerminarSalto()
    {
        this.saltando = false;
    }

    public void ManejarDisparo()
    {

        //Control.
        float ejeFuego1 = Input.GetAxisRaw("Fire1");

        //Verifica que sea posible disparar en el momento.
        if (!MinijuegoAbierto)
        {
            if (ejeFuego1 != 0f && PuedeDisparar(esperarDisparo))
            {
                Soundplayer.PlayOneShot(disparoSound);

                Instantiate(this.prefabDisparo, new Vector2(this.transform.position.x, this.transform.position.y + 0.3f) , this.transform.rotation);
                //FindObjectOfType<Disparo>().danoRealizado(danoDisparo);

                //Detener al jugador
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                this.disparando = true;
                this.GetComponent<Animator>().SetTrigger("Disparar");
                this.esperarDisparo = Time.time + this.cadenciaDeDisparo;
            }
        }
        
    }
   
    protected virtual void ManejarHabilidadEspecial()
    {
       
    }

    private bool PuedeDisparar(float tiempoEsperaDisparo)
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 
        return Time.time > tiempoEsperaDisparo && !saltando;
    }

    //Se llama desde la animación de disparo cuando esta termina.
    private void TerminarDisparar()
    {
        disparando = false;
    }

    public virtual void RecibirDano(int danoRecibido)
    {
        Soundplayer.PlayOneShot(danoRecibidoSound);
        EfectoRecibirDano();
        Invoke("TerminarEfectoRecibirDano", 0.1f);
      if(escudo <= 0)
        {
            bateria = bateria - danoRecibido;
            Debug.Log(bateria);

            FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);

            if (bateria <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("GAME OVER");
            }
            else
            {
                //if (mirandoDerecha)
                //{
                //    this.GetComponent<Rigidbody2D>().position = new Vector2(this.GetComponent<Rigidbody2D>().position.x - 1, this.GetComponent<Rigidbody2D>().position.y);
                //}
                //else
                //{
                //    this.GetComponent<Rigidbody2D>().position = new Vector2(this.GetComponent<Rigidbody2D>().position.x + 1, this.GetComponent<Rigidbody2D>().position.y);
                //}
            }
        }
        else
        {
            escudo = escudo - danoRecibido;
            Debug.Log("Escudo" + escudo);
        }
        
    }

    protected void EfectoRecibirDano()
    {
        sr.material = MatWhite;
    }

    protected void TerminarEfectoRecibirDano()
    {
        sr.material = Matdefault;
    }

    //Funciones de control de disparo de Rotom.
    public void RecibirDanoRotom(int danoRecibido)
    {
        this.RecibirDano(danoRecibido);
      
        if (Relentizar(esperarRelentizar) && atrapado == false)
        {
            velocidad = velocidadRelentizado;
        }

        esperarRelentizar = Time.time + 2f;
    }

    private bool Relentizar(float tiempo)
    {
        return Time.time > tiempo;
    }

    protected void ComprobarRelentizado()
    {
        if (Relentizar(esperarRelentizar) && atrapado == false)
        {
            velocidad = velocidadEstandar;
        }

       
    }

    //Funciones de control de HE de SCRUM.
    public void AtrapadoPorSCRUM(int danoRecibido)
    {
        this.RecibirDano(danoRecibido);
            contadorLiberacion = 0f;
            velocidad = 0f;
            atrapado = true;

        //ComprobarAtrapado();

      
    }

    protected void ComprobarAtrapado()
    {
        if (contadorLiberacion < 5)
        {
            atrapado = true;
          
            if (Atrapado(esperarDanoAtrapado))
            {
                bateria = bateria - 5;
                FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);
                esperarDanoAtrapado = Time.time + 1f;
            }
        }
        else
        {
            atrapado = false;
            if (!Relentizar(esperarRelentizar))
            {
                velocidad = velocidadRelentizado;
            }
            else
            {
                velocidad = velocidadEstandar;
            }
        }
    }

    private bool Atrapado(float tiempo)
    {
        return Time.time > tiempo;
    }

    

    public int BateriaGetter()
    {
        return bateria;
    }

    public void BateriaSetter(int _bateria)
    {
       if(_bateria > bateriaMax)
        {
            bateria = bateriaMax;
        }
        else
        {
            bateria = _bateria;
        }
        FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);
    }

    public float NivelHEGetter()
    {
        return nivelHE;
    }

    public float DanoGetter()
    {
        return dano;
    }

    public float velocidadGetter()
    {
        return velocidad;
    }

    public void velocidadSetter(float _velocidad)
    {
        velocidad = _velocidad;
    }

    public float CadenciaDisparoGetter()
    {
        return cadenciaDeDisparo;
    }

    public void CargarJugador()
    {
        string[] data = GeneralPlayerData.CargarInfo(); 
        
        float[] valoresCargados = JugadorData.CargarJugador("/" + data[2] + ".FixIt");

        bateria = (int)valoresCargados[0];
        dano = valoresCargados[1];
        nivelHE = valoresCargados[2];
        velocidad = valoresCargados[3];
        cadenciaDeDisparo = valoresCargados[4];
    }




    //CAMPO GRAVITATORIO GANON

    public bool enElAireGetter()
    {
        return saltando;
    }

    public bool MirandoDerechaGetter()
    {
        return mirandoDerecha;
    }

    //FUNCIONES EFECTOS SCRUM MASTER

    public void Confundir()
    {
        confundido = true;
        esperarConf = Time.time + duracionConf;
    }

    protected void MovimientoInvertido()
    {

        if (!disparando)
        {
            //Control y movimiento
            float ejeHorizontal = Input.GetAxisRaw("Horizontal");
            this.GetComponent<Rigidbody2D>().velocity = new Vector2((-1 * ejeHorizontal) * velocidad, this.GetComponent<Rigidbody2D>().velocity.y);

            //Animacion
            this.GetComponent<Animator>().SetBool("EnMovimiento", this.GetComponent<Rigidbody2D>().velocity.x != 0f);

            //Dirección a la que mira 
            if (this.GetComponent<Rigidbody2D>().velocity.x > 0f && !mirandoDerecha)
            {
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                this.mirandoDerecha = true;
            }
            else if (this.GetComponent<Rigidbody2D>().velocity.x < 0f && mirandoDerecha)
            {
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                this.mirandoDerecha = false;
            }
        }
    }
}
