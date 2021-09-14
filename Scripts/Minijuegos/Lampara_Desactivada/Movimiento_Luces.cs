using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento_Luces : MonoBehaviour
{
    [SerializeField] GameObject[] Oscuridad = new GameObject[3];
    private float esperarAvance = 0f;
    private float esperarRetroceder = 0f;

    private int velocidadRandom;
    private float[] velocidad = new float[3] { 2f, 4f, 7.5f};
    private float[] velocidadAntenas = new float[3];
    private float esperarCambio = 2f;
    private bool[] Ganar = new bool[3]{ false, false, false};

    private bool debeReiniciar = false;

    [SerializeField] AudioSource player;
    [SerializeField] AudioClip victoria;
    [SerializeField] AudioClip derrota;
    private bool[] sonidoLamp = new bool[3] { false, false, false };
    [SerializeField] Sprite generadorLuz;
    [SerializeField] Sprite generadorNoLuz;
    [SerializeField] GameObject[] generadores = new GameObject[3];
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Oscuridad[0].gameObject.name);
       
      for(int x = 0; x < 3; x++)
        {
            velocidadAntenas[x] = velocidad[Random.Range(0, 3)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debeReiniciar)
        {
            esperarAvance = Time.time + 2f;
            esperarRetroceder = Time.time + 1f;
            debeReiniciar = false;
        }

        ComprobarGanar();
        Resultado();
        CambioVelocidad();
        AvanceOscuridad();
        RetrocederOscuridad();
    }

    private void AvanceOscuridad()
    {
        if (DebeAvanzarOscuridad(esperarAvance))
        {
           for(int x = 0; x < Oscuridad.Length; x++){
                  if(x != FindObjectOfType<Lampara_Desactivada>().lamparaApoyada - 1 && Oscuridad[x].GetComponent<RectTransform>().sizeDelta.x > 0f)
                  {
                    Oscuridad[x].GetComponent<RectTransform>().sizeDelta = new Vector2(Oscuridad[x].GetComponent<RectTransform>().sizeDelta.x + velocidadAntenas[x], 43.5867f);
                  }
           }
              esperarAvance = Time.time + 1f;
        }
    }

    private void RetrocederOscuridad()
    {
        if (DebeAvanzarOscuridad(esperarRetroceder))
        {
            for (int x = 0; x < Oscuridad.Length; x++)
            {
                if (x == FindObjectOfType<Lampara_Desactivada>().lamparaApoyada - 1 )
                {
                    Oscuridad[x].GetComponent<RectTransform>().sizeDelta = new Vector2(Oscuridad[x].GetComponent<RectTransform>().sizeDelta.x - 8f, 43.5867f);
                }
            }
            esperarRetroceder = Time.time + 1f;
        }
    }

    private void CambioVelocidad()
    {
        if (DebeCambiarVelocidad(esperarCambio))
        {
            for(int x = 0; x < velocidad.Length; x++)
            {
                velocidadRandom = Random.Range(0,3);
                velocidadAntenas[x] = velocidad[velocidadRandom];
            }
            Debug.Log(velocidadAntenas[0]);
            Debug.Log(velocidadAntenas[1]);
            Debug.Log(velocidadAntenas[2]);
            esperarCambio = Time.time + 2f;
        }
    }

    private void Resultado()
    {
        for(int x = 0; x< 3; x++)
        {
            if(Oscuridad[x].GetComponent<RectTransform>().sizeDelta.x >= 55.7168f + 30f)
            {
                player.PlayOneShot(derrota); 
                Reinicio();
                FindObjectOfType<Lampara_Desactivada>().Derrota();
            }
        }
    }

    private void Reinicio()
    {
        for(int x = 0; x< Oscuridad.Length; x++)
        {
            Oscuridad[x].GetComponent<RectTransform>().sizeDelta = new Vector2( 40f, 43.5867f);
            sonidoLamp[x] = false;
            generadores[x].gameObject.GetComponent<Image>().sprite = generadorNoLuz;
        }

        Ganar[0] = Ganar[1] = Ganar[2] = false;
        debeReiniciar = true;

    }

    private void ComprobarGanar()
    {
        for (int x = 0; x < Oscuridad.Length; x++)
        {
            if (Oscuridad[x].GetComponent<RectTransform>().sizeDelta.x <= 0)
            {
                Ganar[x] = true;
                if(Ganar[x] && !sonidoLamp[x])
                {
                    player.PlayOneShot(victoria);
                    generadores[x].gameObject.GetComponent<Image>().sprite = generadorLuz;
                    sonidoLamp[x] = true;
                }
            }
            else
            {
                Ganar[x] = false;
            }
        }

        if(Ganar[0] && Ganar[1] && Ganar[2])
        {
            player.PlayOneShot(victoria);
            FindObjectOfType<Lampara_Desactivada>().Victoria();
        }
    }

    private bool DebeAvanzarOscuridad(float esperarAvance)
    {
        return Time.time > esperarAvance;
    }

    private bool DebeCambiarVelocidad(float esperarCambio)
    {
        return Time.time > esperarCambio;
    }
}