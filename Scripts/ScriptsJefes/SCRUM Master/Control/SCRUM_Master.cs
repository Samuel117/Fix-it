using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SCRUM_Master : Jefes
{
    public bool derribado = false;
    private float duracionDerribado = 8f;
    private float esperarDerribado = 0f;
    public float bateriaPatas = 500f;
    //Ataque de Red
    private float tiempoEsperaRed = 0f;
    private float cadenciaRed = 5f;
    //Ataque de Confución
    private float tiempoEsperaConf = 0f;
    private float cadenciaConf = 20f;
    Vector2 posicionConf;
    //Ataque de Tormenta
    private float tiempoEsperaTorm = 0f;
    private float cadenciaTorm = 25f;
    private float cadenciaRayo = 2f;
    private float esperarRayo = 0f;
    private float contadorRayos = 0;
    private Vector2 posicionRayo;
    //HABILIDAD ESPECIAL
    //private float tiempoEsperarInvocar = 0f;
    //private float cadenciaInvocar = 1f;
    //private float esperarInvocar = 0f;
    //private float contadorScrum = 0;
    private Vector2[] posicionScrum = new Vector2[2];
    //Prefabs de SCRUM Master
    [SerializeField] private GameObject prefabRed;
    [SerializeField] private GameObject prefabConfusion;
    [SerializeField] private GameObject prefabTormenta;
    [SerializeField] private GameObject prefabScrum;
    [SerializeField] private GameObject SMdesactivado;
    [SerializeField] private GameObject confusionEffect;
    [SerializeField] private GameObject inicioTormenta;

    [SerializeField] protected SpriteRenderer sr;
    protected Material Matdefault;
    [SerializeField] protected Material MatWhite;
    //Variables de condiciones
    private int ataqueRandom;
    private float tiempoEsperaAtaques = 10f;
    private float CadenciaAtaques = 6f;
    private bool Lanzado;

    string[] data = new string[3];

    private float retardoInicio;


    // Start is called before the first frame update
    void Start()
    {
        bateria = 2800f;//2800
        cadenciaDeDisparoBasico = 3f;
        alcance = 10f;
        bateriaPatas = 500;
        posicionConf = new Vector2(0, 0);
        engranajesDerrota = 250;

        data = GeneralPlayerData.CargarInfo();
        Matdefault = sr.material;

        retardoInicio = Time.time + 5f;
        tiempoEsperaAtaques = Time.time + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<Jugador>() != null)
        {
            ComprobarDerribado();
            if (!derribado)
            {
                if (contadorRayos != 0)
                {
                    Tormenta();
                }
                if (PuedeUsarHabilidad(tiempoEsperaAtaques))
                {
                    Lanzado = false;
                    do
                    {
                        ataqueRandom = Random.Range(1, 4);
                        UsarAtaques();
                    } while (!Lanzado);
                    tiempoEsperaAtaques = Time.time + CadenciaAtaques;
                }

                if (Time.time > retardoInicio)
                {
                    AtaqueBasicoSM();
                }
            }
        }
    }

    private void LanzarRed()
    {
        if (PuedeLanzarRed(tiempoEsperaRed))
        {
            //instanciar el objeto
            this.transform.Find("SMSprites").gameObject.GetComponent<Animator>().SetTrigger("Disparar");
            Instantiate(this.prefabRed, new Vector2(this.transform.position.x + 2f, this.transform.position.y - 2f), this.transform.rotation);
            tiempoEsperaRed = Time.time + cadenciaRed;
            Lanzado = true;
        }
    }

    private void Confusion()
    {
        if (PuedeUsarConfusion(tiempoEsperaConf))
        {
            this.transform.Find("SMSprites").gameObject.GetComponent<Animator>().SetTrigger("Disparar");
            //instanciar el objeto
            Instantiate(this.prefabConfusion, FindObjectOfType<Jugador>().transform.position, this.transform.rotation);
            Instantiate(this.confusionEffect, FindObjectOfType<Jugador>().transform.position, this.confusionEffect.transform.rotation);
            tiempoEsperaConf = Time.time + cadenciaConf;
            Lanzado = true;
        }
    }

    private void Tormenta()
    {
        if (PuedeUsarTormenta(tiempoEsperaTorm))
        {
            if(contadorRayos == 0)
            {
                this.transform.Find("SMSprites").gameObject.GetComponent<Animator>().SetTrigger("Tormenta");
                Instantiate(inicioTormenta, new Vector2(-3.688835f, 8.116902f), inicioTormenta.transform.rotation);
            }
          
            if (Time.time > esperarRayo)
            {
                posicionRayo = new Vector2(FindObjectOfType<Jugador>().transform.position.x, 8f);

                //instanciar el objeto
                Instantiate(this.prefabTormenta, posicionRayo, this.prefabTormenta.transform.rotation);
                contadorRayos++;
                esperarRayo = Time.time + cadenciaRayo;
            }
            Lanzado = true;
        }
       
        if(contadorRayos == 5)
        {
            tiempoEsperaTorm = Time.time + cadenciaTorm;
            contadorRayos = 0;
        }
    }

    private bool PuedeLanzarRed(float tiempoEsperaRed)
    {
        return Time.time > tiempoEsperaRed;
    }

    private bool PuedeUsarConfusion(float tiempoEsperaConf)
    {
        return Time.time > tiempoEsperaConf;
    }

    private bool PuedeUsarTormenta(float tiempoEsperaTorm)
    {
        return Time.time > tiempoEsperaTorm;
    }

    private bool PuedeUsarHabilidad(float tiempoEspera)
    {
        return Time.time > tiempoEspera;
    }

    
    public void recibirDanoPatas(float dano)
    {
        EfectoRecibirDano();
        Invoke("TerminarEfectoRecibirDano", 0.1f);

        bateriaPatas -= dano;

        FindObjectOfType<SMPatas>().ActualizarPatas();

        if(bateriaPatas <= 0)
        {
            derribado = true;
            this.transform.Find("SMSprites").gameObject.GetComponent<Animator>().SetBool("Derribado", true);
            esperarDerribado = Time.time + duracionDerribado;

            this.GetComponent<BoxCollider2D>().enabled = false;
        }

        Debug.Log("Bateria patas: " + bateriaPatas);
        Debug.Log("Derribado: " + derribado);
    }

    public void recibirDanoBateria(float dano)
    {
        EfectoRecibirDano();
        Invoke("TerminarEfectoRecibirDano", 0.1f);

        bateria -= dano;
        FindObjectOfType<BateriaJefe>().ActualizarBateriaJefe((int)bateria);

        if(bateria <= 0)
        {
            if(int.Parse(data[0]) > 9)
            {
                FindObjectOfType<ControladorNvl>().SumarEngranajes(engranajesDerrota / 2);
            }
            else
            {
                FindObjectOfType<ControladorNvl>().SumarEngranajes(engranajesDerrota);
            }
            Instantiate(SMdesactivado, new Vector2(this.transform.position.x, this.transform.position.y + 2f), SMdesactivado.transform.rotation);
            Destroy(this.gameObject);
        }

        Debug.Log("Bateria principal: " + bateria);
    }

    private void ComprobarDerribado()
    {
        if (derribado && Time.time > esperarDerribado)
        {
            derribado = false;
            this.transform.Find("SMSprites").gameObject.GetComponent<Animator>().SetTrigger("Levantar");
            this.transform.Find("SMSprites").gameObject.GetComponent<Animator>().SetBool("Levantandose", true);
            this.transform.Find("SMSprites").gameObject.GetComponent<Animator>().SetBool("Derribado", false);
            bateriaPatas = 500f;
            FindObjectOfType<SMPatas>().ActualizarPatas();
            //this.GetComponent<BoxCollider2D>().enabled = true;
            //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 5);
            Debug.Log("Derribado: " + derribado);

            HabilidadEspecial();
        }
    }


    protected override void HabilidadEspecial()
    {
        float pos = FindObjectOfType<SCRUM_Master>().transform.position.x + 5;

        for(int x = 0; x < posicionScrum.Length; x++)
        {
            posicionScrum[x] = new Vector2(pos, 6f);
            pos += 3;

            //instanciar el objeto
            Instantiate(this.prefabScrum, posicionScrum[x], this.transform.rotation);
        }
    }

    //private bool PuedeInvocar(float tiempoEsperarInvocar)
    //{
    //    return Time.time > tiempoEsperarInvocar;
    //}

    private void UsarAtaques()
    {
        if(contadorRayos != 0)
        {
            Tormenta();
        }
        switch (ataqueRandom)
        {
            case 1:
                Debug.Log("Telaraña");
                LanzarRed();
                break;
            case 2:
                Debug.Log("Confucion");
                Confusion();
                break;
            case 3:
                Debug.Log("Tormenta");
                Tormenta();
                break;
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
}
