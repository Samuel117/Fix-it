using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetraRandom : MonoBehaviour
{

    private float cadenciaLetras = 2f;
    private float esperarCambioLetra = 1.6f;
    private char letraRandom;
    public int cantidadpresionada;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        CambiarLetra();
        ComprobarLetraJugador();

    }

    private void CambiarLetra()
    {
        if (DebeCambiarLetra(esperarCambioLetra))
        {
            letraRandom = (char)('A' + UnityEngine.Random.Range(0, 26));
            this.GetComponent<TextMeshProUGUI>().text = letraRandom.ToString();

            FindObjectOfType<BarraAgua>().AvanceJugador();
            FindObjectOfType<BarraAgua>().emapate = FindObjectOfType<BarraAgua>().EmpateJugador();

            cantidadpresionada = 0;
            esperarCambioLetra = Time.time + cadenciaLetras;
        }
    }

    private bool DebeCambiarLetra(float esperarCambioLetra)
    {
        return Time.time > esperarCambioLetra;
    }

    private void ComprobarLetraJugador()
    {
        letraRandom = Char.ToLower(letraRandom);

        if (Input.GetKeyUp(letraRandom.ToString()))
        {
            cantidadpresionada++;
            Debug.Log("Tecla presionada: " + cantidadpresionada);
        }
    }
}