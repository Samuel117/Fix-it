using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FocosColeccion : MonoBehaviour
{
    [SerializeField] AudioSource player;
    [SerializeField] AudioClip cambio;
    [SerializeField] AudioClip error;

    private Color RojoApagado = new Color32(102, 5, 5, 255);
    private Color RojoEncendido = new Color32(248, 0, 0, 255);
    private Color AzulApagado = new Color32(23, 22, 73, 255);
    private Color AzulEncendido = new Color32(10, 0, 255, 255);
    private Color VerdeApagado = new Color32(26, 77, 14, 255);
    private Color VerdeEncendido = new Color32(47, 251, 0, 255);
    private Color AmarilloApagado = new Color32(115, 118, 30, 255);
    private Color AmarilloEncendido = new Color32(244, 255, 1, 255);

    private int[,] secuencias = new int[6, 8]
    {
        { 1, 2, 3, 4, 1, 2, 3, 4 },
        { 1, 2, 1, 2, 1, 2, 1, 2 },
        { 4, 1, 3, 2, 4, 1, 3, 2 },
        { 4, 3, 2, 1, 2, 3, 4, 1 },
        { 3, 1, 3, 2, 1, 4, 3, 4 },
        { 1, 4, 1, 4, 1, 4, 1, 4 }
    };

    private bool focoEncendido = false;
    private float esperarApagar = 0f;
    private int index = 0;
    private int secuenciaRandom = 0;
    private int focosAcertados;
    private float esperarSecuencia;

    private Image focoRojo, focoAzul, focoVerde, focoAmarillo;


    void Start()
    {
        focoRojo = this.transform.Find("Foco_Rojo").GetComponent<Image>();
        focoAzul = this.transform.Find("Foco_Azul").GetComponent<Image>();
        focoVerde = this.transform.Find("Foco_Verde").GetComponent<Image>();
        focoAmarillo = this.transform.Find("Foco_Amarillo").GetComponent<Image>();
        secuenciaRandom = Random.Range(0, 6);
        IniciarFocos();

        esperarSecuencia = Time.time + 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (index < 8 && DebeIniciarSecuencia(esperarSecuencia))
        {
            Secuencia();
            this.transform.Find("Mensaje").gameObject.GetComponent<TextMeshProUGUI>().text = "Memoriza la secuencia!";
        }
        else
        {
            if (index == 8)
            {
                this.transform.Find("Mensaje").gameObject.GetComponent<TextMeshProUGUI>().text = "Repite la secuencia!";
            }

            if (focosAcertados == 8)
            {
                FindObjectOfType<Cristal_Datos>().Victoria();

                Debug.Log("Minijuego completado");
            }
        }

    }

    private void IniciarFocos()
    {
        focoRojo.color = RojoApagado;
        focoAzul.color = AzulApagado;
        focoVerde.color = VerdeApagado;
        focoAmarillo.color = AmarilloApagado;

    }

    private void Secuencia()
    {
        if (index < 8)
        {
            if (secuencias[secuenciaRandom, index] == 1 && !focoEncendido)
            {
                //player.PlayOneShot(cambio);
                focoRojo.color = RojoEncendido;
                focoEncendido = true;
                esperarApagar = Time.time + 1f;
            }

            if (secuencias[secuenciaRandom, index] == 2 && !focoEncendido)
            {
                //player.PlayOneShot(cambio);
                focoAzul.color = AzulEncendido;
                focoEncendido = true;
                esperarApagar = Time.time + 1f;
            }

            if (secuencias[secuenciaRandom, index] == 3 && !focoEncendido)
            {
                //player.PlayOneShot(cambio);
                focoVerde.color = VerdeEncendido;
                focoEncendido = true;
                esperarApagar = Time.time + 1f;
            }
            if (secuencias[secuenciaRandom, index] == 4 && !focoEncendido)
            {
                //player.PlayOneShot(cambio);
                focoAmarillo.color = AmarilloEncendido;
                focoEncendido = true;
                esperarApagar = Time.time + 1f;
            }

            if (DebeApagar(esperarApagar))
            {
                IniciarFocos();
                focoEncendido = false;
                index++;
                Debug.Log("foco secuencia: " + index);
            }
        }
    }

    private bool DebeApagar(float esperarApagar)
    {
        return Time.time > esperarApagar;
    }

    private void OnMouseDown()
    {
        Debug.Log("Presionado");
    }

    public void RojoPresionado()
    {
        if (index >= 8)
        {
            IniciarFocos();
            focoRojo.color = RojoEncendido;

            if (secuencias[secuenciaRandom, focosAcertados] == 1)
            {
                //player.PlayOneShot(cambio);
                focosAcertados++;
                Debug.Log("Correcto!");
            }
            else
            {
                //player.PlayOneShot(error);
                Debug.Log("Perdiste!");
                FindObjectOfType<Cristal_Datos>().Derrota();

            }
        }
    }

    public void AzulPresionado()
    {
        if (index >= 8)
        {
            IniciarFocos();
            focoAzul.color = AzulEncendido;

            if (secuencias[secuenciaRandom, focosAcertados] == 2)
            {
                //player.PlayOneShot(cambio);
                focosAcertados++;
                Debug.Log("Correcto!");
            }
            else
            {
                //player.PlayOneShot(error);
                Debug.Log("Perdiste!");
                FindObjectOfType<Cristal_Datos>().Derrota();
            }
        }
    }

    public void VerdePresionado()
    {
        if (index >= 8)
        {
            IniciarFocos();
            focoVerde.color = VerdeEncendido;

            if (secuencias[secuenciaRandom, focosAcertados] == 3)
            {
                //player.PlayOneShot(cambio);
                focosAcertados++;
                Debug.Log("Correcto!");
            }
            else
            {
                //player.PlayOneShot(error);
                Debug.Log("Perdiste!");
                FindObjectOfType<Cristal_Datos>().Derrota();
            }
        }
    }

    public void AmarilloPresionado()
    {
        if (index >= 8)
        {
            IniciarFocos();
            focoAmarillo.color = AmarilloEncendido;

            if (secuencias[secuenciaRandom, focosAcertados] == 4)
            {
                //player.Play();
                focosAcertados++;
                Debug.Log("Correcto!");
            }
            else
            {
                //player.PlayOneShot(error);
                Debug.Log("Perdiste!");
                FindObjectOfType<Cristal_Datos>().Derrota();
            }
        }
    }

    private bool DebeIniciarSecuencia(float esperarSecuencia)
    {
        return Time.time > esperarSecuencia;
    }
}
