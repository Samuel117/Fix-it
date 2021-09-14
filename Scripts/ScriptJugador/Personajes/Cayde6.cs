using UnityEngine;

public class Cayde6 : Jugador
{
    //Valores unicos para personaje Cayde-6. (Deberan leerse desde los archivos del juego)
    //private float bateriaCayde6 = 60f;
    //private float velocidadCayde6 = 10f;
    //private float cadenciaDeDisparoCayde6 = 0.5f;
    //private float nivelHabilidadCayde6 = 3f;
    //private float danoCayde6 = 30f;

    //Control habilidad especial.
    private float esperarHabilidadEspecial = 1f;
    private float cadenciaHabilidadEspecial = 1f;
    private float retrasoPorDano = 0f;
    public bool fenix;

    [SerializeField] AudioSource player;
    [SerializeField] AudioClip recargaSonido;
    [SerializeField] GameObject recargaEfecto;

    void Start()
    {
        //Inicializamos los valores unicos de Skippy. 

        CargarJugador();
        bateriaMax = bateria;

        //bateria = bateriaCayde6;
        //dano = danoCayde6;
        //nivelHE = nivelHabilidadCayde6;

        //velocidad = velocidadCayde6;
        //cadenciaDeDisparo = cadenciaDeDisparoCayde6;
        velocidadRelentizado = velocidad / 2;
        velocidadEstandar = velocidad;
        Matdefault = sr.material;
        if (nivelHE == 3)
        {
            fenix = true;
        }
        else
        {
            fenix = false;
        }
        
        //GuardarJugador();
    }

    //void FixedUpdate()
    //{
    //    //Controles de movimiento 
    //    ManejarMovimiento();
    //    ManejarSalto();
    //}

    //private void Update()
    //{
    //    Debug.Log(bateria);

    //    //Controles de disparo
    //    ManejarDisparo();
    //    ManejarHabilidadEspecial();
    //}

    protected override void ManejarHabilidadEspecial()
    {
        if (nivelHE == 1)
        {
            //Si el jugador no se mueve y no esta en el aire.
            if (Input.GetAxisRaw("Horizontal") == 0 && !saltando)
            {
                //Retraso de 1 segundo entre regeneraciones de salud mas una penalización por recibir daño.
                if (PuedeUsarHabilidadEspecial(esperarHabilidadEspecial + retrasoPorDano))
                {
                    if (!FindObjectOfType<UI_Habilidad>().ready)
                    {
                        FindObjectOfType<UI_Habilidad>().HElista();
                    }

                    RegenerarSalud();
                    FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);
                    retrasoPorDano = 0;
                }
            }
            else
            {
                //Asegura que se espere 1 segundo antes de comenzar a regenerar salud tras quedarse inmovil.
                esperarHabilidadEspecial = Time.time + cadenciaHabilidadEspecial;
            }
        }
        else
        {
            if (PuedeUsarHabilidadEspecial2(esperarHabilidadEspecial + retrasoPorDano))
            {
                if (!FindObjectOfType<UI_Habilidad>().ready)
                {
                    FindObjectOfType<UI_Habilidad>().HElista();
                }

                RegenerarSalud();
                FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);
                retrasoPorDano = 0;
            }
        }
    }

    public override void RecibirDano(int danoRecibido)
    {
        Soundplayer.PlayOneShot(danoRecibidoSound);
        EfectoRecibirDano();
        Invoke("TerminarEfectoRecibirDano", 0.1f);

        esperarHabilidadEspecial = Time.time + cadenciaHabilidadEspecial;
        bateria = bateria - danoRecibido;
        FindObjectOfType<UI_Habilidad>().HEnoLista();

        FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);

        //Debug.Log(bateria);
        if (bateria <= 0 && fenix == true && nivelHE == 3)
        {
            bateria = bateriaMax / 2;
            fenix = false;
            FindObjectOfType<Bateria>().ActualizarBateria((int)bateria);
            player.PlayOneShot(recargaSonido);
            Instantiate(recargaEfecto, new Vector2(this.transform.position.x, this.transform.position.y - 0.7f), recargaEfecto.transform.rotation);
        }
        else if (bateria <= 0 && fenix == false)
        {
            Destroy(this.gameObject);
            Debug.Log("GAME OVER");
        }
        else
        {
            //Retraso en segundos para reactivación de la regeneración de salud.
            retrasoPorDano = 2;

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

    private void RegenerarSalud()
    {
        if (bateria < bateriaMax)
        {
            bateria += 5;
            if (bateria > bateriaMax)
            {
                bateria = bateriaMax;
            }
            //Debug.Log(bateria);
        }
        esperarHabilidadEspecial = Time.time + cadenciaHabilidadEspecial;
    }

    private bool PuedeUsarHabilidadEspecial(float tiempoEsperarHabilidadEspecial)
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 
        return Time.time > tiempoEsperarHabilidadEspecial && Input.GetAxisRaw("Vertical") == 0;
    }

    private bool PuedeUsarHabilidadEspecial2(float tiempoEsperarHabilidadEspecial)
    {
        return Time.time > tiempoEsperarHabilidadEspecial;
    }

}