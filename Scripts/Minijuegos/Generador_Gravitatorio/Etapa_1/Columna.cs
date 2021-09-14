using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Columna : MonoBehaviour
{

    [SerializeField] GameObject LetraRandom;
    private float EsperarLetra = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SeleccionTiempo();
    }

    // Update is called once per frame
    void Update()
    {
        if (PuedeGenerar(EsperarLetra))
        {
            GenerarLetra();
            SeleccionTiempo();
        }
    }

    private void GenerarLetra()
    {
        Instantiate(this.LetraRandom, this.transform.Find("PosicionLetra").position, this.transform.rotation, this.gameObject.transform);
    }

    private bool PuedeGenerar(float Espera)
    {
        return Time.time > Espera;
    }

    private void SeleccionTiempo()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                EsperarLetra = Time.time + 6f;
                break;
            case 1:
                EsperarLetra = Time.time + 4f;
                break;
            case 2:
                EsperarLetra = Time.time + 2f;
                break;
            default:
                EsperarLetra = Time.time + 6f;
                break;
        }
    }
}
