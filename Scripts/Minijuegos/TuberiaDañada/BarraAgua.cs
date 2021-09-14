using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraAgua : MonoBehaviour
{
    private float esperarAvance = 1f;
    private float cadenciaAvance = 0.4f;
    public bool emapate = false;


    //private int avanzar = 7; //7 o mas
    //private int empatar = 6; //5 o 6
    //private int retroceder = 4;// 4 o menos

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!emapate)
        {
            AvanceAgua();
        }
        Victoria();
        Derrota();

    }


    private void AvanceAgua()
    {
            if (PuedeAvanzar(esperarAvance))
            {
                //Avanzar barra azul.
                this.GetComponent<BoxCollider2D>().size = new Vector2(this.GetComponent<BoxCollider2D>().size.x + 10, 100);
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x + 10, 100);
                esperarAvance = Time.time + cadenciaAvance;
            }
    }

    private bool PuedeAvanzar(float esperarAvance)
    {
        return Time.time > esperarAvance;
    }

    private void Victoria()
    {
        if(this.GetComponent<RectTransform>().sizeDelta.x <= 10)
        {
            FindObjectOfType<TuberiaDanada>().Victoria();
        }
    }

    private void Derrota()
    {
        if (this.GetComponent<RectTransform>().sizeDelta.x >= 680)
        {
            FindObjectOfType<TuberiaDanada>().Derrota();
        }
    }

    public void AvanceJugador()
    {
        if (FindObjectOfType<LetraRandom>().cantidadpresionada >= 6)
        {
            //Avanzar barra azul.
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x - 100, 100);
        }

       
    }

    public bool EmpateJugador()
    {
       return FindObjectOfType<LetraRandom>().cantidadpresionada >= 4 && FindObjectOfType<LetraRandom>().cantidadpresionada <= 5;
    }
}