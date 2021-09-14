using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Helicoptero : Minijuegos
{
    [SerializeField] private GameObject reparado;
    [SerializeField] private GameObject mosca;
    float esperarMosca = 0;
    public int  puntos = 30;
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip victoria;
    [SerializeField] AudioClip derrota;

    
    // Start is called before the first frame update
    void Start()
    {
        engranajes = 10;
        IntentosDentro = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.Find("Canvas").gameObject.activeSelf == true)
        {
            GenerarMoscas();
            Cursor.visible = false;

            if(puntos <= 0)
            {
                Victoria();
            }

            if(IntentosDentro <= 0)
            {
                Derrota();
            }
        }
    }

    protected override void Recompensa()
    {
        Instantiate(reparado,  new Vector2 (this.transform.position.x, FindObjectOfType<Jugador>().transform.position.y + 1f), this.transform.rotation);

        //Recuperar 10 puntos de bateria.
        int bateria = FindObjectOfType<Jugador>().BateriaGetter();

        FindObjectOfType<Jugador>().BateriaSetter(bateria + 20);
        CerrarMinijuegoVictoria();
    }

    protected override void Sansion()
    {
        //Pierde 10 puntos de bateria.
        FindObjectOfType<Jugador>().RecibirDano(10);
        
        //Cerrar minijuego.
        CerrarMinijueogDerrota();
    }

    public void Victoria()
    {
        player.PlayOneShot(victoria);
        Cursor.visible = true;
        Recompensa();
    }

    public void Derrota()
    {
        player.PlayOneShot(derrota);
        Reiniciar();
        Cursor.visible = true;
        Sansion();
    }

    private void GenerarMoscas()
    {
        if (DebeGenerarMosca(esperarMosca))
        {
            int posY = Random.Range(100, 850);
            //Instanciar mosca 
            Instantiate(this.mosca, new Vector2(0,posY), Quaternion.Euler(0f, 0f, 0f), this.transform.Find("Canvas").Find("Moscas").gameObject.transform);
            esperarMosca = Time.time + 0.5f;
        }
    }

    private bool DebeGenerarMosca(float esperarMosca)
    {
        return Time.time > esperarMosca;
    }

    public void actualizarContador()
    {
        this.transform.Find("Canvas").Find("MoscasRestantes").gameObject.GetComponent<TextMeshProUGUI>().text = "Moscas restantes: " + puntos + " / 30";
    }

    public void actualizarIntentos()
    {
        this.transform.Find("Canvas").Find("Intentos").gameObject.GetComponent<TextMeshProUGUI>().text = "Intentos: " +  IntentosDentro;
    }

    private void Reiniciar()
    {
        puntos = 30;
        IntentosDentro = 3;
        actualizarIntentos();
        actualizarContador();
    }
}
