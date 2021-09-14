using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ganon : Jefes
{
    //Ataque Get Over Here
    private int bateriaGeneradorGOH = 250;
    private float timepoEsperaGOH = 20f;
    private bool GOHactivado = false;
    private int GOHdano = 30;
    
    //Ataque electro shock
    private Vector2[] posicionElectroshock = new Vector2[3];
    private float esperarElectroshock = 10f;
    private float cadenciaElectroshock = 20f;
    //Imitacion
    private float esperarImitacion = 10f;
    private float cadenciaImitacion = 3f;
    private int ataqueRandom = 0;
    //Destruccion
    private Vector2 posicionMinijuego;
    private Vector2 posicionEnemigo;
    private float esperarDestruccion = 10f;
    private float cadenciaDestruccion = 30f;
    private float UsarDestruccion = 0f;
    //Final Flash
    private float bateriaGeneradorFF = 200f;
    private bool FFactivado = false;
    private float timepoEsperaFF = 0f;
    private float cargaFF = 15f;
    private float cadenciaFF = 40f;
    private float tiempoFF = 10f;
    //Prefabs
    [SerializeField] private GameObject prefabElectroshock;
    [SerializeField] private GameObject[] prefabImitaciones = new GameObject[3];
    [SerializeField] private GameObject[] prefabMinujuegos;
    [SerializeField] private GameObject[] prefabEnemigos = new GameObject[4];
    [SerializeField] private Material MatWhite;
   
    private Material MatDefaultGOH;
    private Material MatDefaultGFF;
    private Material MatDefaultPrincipal;
    [SerializeField] private SpriteRenderer sr_GOH;
    [SerializeField] private SpriteRenderer sr_GFF;
    [SerializeField] private SpriteRenderer sr_Principal;
    [SerializeField] private GameObject FF_Peligro;
    [SerializeField] private GameObject[] PosicionesDisparos = new GameObject[3];

    private float retardo;

    //Control de habilidades
    private int HabilidadRandom;
    private bool lanzado;
    private float tiempoEsperaAtaques = 10f;
    private float CadenciaAtaques = 4f;
    //Uso de lanzamiento de ataques
    private int Ataques = 50;
    private int contadorAtaques = 0;
    private float tiempoRecuperacion = 0;
    private float cadenciaRecuperacion = 8f;

    string[] data = new string[3];

    // Start is called before the first frame update
    void Start()
    {
        MatDefaultGOH = sr_GOH.material;
        MatDefaultGFF = sr_GFF.material;
        MatDefaultPrincipal = sr_Principal.material;

        bateria = 5000f;//8000
        cadenciaDeDisparoBasico = 1f;
        alcance = 10f;
        engranajesDerrota = 500;

        retardo = Time.time + 3f;

        data = GeneralPlayerData.CargarInfo();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time > retardo)
        {
            if (Time.time > esperarDestruccion)
            {
                if (contadorAtaques >= Ataques)
                {
                    contadorAtaques = 0;
                    tiempoRecuperacion = Time.time + cadenciaRecuperacion;
                    this.transform.Find("GeneradorPrincipal").gameObject.SetActive(true);
                    this.gameObject.GetComponent<Animator>().SetBool("Derribado", true);

                    Debug.Log("Ando sobrecalentado");
                }
                if (esperaRecuperacion(tiempoRecuperacion))
                {
                    this.transform.Find("GeneradorPrincipal").gameObject.SetActive(false);
                    this.gameObject.GetComponent<Animator>().SetBool("Derribado", false);

                    DispararFF();
                    AtaqueBasico();
                    if (GOHactivado)
                    {
                        GetOverHere();
                    }
                    if (PuedeUsarHabilidad(tiempoEsperaAtaques))
                    {
                        lanzado = false;
                        do
                        {
                            HabilidadRandom = Random.Range(1, 6);
                            Debug.Log("El random es: " + HabilidadRandom);
                            UsarAtaques();
                        } while (!lanzado);
                        tiempoEsperaAtaques = Time.time + CadenciaAtaques;
                    }
                }
            }
            else if (FindObjectOfType<Enemigo>() == null && FindObjectOfType<Minijuegos>() == null)
            {
                esperarDestruccion = 0;
            }
        }
    }

    protected override void AtaqueBasico()
    {
        //Verifica que sea posible disparar en el momento.
        if (PuedeDisparar(esperarDisparo))
        {
            
            Instantiate(this.prebafAtaqueBasico, PosicionesDisparos[Random.Range(0,3)].transform.position, this.transform.rotation);

            this.disparando = true;

            // this.GetComponent<Animator>().SetTrigger("Disparar");
            this.esperarDisparo = Time.time + this.cadenciaDeDisparoBasico;
        }
    }

    private void Electroshock()
    {
        
        if (PuedeUsarElectroshock(esperarElectroshock))
        {
            contadorAtaques += 6;
            float pos = FindObjectOfType<Ganon>().transform.position.x + 5f;

            prefabElectroshock.SetActive(true);
            
            esperarElectroshock = Time.time + cadenciaElectroshock;
            lanzado = true;
        }
    }

    private bool PuedeUsarElectroshock(float esperarElectroshock)
    {
        return Time.time > esperarElectroshock;
    }

    private void Imitacion()
    {
        
        //Verifica que sea posible disparar en el momento.
        //Hacer 3 prefabs de las imitaciones que sigan al jugador y que no tengan rango.
        if (PuedeImitar(esperarImitacion))
        {
            contadorAtaques += 3;
            ataqueRandom = Random.Range(0,3);

            Instantiate(this.prefabImitaciones[ataqueRandom], this.transform.position, this.prefabImitaciones[ataqueRandom].transform.rotation);

            this.disparando = true;

            // this.GetComponent<Animator>().SetTrigger("Disparar");
            this.esperarImitacion = Time.time + this.cadenciaImitacion;
            lanzado = true;
        }
    }

    private bool PuedeImitar(float tiempoEsperaImitacion)
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 
        return Time.time > tiempoEsperaImitacion;
    }

    private void GetOverHere()
    {
        
        if (PuedeUsarGOH(timepoEsperaGOH) && !GOHactivado)
        {
            contadorAtaques += 6;
            //this.transform.Find("CampoGravitatorio").gameObject.SetActive(true);
            this.transform.Find("Generador").gameObject.SetActive(true);
            bateriaGeneradorGOH = 250;
            GOHactivado = true;

        }else if (GOHactivado)
        {
            
            if (FindObjectOfType<Jugador>()!= null && FindObjectOfType<Jugador>().disparando)
            {
                this.transform.Find("CampoGravitatorio").gameObject.GetComponent<AreaEffector2D>().drag = 200;
            }
            else
            {
                this.transform.Find("CampoGravitatorio").gameObject.GetComponent<AreaEffector2D>().drag = 0;
            }
        }
        lanzado = true;
    }

    private bool PuedeUsarGOH(float tiempoEsperaGOH)
    {
        //Establece si el jugador puede disparar o usar su habilidad especial. 
        return Time.time > tiempoEsperaGOH;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<Jugador>() != null && GOHactivado)
    //    {
    //        FindObjectOfType<Jugador>().RecibirDano(GOHdano);
    //        this.transform.Find("CampoGravitatorio").gameObject.SetActive(false);
    //        this.transform.Find("Generador").gameObject.SetActive(false);
    //        timepoEsperaGOH = Time.time + 15f;

    //        GOHactivado = false;
    //    }
    //}

    public void GOH_Controller()
    {
        FindObjectOfType<Jugador>().RecibirDano(GOHdano);
        this.transform.Find("CampoGravitatorio").gameObject.SetActive(false);
        this.transform.Find("Generador").gameObject.GetComponent<Animator>().SetTrigger("Derrotado");
        timepoEsperaGOH = Time.time + 15f;
        GOHactivado = false;
    }

    public void RecibirDanoGenerador(int dano)
    {
        EfectoRecibirDano(sr_GOH);
        Invoke("TerminarEfectoRecibirDanoGOH", 0.1f);
        bateriaGeneradorGOH = bateriaGeneradorGOH - dano;
        
        if(bateriaGeneradorGOH <= 0)
        {
            GOHactivado = false;
            this.transform.Find("CampoGravitatorio").gameObject.SetActive(false);
            this.transform.Find("Generador").gameObject.GetComponent<Animator>().SetTrigger("Derrotado");
            //this.transform.Find("Generador").gameObject.SetActive(false);
            timepoEsperaGOH = Time.time + 15f;
            bateria = bateria - 250;
            FindObjectOfType<BateriaJefe>().ActualizarBateriaJefe((int)bateria);
            Debug.Log("Bateria: " + bateria);
        }
    }

    protected void EfectoRecibirDano(SpriteRenderer sr)
    {
        sr.material = MatWhite;
    }

    protected void TerminarEfectoRecibirDanoGOH()
    {
        sr_GOH.material = MatDefaultGOH;
    }

    protected void TerminarEfectoRecibirDanoGFF()
    {
        sr_GFF.material = MatDefaultGFF;
    }

    protected void TerminarEfectoRecibirDanoPrincipal()
    {
        sr_Principal.material = MatDefaultPrincipal;
    }


    public void recibirDanoBateria(float dano)
    {
        if (FindObjectOfType<Destruccion>() == null)
        {
            bateria -= dano;
            FindObjectOfType<BateriaJefe>().ActualizarBateriaJefe((int)bateria);

            if (bateria <= 0)
            {
                
                Destroy(this.gameObject);
            }

            Debug.Log("Bateria principal: " + bateria);
        }
    }

    private void Destruccion()
    {
        
        if (PuedeDestruir(UsarDestruccion))
        {
            contadorAtaques += 8;
            
            float posEnemigos = FindObjectOfType<Ganon>().transform.position.x + 20f;

            int ran = Random.Range(0, 4);

            switch (ran)
            {
                case 0:
                    {
                        posicionMinijuego = new Vector2(2.5f, 130f);
                        break;
                    }
                case 1:
                    {
                        posicionMinijuego = new Vector2(6.5f, 130.52f);
                        break;
                    }
                case 2:
                    {
                        posicionMinijuego = new Vector2(6.5f, 132f);
                        break;
                    }
                case 3:
                    {
                        posicionMinijuego = new Vector2(6.5f, 130.88f);
                        break;
                    }
            }
            
            if(FindObjectOfType<Minijuegos>()!= null)
            {
                Destroy(FindObjectOfType<Minijuegos>().gameObject);
            }

            Instantiate(this.prefabMinujuegos[ran], posicionMinijuego, prefabMinujuegos[ran].transform.rotation);
            FindObjectOfType<Minijuegos>().gameObject.SetActive(true);
            
            int enemigo = 0;
            for (int x = 0; x < 3; x++)
            {
                enemigo = Random.Range(0, 4);
                posicionEnemigo = new Vector2(posEnemigos, 129.25f);
                Instantiate(this.prefabEnemigos[enemigo], posicionEnemigo, this.prefabEnemigos[enemigo].transform.rotation);
                posEnemigos += 2;
            }

            esperarDestruccion = Time.time + cadenciaDestruccion;
            UsarDestruccion = Time.time + cadenciaDestruccion + 5f;
            lanzado = true;
        }
    }

    private bool PuedeDestruir(float esperar)
    {
        return Time.time > esperar;
    }

    private void FinalFlash()
    {
        
        if (PuedeUsarFF(tiempoFF) && !FFactivado)
        {
            //Activar animación FF
            contadorAtaques += 6;
            this.transform.Find("GeneradorFF").gameObject.SetActive(true);
            FF_Peligro.SetActive(true);
            bateriaGeneradorGOH = 200;
            FFactivado = true;
            timepoEsperaFF = Time.time + cargaFF; //Timepo de espera para disparar.
            tiempoFF = Time.time + cadenciaFF; // Cadencia para activar FF.
            lanzado = true;
        }
    }

    private void DispararFF()
    {
        if (Time.time > timepoEsperaFF && FFactivado)
        {
            FindObjectOfType<Jugador>().RecibirDano(50); //70
            //this.transform.Find("GeneradorFF").gameObject.SetActive(false);
            this.transform.Find("GeneradorFF").gameObject.GetComponent<Animator>().SetTrigger("Derrotado");
            this.transform.Find("FF").gameObject.SetActive(true);
            FFactivado = false;
        }else if(bateriaGeneradorFF <= 0)
        {
            //this.transform.Find("GeneradorFF").gameObject.SetActive(false);
            this.transform.Find("GeneradorFF").gameObject.GetComponent<Animator>().SetTrigger("Derrotado");
            FFactivado = false;
            bateriaGeneradorFF = 200;
        }
    }

    private bool PuedeUsarFF(float esperarFF)
    {
        return Time.time > esperarFF;
    }

    public void RecibirDanoGeneradorFF(int dano)
    {
        //Efecto recibir daño 
        EfectoRecibirDano(sr_GFF);
        Invoke("TerminarEfectoRecibirDanoGFF", 0.1f);

        bateriaGeneradorFF = bateriaGeneradorFF - dano;

        if (bateriaGeneradorFF <= 0)
        {
            //Desactivar Animación 

            this.transform.Find("GeneradorFF").gameObject.GetComponent<Animator>().SetTrigger("Derrotado");
            FFactivado = false;

            bateria = bateria - 200;
            FindObjectOfType<BateriaJefe>().ActualizarBateriaJefe((int)bateria);
            Debug.Log("Bateria: " + bateria);
            bateriaGeneradorFF = 200;
        }
    }

    private bool PuedeUsarHabilidad(float tiempoEspera)
    {
        return Time.time > tiempoEspera;
    }

    public void RecibirDanoPrincipal(float dano)
    {
        EfectoRecibirDano(sr_Principal);
        Invoke("TerminarEfectoRecibirDanoPrincipal", 0.1f);

        bateria = bateria - dano;
        FindObjectOfType<BateriaJefe>().ActualizarBateriaJefe((int)bateria);

        if (bateria <= 0)
        {
            if (int.Parse(data[0]) > 10)
            {
                FindObjectOfType<ControladorNvl>().SumarEngranajes(engranajesDerrota / 2);
            }
            else
            {
                FindObjectOfType<ControladorNvl>().SumarEngranajes(engranajesDerrota);
                Debug.Log("MANDANDO DINERO");
            }

            Debug.Log("FUNADO");
            Destroy(this.gameObject);
        }
        Debug.Log("Bateria principal: " + bateria);
    }

    private void UsarAtaques()
    {
        
        switch (HabilidadRandom)
        {
            case 1:
                Imitacion();
                break;
            case 2:
                Electroshock();
                break;
            case 3:
                GetOverHere();
                break;
            case 4:
                if (!FFactivado)
                {
                    Destruccion();
                }
                break;
            case 5:
                FinalFlash();
                break;
        }
        Debug.Log("Van de ataques: " + contadorAtaques);
    }

    private bool esperaRecuperacion(float tiempoRecuperar)
    {
        return Time.time > tiempoRecuperar;
    }
}
