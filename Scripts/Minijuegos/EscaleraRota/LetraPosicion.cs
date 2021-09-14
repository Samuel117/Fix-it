using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetraPosicion : MonoBehaviour
{
    private float cadenciaLetras = 1.2f;
    private float esperarCambioLetra = 0;
    private char letraRandom;
    public int ContadorLetras = 0;
    private string Cadena;
    public bool retardo = true;

    [SerializeField] public AudioSource player;
    [SerializeField] AudioClip cambioLetra;
    [SerializeField] public AudioClip Victoria;
    [SerializeField] public AudioClip Derrota;

    // Start is called before the first frame update
    void Start()
    {
        ContadorLetras = 0;
        Cadena = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<EscaleraRota>().MinijuegoAbierto)
        {
            if(ContadorLetras == 0 && retardo)
            {
                esperarCambioLetra = Time.time + 2.5f;
                retardo = false;
            }

            if(ContadorLetras == 6 && PuedeCambiar(esperarCambioLetra))
            {

                FindObjectOfType<EscaleraRota>().ActivarMensaje();
                this.transform.gameObject.SetActive(false);
                Debug.Log(Cadena);
                ContadorLetras = 0;
                retardo = true;
                esperarCambioLetra = 0;
                FindObjectOfType<CompararLetras>().Cadena(Cadena);
                Cadena = null;
                this.GetComponent<CanvasGroup>().alpha = 0;
            }
            else
            {
                if (ContadorLetras < 6)
                {
                    LetraPos();
                }
            }

        }

        
    }

    private void LetraPos()
    {
        if (PuedeCambiar(esperarCambioLetra))
        {
            this.GetComponent<CanvasGroup>().alpha = 255;
            letraRandom = (char)('A' + Random.Range(0, 26));
            Cadena += letraRandom.ToString();
            this.transform.Find("Letra").gameObject.GetComponent<TextMeshProUGUI>().text = letraRandom.ToString();
            this.transform.position = new Vector2(560 + Random.Range(-480,480),240 + Random.Range(-160,160));

                esperarCambioLetra = Time.time + cadenciaLetras;
                      
            player.PlayOneShot(cambioLetra);

            ContadorLetras++;
        }

    }

    private bool PuedeCambiar(float tiempo)
    {
        return Time.time > tiempo;
    }
}
