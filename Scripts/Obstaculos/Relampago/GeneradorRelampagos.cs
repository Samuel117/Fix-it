using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorRelampagos : MonoBehaviour
{
    [SerializeField] private GameObject relampago;
    [SerializeField] private int contador;
    [SerializeField] private GameObject puntoAparicion;
    private float esperarRelampago = 5f;
    private float esperarZona = 0f;
    private bool debeGenerarRelampago = false;

    private Vector2 posicion;

    // Start is called before the first frame update
    void Start()
    {
        esperarRelampago = Time.time + 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Jugador>()!= null && FindObjectOfType<Jugador>().MinijuegoAbierto)
        {
            esperarRelampago = Time.time + 3f;
            esperarZona = Time.time + 1f;
        }

        if(DebeGenerarRelampago(esperarRelampago) && contador > 0  && !FindObjectOfType<Jugador>().saltando || debeGenerarRelampago)
        {
            if (!debeGenerarRelampago)
            {
                    posicion = new Vector2(FindObjectOfType<Jugador>().transform.position.x + Random.Range(-3, 3), FindObjectOfType<Jugador>().transform.position.y + 10f);

                    Instantiate(puntoAparicion, new Vector2(posicion.x, posicion.y - 11f), puntoAparicion.transform.rotation);

                    esperarZona = Time.time + 1f;
            }

            debeGenerarRelampago = true;

            if (EsperarZona(esperarZona) && !FindObjectOfType<Jugador>().saltando)
            {
                    Instantiate(relampago, new Vector2(posicion.x, posicion.y - 7f), relampago.transform.rotation);

                esperarRelampago = Time.time + 5f;
                contador--;
                debeGenerarRelampago = false;
            }
            
        }
    }

    private bool DebeGenerarRelampago(float esperarRelampago)
    {
        return Time.time > esperarRelampago;
    }

    private bool EsperarZona(float esperarZona)
    {
        return Time.time > esperarZona;
    }
}
