using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraBlanca : MonoBehaviour
{
    public int columna = 0;

    // Update is called once per frame
    void Update()
    {
        if(columna > 4)
        {
            FindObjectOfType<PuertaSellada>().Ganar();
            Debug.Log("Ya ganastes");
        }
        else
        {
            SeleccionarLetra();
        }
    }

    private void SeleccionarLetra()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Comprobar");
            columna = FindObjectOfType<Columnas>().ComprobarLetra();
        }

    }
}