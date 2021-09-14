using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Posicion : MonoBehaviour
{
    private bool debeReparar = false;
    [SerializeField] private GameObject reparacion;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject tiempo;
    private Vector2 posicionReparacion;
    private Quaternion rotacionReparacion;

    int segundosRest = 12;
    float espera = 1f;



    [SerializeField] AudioSource player;
    [SerializeField] AudioClip ladrillo;
    [SerializeField] AudioClip victoria;
    [SerializeField] AudioClip derrota;


    private Vector2[] posiciones = new Vector2[29];
    private int pos = 0;
    private float duracion;
    
    // Start is called before the first frame update
    void Start()
    {
        duracion = Time.time + 12f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;

        if(pos == 29)
        {
            player.PlayOneShot(victoria);
            FindObjectOfType<Muro_Fracturado>().Victoria();
        }

        if(Time.time > duracion && pos < 29)
        {
            player.PlayOneShot(derrota);
            FindObjectOfType<Muro_Fracturado>().Derrota();
        }

        if (MenosTimepo(espera))
        {
            segundosRest--;
            tiempo.gameObject.GetComponent<TextMeshProUGUI>().text = "Segundos: " + segundosRest;
            espera = Time.time + 1f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //debeReparar = true;
        posicionReparacion = collision.gameObject.GetComponent<RectTransform>().position;
        rotacionReparacion = collision.gameObject.GetComponent<RectTransform>().rotation;
        
        if (Input.GetMouseButton(0))
        {
            if(pos == 0)
            {
                player.PlayOneShot(ladrillo);
                Instantiate(reparacion, posicionReparacion, rotacionReparacion, canvas.transform);
                posiciones[pos] = posicionReparacion;
                debeReparar = false;
                pos++;
            }
            else
            {
                for(int x = 0; x <= pos; x++)
                {
                    if(posicionReparacion == posiciones[x])
                    {
                        debeReparar = false;
                        break;
                    }
                    else
                    {
                        debeReparar = true;
                    }
                }

                if (debeReparar)
                {
                    player.PlayOneShot(ladrillo);
                    Instantiate(reparacion, posicionReparacion, rotacionReparacion, canvas.transform);
                    posiciones[pos] = posicionReparacion;
                    debeReparar = false;
                    pos++;
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        debeReparar = false;
    }

    private bool MenosTimepo(float espera)
    {

        return Time.time > espera;
    }
}